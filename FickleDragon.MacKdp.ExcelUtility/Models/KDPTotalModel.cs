using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.ExcelUtility.Models
{
    public class KDPTotalModel
    {
        public string Heading { get; set; }
        public int Count { get; set; }
        public decimal Revenue { get; set; }

        public string CountStr {  get { return Count.ToString("#,##0");  } }
        public string RevenueStr { get {  return Revenue.ToString( "$#,##0"); } }
    }
}
