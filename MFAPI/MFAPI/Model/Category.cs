using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public BusinessType BusinessType { get; set; }
        public int BusinessTypeId { get; set; }
        public string Categoryurl { get; set; }
    }
}
