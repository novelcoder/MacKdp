using FickleDragon.MacKdp.DataContracts;
using FickleDragon.MacKdp.ExcelUtility.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


/* 
 * 64 ,64, 64, 404040 gray 
 * 2, 73, 89, 204959 dark blue
 * 3, 126, 140, 037EBC light blue
 * 246, 76, 39, F24C27 orange
 */

namespace FickleDragon.MacKdp.ExcelUtility
{
    public class BarChartUtility
    {
        class tt
        {
            public string ASIN { get; set; }
            public int KENP { get; set; }
            public decimal Royalty { get; set; }
            public decimal PerBook { get; set; }
        };

        public static BarChartResultModel DollarPerKUBook(string emailAddress, List<string> ASINs, List<string> dateStrings)
        {
            var result = new BarChartResultModel();
            var dates = LoadDateStrings(dateStrings);
            decimal maxVal = 10;
            var byAsin = new List<tt>();

            if (ASINs != null && dates != null)
            {
                result.data = new BarChartModel();
                result.data.labels = new List<string>();
                result.data.datasets = new List<ChartDataSetModel>();


                using (var dbContext = new KdpDbContext())
                {
                    var royaltyNormalizedEntries = NormalizeRoyalty(
                                dbContext.BookEntries.Where(x => x.KENP > 0),
                                dbContext, emailAddress, ASINs, dates);

                    byAsin = royaltyNormalizedEntries
                                        .GroupBy(d => new { d.ASIN })
                                        .Select(d => new tt
                                        {
                                            ASIN = d.Key.ASIN,
                                            KENP = d.Sum(b => b.KENP),
                                            Royalty = d.Sum(b => b.Total)
                                        })
                                        .OrderBy(d => d.ASIN)
                                        .ToList();

                    // run through and fix up some entries
                    foreach (var book in byAsin)
                    {
                        var amazonEntry = dbContext.ASINPageCounts.FirstOrDefault(x => x.ASIN == book.ASIN && x.User == emailAddress);
                        if (amazonEntry == null)
                        {
                            amazonEntry = new ASINPageCount
                            {
                                ASIN = book.ASIN,
                                User = emailAddress,
                                //AmazonPageCount = AmazonLookupUtility.LookupItem(book.ASIN),
                                KUPageCount = 0
                            };
                            dbContext.ASINPageCounts.Add(amazonEntry);
                        }

                        int pagesPerBook = (amazonEntry.KU2PageCount > 0) ? amazonEntry.KU2PageCount : (int)((decimal)amazonEntry.AmazonPageCount * 1.66M);
                        int numberBooksSold = book.KENP / pagesPerBook;
                        if (numberBooksSold != 0)
                            book.PerBook = book.Royalty / numberBooksSold;
                        maxVal = Math.Max(maxVal, book.PerBook);

                    }

                    dbContext.SaveChanges();

                } // end of data context


                foreach (var book in byAsin)
                {
                    result.data.labels.Add(book.ASIN);
                }

                var perBook = new ChartDataSetModel()
                {
                    label = "$ Per Book",
                    data = new List<decimal>()
                };
                perBook.SetColors("2, 73, 89");

                foreach (var book in byAsin)
                    perBook.data.Add(book.PerBook);

                result.data.datasets.Add(perBook);
            }

            return result;
        }

        public static BarChartResultModel GetBarCountByMonth(string emailAddress, List<string> ASINs, List<string> dateStrings, bool includeFree)
        {
            var result = new BarChartResultModel();
            var dates = LoadDateStrings(dateStrings);
            decimal minVal = 0;
            decimal maxVal = 100;
            decimal grand = 0;

            if (ASINs != null && dates != null)
            {
                result.data = new BarChartModel();
                result.data.labels = new List<string>();
                result.data.datasets = new List<ChartDataSetModel>();

                using (var dbContext = new KdpDbContext())
                {
                    var byMonth = dbContext.BookEntries
                                        .Where(x => x.WorkbookFile.EmailAddress == emailAddress
                                             && ASINs.Contains(x.ASIN)
                                             && dates.Contains(x.WorkbookFile.FileDate))
                                        .GroupBy(d => new { d.WorkbookFile.FileDate })
                                        .Select(d =>
                                                new
                                                {
                                                    Date = d.Key.FileDate,
                                                    SoldBooks = d.Sum(b => b.SoldBooks),
                                                    KollBooks = d.Sum(b => b.KOLLBooks),
                                                    FreeBooks = d.Sum(b => b.FreeBooks)
                                                })
                                        .OrderBy(d => d.Date)
                                        .ToList();

                    foreach (var item in byMonth)
                    {
                        result.data.labels.Add(item.Date.ToString("MMM-yy"));
                    }

                    var sold = new ChartDataSetModel()
                    {
                        label = "#Sold",
                        data = new List<decimal>()
                    };
                    sold.SetColors("2, 73, 89");

                    var koll = new ChartDataSetModel()
                    {
                        label = "#Koll",
                        data = new List<decimal>()
                    };
                    koll.SetColors("246, 76, 39");

                    var free = new ChartDataSetModel()
                    {
                        label = "#Free",
                        data = new List<decimal>()
                    };
                    free.SetColors("3, 126, 140");


                    foreach (var item in byMonth)
                    {
                        int totalCount = (int)(item.SoldBooks + item.KollBooks);
                        if (includeFree)
                            totalCount += (int)item.FreeBooks;

                        maxVal = Math.Max(totalCount, maxVal);
                        sold.data.Add((int)item.SoldBooks);
                        koll.data.Add((int)item.KollBooks);

                        if (includeFree)
                        {
                            free.data.Add((int)item.FreeBooks);
                            grand += item.FreeBooks;
                        }

                        grand += totalCount;
                    }

                    result.data.datasets.Add(sold);
                    result.data.datasets.Add(koll);
                    if (includeFree)
                        result.data.datasets.Add(free);
                }
            }


            result.options = new ChartJSOptions
            {
                scaleStepWidth = (((int)(maxVal - minVal) - (int)(maxVal - minVal) % 50) + 50) / 5,
                scaleStartValue = (int)minVal - (int)(minVal % 50)
            };

            if (includeFree)
                result.TotalLabel = "Num Books";
            else
                result.TotalLabel = "Num Sold/Koll";

            result.TotalValue = string.Format("{0:#,##0}", grand);

            return result;
        }

