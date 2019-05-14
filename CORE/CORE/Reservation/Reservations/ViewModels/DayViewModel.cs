using Reservations.Database;
using Reservations.Viewmodels;
using Reservations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class DayViewModel
{
    public DayViewModel()
    {
        WorkTime = new HashSet<WorkTimeViewModel>();
    }

    public int Id { get; set; }
    //public int WeekDayId { get; set; }
    //public int ScheduleId { get; set; }

    public WeekDayViewModel WeekDay { get; set; }
    public ICollection<WorkTimeViewModel> WorkTime { get; set; }
}