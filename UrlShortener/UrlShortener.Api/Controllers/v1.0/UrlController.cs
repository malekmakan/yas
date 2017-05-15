namespace UrlShortener.Api.Controllers.v1._0
{
    using System.Web.Http;

    using UrlShortener.Api.Models;
    using UrlShortener.Data.Contract;
    using UrlShortener.Model;
    using UrlShortener.Utility;

    using WebApi.OutputCache.V2;
    
    [RoutePrefix("v{version:apiVersion}/url")]

    // cache requests 
    [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
    [AutoInvalidateCacheOutput]
    public class UrlController : ApiController
    {
        private readonly IUrlsRepository urlsRepository;
        
        public UrlController(IUrlsRepository urlsRepository)
        {
            this.urlsRepository = urlsRepository;
        }

        /// <summary>
        /// Makes given url short
        /// </summary>
        /// <param name="inflateUrl">long url</param>
        /// <returns>Shortented Url</returns>
        [HttpGet]
        [Route("getShortenedUrl")]
        public UrlInfo GetShortenedUrl(
            string inflateUrl)
        {
            // make short url
            var newUrl = new Url { InflateUrl = inflateUrl, DefaultDomain = Environment.GetCurrentDomainName() };
            var shortendUrl = this.urlsRepository.GetShortenedUrl(newUrl);

            // return it
            return new UrlInfo { ShortenedUrl = shortendUrl };
        }

        /// <summary>
        /// Get original url out of short url
        /// </summary>
        /// <param name="shortenedUrl"></param>
        /// <returns>Original (long) url</returns>
        [HttpGet]
        [Route("getInflateUrl")]
        public UrlInfo GetInflateUrl(
            string shortenedUrl)
        {
            // revert original url
            var newUrl = new Url { ShortenedUrl = shortenedUrl };
            var inflateUrl = this.urlsRepository.GetInflateByshortenedUrl(newUrl.ShortenedUrl);

            // return inflate url or proper message
            return new UrlInfo { InflateUrl = inflateUrl ?? "Not found." };
        }
    }
}