using Microsoft.EntityFrameworkCore;

namespace StudentManagementApi.Models
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}