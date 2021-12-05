using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
    public class KDPChartModel
    {
        public KDPChartModel()
        {
            ChartItems = new List<KDPChartItem>();
            Totals = new List<KDPTotalModel>();
        }

        public List<KDPChartItem> ChartItems { get; set; }
        public List<KDPTotalModel> Totals {get; set;}
    }
}