using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
