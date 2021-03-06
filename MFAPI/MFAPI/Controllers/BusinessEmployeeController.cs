﻿using System;
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
    public class BusinessEmployeeController : ControllerBase
    {
        private readonly SqlDbContext _context;
        public BusinessEmployeeController(SqlDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<BusinessEmployee>>> Get()
        {
            return await _context.tblBusinessEmployee.ToListAsync();
        }

        [HttpGet]
        [Route("GetById/{BusinessId}")]
        public async Task<ActionResult<IEnumerable<BusinessEmployee>>> GetById(int BusinessId)
        {
            return await _context.tblBusinessEmployee.Where(s => s.BusinessId == BusinessId).ToListAsync();
        }
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] BusinessEmployee model)
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
        public async Task<BusinessEmployee> Edit(int id)
        {
            return await _context.tblBusinessEmployee.FindAsync(id);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] BusinessEmployee model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.BusinessEmployeeId)
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
                var employee = await _context.tblBusinessEmployee.FindAsync(id);
                if (employee == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblBusinessEmployee.Remove(employee);
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
