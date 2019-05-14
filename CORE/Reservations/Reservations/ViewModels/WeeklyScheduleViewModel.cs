using Reservations.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reservations.Viewmodels;

namespace Reservations.ViewModels
{
    public class WeeklyScheduleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FkUserId { get; set; }

        //public AspNetUsers FkUser { get; set; }
        public ICollection<DayViewModel> Day { get; set; }

        public  static List<WeeklyScheduleViewModel> Convert(List<WeeklySchedule> model)
        {
            return model.Select(x => new WeeklyScheduleViewModel
            {
                Id = x.Id,
                Title = x.Title,
                FkUserId = x.FkUserId,
                Day = x.Day.Select(c => new DayViewModel
                {
                    Id = c.Id,
                    WorkTime = c.WorkTime.Select(d => new WorkTimeViewModel
                    {
                        Id = d.Id,
                        MinutesFrom = d.MinutesFrom,
                        MinutesTo = d.MinutesTo,
                    }).ToList(),
                    WeekDay = new WeekDayViewModel
                    {
                        Id = c.WeekDay.Id,
                        Name = c.WeekDay.Name
                    } 
                }).ToList()
            }).ToList();
        }
    }
}
