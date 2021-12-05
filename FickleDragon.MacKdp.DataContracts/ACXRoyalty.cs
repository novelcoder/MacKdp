using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FickleDragon.MacKdp.DataContracts
{
    public class ACXRoyalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ACXRoyaltyID { get; set; }

        [ForeignKey("ACXProduct")]
        public int ACXProductID { get; set; }
        public virtual ACXProduct ACXProduct { get; set; }

        public ACXRoyaltyTypeEnum ACXRoyaltyTypeID { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetSales { get; set; }
        public decimal RoyaltyEarned { get; set; }
        public string ACXMarket { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
