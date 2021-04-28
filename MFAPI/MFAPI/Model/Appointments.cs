using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        public string AppointmentNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AppointmentTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string UserServices { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public string BookingStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ModeOfPayment { get; set; }
        public string PaymentPlace { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public double Tax { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int BusinessEmployeeId { get; set; }
        public BusinessEmployee BusinessEmployee { get; set; }
    }
}
