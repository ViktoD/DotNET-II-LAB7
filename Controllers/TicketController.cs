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
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
           
            return await _db.Tickets.Include(p => p.Reader).ToListAsync();
        }
        

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            return await _db.Tickets.FirstAsync(p => p.ID == id);
        }

        [HttpPost]
        public async Task AddTicket(Ticket ticket)
        {
            Reader? reader = await _db.Readers.FindAsync(ticket.ReaderID);
            if(reader != null)
            {
                ticket.Reader = reader;
                await _db.Tickets.AddAsync(ticket);
                await _db.SaveChangesAsync();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteTicket(int id)
        {
            _db.Tickets.Remove(await _db.Tickets.FirstAsync(p => p.ID == id));
            await _db.SaveChangesAsync();
        }
    }
}
