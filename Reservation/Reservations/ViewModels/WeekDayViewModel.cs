using Reservations.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.Viewmodels
{
    public partial class WeekDayViewModel
    {
        public WeekDayViewModel()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
