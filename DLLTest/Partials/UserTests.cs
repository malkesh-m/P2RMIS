using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for User entity object
    /// </summary>
    [TestClass()]
    public partial class UserTests: DllBaseTest
    {
        #region Constants
        private string _happyFirstNameUpperCase = "FirstName";
        private string _happyLastNameUpper = "LastName";
        private string _shortLastNameUpper = "Last";
        private string _equalFirstNameUpper = "F";
        private string _equalLastNameUpper = "Lastn";

        private string _happyFirstNameLowerCase = "firstName";
        private string _happyLastNameLower = "lastName";
        private string _shortLastNameLower = "last";
        private string _equalFirstNameLower = "f";
        private string _equalLastNameLower = "lastn";

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
        #region CounstructUserName
        /// <summary>
        /// Test CounstructUserName - happy path
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void HappyPathTestUpper()
        {
            //
            // Set up local data
            //
            int count = 5;
            //
            // Test
            //
            string result = User.CounstructUserName(_happyFirstNameUpperCase, _happyLastNameUpper, count);
            //
            // Verify
            //
            Assert.AreEqual("FLastN005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - happy path
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void HappyPathTestLower()
        {
            //
            // Set up local data
            //
            int count = 5;
            //
            // Test
            //
            string result = User.CounstructUserName(_happyFirstNameLowerCase, _happyLastNameLower, count);
            //
            // Verify
            //
            Assert.AreEqual("FLastN005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - short last name
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void ShortLastNameTestUpper()
        {
            //
            // Set up local data
            //
            int count = 5;
            //
            // Test
            //
            string result = User.CounstructUserName(_happyFirstNameUpperCase, _shortLastNameUpper, count);
            //
            // Verify
            //
            Assert.AreEqual("FLast005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - short last name
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void ShortLastNameTestLower()
        {
            //
            // Set up local data
            //
            int count = 5;
            //
            // Test
            //
            string result = User.CounstructUserName(_happyFirstNameLowerCase, _shortLastNameLower, count);
            //
            // Verify
            //
            Assert.AreEqual("FLast005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - large count
        /// </summary>
        //[TestMethod()]
        [Category("User.CounstructUserName")]
        public void LargeCountTestUpper()
        {
            //
            // Set up local data
            //
            int count = 1000;
            //
            // Test
            //
            string result = User.CounstructUserName(_happyFirstNameUpperCase, _happyLastNameUpper, count);
            //
            // Verify
            //
            Assert.AreEqual("FLastN005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - short last name
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void EqualNameTestUpper()
        {
            //
            // Set up local data
            //
            int count = 5;
            //
            // Test
            //
            string result = User.CounstructUserName(_equalFirstNameUpper, _equalLastNameUpper, count);
            //
            // Verify
            //
            Assert.AreEqual("FLastn005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - short last name
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void EqualNameTestLower()
        {
            //
            // Set up local data
            //
            int count = 5;
            //
            // Test
            //
            string result = User.CounstructUserName(_equalFirstNameLower, _equalLastNameLower, count);
            //
            // Verify
            //
            Assert.AreEqual("FLastn005", result, "Generated user name was not as expected");
        }
        /// <summary>
        /// Test CounstructUserName - short last name
        /// </summary>
        [TestMethod()]
        [Category("User.CounstructUserName")]
        public void LargeCounterTest()
        {
            //
            // Set up local data
            //
            int count = 1000;
            //
            // Test
            //
            string result = User.CounstructUserName(_equalFirstNameLower, _equalLastNameLower, count);
            Assert.AreEqual("FLastn1000", result, "Generated user name was not as expected");
        }
        #endregion
        #region Activate Tests
        /// <summary>
        /// Test Activate
        /// </summary>
        [TestMethod()]
        [Category("User.Activate")]
        public void ActivateTest()
        {
            //
            // Set up local data
            //
            User u = new User { };
            u.UserAccountStatus.Add(new UserAccountStatu());
            //
            // Test
            //
            var result = u.Activate(_goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(_goodUserId, result.ModifiedBy, "Modified by was not as expected");
            Assert.IsNotNull(result.ModifiedDate, "Modified Date was not as expected");
            Assert.AreEqual(AccountStatu.Indexes.Active, result.AccountStatusId, "AccountStatusId was not as expected");
            Assert.AreEqual(AccountStatusReason.Indexes.AwaitingCredentials, result.AccountStatusReasonId, "AccountStatusReasonId was not as expected");
            //
            //  These should not have changed
            //
            Assert.AreEqual(0, result.UserAccountStatusId, "UserAccountStatusId was not as it should be");
            Assert.AreEqual(0, result.UserId, "UserId was not as it should be");
            Assert.IsNull(result.CreatedBy, "CreatedBy was not as it should be");
            Assert.IsNull(result.CreatedDate, "CreatedDate was not as it should be");
            Assert.IsNull(result.DeletedBy, "DeletedBy was not as it should be");
            Assert.IsNull(result.DeletedDate, "DeletedDate was not as it should be");
            Assert.IsNull(result.AccountStatu, "AccountStatu was not as it should be");
            Assert.IsNull(result.AccountStatusReason, "AccountStatusReason was not as it should be");
            Assert.IsNull(result.User, "User was not as it should be");
        }
		
        #endregion
        #region Deactivate Tests
        /// <summary>
        /// Test Deactivate
        /// </summary>
        [TestMethod()]
        [Category("User.Dectivate")]
        public void DectivateTest()
        {
            //
            // Set up local data
            //
            User u = new User { };
            u.UserAccountStatus.Add(new UserAccountStatu());
            //
            // Test
            //
            var result = u.Deactivate(AccountStatusReason.Indexes.PermCredentials, _goodUserId);
            //
            // Verify
            //
            Assert.AreEqual(_goodUserId, result.ModifiedBy, "Modified by was not as expected");
            Assert.IsNotNull(result.ModifiedDate, "Modified Date was not as expected");
            Assert.AreEqual(AccountStatu.Indexes.Inactive, result.AccountStatusId, "AccountStatusId was not as expected");
            Assert.AreEqual(AccountStatusReason.Indexes.PermCredentials, result.AccountStatusReasonId, "AccountStatusReasonId was not as expected");
            //
            //  These should not have changed
            //
            Assert.AreEqual(0, result.UserAccountStatusId, "UserAccountStatusId was not as it should be");
            Assert.AreEqual(0, result.UserId, "UserId was not as it should be");
            Assert.IsNull(result.CreatedBy, "CreatedBy was not as it should be");
            Assert.IsNull(result.CreatedDate, "CreatedDate was not as it should be");
            Assert.IsNull(result.DeletedBy, "DeletedBy was not as it should be");
            Assert.IsNull(result.DeletedDate, "DeletedDate was not as it should be");
            Assert.IsNull(result.AccountStatu, "AccountStatu was not as it should be");
            Assert.IsNull(result.AccountStatusReason, "AccountStatusReason was not as it should be");
            Assert.IsNull(result.User, "User was not as it should be");
        }
        #endregion
    }
}
