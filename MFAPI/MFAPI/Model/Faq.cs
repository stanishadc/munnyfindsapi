using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class Faq
    {
        [Key]
        public int FaqId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }


    }
}
