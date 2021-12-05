using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.DataContracts
{
	public class RoyaltyNormalizedBook
	{
		public string ASIN { get; set; }
		public DateTime Date { get; set; }
		public decimal Royalty { get; set; }
		public decimal Koll { get; set; }
		public int KENP { get; set; }
		public decimal Total { get { return Royalty + Koll; } }
        public int SoldBooks { get; set; }
	}
}
