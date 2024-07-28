using BookManagementWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagementWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
