namespace UrlShortener.Api.Controllers.v1._0
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.Web.Http;

    using UrlShortener.Data.Contract;
    using UrlShortener.Model;

    using Environment = UrlShortener.Utility.Environment;

    [ApiVersionNeutral]
    public class RootController : ApiController
    {
        private readonly IUrlsRepository urlsRepository;
        
        public RootController(IUrlsRepository urlsRepository)
        {
            this.urlsRepository = urlsRepository;
        }

        // redirector permanently redirects urls with proper headers 
        [HttpGet]
        [Route("{code}")]
        public HttpResponseMessage ForwardToInflateUrl(
            string code)
        {
            // grab url
            var newUrl = new Url { ShortenedUrl = Environment.GetCurrentDomainName() + "/" + code };

            // get the original url if it is in DB
            var inflateUrl = this.urlsRepository.GetInflateByshortenedUrl(newUrl.ShortenedUrl);

            // should be a standard message in case the requested url does not exist
            if (string.IsNullOrEmpty(inflateUrl)) return new HttpResponseMessage(HttpStatusCode.NotFound);

            // if query was successfull then ready to customize header and redirect
            // 301, "Moved Permanently"—recommended for SEO
            var response = this.Request.CreateResponse(HttpStatusCode.Moved);

            // okay let the host knows who we are!
            response.Headers.Add("X-Forwarded-For", "Url Shortener - YAS .NET");

            // move to the host
            response.Headers.Location = new Uri(inflateUrl);
            return response;
        }
    }
}