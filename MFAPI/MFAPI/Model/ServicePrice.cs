using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Description { get; set; }
        public string ServicePriceName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public int BusinessId { get; set; }
        [NotMapped]
        public int BusinessTypeId { get; set; }
        [NotMapped]
        public int CategoryId { get; set; }
        [NotMapped]
        public string ServiceName { get; set; }
    }
}
