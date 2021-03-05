using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public string Limitation { get; set; }
        public int SalonId { get; set; }
        public Salons Salon { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
