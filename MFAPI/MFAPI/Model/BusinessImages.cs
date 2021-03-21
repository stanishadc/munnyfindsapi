using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MFAPI.Model
{
    public class BusinessImages
    {
        [Key]
        public int BusinessImageId { get; set; }
        public string ImageName { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }
    }
}
