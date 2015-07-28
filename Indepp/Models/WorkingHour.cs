using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Indepp.Models
{
    public class WorkingHour
    {
        public int ID { get; set; }
        public int PlaceID { get; set; }

        public DayOfWeek Day { get; set; }
        public string OpenTime { get; set; }

        public List<WorkingHour> PopulateHours()
        {
            var workingHours = new List<WorkingHour>
                {
                    new WorkingHour() { Day =  DayOfWeek.Monday, OpenTime = "" },
                    new WorkingHour() { Day =  DayOfWeek.Tuesday, OpenTime = "" },
                    new WorkingHour() { Day =  DayOfWeek.Wednesday, OpenTime = "" },
                    new WorkingHour() { Day =  DayOfWeek.Thursday, OpenTime = "" },
                    new WorkingHour() { Day =  DayOfWeek.Friday, OpenTime = "" },
                    new WorkingHour() { Day =  DayOfWeek.Saturday, OpenTime = "" },
                    new WorkingHour() { Day =  DayOfWeek.Sunday, OpenTime = "" }
                };

            return workingHours;
        }
    }
}