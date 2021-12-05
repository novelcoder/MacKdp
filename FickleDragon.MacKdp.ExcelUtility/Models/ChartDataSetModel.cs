using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
	public class ChartDataSetModel
	{
		public string label { get; set; }
		public string fillColor { get; set; }
		public string strokeColor { get; set; }
		public string highlightFill { get; set; }
		public string highlightStroke { get; set; }

		public List<decimal> data { get; set; }

		internal void SetColors(string rgb)
		{
			fillColor = string.Format("rgba({0}, 0.5)", rgb);
			strokeColor = string.Format("rgba({0}, 0,8)", rgb);
			highlightFill = string.Format("rgba({0}, 0.75)", rgb);
			highlightStroke = string.Format("rgba({0}, 1)", rgb);
		}
	}
}
