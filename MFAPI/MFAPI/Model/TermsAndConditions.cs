﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class TermsAndConditions
    {
        [Key]
        public int TermsAndconditionsId { get; set; }
        public string Title { get; set; }
        public string ParaGraph { get; set; }
        public string ParaGraph1 { get; set; }
    }
}
