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

        [Range(typeof(TimeSpan), "00:00", "23:59")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? OpenTime { get; set; }

        [Range(typeof(TimeSpan), "00:00", "23:59")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? CloseTime { get; set; }

        public List<WorkingHour> PopulateHours()
        {
            var workingHours = new List<WorkingHour>
                {
                    new WorkingHour() { Day =  DayOfWeek.Monday },
                    new WorkingHour() { Day =  DayOfWeek.Tuesday },
                    new WorkingHour() { Day =  DayOfWeek.Wednesday },
                    new WorkingHour() { Day =  DayOfWeek.Thursday },
                    new WorkingHour() { Day =  DayOfWeek.Friday },
                    new WorkingHour() { Day =  DayOfWeek.Saturday },
                    new WorkingHour() { Day =  DayOfWeek.Sunday }
                };

            return workingHours;
        }
    }
}