using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations
{
    public class AppSettings
    {
        public string JWT_Secret { get; set; }
        public double JWT_Expiration_Time_Minutes { get; set; }
    }
}
