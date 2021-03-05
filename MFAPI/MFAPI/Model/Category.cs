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
        public User User { get; set; }
        public int UserId { get; set; }
        public string Categoryurls { get; set; }
    }
}
