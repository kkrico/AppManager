using System.Data.Entity;
using System.Diagnostics;
using AppManager.Data.Entity;

namespace AppManager.Data.Access
{
    public class AppManagerDbContext : DbContext
    {
        public AppManagerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            this.Database.Log += s => Debug.WriteLine(s);
            Database.SetInitializer<AppManagerDbContext>(null);
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<IISApplication> IISApplication { get; set; }
        public virtual DbSet<IISApplicationSoapService> IISApplicationSoapService { get; set; }
        public virtual DbSet<IISWebSite> IISWebSite { get; set; }
        public virtual DbSet<Logentry> Logentry { get; set; }
        public virtual DbSet<SoapEndpoint> SoapEndpoint { get; set; }
        public virtual DbSet<SoapService> SoapService { get; set; }
        public virtual DbSet<Webserver> Webserver { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(e => e.Nameapplication)
                .IsUnicode(false);

            modelBuilder.Entity<Application>()
                .Property(e => e.Initialsapplication)
                .IsUnicode(false);

            modelBuilder.Entity<IISApplication>()
                .Property(e => e.Applogpath)
                .IsUnicode(false);

            modelBuilder.Entity<IISApplication>()
                .Property(e => e.Physicalpath)
                .IsUnicode(false);

            modelBuilder.Entity<IISApplication>()
                .Property(e => e.Logicalpath)
                .IsUnicode(false);

            modelBuilder.Entity<IISApplication>()
                .Property(e => e.Apppollname)
                .IsUnicode(false);

            modelBuilder.Entity<IISApplication>()
                .Property(e => e.Iislogpath)
                .IsUnicode(false);

            modelBuilder.Entity<IISApplication>()
                .HasMany(e => e.Iisapplicationsoapservice)
                .WithRequired(e => e.Iisapplication)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IISWebSite>()
                .Property(e => e.Namewebsite)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebSite>()
                .Property(e => e.Apppollname)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebSite>()
                .Property(e => e.Adresswebsite)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebSite>()
                .Property(e => e.Iislogpath)
                .IsUnicode(false);

            modelBuilder.Entity<IISWebSite>()
                .Property(e => e.Aliasiiswebsite)
                .IsUnicode(false);

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

            modelBuilder.Entity<SoapEndpoint>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<SoapService>()
                .Property(e => e.Namesoapservice)
                .IsUnicode(false);

            modelBuilder.Entity<SoapService>()
                .HasMany(e => e.Iisapplicationsoapservice)
                .WithRequired(e => e.Soapservice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Webserver>()
                .Property(e => e.Namewebserver)
                .IsUnicode(false);
        }
    }
}
