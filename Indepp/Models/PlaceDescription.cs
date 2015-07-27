using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indepp.Models
{
    public class PlaceDescription
    {
        [Key]
        [ForeignKey("Place")]
        public int PlaceID { get; set; }
        public virtual Place Place { get; set; }

        [AllowHtml]
        public string Description { get; set; }
    }
}