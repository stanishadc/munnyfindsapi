using MFAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MFAPI.Common
{
    public class SqlDbContext:DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }
        public DbSet<Category> tblCategory { get; set; }
        public DbSet<Appointments> tblAppointements { get; set; }
        public DbSet<User> tblUser { get; set; }
    }
}
