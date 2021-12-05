using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.DataContracts
{
	public class ASINPageCount
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int ASINPageCountID { get; set; }

		public string ASIN { get; set; }
		
		public string User { get; set; }

		public int KUPageCount { get; set; }

        public int KU2PageCount { get; set; }

        public int AmazonPageCount { get; set; }
	}
}
