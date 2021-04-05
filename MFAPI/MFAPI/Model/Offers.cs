using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class Offers
    {
        public int OffersId { get; set; }
        public string Title { get; set; }
        public string Images { get; set; }
        public string OfferCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
