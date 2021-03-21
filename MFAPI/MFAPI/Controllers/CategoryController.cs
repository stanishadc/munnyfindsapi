using MFAPI.Common;
using MFAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CategoryController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _context.tblCategory.ToListAsync();
        }

        [HttpGet]
        [Route("GetByStatus")]
        public async Task<ActionResult<IEnumerable<Category>>> GetByStatus()
        {
            return await _context.tblCategory.Where(c => c.Status == true).ToListAsync();
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
    }
}
