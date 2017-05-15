namespace UrlShortener.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UrlShortener.Data.Contract;
    using UrlShortener.Model;
    using UrlShortener.Storage;

    public class UrlsRedisRepository : IRepository<Url>, IUrlsRepository
    {
        private readonly RedisContext redis = new RedisContext();
        
        /// <summary>
        /// gets all stored key in redis db
        /// </summary>
        /// <returns></returns>
        public IQueryable<Url> GetAll()
        {
            var keys = this.redis.GetAllKeys(q => true);
            var urls = keys.Select(key => new Url { InflateUrl = key, ShortenedUrl = this.redis.Get<string>(key) }).ToList();

            return urls.AsQueryable();
        }

        // in redis context it dose not mean anything
        public Url GetById(
            Guid id)
        {
            throw new NotImplementedException("In Redis context should use Key to get a value.");
        }

        /// <summary>
        /// adds key value pair to redis
        /// </summary>
        /// <param name="entity"></param>
        public void Add(
            Url entity)
        {
            this.redis.Add(entity.InflateUrl, entity.ShortenedUrl);
        }

        /// <summary>
        /// updates a key value pair (not key but value)
        /// </summary>
        /// <param name="entity"></param>
        public void Update(
            Url entity)
        {
            if (this.InflateUrlIsExist(entity.InflateUrl))
            {
                this.redis.Remove(entity.InflateUrl);
            }

            this.Add(entity);
        }

        /// <summary>
        /// removes a key with its value from redis db
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(
            Url entity)
        {
            this.redis.Remove(entity.InflateUrl);
        }

        // in redis context it does not mean anything 
        public void DeleteById(
            Guid id)
        {
            throw new NotImplementedException("In Redis context should use Key to delete a value.");
        }

        /// <summary>
        /// gets shotened url
        /// if shortened url don't exist in DB create new, else find shortened url
        /// </summary>
        /// <param name="newUrl"></param>
        /// <returns>short url</returns>
        public string GetShortenedUrl(
            Url newUrl)
        {
            // if inflate url is exist, do not save in DB and find shortened url
            if (this.InflateUrlIsExist(newUrl.InflateUrl))
            {
                return this.GetShortenedByInflateUrl(newUrl.InflateUrl);
            }
            
            // generate url in short form
            newUrl.InflateUrl = !string.IsNullOrWhiteSpace(newUrl.InflateUrl) ? newUrl.InflateUrl.Trim().ToLower() : "";
            newUrl.ShortenedUrl = $"{newUrl.DefaultDomain}/{Guid.NewGuid().GetHashCode():x}";
            
            // add to db
            this.Add(newUrl);
            
            // then return it
            return newUrl.ShortenedUrl;
        }

        /// <summary>
        /// get inflate url by shortened url
        /// </summary>
        /// <param name="shortenedUrl"></param>
        /// <returns>original long url</returns>
        public string GetInflateByshortenedUrl(
            string shortenedUrl)
        {
            return this.GetByShortenedUrl(shortenedUrl)?.InflateUrl ?? "";
        }

        /// <summary>
        /// check inflate url exist in DB or no
        /// </summary>
        /// <param name="infateUrl"></param>
        /// <returns>true if it exists in db</returns>
        private bool InflateUrlIsExist(
            string infateUrl)
        {
            // check inflate url is null or empty then convert to lower case
            infateUrl = !string.IsNullOrWhiteSpace(infateUrl) ? infateUrl.Trim().ToLower() : "";
            return this.redis.Contains(infateUrl);
        }

        /// <summary>
        /// get whole entity by inflate url
        /// </summary>
        /// <param name="inflateUrl"></param>
        /// <returns>Url object containing both short and original urls</returns>
        private Url GetByInflateUrl(
            string inflateUrl)
        {
            inflateUrl = !string.IsNullOrWhiteSpace(inflateUrl) ? inflateUrl.Trim().ToLower() : "";
            return new Url { ShortenedUrl = this.redis.Get<string>(inflateUrl) };
        }

        /// <summary>
        /// get shortened url by inflate url
        /// </summary>
        /// <param name="inflateUrl"></param>
        /// <returns>short url</returns>
        public string GetShortenedByInflateUrl(
            string inflateUrl)
        {
            return this.GetByInflateUrl(inflateUrl)?.ShortenedUrl ?? "";
        }

        /// <summary>
        /// get whole etity by shortened url
        /// </summary>
        /// <param name="shortenedUrl"></param>
        /// <returns>Url object containing both short and original urls</returns>
        public Url GetByShortenedUrl(
            string shortenedUrl)
        {
            shortenedUrl = !string.IsNullOrWhiteSpace(shortenedUrl) ? shortenedUrl.Trim().ToLower() : "";
            return this.GetAll().FirstOrDefault(url => url.ShortenedUrl == shortenedUrl);
        }
    }
}