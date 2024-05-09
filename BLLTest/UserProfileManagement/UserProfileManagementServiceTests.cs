using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.UserProfileManagement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
		
		
namespace BLLTest.UserProfileManagement
{
    /// <summary>
    /// Unit tests for UserProfileManagementService
    /// </summary>
    [TestClass()]
    public partial class UserProfileManagementServiceTests : BLLBaseTest
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
            base.InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            base.CleanUpMocks();
        }
        //
        #endregion
        #endregion
        #region SearchUser - Create
        /// <summary>
        /// Test supplying all three parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithThreeParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithTwoParametersEmailNullTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = null;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithTwoParametersTLastNameNullest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = null;
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithTwoParametersFirstNameNullTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = null;
            string lastName = "last name";
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithTwoParametersEmailEmptyTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithTwoParametersTLastNameEmptyTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = string.Empty;
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithTwoParametersFirstNameEmptyTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = "last name";
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterEmailEmptyTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterTLastNameEmptyTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = "last name";
            string email = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterFirstNameEmptyTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = string.Empty;
            string email = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterEmailNullTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = null;
            string lastName = null;
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterTLastNameNullTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = null;
            string lastName = "last name";
            string email = null;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterFirstNameNullTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = null;
            string email = null;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterEmailMixTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = null;
            string lastName = string.Empty;
            string email = "email@address.com";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterTLastNameMixTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = "last name";
            string email = null;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserCreateWithOneParameterFirstNameMixTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = null;
            string email = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, resultModel);
        }
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "UserProfileManagementService.SearchUser detected an invalid parameter: firstName, lastName & email was [null]")]
        public void SearchUserCreateFailureNullTest()
        {
            //
            // Test
            //
            UserSearchFailureTest(null, null, null);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for UserSerach
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void UserSearchSuccessTest(string firstName, string lastName, string email, ResultModel<IFoundUserModel> resultModel)
        {
            //
            // set the expectations
            //
            MockUnitOfWorkUserRepository();
            Expect.Call(this.theUserRepositoryMock.FindUser(firstName, lastName, email)).Return(resultModel);
            theMock.ReplayAll();

            var result = this.theTestUserProfileManagementService.SearchUser(firstName, lastName, email);
            //
            // Test the assertions
            //
            Assert.IsNotNull(result.ModelList, "ModelList was null and it should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), result.ModelList.Count(), "incorrect number of entries returned");

            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for an unsuccessful test for SetOrderOfReview
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void UserSearchFailureTest(string firstName, string lastName, string email)
        {
            //
            // set the expectations
            //
            MockUnitOfWorkUserRepository();
            theMock.ReplayAll();

            var result = this.theTestUserProfileManagementService.SearchUser(firstName, lastName, email);
        }
        #endregion

        #endregion
        #region Search User - Modify an existing user
        /// <summary>
        /// Test supplying two parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "UserProfileManagementService.SearchUser detected an invalid parameter: firstName, lastName, email, userName, userId and vendorId was [null or <=0]")]
        public void SearchUserModifyFailureNullTest()
        {
            //
            // Test
            //
            UserSearchFailureTest(null, null, null, null, 0);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyeWithFirstNameNullParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = null;
            string lastName = "last name";
            string email = "email@address.com";
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithLastNameNullParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = null;
            string email = "email@address.com";
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithEmailNullParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = null;
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithUserNameNullParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = "email@address.com";
            string userName = null;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithFirstNameEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = "last name";
            string email = "email@address.com";
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithLastNameEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = string.Empty;
            string email = "email@address.com";
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithEmailEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = string.Empty;
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithUserNameEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = "email@address.com";
            string userName = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying all five parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithFiveParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = "email@address.com";
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying three parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithTwoFirstLastEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = "email@address.com";
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithTwoLastEmailEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = string.Empty;
            string email = string.Empty;
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithTwoEmailUserNameEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = "last name";
            string email = string.Empty;
            string userName = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying four parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyWithTwoFirstUserEmptyParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = "last name";
            string email = "email@address.com";
            string userName = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        /// <summary>
        /// Test supplying one parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyOnlyFirstNameParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = "first name";
            string lastName = string.Empty;
            string email = string.Empty;
            string userName = "userName";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, 0, resultModel);
        }
        /// <summary>
        /// Test supplying one parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyOnlyLastNameParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = "last name";
            string email = string.Empty;
            string userName = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, 0, resultModel);
        }
        /// <summary>
        /// Test supplying one parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyOnlyEmailParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = "email@address.com";
            string userName = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, 0, resultModel);
        }
        /// <summary>
        /// Test supplying one parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyOnlyUserNameParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = string.Empty;
            string userName = "user name";
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, 0, resultModel);
        }
        /// <summary>
        /// Test supplying one parameter values
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementService")]
        public void SearchUserModifyOnlyUserParametersTest()
        {
            //
            // Set up local data
            //
            var resultModel = this.BuildResult<IFoundUserModel, FoundUserModel>(5);
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = string.Empty;
            string userName = string.Empty;
            //
            // Test
            //
            UserSearchSuccessTest(firstName, lastName, email, userName, _goodUserId, resultModel);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for UserSerach
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void UserSearchSuccessTest(string firstName, string lastName, string email, string userName, int userId, ResultModel<IFoundUserModel> resultModel)
        {
            //
            // set the expectations
            //
            MockUnitOfWorkUserRepository();
            Expect.Call(this.theUserRepositoryMock.FindUser(firstName, lastName, email, userName, userId, null)).Return(resultModel);
            theMock.ReplayAll();

            var result = this.theTestUserProfileManagementService.SearchUser(firstName, lastName, email, userName, userId, null);
            //
            // Test the assertions
            //
            Assert.IsNotNull(result.ModelList, "ModelList was null and it should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), result.ModelList.Count(), "incorrect number of entries returned");

            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for an unsuccessful test for SetOrderOfReview
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void UserSearchFailureTest(string firstName, string lastName, string email, string userName, int userId)
        {
            //
            // set the expectations
            //
            MockUnitOfWorkUserRepository();
            theMock.ReplayAll();

            var result = this.theTestUserProfileManagementService.SearchUser(firstName, lastName, email, userName, userId, null);
        }
        #endregion
        #endregion
        #region - Profile
        /// <summary>
        /// Test supplying all three parameter values
        /// </summary>
        //[TestMethod()]
        //[Category("UserProfileManagementService")]
        public void GetUserDegreeSingleTest()
        {
            //
            // Set up local data
            //
            int userInfoId = 5;

            List<UserDegree> theList = new List<UserDegree>();
            theList.Add(new Sra.P2rmis.Dal.UserDegree{ UserDegreeId = 100, DegreeId = 5, UserInfoId = userInfoId });
            //
            // Test
            //
            MockUnitOfWorkUserDegreeRepository();
            Expect.Call(this.theUserDegreeRepositoryMock.GetAll()).Return(theList);
            theMock.ReplayAll();

            var result = this.theTestUserProfileManagementService.GetUserDegree(userInfoId);

            Assert.IsNotNull(result.ModelList, "ModelList was null and it should not be");
            var v = result.ModelList.ToList();
            Assert.AreEqual(theList.Count(), v.Count(), "incorrect number of entries returned");

            theMock.VerifyAll();
        }
        #endregion
    }	
}
