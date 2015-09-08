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

        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string Address3 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string County { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(32)]
        public string PostCode { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; } 
    }
}