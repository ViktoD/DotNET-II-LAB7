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
        [Route("GetInfoBooks")]
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
        [Route("AddInfoBook")]
        public async Task<ActionResult<InfoBook>> AddInfoBook(InfoBook infoBook)
        {
            await _db.InfoBooks.AddAsync(infoBook);
            await _db.SaveChangesAsync();
            return infoBook;
        }

        [HttpPost]
        [Route("DeleteInfoBook")]
        public async Task<ActionResult<InfoBook>> DeleteInfoBook(InfoBook infoBook)
        {
            _db.InfoBooks.Remove(infoBook);
            await _db.SaveChangesAsync();
            return infoBook;
        }
    }
}
