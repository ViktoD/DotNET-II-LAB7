using lab7.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace lab7.Server.Database
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options) : base(options)
        {

        }

        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<InfoBook> InfoBooks { get; set; } = null!;
    }
}
