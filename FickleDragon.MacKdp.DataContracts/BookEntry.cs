using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FickleDragon.MacKdp.DataContracts
{
	public class BookEntry
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int BookEntryID { get; set; }

		public string ASIN { get; set; }
		public string Title { get; set; }
		public int SoldBooks { get; set; }
		public int KOLLBooks { get; set; }
		public decimal Royalty { get; set; }
		public decimal KOLLShare { get; set; }
		public int FreeBooks { get; set; }
		public int KENP { get; set; }

		[ForeignKey("WorkbookFile")]
		public int WorkbookFileID { get; set; }

		[ForeignKey("RoyaltyType")]
		public string RoyaltyTypeID { get; set; }

		public DateTime SalesMonth;

		public virtual RoyaltyType RoyaltyType { get; set; }
		public virtual WorkbookFile WorkbookFile { get; set; }
	}
}
