using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class AboutUs
    {
        [Key]
        public int AboutUsId { get; set; }
        public string Title { get; set; }
        public string ParaGraph { get; set; }
        public string ParaGraph1 { get; set; }
        public string SubTitle { get; set; }
        public string SubTitle1 { get; set; }
        public string SubTitle2 { get; set; }
        public string SubParaGraph { get; set; }
        public string SubParaGraph1 { get; set; }
        public string SubParaGraph2 { get; set; }
    }
}
