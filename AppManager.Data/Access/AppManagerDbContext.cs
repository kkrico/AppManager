using System.Data.Entity;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public partial class AppManagerDbContext : DbContext
    {
        public AppManagerDbContext()
            : this(default (string))
        {
        }

        public AppManagerDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }   

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<IISWebsite> IISWebsite { get; set; }
        public virtual DbSet<IISWebsitesoapservice> IISWebsitesoapservice { get; set; }
        public virtual DbSet<Logentry> Logentry { get; set; }
        public virtual DbSet<Soapendpoint> Soapendpoint { get; set; }
        public virtual DbSet<Soapservice> Soapservice { get; set; }
        public virtual DbSet<Webserver> Webserver { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(e => e.Nameapplication)
                .IsUnicode(false);

            modelBuilder.Entity<Application>()
                .Property(e => e.Initialsapplication)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebsite>()
                .Property(e => e.Namewebsite)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebsite>()
                .Property(e => e.Applogpath)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebsite>()
                .HasMany(e => e.IISWebsitesoapservices)
                .WithRequired(e => e.IISWebsite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Logentry>()
                .Property(e => e.Urlpath)
                .IsUnicode(false);

            modelBuilder.Entity<Logentry>()
                .Property(e => e.Method)
                .IsUnicode(false);

            modelBuilder.Entity<Logentry>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Logentry>()
                .Property(e => e.Hash)
                .IsUnicode(false);

            modelBuilder.Entity<Soapendpoint>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Soapservice>()
                .Property(e => e.Namesoapservice)
                .IsUnicode(false);

            modelBuilder.Entity<Soapservice>()
                .HasMany(e => e.Iiswebsitesoapservice)
                .WithRequired(e => e.Soapservice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Webserver>()
                .Property(e => e.Namewebserver)
                .IsUnicode(false);
        }
    }
}
