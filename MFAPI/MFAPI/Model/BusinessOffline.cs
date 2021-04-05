using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Model
{
    public class BusinessOffline
    {
        public int BusinessOfflineId { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public DateTime OfflineDate { get; set; }
    }
}
