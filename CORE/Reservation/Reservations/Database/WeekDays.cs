using System;
using System.Collections.Generic;

namespace Reservations.Database
{
    public partial class WeekDays
    {
        public WeekDays()
        {
            Day = new HashSet<Day>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Day> Day { get; set; }
    }
}
