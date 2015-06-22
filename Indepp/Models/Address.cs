using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Couty { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}