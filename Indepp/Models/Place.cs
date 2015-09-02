using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class Place
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        public string Website { get; set; }
        public string Telephone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; }

        public bool UserContributed { get; set; }
        public bool Reviewed { get; set; }

        public virtual Address Address { get; set; }
        public virtual PlaceDescription PlaceDescription { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<WorkingHour> WorkingHours { get; set; }
    }
}