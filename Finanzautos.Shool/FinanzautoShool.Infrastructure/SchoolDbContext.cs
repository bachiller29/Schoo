using FinanzautoShool.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinanzautoShool.Infrastructure
{
    public class SchoolDbContext(DbContextOptions<SchoolDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
    }
}
