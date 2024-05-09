using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Sra.P2rmis.CrossCuttingServices.CacheServices;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// On-line scoring cache implementation.
    /// </summary>
    public class OnLineScoringCacheService : CacheServices
    {
        /// <summary>
        /// Key for the applications cache key list 
        /// </summary>
        private static string CacheKeyListKey = "CacheKeyListKey";
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<int, string> CacheBreakerKeys = new Dictionary<int, string>();
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<int, string> CacheKeyListKeyKeys = new Dictionary<int, string>();
        /// <summary>
        /// Polling result indicating no application is in the cache.
        /// </summary>
        private const int NoApplicationInCache = 0;
        /// <summary>
        /// Policy to use on the item in the cache
        /// </summary>
        private static CacheItemPolicy Policy(string cacheBreaker)
        {
            CacheItemPolicy cachePolicy = new CacheItemPolicy();
            string[] keys = { cacheBreaker };
            cachePolicy.ChangeMonitors.Add(MemoryCache.Default.CreateCacheEntryChangeMonitor(keys));
            return cachePolicy;
        }
        /// <summary>
        /// Add an object to the cache
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="itemToAdd">Object to add to the cache</param>
        public static void Add(string key, int sessionPanelId, object itemToAdd)
        {
            var v = CacheBreakerKeys[sessionPanelId];
            CacheServices.Add(key, itemToAdd, Policy(v));
        }
        #region Cache Control
        /// <summary>
        /// Initialize the cache ChangeMonitorEntry.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        internal static void InitialzeCache(string cacheBreaker, int applicationId)
        {
            /// MAY BE ABLE TO REMOVE APPLICATION ID PARAMETER
            CacheServices.InvalidateCache(cacheBreaker);
        }
        /// <summary>
        /// Invalidate the cache.
        /// </summary>
        public static void InvalidateCache(int sessionPanelId, int applicationId)
        {
            // 
            // try to get a cache breaker key for this SessionPanel
            //
            string x = null;
            if (CacheBreakerKeys.TryGetValue(sessionPanelId, out x))
            {
                string cacheBreaker = MakeCacheBreaker(applicationId, sessionPanelId);
                CacheServices.Remove(cacheBreaker);
                CacheBreakerKeys.Remove(sessionPanelId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <param name="applicationId"></param>
        public static void InitializeCache(int applicationId, int sessionPanelId)
        {
            LazyInitialization(applicationId, sessionPanelId);
        }
        /// <summary>
        /// Retrieves the Score cache entry if one exists.  If one does not 
        /// exist it creates it and adds it to the cache.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        private static void LazyInitialization(int applicationId, int sessionPanelId)
        {
            // 
            // try to get a cache breaker key for this SessionPanel
            //
            string newCacheBreaker = MakeCacheBreaker(applicationId, sessionPanelId);
            string currentCacheBreaker = null;
            if (!CacheBreakerKeys.TryGetValue(sessionPanelId, out currentCacheBreaker))
            {
                InitialzeCache(newCacheBreaker, applicationId);
                CacheBreakerKeys[sessionPanelId] = newCacheBreaker;
            }
            ///
            /// There is a cache breaker key in for this SessionPanel, but is it
            /// the same one?
            /// 
            else if (newCacheBreaker != currentCacheBreaker)
            {
                CacheServices.Remove(currentCacheBreaker);
                InitialzeCache(newCacheBreaker, applicationId);
                CacheBreakerKeys[sessionPanelId] = newCacheBreaker;
            }
        }

        /// <summary>
        /// Construct cache breaker key
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns><Cache breaker key value for the specified application & panel/returns>
        private static string MakeCacheBreaker(int applicationId, int sessionPanelId)
        {
            return string.Format("{0}-{1}", sessionPanelId, applicationId);
        }
        /// <summary>
        /// Construct cache key list key
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns></returns>
        private static string MakeCacheKeyListKey(int sessionPanelId)
        {
            return string.Format("{0}-{1}", CacheKeyListKey, sessionPanelId);
        }
        /// <summary>
        /// Retrieves the ScoreCacheEntry identified by the keyValue.  If the 
        /// cache entry does not exist a cache entry is created and added to the
        /// cache.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="keyValue">Key to use</param>
        /// <returns>ScoreCacheEntry for the key</returns>
        internal static ScoreCacheEntry GetOrCreateEntry(int applicationId, int sessionPanelId, int keyValue)
        {
            LazyInitialization(applicationId, sessionPanelId);
            string key = keyValue.ToString();

            ScoreCacheEntry sce = OnLineScoringCacheService.Get(key) as ScoreCacheEntry;
            //
            // If there was no cache entry then create one and add it to the cache
            //
            if (sce == null)
            {
                sce = new ScoreCacheEntry(key);
                OnLineScoringCacheService.Add(key, sessionPanelId, sce);
                ScoreCacheKeyList(sessionPanelId).Add(key);
            }
            return sce;
        }
        /// <summary>
        /// Get or create the cache key list
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>Cache key list</returns>
        public static List<string> ScoreCacheKeyList(int sessionPanelId)
        {
            string key = null;
            if (!CacheKeyListKeyKeys.TryGetValue(sessionPanelId, out key))
            {
                key = MakeCacheKeyListKey(sessionPanelId);
                CacheKeyListKeyKeys[sessionPanelId] = key;
            }

            List<string> result = OnLineScoringCacheService.Get(key) as List<string>;

            if (result == null)
            {
                result = new List<string>();
                OnLineScoringCacheService.Add(key, sessionPanelId, result);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="sessionId">SessionPanel entity identifier</param>
        /// <returns>True if the current application is identified by the key; false otherwise</returns>
        public static bool IsCurrentApplication(int applicationId, int sessionId)
        {
            string key = MakeCacheBreaker(applicationId, sessionId);
            return CacheBreakerKeys.Values.Contains(key);
        }
        /// <summary>
        /// Returns the current Application entity identifier of the specified meeting session.
        /// </summary>
        /// <param name="sessionId">SessionPanel entity identifier</param>
        /// <returns>Application entity identifier</returns>
        public static int SessionActiveApplication(int sessionPanelId)
        {
            string key = null;
            string s = null;

            if (CacheBreakerKeys.TryGetValue(sessionPanelId, out key))
            {
                int i = key.IndexOf("-");
                s = key.Substring(++i);
            }
            return (s == null) ? NoApplicationInCache : Convert.ToInt32(s);
        }
        #endregion
    }
}
