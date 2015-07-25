using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class Address
    {
        [Key]
        [ForeignKey("Place")]
        public int PlaceID { get; set; }
        public virtual Place Place { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; } 
    }
}