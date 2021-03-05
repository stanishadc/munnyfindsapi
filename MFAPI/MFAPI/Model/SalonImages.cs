using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class SalonImages
    {
        [Key]
        public int SalonImageId { get; set; }
        public string ImageName { get; set; }
        public int SalonId { get; set; }
        public Salons Salon { get; set; }
        public bool Status { get; set; }
    }
}
