using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class AppointmentServices
    {
        [Key]
        public int AppointmentServiceId { get; set; }
        public int ServicePriceId { get; set; }
        public ServicePrice ServicePrice { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double Price { get; set; }
        public string Duration { get; set; }
        public int AppointmentId { get; set; }
        public Appointments Appointments { get; set; }
    }
}
