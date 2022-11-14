using lab7.Server.Database;
using lab7.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoBookController : ControllerBase
    {
        private readonly DbConnection _db;

        public InfoBookController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<InfoBook>>> GetInfoBooks()
        {
            return await _db.InfoBooks.Include(p => p.Ticket).Include(p => p.Book).Include(p => p.Ticket.Reader).OrderBy(p => p.Ticket.Reader.Surname).ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<InfoBook>> GetInfoBook(int id)
        {
            return await _db.InfoBooks.FirstAsync(p => p.ID == id);
        }

        [HttpPost]
        public async Task AddInfoBook(InfoBook infoBook)
        {
            Book? book = await _db.Books.FindAsync(infoBook.BookID);
            Ticket? ticket = await _db.Tickets.FindAsync(infoBook.TicketID);
            Reader? reader = await _db.Readers.FindAsync(infoBook.Ticket.ReaderID);
            
            if(book != null && ticket != null && reader != null)
            {
                infoBook.Book = book;
                infoBook.Ticket = ticket;
                infoBook.Ticket.Reader = reader;
                await _db.InfoBooks.AddAsync(infoBook);
                await _db.SaveChangesAsync();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteInfoBook(int id)
        {
            _db.InfoBooks.Remove(await _db.InfoBooks.FirstAsync(p => p.ID == id));
            await _db.SaveChangesAsync();
        }
    }
}
