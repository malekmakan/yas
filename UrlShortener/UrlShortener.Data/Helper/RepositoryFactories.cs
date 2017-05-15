namespace UrlShortener.Data.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using UrlShortener.Data.Contract;

    /// <summary>
    ///     Url Shortener Repositories.
    /// </summary>
    public class RepositoryFactories
    {
        private readonly IDictionary<Type, Func<DbContext, object>> repositoryFactories;

        public RepositoryFactories()
        {
            this.repositoryFactories = this.GetUrlShortenerFactories();
        }

        public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
        {
            this.repositoryFactories = factories;
        }

        private IDictionary<Type, Func<DbContext, object>> GetUrlShortenerFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                   {
                       // url
                       {
                           typeof(IUrlsRepository),
                           dbContext => new UrlsRepository(dbContext)
                       }

                       // in case we have more other stuff should be placed here (more entities)
                       // ...
                   };
        }

        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            this.repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return this.GetRepositoryFactory<T>() ?? this.DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new UrlShortenerRepository<T>(dbContext);
        }
    }
}