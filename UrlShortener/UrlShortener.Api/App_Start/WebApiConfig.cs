namespace UrlShortener.Api
{
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.Routing;

    using Microsoft.Web.Http.Routing;

    public static class WebApiConfig
    {
        public static void Register(
            HttpConfiguration config)
        {
            // Web API configuration and services
            // api version
            var constraintResolver = new DefaultInlineConstraintResolver
                                     {
                                         ConstraintMap =
                                         {
                                             ["apiVersion"] =
                                                 typeof(
                                                 ApiVersionRouteConstraint
                                                 )
                                         }
                                     };

            config.MapHttpAttributeRoutes(constraintResolver);
            config.AddApiVersioning();

            // let's have a home page (which is our client app)
            config.Routes.MapHttpRoute("Index", "{id}.html", new { id = "index" });

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            // json format
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            // xml format
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}