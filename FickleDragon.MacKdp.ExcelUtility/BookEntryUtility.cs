using FickleDragon.MacKdp.DataContracts;
using FickleDragon.MacKdp.ExcelUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility
{
	public class BookEntryUtility
	{
		public static List<BookEntry> FilteredList(BookListFilter filter)
		{
			List<BookEntry> result = null;

			using (var dbContext = new KdpDbContext())
			{
				result = dbContext.BookEntries.ToList();
			}

			return result;
		}

		public static VisualIndexModel GetVisualIndexModel(string emailAddress)
		{
			VisualIndexModel result = new VisualIndexModel();

			result.Books = new List<BookNameModel>();

			using (var dbContext = new KdpDbContext())
			{
				var bookNames = (from xx in dbContext.BookEntries
								 select new BookNameModel { ASIN = xx.ASIN, Title = xx.Title })
									.Distinct()
									.OrderBy(x => x.ASIN);
				result.Books.AddRange(bookNames);

				result.Years = new List<YearModel>();
				int lastYear = 0;
				YearModel currentYear = null;
                int greatestYear = dbContext.WorkbookFiles.Max(x => x.FileDate).Year;
				foreach (var file in dbContext.WorkbookFiles
									.OrderBy(x => x.FileDate))
				{
					if (file.FileDate.Year != lastYear)
					{
						currentYear = new YearModel { Year = file.FileDate.ToString("yyyy"), Months = new List<DisplayMonthModel>() };
						lastYear = file.FileDate.Year;
						result.Years.Add(currentYear);
					}

					currentYear.Months.Add(new DisplayMonthModel
								{
									Key = file.FileDate.ToString("MM/dd/yyyy"),
									Display = file.FileDate.ToString("MMM-yy"),
                                    Selected = (file.FileDate.Year == greatestYear) ? "selected=\"selected\"" : string.Empty
								});
				}
			}
			return result;
		}

		public static int DeleteData(string email)
		{
			int count = 0;
			using (var dbContext = new KdpDbContext())
			{
				var files = dbContext.WorkbookFiles;
				count = files.Count();

				foreach (var file in files)
				{
					var books = dbContext.BookEntries.Where(x => x.WorkbookFileID == file.WorkbookFileID);
					dbContext.BookEntries.RemoveRange(books);
					dbContext.WorkbookFiles.Remove(file);
				}

				dbContext.SaveChanges();
			}
			return count;
		}

		public static FilesLoadedModel GetFilesLoadedModel(string email)
		{
			FilesLoadedModel result = new FilesLoadedModel();

			using (var dbContext = new KdpDbContext())
			{
				var files = dbContext.WorkbookFiles.ToList().OrderByDescending(x => x.FileDate);
				if (files.Count() > 0)
				{
					result.DataLoadedMessage = string.Format("You have {0} files loaded in our database. The most recent is dated {1:MM/dd/yyyy}", files.Count(), files.FirstOrDefault().FileDate);
				}
				else
				{
					result.DataLoadedMessage = "Good thing you're here. You haven't loaded any files.";
				}
			}
			return result;
		}
	}
}
