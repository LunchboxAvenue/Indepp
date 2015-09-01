using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.ViewModels
{
    public class ArticleAndBlogPost
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }
        public int? PlaceID { get; set; }
    }
}