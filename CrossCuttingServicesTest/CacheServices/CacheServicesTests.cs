using System.Collections.Generic;
using System.Runtime.Caching;
using NUnit.Framework;
using Service = Sra.P2rmis.CrossCuttingServices.CacheServices;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace Sra.P2rmis.CrossCuttingServicesTest.CacheServices
{
    /// <summary>
    /// Unit tests for CacheServices
    /// </summary>
    [TestClass()]
    public class CacheServicesTests
    {
        string key0 = "testKey0";
        string key1 = "testKey1";
        string key2 = "testKey2";
        string key3 = "testKey3";
        string key4 = "testKey4";

        string keyA = "testKeyA";

        string objectToAdd0 = "abcdefg";
        string objectToAdd1 = "abcdefg";
        int objectToAdd2 = 6;


        string objectToAddA = "abcdefg";

        TestCacheClass object0 = new TestCacheClass {SP = "1", P1 = 1, P2 = 2, P3 = 3, P4 = 4 };
        TestCacheClass object1 = new TestCacheClass {SP = "2", P1 = 10, P2 = 20, P3 = 30, P4 = 40 };
        TestCacheClass object2 = new TestCacheClass {SP = "3", P1 = 100, P2 = 200, P3 = 300, P4 = 400 };
        TestCacheClass object3 = new TestCacheClass {SP = "4", P1 = 1000, P2 = 2000, P3 = 3000, P4 = 4000 };
        TestCacheClass object4 = new TestCacheClass {SP = "5", P1 = 10000, P2 = 20000, P3 = 30000, P4 = 40000 };

        #region Overhead
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            TestCacheOne.InitialzeCache();
            TestCacheTwo.InitialzeCache();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            TestCacheOne.InvalidateCache();
            TestCacheTwo.InvalidateCache();
        }
        #endregion
        #endregion
        #region Add Tests
        /// <summary>
        /// Test Add an entry to the cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void AddTest()
        {
            long countBefore = MemoryCache.Default.GetCount();

            TestCacheOne.Add(key0, objectToAdd0);

            long countAfter = MemoryCache.Default.GetCount();
            Assert.AreEqual(countAfter, countBefore + 1, "entry was not added");
        }
        /// <summary>
        /// Test Add an entry to the cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void AddSecondServiceTest()
        {
            long countBefore = MemoryCache.Default.GetCount();

            TestCacheOne.Add(key0, objectToAdd0);
            TestCacheTwo.Add(keyA, objectToAddA);

            Assert.AreEqual(countBefore + 2, MemoryCache.Default.GetCount(), "entry was not added");
        }	
        #endregion
        #region Get Tests
        /// <summary>
        /// Test retrieving an entry to the cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void GetTest()
        {
            TestCacheOne.Add(key0, objectToAdd0);

            object result = TestCacheOne.Get(key0);

            Assert.AreEqual(objectToAdd0, result, "returned object was not as expected");
        }
        /// <summary>
        /// Test retrieving an entry to the cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void GetTwoEntriesTest()
        {
            TestCacheOne.Add(key1, objectToAdd1);
            TestCacheOne.Add(key2, objectToAdd2);

            object result1 = TestCacheOne.Get(key1);
            object result2 = TestCacheOne.Get(key2);

            Assert.AreEqual(objectToAdd1, result1, "returned object 1 was not as expected");
            Assert.AreEqual(objectToAdd2, result2, "returned object 2 was not as expected");
        }
        /// <summary>
        /// Test retrieving an entry to the cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void GetSecondserviceTest()
        {
            TestCacheOne.Add(key0, objectToAdd0);
            TestCacheTwo.Add(keyA, objectToAddA);

            object result0 = TestCacheOne.Get(key0);
            object resultA = TestCacheOne.Get(keyA);

            Assert.AreEqual(objectToAdd0, result0, "returned object was not as expected");
            Assert.AreEqual(objectToAddA, resultA, "returned object was not as expected");
        }
        #endregion
        #region Initialize Tests
        /// <summary>
        /// Test initializing a cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void InitializeTest()
        {
            //
            // have to get rid of the watched cache entry that was put in by init
            //
            TestCacheOne.Remove(TestCacheOne.CacheBreaker);

            TestCacheOne.InitialzeCache();

            long countAfter = MemoryCache.Default.GetCount();
            object o = MemoryCache.Default.Get(TestCacheOne.CacheBreaker);
            //
            // We expect 2 for the CacheBreaker keys
            //
            Assert.AreEqual(2, MemoryCache.Default.GetCount(), "Cache was not initialized");
            Assert.IsNotNull(o, "cache key was not returned");
        }
        /// <summary>
        /// Test initializing a cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void InitializeTwoServicesTest()
        {
            //
            // have to get rid of the watched cache entry that was put in by init
            //
            TestCacheOne.Remove(TestCacheOne.CacheBreaker);
            TestCacheOne.Remove(TestCacheTwo.CacheBreaker);

            TestCacheOne.InitialzeCache();
            TestCacheTwo.InitialzeCache();

            long countAfter = MemoryCache.Default.GetCount();
            object o0 = MemoryCache.Default.Get(TestCacheOne.CacheBreaker);
            object oA = MemoryCache.Default.Get(TestCacheTwo.CacheBreaker);
            //
            // We expect 2 for the CacheBreaker keys
            //
            Assert.AreEqual(2, MemoryCache.Default.GetCount(), "Cache was not initialized");
            Assert.IsNotNull(o0, "cache key was not returned");
            Assert.IsNotNull(oA, "cache key was not returned");
        }
        #endregion
        #region Invalidate Tests
        /// <summary>
        /// Test invalidating a cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void InvalidateTest()
        {
            TestCacheOne.Add(key1, objectToAdd1);
            TestCacheOne.Add(key2, objectToAdd2);

            TestCacheOne.InvalidateCache();
            //
            // We expect 2 for the CacheBreaker key
            //
            Assert.AreEqual(2, MemoryCache.Default.GetCount(), "Cache was not invalidated correctly");
        }
        /// <summary>
        /// Test invalidating a cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void InvalidateTwoServicesTest()
        {
            TestCacheOne.Add(key1, objectToAdd1);
            TestCacheOne.Add(key2, objectToAdd2);

            TestCacheOne.InvalidateCache();
            TestCacheTwo.InvalidateCache();
            //
            // We expect 2 for the CacheBreaker key
            //
            Assert.AreEqual(2, MemoryCache.Default.GetCount(), "Cache was not invalidated correctly");
        }
        /// <summary>
        /// Test invalidating a cache
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void InvalidateTwoServicesOnlyOneTest()
        {
            TestCacheOne.Add(key1, objectToAdd1);
            TestCacheOne.Add(key2, objectToAdd2);
            TestCacheTwo.Add(keyA, objectToAddA);

            TestCacheOne.InvalidateCache();
            //
            // We expect 2 for the CacheBreaker key & 1 for the entry added to two.
            //
            Assert.AreEqual(3, MemoryCache.Default.GetCount(), "Cache was not invalidated correctly");
        } 
        #endregion
        #region Remove Tests

        #endregion
        #region GetValues
        /// <summary>
        /// Test GetValues for multiple cache entries retrieval
        /// </summary>
        [TestMethod()]
        [Category("CacheService")]
        public void GetValuesTest()
        {
            long countBefore = MemoryCache.Default.GetCount();
            List<string> keys = new List<string>() { key0, key1, key2, key3, key4 };

            TestCacheOne.Add(key0, object0);
            TestCacheOne.Add(key1, object1);
            TestCacheOne.Add(key2, object2);
            TestCacheOne.Add(key3, object3);
            TestCacheOne.Add(key4, object4);

            long countAfter = MemoryCache.Default.GetCount();
            Assert.AreEqual(countAfter, countBefore + 5, "entries was not added");

            IDictionary<string, object> result = TestCacheOne.GetValues(keys);

            Assert.AreEqual(keys.Count, result.Count, "dictionary did not contain number expected");
            Assert.IsTrue(result.Keys.Contains(key0), "result did not contain key0");
            Assert.IsTrue(result.Keys.Contains(key1), "result did not contain key1");
            Assert.IsTrue(result.Keys.Contains(key2), "result did not contain key2");
            Assert.IsTrue(result.Keys.Contains(key3), "result did not contain key3");
            Assert.IsTrue(result.Keys.Contains(key4), "result did not contain key4");

            Assert.AreSame(result[key0], object0, "object 0 not the same");
            Assert.AreSame(result[key1], object1, "object 1 not the same");
            Assert.AreSame(result[key2], object2, "object 2 not the same");
            Assert.AreSame(result[key3], object3, "object 3 not the same");
            Assert.AreSame(result[key4], object4, "object 4 not the same");
        }   
        #endregion
    }

    /// <summary>
    /// Test cache wrapper to access the CacheService methods.
    /// </summary>
    public class TestCacheOne : Service.CacheServices
    {
        public static string CacheBreaker = "CacheBreakerOne";
        /// <summary>
        /// Policy to use on the item in the cache
        /// </summary>
        private static CacheItemPolicy Policy()
        {
            CacheItemPolicy cachePolicy = new CacheItemPolicy();
            string[] keys = { CacheBreaker };
            cachePolicy.ChangeMonitors.Add(MemoryCache.Default.CreateCacheEntryChangeMonitor(keys));
            return cachePolicy;
        }
        /// <summary>
        /// Add an object to the cache
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        /// <param name="itemToAdd">Object to add to the cache</param>
        public static void Add(string key, object itemToAdd)
        {
            Service.CacheServices.Add(key, itemToAdd, Policy());
        }
        /// <summary>
        /// Retrieve an object from the cache.  If the requested entry is not in the cache
        /// null is returned.
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        public static new object Get(string key)
        {
            return Service.CacheServices.Get(key);
        }
        #region Cache Control
        /// <summary>
        /// Initialize the cache ChangeMonitorEntry.
        /// </summary>
        public static void InitialzeCache()
        {
            Service.CacheServices.InitialzeCache(CacheBreaker);
        }
        /// <summary>
        /// Invalidate the cache.
        /// </summary>
        public static void InvalidateCache()
        {
            Service.CacheServices.InvalidateCache(CacheBreaker);
        }
        /// <summary>
        /// Removes an item from the cache if it exists.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        public static new void Remove(string key)
        {
            Service.CacheServices.Remove(key);
        }
        #endregion
    }
    /// <summary>
    /// Second test cache wrapper to access the CacheService methods.
    /// </summary>
    public class TestCacheTwo : Service.CacheServices
    {
        public static string CacheBreaker = "CacheBreakerTwo";
        /// <summary>
        /// Policy to use on the item in the cache
        /// </summary>
        private static CacheItemPolicy Policy()
        {
            CacheItemPolicy cachePolicy = new CacheItemPolicy();
            string[] keys = { CacheBreaker };
            cachePolicy.ChangeMonitors.Add(MemoryCache.Default.CreateCacheEntryChangeMonitor(keys));
            return cachePolicy;
        }
        /// <summary>
        /// Add an object to the cache
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        /// <param name="itemToAdd">Object to add to the cache</param>
        public static void Add(string key, object itemToAdd)
        {
            Service.CacheServices.Add(key, itemToAdd, Policy());
        }
        /// <summary>
        /// Retrieve an object from the cache.  If the requested entry is not in the cache
        /// null is returned.
        /// </summary>
        /// <param name="key">Cache entry key to use</param>
        public static new object Get(string key)
        {
            return Service.CacheServices.Get(key);
        }
        #region Cache Control
        /// <summary>
        /// Initialize the cache ChangeMonitorEntry.
        /// </summary>
        public static void InitialzeCache()
        {
            Service.CacheServices.InitialzeCache(CacheBreaker);
        }
        /// <summary>
        /// Invalidate the cache.
        /// </summary>
        public static void InvalidateCache()
        {
            Service.CacheServices.InvalidateCache(CacheBreaker);
        }
        /// <summary>
        /// Removes an item from the cache if it exists.
        /// </summary>
        /// <param name="key">Cache entry key</param>
        public static new void Remove(string key)
        {
            Service.CacheServices.Remove(key);
        }
        #endregion
    }
    /// <summary>
    /// Test class for GetValues
    /// </summary>
    public class TestCacheClass
    {
        public string SP { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
    }
}
