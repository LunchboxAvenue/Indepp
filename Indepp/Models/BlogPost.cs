using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indepp.Models
{
    public class BlogPost
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        public DateTime PostedOn { get; set; }
        public int? PlaceID { get; set; }
    }
}