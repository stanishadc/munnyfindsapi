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
    public class CategoryController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(SqlDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _context.tblCategory
              .Select(x => new Category()
              {
                  CategoryId = x.CategoryId,
                  CategoryName = x.CategoryName,
                  Categoryurl = x.Categoryurl,
                  Status = x.Status,
                  BusinessTypeId = x.BusinessTypeId,
                  CreatedDate = x.CreatedDate,
                  UpdatedDate = x.UpdatedDate,
                  ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                  BusinessType = x.BusinessType
              }).ToListAsync();
        }
        [HttpGet]
        [Route("GetTopCategory")]
        public async Task<ActionResult<IEnumerable<Category>>> GetTopCategory()
        {
            return await _context.tblCategory
              .Select(x => new Category()
              {
                  CategoryId = x.CategoryId,
                  CategoryName = x.CategoryName,
                  Categoryurl = x.Categoryurl,
                  Status = x.Status,
                  BusinessTypeId = x.BusinessTypeId,
                  CreatedDate = x.CreatedDate,
                  UpdatedDate = x.UpdatedDate,
                  ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                  BusinessType = x.BusinessType
              }).Take(5).ToListAsync();
        }
        [HttpGet]
        [Route("GetByType/{BusinessTypeId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetByType(int BusinessTypeId)
        {
            return await _context.tblCategory
              .Select(x => new Category()
              {
                  CategoryId = x.CategoryId,
                  CategoryName = x.CategoryName,
                  Categoryurl = x.Categoryurl,
                  Status = x.Status,
                  BusinessTypeId = x.BusinessTypeId,
                  CreatedDate = x.CreatedDate,
                  UpdatedDate = x.UpdatedDate,
                  ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                  BusinessType = x.BusinessType
              }).Where(c => c.BusinessTypeId == BusinessTypeId).ToListAsync();
        }
        [HttpGet]
        [Route("GetByStatus")]
        public async Task<ActionResult<IEnumerable<Category>>> GetByStatus()
        {
            return await _context.tblCategory
              .Select(x => new Category()
              {
                  CategoryId = x.CategoryId,
                  CategoryName = x.CategoryName,
                  Categoryurl = x.Categoryurl,
                  Status = x.Status,
                  BusinessTypeId = x.BusinessTypeId,
                  CreatedDate = x.CreatedDate,
                  UpdatedDate = x.UpdatedDate,
                  ImageSrc = String.Format("{0}://{1}{2}/SalonImages/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                  BusinessType = x.BusinessType
              }).Where(c => c.Status == true).ToListAsync();
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] Category model)
        {
            Response _objResponse = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    model.Categoryurl = urlreplace(model.CategoryName);
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
        public async Task<Category> Edit(int id)
        {
            return await _context.tblCategory.FindAsync(id);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] Category model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.CategoryId)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    model.UpdatedDate = DateTime.Now;
                    model.Categoryurl = urlreplace(model.CategoryName);
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
                var category = await _context.tblCategory.FindAsync(id);
                if (category == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblCategory.Remove(category);
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
