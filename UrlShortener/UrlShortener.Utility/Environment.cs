namespace UrlShortener.Utility
{
    using System;
    using System.Configuration;
    using System.Web;

    /// <summary>
    ///     env. settings
    /// </summary>
    public class Environment
    {
        public static string GetCurrentDomainName()
        {
            // shall we use current domain or hard coded one in web.config file?
            return !Convert.ToBoolean(ConfigurationManager.AppSettings["UseRequestUrl"])
                ? ConfigurationManager.AppSettings["ServerDomain"]
                : HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }
    }
}