        public static KDPChartModel GetBarByMonth(string emailAddress, List<string> ASINs, List<string> dateStrings, bool showACX)
        {
            var result = new KDPChartModel();

            var dates = LoadDateStrings(dateStrings);

            if (ASINs != null && dates != null)
            {

                using (var dbContext = new KdpDbContext())
                {
                    var byMonth = NormalizeRoyalty(dbContext.BookEntries, dbContext, emailAddress, ASINs, dates);

                    var counts = byMonth
                                    .GroupBy(d => new { d.Date })
                                    .Select(d => new
                                    {
                                        Date = d.Key.Date,
                                        Royalty = d.Sum(b => b.Royalty),
                                        Koll = d.Sum(b => b.Koll),
                                        Total = d.Sum(b => b.Royalty + b.Koll),
                                        SoldBooks = d.Sum(b => b.SoldBooks)
                                    })
                                    .OrderBy(x => x.Date)
                                    .ToList();


                    result.ChartItems = counts.Select(it => new KDPChartItem
                    {
                        xAxis = it.Date.ToString("MM-yyyy"),
                        SortOrder = it.Date.ToString("yyyyMMdd"),
                        Royalty = it.Royalty,
                        Date = it.Date,
                        Koll = it.Koll,
                        Total = it.Total,
                        SoldBooks = (int)it.SoldBooks

                    }).ToList();

                    if (showACX)
                    {
                        foreach (var item in result.ChartItems)
                        {
                            var myDate = new DateTime(item.Date.Year, item.Date.Month, 1).AddMonths(1).AddDays(-1);
                            decimal totalAcxRoyalty = dbContext.ACXRoyalties.Where(x => x.EntryDate == myDate)
                                            .Sum(x => (decimal?)x.RoyaltyEarned) ?? 0;
                            decimal countACX = dbContext.ACXRoyalties.Where(x => x.EntryDate == myDate)
                                            .Sum(x => (decimal?)x.Quantity) ?? 0;
                            item.ACX = totalAcxRoyalty;
                            item.ACXCount = (int)countACX;
                        }
                    }
                }
            }

            ProjectTotals(result, showACX);

            return result;
        }

        private static void ProjectTotals(KDPChartModel result, bool showACX)
        {
            result.Totals.Add(new KDPTotalModel
            {
                Heading = "Royalty",
                Count = result.ChartItems.Sum(x => x.SoldBooks),
                Revenue = result.ChartItems.Sum(x => x.Royalty)
            });
            result.Totals.Add(new KDPTotalModel
            {
                Heading = "KU",
                Count = 0,
                Revenue = result.ChartItems.Sum(x => x.Koll)
            });
            if (showACX)
            {
                result.Totals.Add(new KDPTotalModel
                {
                    Heading = "ACX",
                    Count = result.ChartItems.Sum(x => x.ACXCount),
                    Revenue = result.ChartItems.Sum(x => x.ACX)
                });
            }

            result.Totals.Add(new KDPTotalModel
            {
                Heading = "Grand",
                Count = result.Totals.Sum(x => x.Count),
                Revenue = result.Totals.Sum(x => x.Revenue)
            });
        }

