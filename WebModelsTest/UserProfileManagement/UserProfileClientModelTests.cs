using NUnit.Framework;
using Sra.P2rmis.WebModels.UserProfileManagement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest.UserProfileManagement
{

    /// <summary>
    /// Unit tests for UserProfileClientModel
    /// </summary>
    [TestClass()]
    public class UserProfileClientModelTests
    {
        #region Constants
        private const int userClientId = 12345;
        #endregion
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
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion
        #region IsAdd() test     
        /// <summary>
        /// Test IsAdd()
        /// </summary>
        [TestMethod()]
        [Category("UserProfileClientModel")]
        public void IsAddNullTest()
        {
            //
            // Set up local data
            //
            UserProfileClientModel model = new UserProfileClientModel { UserClientId = null };
            //
            // Test
            //
            var result = model.IsAdd();
            //
            // Verify
            //
            Assert.IsTrue(result, "Should have been an add");
        }
        /// <summary>
        /// Test IsAdd()
        /// </summary>
        [TestMethod()]
        [Category("UserProfileClientModel")]
        public void IsAddZeroTest()
        {
            //
            // Set up local data
            //
            UserProfileClientModel model = new UserProfileClientModel { UserClientId = 0 };
            //
            // Test
            //
            var result = model.IsAdd();
            //
            // Verify
            //
            Assert.IsTrue(result, "Should have been an add");
        }
        /// <summary>
        /// Test IsAdd()
        /// </summary>
        [TestMethod()]
        [Category("UserProfileClientModel")]
        public void IsAddValueTest()
        {
            //
            // Set up local data
            //
            UserProfileClientModel model = new UserProfileClientModel { UserClientId = 6 };
            //
            // Test
            //
            var result = model.IsAdd();
            //
            // Verify
            //
            Assert.IsFalse(result, "Should not have been an add");
        }
        #endregion
        #region IsDelete() tests
        /// <summary>
        /// Test IsDelete()
        /// </summary>
        [TestMethod()]
        [Category("UserProfileClientModel")]
        public void IsDeleteEmptyTest()
        {
            //
            // Set up local data
            //
            UserProfileClientModel model = new UserProfileClientModel();
            //
            // Test
            //
            var result = model.IsDelete();
            //
            // Verify
            //
            Assert.IsFalse(result, "Should not have been a delete");
        }
        /// <summary>
        /// Test IsDelete()
        /// </summary>
        [TestMethod()]
        [Category("UserProfileClientModel")]
        public void IsDeleteValueTest()
        {
            //
            // Set up local data
            //
            UserProfileClientModel model = new UserProfileClientModel { UserClientId = userClientId };
            //
            // Test
            //
            var result = model.IsDelete();
            //
            // Verify
            //
            Assert.IsFalse(result, "Should not have been a delete");
            Assert.AreEqual(userClientId, model.UserClientId, "UserClientId was not as expected");
        }
        /// <summary>
        /// Test IsDelete() (and PopulateDelete())
        /// </summary>
        [TestMethod()]
        [Category("UserProfileClientModel")]
        public void IsDeleteTest()
        {
            //
            // Set up local data
            //
            UserProfileClientModel model = new UserProfileClientModel();
            model.PopulateDelete(userClientId);
            //
            // Test
            //
            var result = model.IsDelete();
            //
            // Verify
            //
            Assert.IsTrue(result, "Should have been a delete");
            Assert.AreEqual(userClientId, model.UserClientId, "UserClientId was not as expected");
        }
        #endregion
    }

}
