using System;

namespace FickleDragon.MacKdp.ExcelUtility
{
    public class Workbook
	{
		public static string TitleColName { get { return  "A"; } }
		public static string ASINColName { get { return  "C";} }
		public static string NetSoldColName { get { return "F";} }
		public static string TypeColName { get {  return "H";} }
		public static string RoyaltyColName { get { return "M"; } }

		public int TitleColNum { get; set; }
		public int ASINColNum { get; set; }
		public int NetSoldColNum { get; set; }
		public int TypeColNum { get; set; }
		public int RoyaltyColNum { get; set; }
        public int RoyaltyTypeColNum { get; set; }
        public int SheetNum { get; set; }
		
		public static Workbook ForDate(DateTime date)
		{
			var result = new Workbook
			{
				TitleColNum = 1,
				ASINColNum = 3,
				NetSoldColNum =7, 
				TypeColNum = 9,
				RoyaltyColNum = 15, 
                RoyaltyTypeColNum= 10
			};

            if (date < new DateTime(2017, 07, 01))
            {
                result = new Workbook
                {
                    TitleColNum = 1,
                    ASINColNum = 3,
                    NetSoldColNum = 9,
                    TypeColNum = 6,
                    RoyaltyColNum = 14,
                    RoyaltyTypeColNum = 15,
                    SheetNum = 3
                };
        }
            else if ( date < new DateTime(2013, 04, 01))
			{			
				result = new Workbook 
				{
				TitleColNum = 0,
				ASINColNum = 1,
				NetSoldColNum =5, 
				TypeColNum = 2,
				RoyaltyColNum = 11
				};
			}
			else if (date < new DateTime(2013, 10, 01 ))
			{
				result = new Workbook
				{
					TitleColNum = 0,
					ASINColNum = 2,
					NetSoldColNum = 6,
					TypeColNum = 3,
					RoyaltyColNum = 12
				};

			}

			else if (date < new DateTime(2013, 11, 01))
			{
				result = new Workbook
				{
					TitleColNum = 0,
					ASINColNum = 2,
					NetSoldColNum = 5,
					TypeColNum = 7,
					RoyaltyColNum = 12
				};

			}
			return result;
		}

		public static bool IsKENP(string typeStr)
		{
			return typeStr.Contains("KENP");
		}

		public static bool IsKOLL(string typeStr)
		{
			return typeStr.Contains("KOLL");
		}
	}
}
