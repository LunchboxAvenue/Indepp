using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }
    }
}