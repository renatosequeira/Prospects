namespace Prospects.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Companies.Company> Companies { get; set; }

        public DbSet<Communications.Communication> Communications { get; set; }

        public DbSet<Communications.Helpers.CommunicationType> CommunicationTypes { get; set; }

        public DbSet<Companies.Helpers.ActivitySector> ActivitySectors { get; set; }

        public DbSet<Companies.Helpers.LegalForm> LegalForms { get; set; }

        public System.Data.Entity.DbSet<Prospects.Domain.Companies.Helpers.ComercialStatus> ComercialStatus { get; set; }

        public System.Data.Entity.DbSet<Prospects.Domain.Companies.Helpers.CompanyClassification> CompanyClassifications { get; set; }
    }
}
