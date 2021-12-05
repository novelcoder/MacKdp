using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
    public class KDPChartItem
    {
        public string xAxis { get; set; }
        public string ASIN { get; set; }
        public int ACXCount { get; set; }
        public int SoldBooks { get; set; }
        public decimal Royalty { get; set; }
        public decimal Koll { get; set; }
        public DateTime Date { get; set; }
        public decimal ACX { get; set; }
        public decimal Total { get; set; }
        public string SortOrder { get; set; }
    }
}