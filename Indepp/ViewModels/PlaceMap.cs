using Indepp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.ViewModels
{
    public class PlaceMap
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public ICollection<WorkingHourView> WorkingHours { get; set; }
    }
}