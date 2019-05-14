using System;
using System.Collections.Generic;

namespace Reservations.Database
{
    public partial class SubService
    {
        public SubService()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string ServiceId { get; set; }

        public Service Service { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
    }
}
