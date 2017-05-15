namespace UrlShortener.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using UrlShortener.Data.Contract;
    using UrlShortener.Model;

    /// <summary>
    ///     this is sql kind of data implementation
    /// </summary>
    public class UrlsRepository : UrlShortenerRepository<Url>, IUrlsRepository
    {
        public UrlsRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        ///     get shotend url, if shortened url don't exist in DB create new, else find shortened url.
        /// </summary>
        /// <param name="newUrl"></param>
        /// <returns>Short url</returns>
        public string GetShortenedUrl(
            Url newUrl)
        {
            // if inflate url is exist, do not save in DB and find shortened url
            if (this.InflateUrlIsExist(newUrl.Id, newUrl.InflateUrl))
            {
                return this.GetshortenedByInflateUrl(newUrl.InflateUrl);
            }

            // new url save in DB
            // create object
            newUrl.Id = Guid.NewGuid();
            newUrl.InflateUrl = !string.IsNullOrWhiteSpace(newUrl.InflateUrl) ? newUrl.InflateUrl.Trim().ToLower() : "";
            newUrl.ShortenedUrl = $"{newUrl.DefaultDomain}/{Guid.NewGuid().GetHashCode():x}";
            newUrl.CreationDateTime = DateTime.UtcNow;

            // save it
            this.Add(newUrl);

            // return shortened url
            return newUrl.ShortenedUrl;
        }

        /// <summary>
        ///     get inflate url by shortened url
        /// </summary>
        /// <param name="shortenedUrl"></param>
        /// <returns>original url</returns>
        public string GetInflateByshortenedUrl(
            string shortenedUrl)
        {
            return this.GetByShortenedUrl(shortenedUrl)?.InflateUrl ?? "";
        }

        /// <summary>
        ///     check inflate url exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <param name="infateUrl"></param>
        /// <returns>true if we already have it in db</returns>
        public bool InflateUrlIsExist(
            Guid id,
            string infateUrl)
        {
            // check inflate url is null or empty then convert to lower case
            // we like lower case unified 
            infateUrl = !string.IsNullOrWhiteSpace(infateUrl) ? infateUrl.Trim().ToLower() : "";
            return this.GetAll().Any(tempUrl => tempUrl.Id != id && tempUrl.InflateUrl == infateUrl);
        }

        /// <summary>
        ///     gets shortened url
        ///     if shortened url don't exist in DB create new, else find shortened url
        /// </summary>
        /// <param name="inflateUrl"></param>
        /// <returns>short url</returns>
        public Url GetByInflateUrl(
            string inflateUrl)
        {
            inflateUrl = !string.IsNullOrWhiteSpace(inflateUrl) ? inflateUrl.Trim().ToLower() : "";
            return this.GetAll().FirstOrDefault(tempUrl => tempUrl.InflateUrl == inflateUrl);
        }

        // get shortened url by inflate url
        public string GetshortenedByInflateUrl(
            string inflateUrl)
        {
            return this.GetByInflateUrl(inflateUrl)?.ShortenedUrl ?? "";
        }

        // get one record by shortened url
        public Url GetByShortenedUrl(
            string shortenedUrl)
        {
            shortenedUrl = !string.IsNullOrWhiteSpace(shortenedUrl) ? shortenedUrl.Trim().ToLower() : "";
            return this.GetAll().FirstOrDefault(tempUrl => tempUrl.ShortenedUrl == shortenedUrl);
        }
    }
}