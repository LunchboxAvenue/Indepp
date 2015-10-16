using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.ViewModels
{
    public class WorkingHourView
    {
        public int ID { get; set; }
        public int PlaceID { get; set; }
        public DayOfWeek Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}