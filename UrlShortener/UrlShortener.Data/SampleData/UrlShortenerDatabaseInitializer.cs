namespace UrlShortener.Data.SampleData
{
    using System.Data.Entity;

    public class UrlShortenerDatabaseInitializer : DropCreateDatabaseIfModelChanges<UrlShortenerDbContext>
    {
        protected override void Seed(
            UrlShortenerDbContext context)
        {
            // Seed up
        }
    }
}