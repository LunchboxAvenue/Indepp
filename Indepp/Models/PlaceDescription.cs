using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class PlaceDescription
    {
        public int ID { get; set; }
        public int DayOfTheWeek { get; set; }
        public DateTime? OpeningTime { get; set; }
        public DateTime? ClosingTime { get; set; }
        public string Description { get; set; }
    }
}