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
        [Route("GetReaders")]
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
        [Route("AddReader")]
        public async Task<ActionResult<Reader>> AddReader(Reader reader)
        {
            await _db.Readers.AddAsync(reader);
            await _db.SaveChangesAsync();
            return reader;
        }

        [HttpPost]
        [Route("EditReader")]
        public async Task<ActionResult<Reader>> EditReader(Reader reader)
        {
            _db.Readers.Update(reader);
            await _db.SaveChangesAsync();
            return reader;
        }

        [HttpPost]
        [Route("DeleteReader")]
        public async Task<ActionResult<Reader>> DeleteReader(Reader reader)
        {
            _db.Readers.Remove(reader);
            await _db.SaveChangesAsync();
            return reader;
        }

    }
}
