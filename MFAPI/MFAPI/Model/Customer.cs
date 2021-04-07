using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Password { get; set; }
        public string CustomerOTP { get; set; }
    }
    public class VerifyModel
    {
        public string CustomerEmail { get; set; }
        public string CustomerOTP { get; set; }
    }
}
