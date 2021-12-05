using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.DataContracts
{
	public class RoyaltyType
	{
		[Key]
		public string RoyaltyTypeID { get; set; }
	}
}
