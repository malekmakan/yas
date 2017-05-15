namespace UrlShortener.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Linq.Expressions;

    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core;
    using StackExchange.Redis.Extensions.Newtonsoft;

    using UrlShortener.Utility;

    public class RedisContext
    {
        // Make connection to Redis Server
        public static NewtonsoftSerializer Serializer = new NewtonsoftSerializer();

        public StackExchangeRedisCacheClient Redis;

        public RedisContext()
        {
            try
            {
                var connectionMultiplexer =
                    new Lazy<ConnectionMultiplexer>(
                        () => ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisConnectionString"]));

                this.Redis = new StackExchangeRedisCacheClient(connectionMultiplexer.Value, Serializer);
            }
            catch (Exception exception)
            {
                // Log to local log
                Log.Instance.Error(exception, $"An error occurred in Redis Context.");

                this.Redis = null;
            }
        }

        /// <summary>
        ///     Removes a record by its Key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(
            string key)
        {
            Log.Instance.Info($"Removing item, key=" + key);

            if (this.Redis == null) return;

            lock (this.Redis)
            {
                this.Redis.Remove(key);
            }
        }

        /// <summary>
        ///     Retrieves a Record from cache server with specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(
            string key) where T : class
        {
            return this.Redis?.Get<T>(key);
        }

        /// <summary>
        ///     Checks if there is any record in Redis Database with given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool Contains(
            string key)
        {
            return this.Redis != null && this.Redis.Exists(key);
        }

        /// <summary>
        ///     It will help you to add a value in Redis Database
        /// </summary>
        /// <param name="key">Unique Key</param>
        /// <param name="o">Object to be stored</param>
        public virtual void Add(
            string key,
            object o)
        {
            if (this.Redis == null) return;

            lock (this.Redis)
            {
                this.Redis.Add(key, o);
            }
        }

        /// <summary>
        /// returns all key names
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of key names</returns>
        public virtual IEnumerable<string> GetAllKeys(
            Expression<Func<string, bool>> predicate)
        {
            return this.Redis?.SearchKeys("*").AsQueryable().Where(predicate);
        }
    }
}