using MFAPI.Common;
using MFAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {

        private readonly SqlDbContext _context;

        public AppointmentsController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Appointments>>> Get()
        {
            return await _context.tblAppointements.ToListAsync();
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] Appointments model)
        {
            Response _objResponse = new Response();
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
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
                _objResponse.Status = ex.Message;
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            }
            return _objResponse;
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<Appointments> Edit(int id)
        {
            return await _context.tblAppointements.FindAsync(id);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] Appointments model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.AppointmentId)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    model.UpdatedDate = DateTime.Now;
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
                _objResponse.Status = ex.Message;
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
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
                var Appointements = await _context.tblAppointements.FindAsync(id);
                if (Appointements == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblAppointements.Remove(Appointements);
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
