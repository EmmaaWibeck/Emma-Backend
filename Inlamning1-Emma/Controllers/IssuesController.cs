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
    public class IssuesController : ControllerBase
    {
        private readonly SqlDataContext _context;

        public IssuesController(SqlDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IssueRequest req)
        {
            try
            {
                var datetime = DateTime.Now;
                var issueEntity = new IssueEntity
                {
                    Subject = req.Subject,
                    Description = req.Description,
                    CustomerId = req.CustomerId,
                    Created = datetime,
                    Modified = datetime,
                    StatusId = 1
                };

                _context.Add(issueEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(issueEntity);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var issues = new List<IssueResponse>();
                foreach (var issueEntity in await _context.Issues.Include(x => x.Status).Include(x => x.Customer).ToArrayAsync())
                    issues.Add(new IssueResponse 
                    { 
                        Id = issueEntity.Id,
                        Subject = issueEntity.Subject,
                        Description = issueEntity.Description,
                        Created = issueEntity.Created,
                        Modified = issueEntity.Modified,
                        Status = new StatusResponse
                        {
                            Id = issueEntity.Status.Id,
                            Status = issueEntity.Status.Status
                        },
                        Customer = new CustomerResponse
                        {
                            Id = issueEntity.Customer.Id,
                            FirstName = issueEntity.Customer.FirstName,
                            LastName = issueEntity.Customer.LastName,
                            Email = issueEntity.Customer.Email
                        }
                    
                    });

                return new OkObjectResult(issues);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}
