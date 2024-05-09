using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Sra.P2rmis.CrossCuttingServices.CacheServices
{
    /// <summary>
    /// Simple cache service:
    /// - Implemented using memory cache
    /// - Methods in this class should not be directly called.  They should be called 
    ///   from application specific wrappers.
    /// </summary>
    public class CacheServices
    {
        #region Cache Control
        /// <summary>
        /// Initialize the cache ChangeMonitorEntry.
        /// </summary>
        /// <param name="cacheBreaker">ChangeMonitorEntry key value</param>
        protected static void InitialzeCache(string cacheBreaker)
        {
            SetCacheBreaker(cacheBreaker);
        }
        /// <summary>
        /// Invalidate the cache.  The cache is invalidated by setting a new value for 
        /// the "cache breaker" entry (which has a change monitor watching it).
        /// </summary>
        /// <param name="cacheBreaker">ChangeMonitorEntry key value</param>
        protected static void InvalidateCache(string cacheBreaker)
        {
            SetCacheBreaker(cacheBreaker);
        }
        /// <summary>
        /// Set a value on the cache breaker entry.
        /// </summary>
        /// <param name="cacheBreakerKey">Cache breaker key</param>
        private static void SetCacheBreaker(string cacheBreakerKey)
        {
            //
            // Cache policy for the cache breaker is no expiration.
            //
            var policy = new CacheItemPolicy();
            MemoryCache.Default.Set(cacheBreakerKey, Guid.NewGuid(), policy);
        }
        #region Cache Access
        /// <summary>
        /// Add an object to the cache
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        /// <param name="itemToAdd">Object to add to the cache</param>
        /// <param name="policy">CacheItemPolicy for the object</param>
        protected static void Add(string key, object itemToAdd, CacheItemPolicy policy)
        {
            MemoryCache.Default.Set(key, itemToAdd, policy);
        }

        #endregion
        /// <summary>
        /// Retrieve an object from the cache.  If the requested entry is not in the cache
        /// null is returned.
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        /// <returns>Cache entry specified by the key</returns>
        public static object Get(string key)
        {
            return (!string.IsNullOrEmpty(key)) ? MemoryCache.Default[key]: null ;
        }
        /// <summary>
        /// Removes an item from the cache if it exists.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        protected static void Remove(string key)
        {
            //
            // If there is a key then remove the item
            // from the cache.
            // 
            if (!string.IsNullOrEmpty(key))
            {
                MemoryCache.Default.Remove(key);
            }
        }
        /// <summary>
        /// Retrieve the specified keys from the cache.
        /// </summary>
        /// <param name="keys">Enumerable collection of key values</param>
        /// <returns>Dictionary containing cache entries specified by the keys, indexed by the key</returns>
        public static IDictionary<string, object> GetValues(IEnumerable<string> keys)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            if ((keys != null ) & (!keys.Contains(null)) & (keys.Count() != 0))
            {
                result = MemoryCache.Default.GetValues(keys);
            }
            return result;
        }
        #endregion
    }
}
