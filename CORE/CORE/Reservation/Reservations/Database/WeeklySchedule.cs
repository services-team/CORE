using System;
using System.Collections.Generic;

namespace Reservations.Database
{
    public partial class WeeklySchedule
    {
        public WeeklySchedule()
        {
            Day = new HashSet<Day>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string FkUserId { get; set; }

        public AspNetUsers FkUser { get; set; }
        public ICollection<Day> Day { get; set; }
    }
}
