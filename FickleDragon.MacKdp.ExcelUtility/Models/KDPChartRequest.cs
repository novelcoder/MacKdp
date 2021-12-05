using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FickleDragon.MacKdp.ExcelUtility
{
    public class KDPChartRequest
    {
        public string xAxis { get; set; }
        public decimal Royalty { get; set; }
        public decimal Koll { get; set; }
        public DateTime Date { get; set; }
        public decimal ACX { get; set; }
        public decimal Total { get; set; }
    }
}