using MFAPI.Common;
using MFAPI.Model;
using Microsoft.AspNetCore.Http;
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
    public class BusinessOfflineController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public BusinessOfflineController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<BusinessOffline>>> Get()
        {
            return await _context.tblBusinessOffline.ToListAsync();
        }
        [HttpGet]
        [Route("GetById/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<BusinessOffline>>> GetById(int BusinessId)
        {
            return await _context.tblBusinessOffline.Where(s => s.BusinessId == BusinessId).ToListAsync();
        }
        [HttpGet]
        [Route("getbyurl/{BusinessUrl}")]
        public async Task<ActionResult<IEnumerable<BusinessOffline>>> getbyurl(string BusinessUrl)
        {
            return await _context.tblBusinessOffline.Where(s => s.Business.BusinessUrl == BusinessUrl).ToListAsync();
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] BusinessOffline model)
        {
            Response _objResponse = new Response();
            try
            {
                if (ModelState.IsValid)
                {
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
        public async Task<BusinessOffline> Edit(int id)
        {
            return await _context.tblBusinessOffline.FindAsync(id);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] BusinessOffline model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.BusinessOfflineId)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
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
                var businessoffline = await _context.tblBusinessOffline.FindAsync(id);
                if (businessoffline == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblBusinessOffline.Remove(businessoffline);
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
    }
}
