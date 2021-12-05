using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
	public class BarChartModel
	{
		public List<string> labels { get; set; }
		public List<ChartDataSetModel> datasets { get; set; }
	}
}
