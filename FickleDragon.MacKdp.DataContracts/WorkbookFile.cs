using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FickleDragon.MacKdp.DataContracts
{
    public class WorkbookFile
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int WorkbookFileID { get; set; }

		public string FileName { get; set; }
		public DateTime FileDate { get; set; }
		public string EmailAddress { get; set; }
	}
}
