using MFAPI.Common;
using MFAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BusinessController(SqlDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Business>>> Get()
        {
             return await _context.tblBusiness.Include(c => c.BusinessType).ToListAsync();
        }
        [HttpGet]
        [Route("GetTopBusiness")]
        public async Task<ActionResult<IEnumerable<Business>>> GetTopBusiness()
        {
            return await _context.tblBusiness
               .Select(x => new Business()
               {
                   BusinessId = x.BusinessId,
                   BusinessName = x.BusinessName,
                   BusinessUrl = x.BusinessUrl,
                   ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
               }).Take(5).ToListAsync();
        }
        [HttpGet]
        [Route("GetById/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetById(int BusinessId)
        {
            return await _context.tblBusiness
               .Select(x => new Business()
               {
                   BusinessId = x.BusinessId,
                   BusinessName = x.BusinessName,
                   BusinessUrl = x.BusinessUrl,
                   BusinessTypeId = x.BusinessTypeId,
                   ContactName = x.ContactName,
                   Landline = x.Landline,
                   MobileNo = x.MobileNo,
                   Email = x.Email,
                   Address = x.Address,
                   GoogleMapURL = x.GoogleMapURL,
                   Latitude = x.Latitude,
                   Longitude = x.Longitude,
                   Location = x.Location,
                   ZipCode = x.ZipCode,
                   City = x.City,
                   Country = x.Country,
                   Password = x.Password,
                   About = x.About,
                   Currency = x.Currency,
                   TotalRatings = x.TotalRatings,
                   Rating = x.Rating,
                   ImageName = x.ImageName,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   Status = x.Status,
                   ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
               }).Where(x => x.BusinessId == BusinessId).ToListAsync();
        }
        [HttpGet]
        [Route("GetByURL/{Businessurl}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetByURL(string Businessurl)
        {
            return await _context.tblBusiness
               .Select(x => new Business()
               {
                   BusinessId = x.BusinessId,
                   BusinessName = x.BusinessName,
                   BusinessUrl = x.BusinessUrl,
                   BusinessTypeId = x.BusinessTypeId,
                   ContactName = x.ContactName,
                   Landline = x.Landline,
                   MobileNo = x.MobileNo,
                   Email = x.Email,
                   Address = x.Address,
                   GoogleMapURL = x.GoogleMapURL,
                   Latitude = x.Latitude,
                   Longitude = x.Longitude,
                   Location = x.Location,
                   ZipCode = x.ZipCode,
                   City = x.City,
                   Country = x.Country,
                   Password = x.Password,
                   About = x.About,
                   Currency = x.Currency,
                   TotalRatings = x.TotalRatings,
                   Rating = x.Rating,
                   ImageName = x.ImageName,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   Status = x.Status,
                   ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
               }).Where(s => s.BusinessUrl == Businessurl).ToListAsync();
        }
        [HttpGet]
        [Route("GetByType/{BusinessType}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetByType(string BusinessType)
        {
            return await _context.tblBusiness
               .Select(x => new Business()
               {
                   BusinessId = x.BusinessId,
                   BusinessName = x.BusinessName,
                   BusinessUrl = x.BusinessUrl,
                   BusinessTypeId = x.BusinessTypeId,
                   ContactName = x.ContactName,
                   Landline = x.Landline,
                   MobileNo = x.MobileNo,
                   Email = x.Email,
                   Address = x.Address,
                   GoogleMapURL = x.GoogleMapURL,
                   Latitude = x.Latitude,
                   Longitude = x.Longitude,
                   Location = x.Location,
                   ZipCode = x.ZipCode,
                   City = x.City,
                   Country = x.Country,
                   Password = x.Password,
                   About = x.About,
                   Currency = x.Currency,
                   TotalRatings = x.TotalRatings,
                   Rating = x.Rating,
                   ImageName = x.ImageName,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   Status = x.Status,
                   ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
               }).Where(s => s.BusinessType.Business == BusinessType).ToListAsync();
        }
        [HttpGet]
        [Route("GetListByType/{BusinessType}")]
        public async Task<ActionResult<IEnumerable<Business>>> GetListByType(string BusinessType)
        {
            return await _context.tblBusiness
              .Select(x => new Business()
              {
                  BusinessId = x.BusinessId,
                  BusinessName = x.BusinessName,
                  BusinessUrl = x.BusinessUrl,
                  Rating=x.Rating,
                  Location=x.Location,
                  City=x.City,
                  About=x.About,
                  ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                  BusinessType=x.BusinessType
              }).Where(s => s.BusinessType.Business == BusinessType).ToListAsync();
        }
        [HttpPost]
        [Route("businesslogin")]
        public async Task<Response> businesslogin([FromBody] LoginModel model)
        {
            Response _objResponse = new Response();
            try
            {
                List<Business> businesses = await _context.tblBusiness.Where(u => u.Email == model.Email && u.Password == model.Password).ToListAsync();
                if (businesses.Count > 0)
                {
                    _objResponse.Data = businesses;
                    _objResponse.UserId = businesses[0].BusinessId;
                    _objResponse.Status = "Login Success";
                }
                else
                {
                    _objResponse.Data = "";
                    _objResponse.UserId = 0;
                    _objResponse.Status = "Invalid Credentails";
                }
            }
            catch (Exception ex)
            {
                _objResponse.Data = null;
                _objResponse.Status = ex.Message;
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            }
            return _objResponse;
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] Business model)
        {
            Response _objResponse = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    model.BusinessUrl = urlreplace(model.BusinessName);
                    if (model.ImageFile != null)
                    {
                        model.ImageName = await SaveImage(model.ImageFile);
                    }
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    _objResponse.Status = "Success";
                    _objResponse.Data = null;
                }
                else
                {
                    _objResponse.Status = "Validation Error";
                    _objResponse.Data = null;
                }
            }
            catch (Exception ex)
            {
                _objResponse.Data = null;
                _objResponse.Status = ex.ToString();
                Console.WriteLine("\nMessage ---\n{0}", ex.ToString());
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            }
            return _objResponse;
        }
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<Business> Edit(int id)
        {
            return await _context.tblBusiness.FindAsync(id);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] Business model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.BusinessId)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    model.UpdatedDate = DateTime.Now;
                    model.BusinessUrl = urlreplace(model.BusinessName);
                    if (model.ImageFile != null)
                    {
                        model.ImageName = await SaveImage(model.ImageFile);
                    }
                    _context.Entry(model).Property(x => x.CreatedDate).IsModified = false;
                    _context.Entry(model).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _objResponse.Status = "Success";
                    _objResponse.Data = null;
                }
            }
            catch (Exception ex)
            {
                _objResponse.Data = null;
                _objResponse.Status = ex.ToString();
                Console.WriteLine("\nMessage ---\n{0}", ex.ToString());
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            }
            return _objResponse;
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<Response> Delete(int id)
        {
            Response _objResponse = new Response();
            try
            {
                var business = await _context.tblBusiness.FindAsync(id);
                if (business == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblBusiness.Remove(business);
                    await _context.SaveChangesAsync();
                    _objResponse.Status = "Success";
                    _objResponse.Data = null;
                }
            }
            catch (Exception ex)
            {
                _objResponse.Data = null;
                _objResponse.Status = ex.Message;
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            }
            return _objResponse;
        }
        private string urlreplace(string name)
        {
            return name.Replace(" ", "-");
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile ImageFile)
        {
            string ImageName = new string(Path.GetFileNameWithoutExtension(ImageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            ImageName = ImageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(ImageFile.FileName);
            var ImagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "SalonImages", ImageName);
            using (var fileStream = new FileStream(ImagePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return ImageName;
        }
    }
}
