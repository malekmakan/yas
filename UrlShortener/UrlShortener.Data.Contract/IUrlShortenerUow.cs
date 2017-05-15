namespace UrlShortener.Data.Contract
{
    public interface IUrlShortenerUow
    {
        IUrlsRepository Urls { get; }

        void Commit();
    }
}