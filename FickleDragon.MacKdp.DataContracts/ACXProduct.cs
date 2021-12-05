﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FickleDragon.MacKdp.DataContracts
{
    public class ACXProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ACXProductID { get; set; }
        public string ACXProductKey { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ASIN { get; set; }
    }
}
