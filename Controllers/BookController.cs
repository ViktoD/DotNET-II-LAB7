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
        [Route("GetBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await _db.Books.OrderBy(p => p.Name).ToListAsync();
        }

        [HttpGet("{id:int}")]
        [Route("GetBook/{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _db.Books.FirstAsync(p => p.ID == id);
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return book;
        }

        [HttpPost]
        [Route("EditBook")]
        public async Task<ActionResult<Book>> EditBook(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return book;
        }

        [HttpPost]
        [Route("DeleteBook")]
        public async Task<ActionResult<Book>> DeleteBook(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return book;
        }
    }
}
