using System;
using System.Collections.Generic;

namespace Reservations.Database
{
    public partial class WorkTime
    {
        public int Id { get; set; }
        public int MinutesFrom { get; set; }
        public int MinutesTo { get; set; }
        public int DayId { get; set; }

        public Day Day { get; set; }
    }
}
