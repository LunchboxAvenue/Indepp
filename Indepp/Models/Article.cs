using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class Article
    {
        public int ID { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime PostedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}