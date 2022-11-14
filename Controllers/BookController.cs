using lab7.Server.Database;
using lab7.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DbConnection _db;

        public BookController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await _db.Books.OrderBy(p => p.Name).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _db.Books.FirstAsync(p => p.ID == id);
        }

        [HttpPost]
        public async Task AddBook(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task EditBook(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteBook(int id)
        {
            _db.Books.Remove(await _db.Books.FirstAsync(p => p.ID == id));
            await _db.SaveChangesAsync();
        }
    }
}
