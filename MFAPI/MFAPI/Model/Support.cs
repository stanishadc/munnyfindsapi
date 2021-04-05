using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class Support
    {
        [Key]
        public int SupportId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
    }
}
