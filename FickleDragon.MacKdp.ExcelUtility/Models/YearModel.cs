using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
	public class YearModel
	{
		public string Year { get; set; }
		public List<DisplayMonthModel> Months { get; set; }
	}
}
