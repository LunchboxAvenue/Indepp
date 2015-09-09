using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.ViewModels
{
    public class PlaceFilter
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
    }
}