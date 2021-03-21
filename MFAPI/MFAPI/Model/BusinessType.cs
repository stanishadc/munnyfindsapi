using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class BusinessType
    {
        [Key]
        public int BusinessTypeId { get; set; }
        public string Business { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string BusinessTypeURL { get; set; }
    }
}
