using BookListManagement.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BookListManagement.Server.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
