namespace Prospects.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Companies.Company> Companies { get; set; }
    }
}
