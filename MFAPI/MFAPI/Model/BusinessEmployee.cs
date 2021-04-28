using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class BusinessEmployee
    {
        [Key]
        public int BusinessEmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Designation { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
