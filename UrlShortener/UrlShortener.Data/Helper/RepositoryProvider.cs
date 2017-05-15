namespace UrlShortener.Data.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using UrlShortener.Data.Contract;

    /// <summary>
    ///     Provides repo.
    /// </summary>
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly RepositoryFactories repositoryFactories;

        public RepositoryProvider(RepositoryFactories repositoryFactories)
        {
            this.repositoryFactories = repositoryFactories;
            this.Repositories = new Dictionary<Type, object>();
        }

        protected Dictionary<Type, object> Repositories { get; }

        protected virtual T MakeRepository<T>(
            Func<DbContext, object> factory,
            DbContext dbContext)
        {
            var f = factory ?? this.repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
            }
            var repo = (T)f(dbContext);
            this.Repositories[typeof(T)] = repo;
            return repo;
        }
        
        public DbContext DbContext { get; set; }

        public IRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return this.GetRepository<IRepository<T>>(this.repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        public virtual T GetRepository<T>(
            Func<DbContext, object> factory = null) where T : class
        {
            object repoObj;
            this.Repositories.TryGetValue(typeof(T), out repoObj);
            if (repoObj != null)
            {
                return (T)repoObj;
            }

            return this.MakeRepository<T>(factory, this.DbContext);
        }

        public void SetRepository<T>(
            T repository)
        {
            this.Repositories[typeof(T)] = repository;
        }
        
    }
}