using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFAPI.Common;
using MFAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MFAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly SqlDbContext _context;
        public SupportController(SqlDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Support>>> Get()
        {
            //return await _context.tblSupport.ToListAsync();
           return await _context.tblSupport.Include(s => s.Subject).ToListAsync();
        }
        [HttpGet]
        [Route("GetById/{SupportId}")]
        public async Task<ActionResult<IEnumerable<Support>>> GetById(int SupportId)
        {
            return await _context.tblSupport.Where(s => s.SupportId == SupportId).ToListAsync();
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] Support model)
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
        public async Task<Support> Edit(int id)
        {
            return await _context.tblSupport.FindAsync(id);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] Support model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.SupportId)
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
    }
}
