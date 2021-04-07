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
    public class CustomerController : ControllerBase
    {
        private readonly SqlDbContext _context;
        GlobalData globalData = new GlobalData();
        public CustomerController(SqlDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            return await _context.tblCustomer.ToListAsync();
        }
        [HttpGet]
        [Route("GetById/{CustomerId}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetById(int CustomerId)
        {
            return await _context.tblCustomer.Where(s => s.CustomerId == CustomerId).ToListAsync();
        }
        [HttpPost]
        [Route("customerlogin")]
        public async Task<Response> customerlogin([FromBody] LoginModel model)
        {
            Response _objResponse = new Response();
            try
            {
                List<Customer> customers = await _context.tblCustomer.Where(u => u.CustomerEmail == model.Email && u.Password == model.Password).ToListAsync();
                if (customers.Count > 0)
                {
                    _objResponse.Data = customers;
                    _objResponse.UserId = customers[0].CustomerId;
                    _objResponse.Status = "Login Success";
                }
                else
                {
                    _objResponse.Data = "";
                    _objResponse.UserId = 0;
                    _objResponse.Status = "Invalid Credentails";
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
        [HttpPost]
        [Route("verifyCustomer")]
        public async Task<Response> verifyCustomer([FromBody] VerifyModel model)
        {
            Response _objResponse = new Response();
            try
            {
                List<Customer> customers = await _context.tblCustomer.Where(u => u.CustomerEmail == model.CustomerEmail && u.CustomerOTP == model.CustomerOTP).ToListAsync();
                if (customers.Count > 0)
                {
                    Customer c = (from x in _context.tblCustomer
                                  where x.CustomerEmail == model.CustomerEmail
                                  select x).First();
                    c.Status = true;
                    await _context.SaveChangesAsync();

                    _objResponse.Data = null;
                    _objResponse.UserId = customers[0].CustomerId;
                    _objResponse.Status = "Account Verified";
                }
                else
                {
                    _objResponse.Data = "";
                    _objResponse.UserId = 0;
                    _objResponse.Status = "Invalid OTP";
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
        [HttpPost]
        [Route("Insert")]
        public async Task<Response> Insert([FromForm] Customer model)
        {
            Response _objResponse = new Response();
            try
            {
                List<Customer> customers = await _context.tblCustomer.Where(u => u.CustomerEmail == model.CustomerEmail).ToListAsync();
                if (customers.Count > 0)
                {
                    _objResponse.Status = "Email Already Exists!";
                    _objResponse.Data = null;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        model.CreatedDate = DateTime.Now;
                        model.UpdatedDate = DateTime.Now;
                        model.CustomerOTP= globalData.GenerateRandomNo().ToString();
                        _context.Add(model);
                        await _context.SaveChangesAsync();
                        _objResponse.Status = "Success";
                        _objResponse.Data = "";
                        var a = Task.Factory.StartNew(() => globalData.SendEmail("Dear User, <br /> Please Enter the OTP " + model.CustomerOTP + "to verify your account", "Please Verify Email | MunyFinds.com", model.CustomerEmail));
                    }
                    else
                    {
                        _objResponse.Status = "Please enter correct data";
                        _objResponse.Data = null;
                    }
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
        public async Task<Customer> Edit(int id)
        {
            return await _context.tblCustomer.FindAsync(id);
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<Response> Update(int id, [FromForm] Customer model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.CustomerId)
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
        [HttpPut]
        [Route("ChangePassword/{id}")]
        public async Task<Response> ChangePassword(int id, [FromForm] Customer model)
        {
            Response _objResponse = new Response();
            try
            {
                if (id != model.CustomerId)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    model.UpdatedDate = DateTime.Now;
                    _context.Entry(model).Property(x => x.CustomerName).IsModified = false;
                    _context.Entry(model).Property(x => x.CustomerEmail).IsModified = false;
                    _context.Entry(model).Property(x => x.CustomerMobile).IsModified = false;
                    _context.Entry(model).Property(x => x.Status).IsModified = false;
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
                var customer = await _context.tblCustomer.FindAsync(id);
                if (customer == null)
                {
                    _objResponse.Status = "No record found";
                    _objResponse.Data = null;
                }
                else
                {
                    _context.tblCustomer.Remove(customer);
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
