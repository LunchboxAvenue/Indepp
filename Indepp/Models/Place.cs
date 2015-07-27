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

        public virtual Address Address { get; set; }
        public virtual PlaceDescription PlaceDescription { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<WorkingHours> WorkingHours { get; set; }

        public Place()
        {
            WorkingHours = new List<WorkingHours>
            {
                new WorkingHours(DayOfWeek.Monday, null, null),
                new WorkingHours(DayOfWeek.Tuesday, null, null),
                new WorkingHours(DayOfWeek.Wednesday, null, null),
                new WorkingHours(DayOfWeek.Thursday, null, null),
                new WorkingHours(DayOfWeek.Friday, null, null),
                new WorkingHours(DayOfWeek.Saturday, null, null),
                new WorkingHours(DayOfWeek.Sunday, null, null)
            };
        }
    }
}