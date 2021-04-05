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
        public DbSet<AboutUs> tblAboutUs { get; set; }
        public DbSet<Faq> tblFaq { get; set; }
        public DbSet<TermsAndConditions> tblTermsAndConditions { get; set; }
        public DbSet<ContactUs> tblContactUs { get; set; }
        public DbSet<Offers> tblOffers { get; set; }
        public DbSet<Subject> tblSubject { get; set; }
        public DbSet<PrivacyPolicy> tblPrivacyPolicy { get; set; }
        public DbSet<Support> tblSupport { get; set; }
        public DbSet<AppointmentServices> tblAppointmentService { get; set; }
        public DbSet<BusinessOffline> tblBusinessOffline { get; set; }
    }
}
