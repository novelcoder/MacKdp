using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.DataContracts
{
	public class ExchangeRate
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int ExchangeRateID { get; set; }

		[ForeignKey("RoyaltyType")]
		public string RoyaltyTypeID { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal ToUSD { get; set; }

		public virtual RoyaltyType RoyaltyType { get; set; }
	}
}
