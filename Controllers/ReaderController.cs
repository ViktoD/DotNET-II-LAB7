using lab7.Server.Database;
using lab7.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly DbConnection _db;

        public ReaderController(DbConnection db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reader>>> GetReaders()
        {
            return await _db.Readers.OrderBy(p => p.Surname).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reader>> GetReader(int id)
        {
            return await _db.Readers.FirstAsync(p => p.ID == id);
        }

        [HttpPost]
        public async Task AddReader(Reader reader)
        {
   
            await _db.Readers.AddAsync(reader);
            await _db.SaveChangesAsync();
        }

        [HttpPut]
        public async Task EditReader(Reader reader)
        {
            _db.Readers.Update(reader);
            await _db.SaveChangesAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteReader(int id)
        {
            _db.Readers.Remove(await _db.Readers.FirstAsync(p => p.ID == id));
            await _db.SaveChangesAsync();
        }

    }
}
