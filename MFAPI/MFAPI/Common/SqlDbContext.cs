using MFAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MFAPI.Common
{
    public class SqlDbContext:DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }
        public DbSet<BusinessType> tblBusinessType { get; set; }
        public DbSet<Category> tblCategory { get; set; }
        public DbSet<Appointments> tblAppointment { get; set; }
        public DbSet<Customer> tblCustomer { get; set; }
        public DbSet<BusinessImages> tblBusinessImages { get; set; }
        public DbSet<Service> tblService { get; set; }
        public DbSet<ServicePrice> tblServicePrice { get; set; }
        public DbSet<Transaction> tblTransaction { get; set; }
        public DbSet<Business> tblBusiness { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<BusinessAvailability> tblBusinessAvailability { get; set; }

    }
}
