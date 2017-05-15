namespace UrlShortener.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<UrlShortenerDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(
            UrlShortenerDbContext context)
        {
            // seed data in case you like to use sql server
            // EF Code First
        }
    }
}