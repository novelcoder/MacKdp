using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
	public class BarChartResultModel
	{
		public BarChartModel data { get; set; }
		public ChartJSOptions options { get; set; }
		public string TotalLabel { get; set; }
		public string TotalValue { get; set; }
	}
}
