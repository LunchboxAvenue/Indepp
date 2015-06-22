using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class Place
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Address Address { get; set; }
        public PlaceDescription Description { get; set; }
        public Article Article { get; set; }
    }
}