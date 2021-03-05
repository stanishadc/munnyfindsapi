using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        public string AppointmentNo { get; set; }
        public DateTime BookingDate { get; set; }
        public int SalonId { get; set; }
        public int TreatmentPriceId { get; set; }
        public TreatmentPrice TreatmentPrice { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string AppointmentStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ModeOfPayment { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public double Tax { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
