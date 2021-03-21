using System;
using System.ComponentModel.DataAnnotations;
namespace MFAPI.Model
{
    public class ServicePrice
    {
        [Key]
        public int ServicePriceId { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
