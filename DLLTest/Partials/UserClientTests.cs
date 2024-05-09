using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
		
namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for UserClient Extension tests
    /// </summary>
    [TestClass()]
    public class UserClientTests
    {
        private const int userId = 543;
        private const int clientId = 10;

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
        #region Populate tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("UserClient")]
        public void PopulateTest()
        {
            //
            // Set up local data
            //
            UserClient entity = new UserClient();
            //
            // Test
            //
            entity.Populate(userId, clientId);
            //
            // Verify
            //
            Assert.AreEqual(userId, entity.UserID, "user id not properly set");
            Assert.AreEqual(clientId, entity.ClientID, "user not properly set");
            Assert.AreEqual(0, entity.UserClientID, "property was set when it should not have been");
            Assert.IsNull(entity.ModifiedBy, "property was set when it should not have been");
            Assert.IsNull(entity.ModifiedDate, "property was set when it should not have been");
            Assert.IsNull(entity.CreatedBy, "property was set when it should not have been");
            Assert.IsNull(entity.CreatedDate, "property was set when it should not have been");
            Assert.IsNull(entity.DeletedBy, "property was set when it should not have been");
            Assert.IsNull(entity.DeletedDate, "property was set when it should not have been");
            Assert.IsNull(entity.User, "property was set when it should not have been");
            Assert.IsNull(entity.Client, "property was set when it should not have been");
        }
        #endregion
	
    }
		
}
;