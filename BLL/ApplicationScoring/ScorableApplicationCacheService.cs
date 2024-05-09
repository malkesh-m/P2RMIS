using System;
using System.Runtime.Caching;
using Sra.P2rmis.CrossCuttingServices.CacheServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Cache services used for retrieving scorable application data.
    /// </summary>
    public class ScorableApplicationCacheService : CacheServices
    {
        private static string ActiveOrScoringApplicationBaseKey = "ActiveOrScoringApplication";
        private static int ActiveOrScoringApplicationCacheDuration = ConfigManager.MyWorkspaceScorableApplicationCacheDuration;

        /// <summary>
        /// Cache item policy.
        /// </summary>
        /// <param name="durationInSeconds">Cache duration in seconds</param>
        /// <returns></returns>
        private static CacheItemPolicy Policy(int durationInSeconds)
        {
            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(durationInSeconds)
            };
            return policy;
        }
        /// <summary>
        /// Active or scoring application key for cache.
        /// </summary>
        /// <param name="para">Custom parameter.</param>
        /// <returns></returns>
        private static string ActiveOrScoringApplicationKey(string para)
        {
            return string.Format("{0}-{1}", ActiveOrScoringApplicationBaseKey, para);
        }
        /// <summary>
        /// Gets cache for active or scoring application.
        /// </summary>
        /// <param name="para">Custom parameter.</param>
        /// <returns></returns>
        public static object GetActiveOrScoringApplication(string para)
        {
            return CacheServices.Get(ActiveOrScoringApplicationKey(para));
        }
        /// <summary>
        /// Adds cache for active or scoring application.
        /// </summary>
        /// <param name="itemToAdd">Item to be added to cache.</param>
        /// <param name="para">Custom parameter.</param>
        public static void AddActiveOrScoringApplication(object itemToAdd, string para)
        {
            var cacheKey = ActiveOrScoringApplicationKey(para);
            CacheServices.Add(cacheKey, itemToAdd, Policy(ActiveOrScoringApplicationCacheDuration));
        }
    }
}
