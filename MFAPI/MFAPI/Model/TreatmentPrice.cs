using System;
using System.ComponentModel.DataAnnotations;
namespace MFAPI.Model
{
    public class TreatmentPrice
    {
        [Key]
        public int TreatmentPriceId { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
