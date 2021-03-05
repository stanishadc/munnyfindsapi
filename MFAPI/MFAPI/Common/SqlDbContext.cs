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
        public DbSet<Appointments> tblAppointments { get; set; }
        public DbSet<Customer> tblCustomer { get; set; }
        public DbSet<SalonImages> tblSalonImages { get; set; }
        public DbSet<Treatment> tblTreatment { get; set; }
        public DbSet<TreatmentPrice> tblTreatmentPrice { get; set; }
        public DbSet<Transaction> tblTransaction { get; set; }
        public DbSet<Salons> tblSalons { get; set; }
        public DbSet<User> tblUser { get; set; }

    }
}
