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
    public class ServiceController : ControllerBase
    {
        private readonly SqlDbContext _context;
        public ServiceController(SqlDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Service>>> Get()
        {
            return await _context.tblService
               .Select(x => new Service()
               {
                   ServiceId = x.ServiceId,
                   ServiceName = x.ServiceName,
                   Description = x.Description,
                   BusinessId = x.BusinessId,
                   CategoryId = x.CategoryId,
                   Status = x.Status,
                   CreatedDate = x.CreatedDate,
                   UpdatedDate = x.UpdatedDate,
                   BusinessTypeId = x.Business.BusinessTypeId,
                   BusinessName = x.Business.BusinessName,
                   CategoryName = x.Category.CategoryName
               }).ToListAsync();
        }
        [HttpGet]
        [Route("GetByBusinessType/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetByBusinessType(int BusinessId)
        {
            return await _context.tblService.Where(c => c.BusinessId == BusinessId).ToListAsync();
        }
        [HttpGet]
        [Route("GetById/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetById(int BusinessId)
        {
            return await _context.tblService.Where(s => s.BusinessId == BusinessId).Include(c => c.Category).ToListAsync();
        }
        [HttpGet]
        [Route("GetByType/{CategoryId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetByType(int CategoryId)
        {
            return await _context.tblService.Where(c => c.CategoryId == CategoryId).ToListAsync();
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] Service model)
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
        public async Task<Service> Edit(int id)
        {
            return await _context.tblService.FindAsync(id);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] Service model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.ServiceId)
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
                var treatment = await _context.tblService.FindAsync(id);
                if (treatment == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblService.Remove(treatment);
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
