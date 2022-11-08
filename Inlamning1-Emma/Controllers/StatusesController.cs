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
    public class StatusesController : ControllerBase
    {
        private readonly SqlDataContext _context;

        public StatusesController(SqlDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatusRequest req)
        {
            try
            {
                if (!await _context.Statuses.AnyAsync(x => x.Status == req.Status))
                {
                    var statusEntity = new StatusEntity { Status = req.Status };
                    _context.Statuses.Add(statusEntity);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(statusEntity);
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
                var statuses = new List<StatusResponse>();
                foreach (var status in await _context.Statuses.ToArrayAsync())
                    statuses.Add(new StatusResponse { Id = status.Id, Status = status.Status });

                    return new OkObjectResult(statuses);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}
