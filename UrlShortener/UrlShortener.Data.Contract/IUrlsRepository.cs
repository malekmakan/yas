namespace UrlShortener.Data.Contract
{
    using UrlShortener.Model;

    public interface IUrlsRepository
    {
        string GetShortenedUrl(
            Url newUrl);

        string GetInflateByshortenedUrl(
            string shortenedUrl);
    }
}