namespace UrlShortener.Data
{
    using System;

    using UrlShortener.Data.Contract;

    /// <summary>
    /// good to have a unit of work
    /// thanks to John Papa
    /// </summary>
    public class UrlShortenerUow : IUrlShortenerUow, IDisposable
    {
        public UrlShortenerUow(IRepositoryProvider repositoryProvider)
        {
            this.CreateDbContext();

            repositoryProvider.DbContext = this.DbContext;
            this.RepositoryProvider = repositoryProvider;
        }

        private UrlShortenerDbContext DbContext { get; set; }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        protected void CreateDbContext()
        {
            this.DbContext = new UrlShortenerDbContext();

            // Do NOT enable peroxide entities, else serialization fails
            this.DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            this.DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            this.DbContext.Configuration.ValidateOnSaveEnabled = false;

            // a performance tweak !
            //DbContext.Configuration.AutoDetectChangesEnabled = false;
        }

        private T GetRepository<T>() where T : class
        {
            return this.RepositoryProvider.GetRepository<T>();
        }

        #region IUrlShortenerUow Members

        // urls
        public IUrlsRepository Urls => this.GetRepository<IUrlsRepository>();

        /// <summary>
        ///     Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            this.DbContext.SaveChanges();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(
            bool disposing)
        {
            if (!disposing) return;
            this.DbContext?.Dispose();
        }

        #endregion
    }
}