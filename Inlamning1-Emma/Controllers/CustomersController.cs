using Inlamning1_Emma.Data;
using Inlamning1_Emma.Models;
using Inlamning1_Emma.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Inlamning1_Emma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SqlDataContext _context;

        public CustomersController(SqlDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest req)
        {
            try
            {
                if (!await _context.Customers.AnyAsync(x => x.Email == req.Email))
                {
                    var customerEntity = new CustomerEntity { FirstName = req.FirstName, LastName = req.LastName, Email = req.Email };
                    _context.Add(customerEntity);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(customerEntity);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = new List<CustomerResponse>();
                foreach (var customer in await _context.Customers.ToArrayAsync())
                    customers.Add(new CustomerResponse { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email });

                return new OkObjectResult(customers);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


    }
}
