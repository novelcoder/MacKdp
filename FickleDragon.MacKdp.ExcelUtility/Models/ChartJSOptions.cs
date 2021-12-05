using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
	public class ChartJSOptions
	{
		public bool scaleOverlay { get; set; }
		public bool scaleShowGridLines { get; set; }
		public bool scaleOverride { get; set; }
		public string scaleLabel { get; set; }
		public int scaleSteps { get; set; }
		public int scaleStepWidth { get; set; }
		public int scaleStartValue { get; set; }
		public string scaleLineColor { get; set; }
		public int scaleLineWidth { get; set; }
		public bool scaleShowLabels { get; set; }
		public int scaleFontSize { get; set; }
		public string scaleFontColor { get; set; }
		public int barDatasetSpacing { get; set; }
		public int barStrokeWidth { get; set; }
		public ChartJSOptions() 
		{


			scaleOverlay = true;
			scaleShowGridLines = true;
			scaleOverride = true;
			scaleSteps = 5;

			scaleStepWidth = 50;
			scaleStartValue = 0;
			scaleLabel = "<%= addCommas(value) %>";
			scaleLineColor = "rgba(2; 73; 89;.5)";
			scaleLineWidth = 1;
			scaleShowLabels = true;
			scaleFontSize = 16;
			scaleFontColor = "#204959";
			barDatasetSpacing = 2;
			barStrokeWidth = 2;
		}
	}
}
