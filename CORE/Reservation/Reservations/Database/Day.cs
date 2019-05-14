using System;
using System.Collections.Generic;

namespace Reservations.Database
{
    public partial class Day
    {
        public Day()
        {
            WorkTime = new HashSet<WorkTime>();
        }

        public int Id { get; set; }
        public int WeekDayId { get; set; }
        public int ScheduleId { get; set; }

        public WeeklySchedule Schedule { get; set; }
        public WeekDays WeekDay { get; set; }
        public ICollection<WorkTime> WorkTime { get; set; }
    }
}
