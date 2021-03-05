using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Transaction
    {
        [Key]
        public int TransationId { get; set; }
        public int AppointmentId { get; set; }
        public Appointments Appointments { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ModeOfPayment { get; set; }
        public string TransationType { get; set; }
        public string PaymentGateway { get; set; }
        public string PaymentGatewayId { get; set; }
        public double Amount { get; set; }
    }
}
