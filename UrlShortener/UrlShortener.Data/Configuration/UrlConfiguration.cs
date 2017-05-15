namespace UrlShortener.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using UrlShortener.Model;

    /// <summary>
    /// in this class the relation between models should be defined 
    /// one to one 
    /// one to many
    /// many to many
    /// </summary>
    public class UrlConfiguration : EntityTypeConfiguration<Url>
    {
    }
}