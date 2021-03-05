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
            return await _context.tblAppointments.OrderBy(c => c.AppointmentId).ToListAsync();
        }

        [HttpGet]
        [Route("GetById/{AppointmentId}")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetById(int AppointmentId)
        {
            return await _context.tblAppointments.Where(s => s.AppointmentId == AppointmentId).Include(c => c.Customer).Include(tp => tp.TreatmentPrice).Include(t => t.TreatmentPrice.Treatment).Include(s => s.TreatmentPrice.Treatment.Salon).ToListAsync();
        }
        [HttpGet]
        [Route("GetByCustomer/{CustomerId}")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetByCustomer(int CustomerId)
        {
            return await _context.tblAppointments.Where(s => s.CustomerId == CustomerId).Include(c => c.Customer).Include(tp => tp.TreatmentPrice).Include(t => t.TreatmentPrice.Treatment).Include(s => s.TreatmentPrice.Treatment.Salon).ToListAsync();
        }
        [HttpGet]
        [Route("GetBySalon/{SalonId}")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetBySalon(int SalonId)
        {
            return await _context.tblAppointments.Where(s => s.SalonId == SalonId).Include(c => c.Customer).Include(tp => tp.TreatmentPrice).Include(t => t.TreatmentPrice.Treatment).Include(s => s.TreatmentPrice.Treatment.Salon).ToListAsync();
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
                _objResponse.Status = ex.ToString();
                Console.WriteLine("\nMessage ---\n{0}", ex.ToString());
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
            }
            return _objResponse;
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<Appointments> Edit(int id)
        {
            return await _context.tblAppointments.FindAsync(id);
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
                var appointements = await _context.tblAppointments.FindAsync(id);
                if (appointements == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblAppointments.Remove(appointements);
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
