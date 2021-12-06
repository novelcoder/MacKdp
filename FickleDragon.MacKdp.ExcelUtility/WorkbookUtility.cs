using FickleDragon.MacKdp.DataContracts;
using OfficeOpenXml;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FickleDragon.MacKdp.ExcelUtility
{
    public class WorkbookUtility
    {

        public static void Parse(Stream fs, string file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string fileExtension = Path.GetExtension(file);
            ExcelPackage package = null;
            ExcelWorkbook workbook = null;

            switch (fileExtension)
            {
                case ".xls":
                    throw new NotImplementedException(".XLS files are not supported");
                //break;
                case ".xlsx":
                    package = new ExcelPackage(fs);
                    workbook = package.Workbook;
                    break;
                default:
                    throw new Exception("Unknown file extension on " + fileExtension);
            }

            //sheets prior to June 2017 use first sheet, different parsing
            ExcelWorksheet sheet = workbook.Worksheets[0];
            bool afterJune2017 = false;
            var ws = workbook.Worksheets["Total Royalty"];
            if ( ws != null
              && sheet.Cells[1, 1].GetValue<string>() == "Sales Period")
            {
                afterJune2017 = true;
            }


            //after Oct 2019, hardcover is found in 4, total moved to 5
            //for (int iii = 4; iii <= 5; iii++)
            //{
            //    if (workbook.Worksheets.Count >= iii) //protect from bad array reference
            //    {
            //        sheet = workbook.Worksheets[iii];
            //        if (sheet.Name == "Total Royalty"
            //          && sheet.Cells[1, 1].GetValue<string>() == "Sales Period")
            //        {
            //            afterJune2017 = true;
            //            break;
            //        }
            //    }
            //}

            if (afterJune2017)
            {
                ParseKDP2(sheet, file);
            }
            else
            {
                // check for type of sheet
                string dateStr = sheet.GetValue<string>(3, 1);
                const string salesReport = "Sales report";

                if (dateStr != null
                  && dateStr.Length > salesReport.Length
                  && dateStr.Substring(0, salesReport.Length) == salesReport)
                {
                    DateTime workbookEndDate = DateTime.MinValue;
                    DateTime date;
                    int year = 0;
                    string[] tokens = dateStr.Split(' '); // date is last token in dd-mmm-yyyy format
                    if (DateTime.TryParse(tokens[tokens.Length - 1], out date))
                    {
                        workbookEndDate = date;
                    }
                    else if (int.TryParse(tokens[tokens.Length - 1], out year) // this format Sept 2016
                            && year > 2015)
                    {
                        int month = DateTime.ParseExact(tokens[tokens.Length - 3], "MMMM", CultureInfo.CurrentCulture).Month;
                        workbookEndDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                    }

                    if (workbookEndDate == DateTime.MinValue)
                        throw new Exception("Date not located in worksheet[1]");

                    ParseKDP(workbook, file, workbookEndDate);
                }
                else //check for ACX
                {
                    const string acx = "acx";
                    string acxCell = sheet.GetValue<string>(1, 16);
                    if (acxCell?.ToLower() == acx)
                    {
                        ParseACX(sheet, file);
                    }
                    else if (workbook.Worksheets.Count > 1
                           && workbook.Worksheets[2].Name == "Sales Details")
                    {
                        sheet = workbook.Worksheets[2];
                        ParseACX(sheet, file);
                    }
                }
            }
        }

        private static void ParseACX(ExcelWorksheet sheet, string file)
        {
            using (var dbContext = new KdpDbContext())
            {
                string entryDateString = sheet.GetValue<string>(1, 10);
                DateTime entryDate = DateTime.Parse(entryDateString);
                // put us to end of month, not beginning
                entryDate = new DateTime(entryDate.Year, entryDate.Month, 1).AddMonths(1).AddDays(-1);
                var endDim = sheet.Dimension.End;
                for (int row = 4; row <= endDim.Row; row++)
                {
                    string productKey = sheet.GetValue<string>(row, 2);

                    if (productKey != null && productKey.StartsWith("BK_"))
                    {
                        string author = sheet.GetValue<string>(row, 4);
                        string title = sheet.GetValue<string>(row, 3);

                        var acxProduct = dbContext.ACXProducts.FirstOrDefault(x => x.ACXProductKey == productKey);
                        if (acxProduct == null)
                        {
                            int title_x = title.IndexOf(':');
                            if (title_x == -1 && title.IndexOf('(') > 0)
                                title_x = title.IndexOf('(');

                            string justTitle = title.Substring(0, title_x);

                            var bookEntry = dbContext.BookEntries.FirstOrDefault(x => x.Title == justTitle);
                            string asin = "";
                            if (bookEntry != null)
                                asin = bookEntry.ASIN;

                            acxProduct = new ACXProduct
                            {
                                ACXProductKey = productKey,
                                Title = justTitle,
                                Author = author,
                                ASIN = asin
                            };
                            dbContext.ACXProducts.Add(acxProduct);
                            dbContext.SaveChanges();
                        }

                        string market = sheet.GetValue<string>(row, 5);
                        var alcRoyalty = ParseACXRoyaltyModel(sheet, 7, row);
                        var alRoyalty = ParseACXRoyaltyModel(sheet, 10, row);
                        var alopRoyalty = ParseACXRoyaltyModel(sheet, 13, row);

                        InsUpdateACXRoyalty(dbContext, market, acxProduct.ACXProductID, alcRoyalty, ACXRoyaltyTypeEnum.alc, entryDate);
                        InsUpdateACXRoyalty(dbContext, market, acxProduct.ACXProductID, alRoyalty, ACXRoyaltyTypeEnum.al, entryDate);
                        InsUpdateACXRoyalty(dbContext, market, acxProduct.ACXProductID, alopRoyalty, ACXRoyaltyTypeEnum.alop, entryDate);

                        dbContext.SaveChanges();
                    }
                }
            }
        }

        private static ACXRoyaltyModel ParseACXRoyaltyModel(ExcelWorksheet sheet, int colOffset, int rowNum)
        {
            var model = new ACXRoyaltyModel();

            model.Quantity = sheet.GetValue<double>(rowNum, colOffset);
            model.NetSales = sheet.GetValue<double>(rowNum, 1 + colOffset);
            model.RoyaltyEarned = sheet.GetValue<double>(rowNum, 2 + colOffset);

            return model;
        }

        private static void InsUpdateACXRoyalty(KdpDbContext dbContext, string market, int acxProductID, ACXRoyaltyModel model, ACXRoyaltyTypeEnum royaltyTypeID, DateTime entryDate)
        {
            bool insert = false;
            ACXRoyalty royaltyRow = dbContext.ACXRoyalties.FirstOrDefault(x => x.EntryDate == entryDate
                                && x.ACXProductID == acxProductID
                                && x.ACXRoyaltyTypeID == royaltyTypeID
                                && x.ACXMarket == market);

            if (royaltyRow == null)
            {
                insert = true;
                royaltyRow = new ACXRoyalty();
            }

            royaltyRow.ACXProductID = acxProductID;
            royaltyRow.ACXRoyaltyTypeID = royaltyTypeID;
            royaltyRow.ACXMarket = market;
            royaltyRow.NetSales = Convert.ToDecimal(model.NetSales);
            royaltyRow.Quantity = Convert.ToDecimal(model.Quantity);
            royaltyRow.RoyaltyEarned = Convert.ToDecimal(model.RoyaltyEarned);
            royaltyRow.EntryDate = entryDate;

            if (insert)
                dbContext.ACXRoyalties.Add(royaltyRow);
        }

        private static void ParseKDP2(ExcelWorksheet sheet, string file)
        {
            DateTime workbookEndDate = DateTime.Now;
            string dateStr = sheet.GetValue<string>(1, 2);
            string salesPeriodCell = sheet.GetValue<string>(1, 1);
            const string salesPeriod = "Sales Period";
            if (salesPeriodCell.Substring(0, salesPeriod.Length) == salesPeriod)
            {
                DateTime date = DateTime.ParseExact(dateStr, "MMMM yyyy", CultureInfo.CurrentCulture);

                workbookEndDate = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            }

            using (var dbContext = new KdpDbContext())
            {
                var workbookFile = dbContext.WorkbookFiles.FirstOrDefault(x => x.FileDate == workbookEndDate);
                var royaltyTypes = dbContext.RoyaltyTypes.ToList();

                if (workbookFile == null)
                {
                    int lastSlash = file.LastIndexOf('\\');
                    if (lastSlash > 0)
                        file = file.Substring(lastSlash + 1);

                    workbookFile = new WorkbookFile
                    {
                        FileDate = workbookEndDate,
                        FileName = file
                    };
                    dbContext.WorkbookFiles.Add(workbookFile);
                    dbContext.SaveChanges();
                }
                else
                {
                    var bookEntries = dbContext.BookEntries.Where(x => x.WorkbookFileID == workbookFile.WorkbookFileID);
                    dbContext.BookEntries.RemoveRange(bookEntries);
                }

                var endDim = sheet.Dimension.End;
                for (int row = 1; row <= endDim.Row; row++)
                {
                    string asin = sheet.GetValue<string>(row, Workbook.ForDate(workbookEndDate).ASINColNum);

                    // actual sale
                    if (asin != null
                        && asin.Length == 10
                        && asin.Substring(0, 2) == "B0")
                    {
                        string title = sheet.GetValue<string>(row, Workbook.ForDate(workbookEndDate).TitleColNum);
                        double netSold = sheet.GetValue<double>(row, Workbook.ForDate(workbookEndDate).NetSoldColNum);
                        string royaltyTypeString = sheet.GetValue<string>(row, Workbook.ForDate(workbookEndDate).RoyaltyTypeColNum);
                        double royalty = sheet.GetValue<double>(row, Workbook.ForDate(workbookEndDate).RoyaltyColNum);
                        string typeStr = sheet.GetValue<string>(row, Workbook.ForDate(workbookEndDate).TypeColNum);

                        if (dbContext.RoyaltyTypes.FirstOrDefault(x => x.RoyaltyTypeID == royaltyTypeString) == null)
                        {
                            dbContext.RoyaltyTypes.Add(new RoyaltyType { RoyaltyTypeID = royaltyTypeString });
                        }

                        var bookEntry = new BookEntry
                        {
                            RoyaltyTypeID = royaltyTypeString,
                            ASIN = asin,
                            WorkbookFileID = workbookFile.WorkbookFileID,
                            Title = title
                        };

                        if (royalty != 0)
                        {
                            if (typeStr.StartsWith("Kindle Edition"))
                            {
                                bookEntry.KENP += (int)netSold;
                                bookEntry.KOLLShare += Convert.ToDecimal(royalty);
                            }
                            else // implied, book sold (standard)
                            {
                                bookEntry.SoldBooks += (int)netSold;
                                bookEntry.Royalty += Convert.ToDecimal(royalty);
                            }
                        }
                        else
                        {
                            bookEntry.FreeBooks += (int)netSold;
                        }

                        dbContext.BookEntries.Add(bookEntry);
                    }
                    dbContext.SaveChanges();
                }
            }
        }

        private static void ParseKDP(ExcelWorkbook workbook, string file, DateTime workbookEndDate)
        {
            using (var dbContext = new KdpDbContext())
            {
                var sheet = workbook.Worksheets[Workbook.ForDate(workbookEndDate).SheetNum];
                var workbookFile = dbContext.WorkbookFiles.FirstOrDefault(x => x.FileDate == workbookEndDate);
                var royaltyTypes = dbContext.RoyaltyTypes.ToList();

                if (workbookFile == null)
                {
                    int lastSlash = file.LastIndexOf('/');
                    if (lastSlash > 0)
                        file = file.Substring(lastSlash + 1);

                    workbookFile = new WorkbookFile
                    {
                        FileDate = workbookEndDate,
                        FileName = file
                    };
                    dbContext.WorkbookFiles.Add(workbookFile);
                    dbContext.SaveChanges();
                }
                else
                {
                    var bookEntries = dbContext.BookEntries.Where(x => x.WorkbookFileID == workbookFile.WorkbookFileID);
                    dbContext.BookEntries.RemoveRange(bookEntries);
                    dbContext.SaveChanges();
                }

                string lastRoyaltyType = string.Empty;
                var endDim = sheet.Dimension.End;
                var wb = Workbook.ForDate(workbookEndDate);
                for (int row = 1; row <= endDim.Row; row++)
                {
                    string royaltyTypeString = sheet.GetValue<string>(row, wb.RoyaltyTypeColNum);

                    if (royaltyTypeString != null)
                    {
                        royaltyTypeString = royaltyTypeString.Trim().Trim('(', ')');
                        if (dbContext.RoyaltyTypes.FirstOrDefault(x => x.RoyaltyTypeID == royaltyTypeString) != null)
                            lastRoyaltyType = royaltyTypeString;
                    }

                    string asin = sheet.GetValue<string>(row, wb.ASINColNum);
                    if ( asin != null
                      && asin.Length == 10
                      && asin.Substring(0, 2) == "B0")
                    {
                        string title = sheet.GetValue<string>(row, wb.TitleColNum);
                        string typeStr = sheet.GetValue<string>(row, wb.TypeColNum);
                        double netSold = sheet.GetValue<double>(row, wb.NetSoldColNum);
                        double royalty = sheet.GetValue<double>(row, wb.RoyaltyColNum);
                        var bookEntry = new BookEntry
                        {
                            RoyaltyTypeID = lastRoyaltyType,
                            ASIN = asin,
                            WorkbookFileID = workbookFile.WorkbookFileID,
                            Title = title
                        };

                        if (royalty != 0)
                        {
                            if (Workbook.IsKENP(typeStr))
                            {
                                bookEntry.KENP += (int)netSold;
                                bookEntry.KOLLShare += Convert.ToDecimal(royalty);
                            }
                            else if (Workbook.IsKOLL(typeStr))
                            {
                                bookEntry.KOLLBooks += (int)netSold;
                                bookEntry.KOLLShare += Convert.ToDecimal(royalty);
                            }
                            else // implied, book sold (standard)
                            {
                                bookEntry.SoldBooks += (int)netSold;
                                bookEntry.Royalty += Convert.ToDecimal(royalty);
                            }
                        }
                        else
                        {
                            bookEntry.FreeBooks += (int)netSold;
                        }

                        dbContext.BookEntries.Add(bookEntry);
                        dbContext.SaveChanges();
                    }

                }
            }
        }
    }
}
