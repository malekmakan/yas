namespace UrlShortener.Api
{
    using System;
    using System.Web;
    using System.Web.Http;

    /// <summary>
    /// not any special usecase here
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(
            object sender,
            EventArgs e)
        {
        }
    }
}