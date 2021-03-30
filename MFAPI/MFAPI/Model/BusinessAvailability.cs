using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class BusinessAvailability
    {
        [Key]
        public int BusinessAvailabilityId { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public string MondayOpeningTime { get; set; }
        public string TuesdayOpeningTime { get; set; }
        public string WednesdayOpeningTime { get; set; }
        public string ThursdayOpeningTime { get; set; }
        public string FridayOpeningTime { get; set; }
        public string SaturdayOpeningTime { get; set; }
        public string SundayOpeningTime { get; set; }
        public string MondayClosingTime { get; set; }
        public string TuesdayClosingTime { get; set; }
        public string WednesdayClosingTime { get; set; }
        public string ThursdayClosingTime { get; set; }
        public string FridayClosingTime { get; set; }
        public string SaturdayClosingTime { get; set; }
        public string SundayClosingTime { get; set; }
    }
}
