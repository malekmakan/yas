namespace UrlShortener.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using UrlShortener.Data.Configuration;
    using UrlShortener.Data.SampleData;
    using UrlShortener.Model;

    public class UrlShortenerDbContext : DbContext
    {
        static UrlShortenerDbContext()
        {
            //It will should be deleted in production
            Database.SetInitializer(new UrlShortenerDatabaseInitializer());
        }

        // Hey DbContext Do You Know What Is The Default ConnectionString Name?
        // If Not I Wrote To You Below
        public UrlShortenerDbContext()
            : base("UrlShortenerConnection")
        {
        }

        // DbContext Please Welcome New Objects
        // urls
        public DbSet<Url> Urls { get; set; }

        // model creation
        protected override void OnModelCreating(
            DbModelBuilder modelBuilder)
        {
            // DbContext Use Singular Name For DataBase Tables
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // DbContext Please Have a Look to the Following Relations
            // url
            modelBuilder.Configurations.Add(new UrlConfiguration());
        }
    }
}