        public static KDPChartModel GetBarByTitle(string emailAddress, List<string> ASINs, List<string> dateStrings, bool showACX)
        {
            var result = new KDPChartModel();
            var dates = LoadDateStrings(dateStrings);

            if (ASINs != null && dates != null)
            {

                using (var dbContext = new KdpDbContext())
                {
                    result.ChartItems = NormalizeRoyalty(dbContext.BookEntries, dbContext, emailAddress, ASINs, dates)
                                .GroupBy(d => new { d.ASIN })
                                .Select(d => new KDPChartItem
                                {
                                    ASIN = d.Key.ASIN,
                                    Royalty = d.Sum(x => x.Royalty),
                                    SortOrder = d.Key.ASIN,
                                    Koll = d.Sum(x => x.Koll),
                                    Total = d.Sum(x => x.Total),
                                    SoldBooks = d.Sum(b => b.SoldBooks)
                                })
                                .OrderBy(d => d.ASIN)
                                .ToList();

                    foreach (var item in result.ChartItems)
                    {
                        var title = dbContext.BookEntries
                                            .OrderByDescending(x => x.Title.Length)
                                            .First(x => x.ASIN == item.ASIN)
                                            .Title ?? "no data";
                        item.xAxis = ( title.Length > 24) ? title.Substring(0,24) : title;
                    }
                    if (showACX)
                    {
                        foreach (var item in result.ChartItems)
                        {
                            decimal totalAcxRoyalty = dbContext.ACXRoyalties
                                            .Include("ACXProduct")
                                            .Where(x => dates.Contains(x.EntryDate) && x.ACXProduct.ASIN == item.ASIN)
                                            .Sum(x => (decimal?)x.RoyaltyEarned) ?? 0;
                            decimal countACX = dbContext.ACXRoyalties
                                            .Include("ACXProduct")
                                            .Where(x => dates.Contains(x.EntryDate) && x.ACXProduct.ASIN == item.ASIN)
                                            .Sum(x => (decimal?)x.Quantity) ?? 0;
                            item.ACX = totalAcxRoyalty;
                            item.ACXCount = (int)countACX;
                        }
                    }
                }
            }

            ProjectTotals(result, showACX);

            return result;
        }

        private static List<DateTime> LoadDateStrings(List<string> dateStrings)
        {
            if (dateStrings == null)
                return null;

            var result = new List<DateTime>();
            foreach (var item in dateStrings)
                result.Add(DateTime.Parse(item));

            return result;
        }

        private static IEnumerable<RoyaltyNormalizedBook> NormalizeRoyalty(IQueryable<BookEntry> bookQuery, KdpDbContext dbContext, string emailAddress, List<string> ASINs, List<DateTime> dates)
        {
            var byMonthTitle = bookQuery
                                .Where(x => x.WorkbookFile.EmailAddress == emailAddress
                                             && ASINs.Contains(x.ASIN)
                                             && dates.Contains(x.WorkbookFile.FileDate))
                                .GroupBy(d => new { d.ASIN, d.WorkbookFile.FileDate, d.RoyaltyTypeID })
                                .Select(d =>
                                        new
                                        {
                                            RoyaltyTypeID = d.Key.RoyaltyTypeID,
                                            ASIN = d.Key.ASIN,
                                            Date = d.Key.FileDate,
                                            Royalty = d.Sum(b => b.Royalty),
                                            Koll = d.Sum(b => b.KOLLShare),
                                            KENP = d.Sum(b => b.KENP),
                                            SoldBooks = d.Sum(b => b.SoldBooks)
                                        })
                                .OrderBy(d => d.Date)
                                .ToList();

            var intermediate = (from xx in byMonthTitle
                                from er in dbContext.ExchangeRates
                                where xx.RoyaltyTypeID == er.RoyaltyTypeID
                                      && xx.Date >= er.StartDate
                                      && xx.Date <= er.EndDate
                                      && (xx.Royalty > 0 || xx.Koll > 0)
                                select new
                                {
                                    ASIN = xx.ASIN,
                                    Date = xx.Date,
                                    RoyaltyTypeID = er.RoyaltyTypeID,
                                    Royalty = er.ToUSD * xx.Royalty,
                                    PreExchangeRoyalty = xx.Royalty,
                                    ExchangeRate = er.ToUSD,
                                    Koll = er.ToUSD * xx.Koll,
                                    PreExchangeKoll = xx.Koll,
                                    KENP = xx.KENP,
                                    SoldBooks = xx.SoldBooks
                                }).ToList();

            var counts = intermediate
                            .GroupBy(d => new { d.ASIN, d.Date })
                            .Select(d => new RoyaltyNormalizedBook
                            {
                                ASIN = d.Key.ASIN,
                                Date = d.Key.Date,
                                Royalty = d.Sum(b => b.Royalty),
                                Koll = d.Sum(b => b.Koll),
                                KENP = d.Sum(b => b.KENP),
                                SoldBooks = d.Sum(b => b.SoldBooks)
                            });

            return counts;
        }
    }
}