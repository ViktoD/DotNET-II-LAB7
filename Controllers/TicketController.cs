using lab7.Server.Database;
using lab7.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly DbConnection _db;
       
        public TicketController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetTickets")]
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
            return await _db.Tickets.Include(p => p.Reader).ToListAsync();
        }
        

        [HttpGet("{id:int}")]
        [Route("GetTicket/{id:int}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            return await _db.Tickets.FirstAsync(p => p.ID == id);
        }

        [HttpPost]
        [Route("AddTicket")]
        public async Task<ActionResult<Ticket>> AddTicket(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
            return ticket;
        }

        [HttpPost]
        [Route("DeleteTicket")]
        public async Task<ActionResult<Ticket>> DeleteTicket(Ticket ticket)
        {
            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();
            return ticket;
        }
    }
}
