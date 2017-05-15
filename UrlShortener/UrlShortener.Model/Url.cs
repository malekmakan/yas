namespace UrlShortener.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Url")]
    public class Url
    {
        // just in sql 
        public DateTime CreationDateTime { get; set; }

        public Guid Id { get; set; }

        // sql and redis 
        // redis is key-value pair so do not expect too much, but fast
        public string InflateUrl { get; set; }

        public string ShortenedUrl { get; set; }

        // virtual fields
        [NotMapped]
        public string DefaultDomain { get; set; }
    }
}