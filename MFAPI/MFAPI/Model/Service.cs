using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public int BusinessTypeId { get; set; }
        [NotMapped]
        public string BusinessName { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
    }
}
