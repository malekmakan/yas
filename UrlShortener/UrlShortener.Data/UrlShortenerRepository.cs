namespace UrlShortener.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using UrlShortener.Data.Contract;

    /// <summary>
    /// some basic operations on repo.s (CRUD)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UrlShortenerRepository<T> : IRepository<T>
        where T : class
    {
        public UrlShortenerRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            // Initialize Needed Objects
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<T>();
        }

        // Home Scope
        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        #region The Reposotory's Godfather Prepared Common Methods

        public virtual IQueryable<T> GetAll()
        {
            return this.DbSet;
        }

        public virtual T GetById(
            Guid id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(
            T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            this.DbSet.Add(entity);
            this.DbContext.SaveChanges();
        }

        public virtual void Update(
            T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }

        public virtual void Delete(
            T entity)
        {
            DbEntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);
            this.DbContext.SaveChanges();
        }

        public virtual void DeleteById(
            Guid id)
        {
            var entity = this.GetById(id);
            if (entity == null)
            {
                return; // not found; assume already deleted.
            }
            this.Delete(entity);
        }

        #endregion
    }
}