using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class WorkingHours
    {
        public int ID { get; set; }
        public int PlaceID { get; set; }

        public DayOfWeek Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }

        public WorkingHours()
        {

        }

        public WorkingHours(DayOfWeek day, string startTime, string endTime)
        {
            Day = day;
            OpenTime = startTime ?? "00:00";
            CloseTime = endTime ?? "00:00";
        }
    }
}