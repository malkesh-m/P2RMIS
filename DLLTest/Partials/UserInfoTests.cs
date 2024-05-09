using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for UserInfo entity object
    /// </summary>
    [TestClass()]
    public class UserInfoTests : DllBaseTest
    {
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
            this.InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            this.CleanUpMocks();
        }
        #endregion
        #endregion
        #region Default Tests
        /// <summary>
        /// Test  entity's default set up
        /// </summary>
        [TestMethod()]
        [Category("UserInfo")]
        public void DefaultTest()
        {
            //
            // Verify
            //
            Assert.NotNull(UserInfo.Default, "UserInfo Default not initialized");
            Assert.NotNull(UserInfo.Default.UserEmails, "UserInfo's Email list is null & should not be");
            Assert.AreEqual(1, UserInfo.Default.UserEmails.Count(), "UserInfo's Email list is not correct size");
            Assert.IsFalse(UserInfo.Default.UserEmails.ElementAt(0).PrimaryFlag, "UserInfo's Email primary flag set incorrectly");;
            Assert.IsNull(UserInfo.Default.UserEmails.ElementAt(0).Email, "UserInfo's Email address set incorrectly");
        }
        #endregion
    }
		
}
