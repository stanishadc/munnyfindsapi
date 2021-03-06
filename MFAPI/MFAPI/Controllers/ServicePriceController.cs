﻿using MFAPI.Common;
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
    public class ServicePriceController : ControllerBase
    {
        private readonly SqlDbContext _context;
        public ServicePriceController(SqlDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<ServicePrice>>> Get()
        {
            return await _context.tblServicePrice
               .Select(x => new ServicePrice()
               {
                   ServiceId = x.ServiceId,
                   ServicePriceId = x.ServicePriceId,
                   ServicePriceName = x.ServicePriceName,
                   ServiceName = x.Service.ServiceName,
                   Duration = x.Duration,
                   Price = x.Price,
                   Description = x.Description,
                   BusinessId = x.Service.BusinessId,
                   BusinessTypeId = x.Service.Business.BusinessTypeId,
                   CategoryId = x.Service.CategoryId,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   Status = x.Status
               }).ToListAsync();
        }


        [HttpGet]
        [Route("GetById/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<ServicePrice>>> GetById(int BusinessId)
        {
            return await _context.tblServicePrice.Where(s => s.Service.BusinessId == BusinessId).Include(c => c.Service).ToListAsync();
        }
        [HttpGet]
        [Route("GetByServiceId")]
        public async Task<ActionResult<IEnumerable<ServicePrice>>> GetByServiceId()
        {
            return await _context.tblServicePrice.Include(c => c.Service).ToListAsync();
        }
        [HttpGet]
        [Route("GetByBusinessId/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<ServicePrice>>> GetByBusinessId(int BusinessId)
        {
            return await _context.tblServicePrice.Include(c => c.Service).Include(c => c.Service.Business).Where(c => c.Service.BusinessId== BusinessId).ToListAsync();
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] ServicePrice model)
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
        public async Task<ServicePrice> Edit(int id)
        {
            return await _context.tblServicePrice.FindAsync(id);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] ServicePrice model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.ServicePriceId)
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
                var treatmentprice = await _context.tblServicePrice.FindAsync(id);
                if (treatmentprice == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblServicePrice.Remove(treatmentprice);
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
