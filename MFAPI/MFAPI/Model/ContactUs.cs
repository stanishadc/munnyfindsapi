using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class ContactUs
    {
        [Key]
        public int ContactUsId { get; set; }
        public string Title { get; set; }
        public string ParaGraph { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string GoogleMapUrl { get; set; }
    }
}
