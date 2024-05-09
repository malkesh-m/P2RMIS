using System;
using System.Configuration;
using System.Runtime.Caching;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Cache used to save application results.
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// Default cache expiration if none supplied or invalid values
        /// </summary>
        private static double DefaultCacheExpirationTime = 2;
        /// <summary>
        /// Keys used to access entries in the cache.
        /// </summary>
        public static class Keys
        {
            /// <summary>
            /// The following keys are unique to a specific user.  There will be
            /// one entry per user.  Keys are made unique by appending
            /// the user id.
            /// </summary>
            public static string OpenApplications = "Open";
            public static string Sessions = "Sessions";
            public static string Panels = "Panels";
            /// <summary>
            /// The following keys are not unique to a specific user  There will be
            /// one entry..
            /// </summary>
            public static string nonUser = "";
        }
        /// <summary>
        /// Retrieve an object from the cache.
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        public static object Get(string key)
        {
            object result = null;
            ///
            /// If there is a key then retrieve it.
            /// 
            if (!string.IsNullOrEmpty(key))
            {
                result = MemoryCache.Default[key];
            }
            return result;
        }
        /// <summary>
        /// Add an object to the cache
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        /// <param name="itemToAdd">Object to add to the cache</param>
        public static void Add(string key, object itemToAdd)
        {
            try
            {
                ///
                /// Remove the entry if it exists
                /// 
                Remove(key);
                ///
                /// Now set the expiration policy & add the object to the cache.
                /// 
                CacheItemPolicy policy = new CacheItemPolicy();

                double hours = ConvertCacheTime(ConfigurationManager.AppSettings[Invariables.AppConfigKey.CacheHours]);
                double minutes = ConvertCacheTime(ConfigurationManager.AppSettings[Invariables.AppConfigKey.CacheMinutes]);
                double seconds = ConvertCacheTime(ConfigurationManager.AppSettings[Invariables.AppConfigKey.CacheSeconds]);
                ///
                /// If no cache time supplied then use the default
                /// Although this most likely doesn't matter in modern versions of .Net UtcNow seems like a better
                /// way to supply expiration since the cache is ultimately converted to UTC for comparison and takes DST conversions out of the equation
                /// https://stackoverflow.com/questions/1688554/cache-add-absolute-expiration-utc-based-or-not
                /// 
                hours = ((hours == 0) && (minutes == 0) && (seconds == 0)) ? DefaultCacheExpirationTime : hours;
                policy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);

                MemoryCache.Default.Set(key, itemToAdd, policy);
            }
            catch
            {
                ///
                /// Don't need to do anything.  Just make sure it does not error.  Data
                /// will just be re-retrieved later.
                /// 
            }
        }
        /// <summary>
        /// Removes an item from the cache if it exists.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        public static void Remove(string key)
        {
            ///
            /// If there is a key then remove the item
            /// from the cache.
            /// 
            if (!string.IsNullOrEmpty(key))
            {
                MemoryCache.Default.Remove(key);
            }
        }
        /// <summary>
        /// Makes the key user specific.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        /// <param name="id">Unique user identifier</param>
        /// <returns>User specific cache entry key</returns>
        public static string MakeUserSpecific(string key, string id)
        {
            return string.Format("{0}-{1}", key, id);
        }
        /// <summary>
        /// Clears a users cache entries.
        /// </summary>
        /// <param name="sessionId">Users's session identifier</param>
        public static void ClearCacheForSession(string sessionId)
        {
            Cache.Remove(Cache.MakeUserSpecific(Cache.Keys.OpenApplications, sessionId));
            Cache.Remove(Cache.MakeUserSpecific(Cache.Keys.Sessions, sessionId));
            Cache.Remove(Cache.MakeUserSpecific(Cache.Keys.Panels, sessionId));
        }
        #region Helpers
        /// <summary>
        /// Converts the configurable time into a double.
        /// </summary>
        /// <param name="value">Value retrieved from Web.Config</param>
        /// <returns>Valid value; 0 if an error occurred</returns>
        private static double ConvertCacheTime(string value)
        {
            double result = 0;

            try
            {
                result = Convert.ToDouble(value);
            }
            catch
            {
                ///
                /// Must have supplied a bad value or no value.  Defaults to 0;
                /// 
            }
            return result;
        }
	    #endregion
    }
}