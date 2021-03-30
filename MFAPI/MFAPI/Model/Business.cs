using System;
using System.ComponentModel.DataAnnotations;

namespace MFAPI.Model
{
    public class Business
    {
        [Key]
        public int BusinessId { get; set; }
        public string BusinessName{ get; set; }
        public string Businessurl { get; set; }
        public BusinessType BusinessType { get; set; }
        public int BusinessTypeId { get; set; }
        public string ContactName { get; set; }
        public string Landline { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string GoogleMapURL { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Currency { get; set; }
        public string About { get; set; }
        public int TotalRatings { get; set; }
        public double Rating { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
