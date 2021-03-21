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
    public class BusinessImagesController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BusinessImagesController(SqlDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<BusinessImages>>> Get()
        {
            return await _context.tblBusinessImages
                .Select(x => new BusinessImages()
                {
                    BusinessId = x.BusinessId,
                    BusinessImageId = x.BusinessImageId,
                    ImageName = x.ImageName,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Status = x.Status,
                    ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                }).ToListAsync();
        }

        [HttpGet]
        [Route("GetBySalon/{SalonId}")]
        public async Task<ActionResult<IEnumerable<BusinessImages>>> GetBySalon(int SalonId)
        {
            return await _context.tblBusinessImages
               .Select(x => new BusinessImages()
               {
                   BusinessId = x.BusinessId,
                   BusinessImageId = x.BusinessImageId,
                   ImageName = x.ImageName,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   Status = x.Status,
                   ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
               }).Where(x => x.BusinessId == SalonId).ToListAsync();
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] BusinessImages model)
        {
            Response _objResponse = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
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
        public async Task<BusinessImages> Edit(int id)
        {
            return await _context.tblBusinessImages.FindAsync(id);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] BusinessImages model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.BusinessImageId)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    if (model.ImageFile != null)
                    {
                        model.ImageName = await SaveImage(model.ImageFile);
                    }
                    model.UpdatedDate = DateTime.Now;
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
                var salonimages = await _context.tblBusinessImages.FindAsync(id);
                if (salonimages == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblBusinessImages.Remove(salonimages);
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
