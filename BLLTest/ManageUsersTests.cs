using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace BLLTest
{
    /// <summary>
    /// Unit tests for the ManageUsers Service.
    /// </summary>
    [TestClass()]
    public class ManageUsersTests
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
        #region Constants
        private const string Phone1 = "111-222-3333";
        private const string Extension1 = "1111";
        private const string Phone2 = "999-888-5555";
        private const string Extension2 = "2222";
        private const string Phone3 = "000-888-3333";
        private const string Extension3 = "3333";
        private const string EMail1 = "IDoNotNeedAnEmailAddress@server.com";
        private const string EMail2 = "ThisIsAnEmailAddress@server.com";
        private const string FirstName2 = "FirstName2";
        private const string LastName2 = "LastName2";
        private const string MiddleName2 = "MiddleName2";
        private const string FullUserName2 = "FullUserName2";
        private const int DegreeLkpID2 = 100;
        private const int PrefixLkpID2 = 101;
        private const int SuffixLkpID2 = 102;
        private const string FirstName1 = "FirstName1";
        private const string LastName1 = "LastName1";
        private const string MiddleName1 = "MiddleName1";
        private const string FullUserName1 = "FullUserName1";
        private const int DegreeLkpID11 = 200;
        private const int PrefixId1 = 201;
        private const int SuffixLkpID1 = 202;
        #endregion
        #region UpdatePrimaryPhoneTests
        /// <summary>
        /// Test UpdatePrimaryPhone
        /// </summary>
        [Category("UpdatePrimaryPhone")]
        [TestMethod()]
        public void UpdatePrimaryPhoneTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();


            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdatePrimaryPhone(container, Phone3, Extension3);

            Assert.IsNotNull(container.PrimaryPhone, "Primary phone is null");
            Assert.AreEqual(Phone3, container.PrimaryPhone.Phone, string.Format("Primary phone number is not correct but is ({0}) ", container.PrimaryPhone.Phone));
            Assert.AreEqual(Extension3, container.PrimaryPhone.Extension, string.Format("Primary phone number extension is not correct but is ({0}) ", container.PrimaryPhone.Extension));
            Assert.AreEqual(Phone2, container.AlternatePhone.Phone, string.Format("Alternate phone number was changed to {0}", container.AlternatePhone.Phone));
        }
        /// <summary>
        /// Test UpdatePrimaryPhone with null phone number
        /// </summary>
        [Category("UpdatePrimaryPhone")]
        [TestMethod()]
        public void UpdatePrimaryPhoneNullPhoneTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdatePrimaryPhone(container, null, Extension3);

            Assert.IsNotNull(container.PrimaryPhone, "Primary phone is null");
            Assert.AreEqual(null, container.PrimaryPhone.Phone, string.Format("Primary phone number is not correct but is ({0}) ", container.PrimaryPhone.Phone));
            Assert.AreEqual(Extension3, container.PrimaryPhone.Extension, string.Format("Primary phone number extension is not correct but is ({0}) ", container.PrimaryPhone.Extension));
            Assert.AreEqual(Phone2, container.AlternatePhone.Phone, string.Format("Alternate phone number was changed to {0}", container.AlternatePhone.Phone));
        }
        /// <summary>
        /// Test UpdatePrimaryPhone with null extension
        /// </summary>
        [Category("UpdatePrimaryPhone")]
        [TestMethod()]
        public void UpdatePrimaryPhoneNullExtentionTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);

            service.UpdatePrimaryPhone(container, Phone3, null);

            Assert.IsNotNull(container.PrimaryPhone, "Primary phone is null");
            Assert.AreEqual(Phone3, container.PrimaryPhone.Phone, string.Format("Primary phone number is not correct but is ({0}) ", container.PrimaryPhone.Phone));
            Assert.IsNull(container.PrimaryPhone.Extension, string.Format("Primary phone number extension is not correct but is ({0}) ", container.PrimaryPhone.Extension));
            Assert.AreEqual(Phone2, container.AlternatePhone.Phone, string.Format("Alternate phone number was changed to {0}", container.AlternatePhone.Phone));
        }
        /// <summary>
        /// Test UpdatePrimaryPhone with no container
        /// </summary>
        [Category("UpdatePrimaryPhone")]
        [TestMethod()]
        [ExpectedException("System.NullReferenceException")]
        public void UpdatePrimaryPhoneNullContainerTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = null;

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdatePrimaryPhone(container, Phone3, Extension3);
        }
        #endregion
        #region UpdateAlternatePhoneTests
        /// <summary>
        /// Test UpdateAlternatePhone
        /// </summary>
        [Category("UpdateAlternatePhone")]
        [TestMethod()]
        public void UpdateAlternatePhoneTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();


            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateAlternatePhone(container, Phone3, Extension3);

            Assert.IsNotNull(container.AlternatePhone, "Alternate phone is null");
            Assert.AreEqual(Phone1, container.PrimaryPhone.Phone, string.Format("Primary phone number is not correct but is ({0}) ", container.PrimaryPhone.Phone));
            Assert.AreEqual(Extension1, container.PrimaryPhone.Extension, string.Format("Primary phone number extension is not correct but is ({0}) ", container.PrimaryPhone.Extension));
            Assert.AreEqual(Phone3, container.AlternatePhone.Phone, string.Format("Alternate phone number was changed to {0}", container.AlternatePhone.Phone));
            Assert.AreEqual(Extension3, container.AlternatePhone.Extension, string.Format("Alternate phone number extension is not correct but is ({0}) ", container.AlternatePhone.Extension));
        }
        /// <summary>
        /// Test UpdateAlternatePhone with null phone number
        /// </summary>
        [Category("UpdateAlternatePhone")]
        [TestMethod()]
        public void UpdateAlternatePhoneNullPhoneTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateAlternatePhone(container, null, Extension3);

            Assert.IsNotNull(container.AlternatePhone, "Alternate phone is null");
            Assert.AreEqual(Phone1, container.PrimaryPhone.Phone, string.Format("Primary phone number is not correct but is ({0}) ", container.PrimaryPhone.Phone));
            Assert.AreEqual(Extension1, container.PrimaryPhone.Extension, string.Format("Primary phone number extension is not correct but is ({0}) ", container.PrimaryPhone.Extension));
            Assert.AreEqual(null, container.AlternatePhone.Phone, string.Format("Alternate phone number was changed to {0}", container.AlternatePhone.Phone));
            Assert.AreEqual(null, container.AlternatePhone.Extension, string.Format("Alternate phone number extension is not correct but is ({0}) ", container.AlternatePhone.Extension));
        }
        /// <summary>
        /// Test UpdateAlternatePhone with null extension
        /// </summary>
        [Category("UpdateAlternatePhone")]
        [TestMethod()]
        public void UpdateAlternatePhoneNullExtentionTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);

            service.UpdateAlternatePhone(container, Phone3, null);

            Assert.IsNotNull(container.AlternatePhone, "Alternate phone is null");
            Assert.AreEqual(Phone1, container.PrimaryPhone.Phone, string.Format("Primary phone number is not correct but is ({0}) ", container.PrimaryPhone.Phone));
            Assert.AreEqual(Extension1, container.PrimaryPhone.Extension, string.Format("Primary phone number extension is not correct but is ({0}) ", container.PrimaryPhone.Extension));
            Assert.AreEqual(Phone3, container.AlternatePhone.Phone, string.Format("Alternate phone number was changed to {0}", container.AlternatePhone.Phone));
            Assert.AreEqual(null, container.AlternatePhone.Extension, string.Format("Alternate phone number extension is not correct but is ({0}) ", container.AlternatePhone.Extension));
        }
        /// <summary>
        /// Test UpdateAlternatePhone with no container
        /// </summary>
        [Category("UpdateAlternatePhone")]
        [TestMethod()]
        [ExpectedException("System.NullReferenceException")]
        public void UpdateAlternatePhoneNullContainerTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = null;

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateAlternatePhone(container, Phone3, Extension3);
        }
        #endregion
        #region UpdateEMail
        /// <summary>
        /// Test UpdateEMail 
        /// </summary>
        [Category("UpdateEMail")]
        [TestMethod()]
        public void UpdateEMailTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();


            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateEmail(container, EMail2);

            Assert.IsNotNull(container.PrimaryEmail, "EMail is null");
            Assert.AreEqual(EMail2, container.PrimaryEmail.Email, string.Format("EMail is not correct but is ({0}) ", container.PrimaryEmail.Email));
        }
        /// <summary>
        /// Test UpdateEMail 
        /// </summary>
        [Category("UpdateEMail")]
        [TestMethod()]
        public void UpdateEMailNullTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();


            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateEmail(container, null);

            Assert.IsNotNull(container.PrimaryEmail, "EMail is null");
            Assert.IsNull(container.PrimaryEmail.Email, string.Format("EMail is not correct but is ({0}) ", container.PrimaryEmail.Email));
        }
        #endregion
        #region  UpdaateUserInfo Tests
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, LastName2, MiddleName2, FullUserName2, DegreeLkpID2, PrefixLkpID2, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullNameTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, null, LastName2, MiddleName2, FullUserName2, DegreeLkpID2, PrefixLkpID2, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(null, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullLastTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, null, MiddleName2, FullUserName2, DegreeLkpID2, PrefixLkpID2, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(null, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullMiNameest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, LastName2, null, FullUserName2, DegreeLkpID2, PrefixLkpID2, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(null, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullFullNameTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, LastName2, MiddleName2, null, DegreeLkpID2, PrefixLkpID2, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(null, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullDegreeIDTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, LastName2, MiddleName2, FullUserName2, null, PrefixLkpID2, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(null, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullPrefixIdTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, LastName2, MiddleName2, FullUserName2, DegreeLkpID2, null, SuffixLkpID2);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(null, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        /// <summary>
        /// Test UpdateUserInfo
        /// </summary>
        [Category("UpdateUserInfo")]
        [TestMethod()]
        public void UpdateUserInfoNullSuffixTest()
        {
            MockRepository mocks = new MockRepository();
            IUnitOfWork workModel = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            UserAccountContainer container = CreateContainer();

            TestManagerUsers service = new TestManagerUsers(workModel);
            service.UpdateUserInfo(container, FirstName2, LastName2, MiddleName2, FullUserName2, DegreeLkpID2, PrefixLkpID2, null);

            List<UserInfo> usrInfoList = container.User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            Assert.AreEqual(FirstName2, userInfo.FirstName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(LastName2, userInfo.LastName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(MiddleName2, userInfo.MiddleName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(FullUserName2, userInfo.BadgeName, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            //Assert.AreEqual(DegreeLkpID2, userInfo.DegreeLkpID, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
            Assert.AreEqual(PrefixLkpID2, userInfo.PrefixId, string.Format("First name is not correct but is ({0}) ", userInfo.FirstName));
        }
        #endregion
        #region PasswordValidation Tests

        [TestMethod()]
        public void IsPasswordMatchingTest()
        {
            string password = "Test12!@";
            string email = "test@test.com";
            string username = "Tester123";
            bool isMatching = ManageUsers.IsPasswordMatching(password, email, username);
            bool expected = false;
            Assert.AreEqual(expected, isMatching);
        }

        [TestMethod()]
        public void IsPasswordMatchingEmailTest()
        {
            string password = "Test12@yes.com";
            string email = "Test12@yes.com";
            string username = "Tester123";
            bool isMatching = ManageUsers.IsPasswordMatching(password, email, username);
            bool expected = true;
            Assert.AreEqual(expected, isMatching);
        }

        [TestMethod()]
        public void IsPasswordMatchingUsernameTest()
        {
            string password = "Test12@yes.com";
            string email = "Test@yes.com";
            string username = "Test12@yes.com";
            bool isMatching = ManageUsers.IsPasswordMatching(password, email, username);
            bool expected = true;
            Assert.AreEqual(expected, isMatching);
        }

        [TestMethod()]
        public void IsPasswordMatchingBothTest()
        {
            string password = "Test12@yes.com";
            string email = "Test12@yes.com";
            string username = "Test12@yes.com";
            bool isMatching = ManageUsers.IsPasswordMatching(password, email, username);
            bool expected = true;
            Assert.AreEqual(expected, isMatching);
        }

        #endregion
        #region helpers
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private UserAccountContainer CreateContainer()
        {
            UserAccountContainer container = new UserAccountContainer();
            User u = new User();
            UserInfo ui = new UserInfo();
            UserPhone pP = new UserPhone();
            pP.Phone = Phone1;
            pP.Extension = Extension1;
            pP.PrimaryFlag = true;

            UserPhone pA = new UserPhone();
            pA.Phone = Phone2;
            pA.Extension = Extension2;
            pA.PrimaryFlag = false;

            UserEmail email = new UserEmail();
            email.Email = EMail1;
            email.PrimaryFlag = true;

            container.User = u;

            ui.UserPhones.Add(pP);
            ui.UserPhones.Add(pA);
            ui.UserEmails.Add(email);

            ui.FirstName = FirstName1;
            ui.LastName = LastName1;
            ui.MiddleName = MiddleName1;
            ui.BadgeName = FullUserName1;
            //ui.DegreeLkpID = DegreeLkpID11;
            ui.PrefixId = PrefixId1;
            ui.UserInfoID = 5;

            u.UserInfoes.Add(ui);
            return container;
        }

        #endregion
    }
    public class TestManagerUsers : ManageUsers
    {
        public TestManagerUsers(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }
    }

    /// <summary>
    /// Unit tests for the UserAccountContainer.
    /// </summary>
    [TestClass()]
    public class UserAccountContainerTests
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
        #region Constants
        private const string Phone1 = "111-222-3333";
        private const string Extension1 = "1111";
        private const string Phone2 = "999-888-5555";
        private const string Extension2 = "2222";
        #endregion
        #region UpdatePrimaryPhone Tests
        /// <summary>
        /// Test PrimaryPhone
        ///</summary>
        [TestMethod()]
        public void PrimaryPhoneTest()
        {
            UserAccountContainer c = new UserAccountContainer();
            User u = new User();
            UserInfo ui = new UserInfo();
            UserPhone pP = new UserPhone();
            pP.Phone = Phone1;
            pP.Extension = Extension1;
            pP.PrimaryFlag = true;

            UserPhone pA = new UserPhone();
            pA.Phone = Phone2;
            pA.Extension = Extension2;
            pA.PrimaryFlag = false;

            c.User = u;

            ui.UserPhones.Add(pP);
            ui.UserPhones.Add(pA);
            u.UserInfoes.Add(ui);

            Assert.IsNotNull(c.PrimaryPhone);
            Assert.AreEqual(Phone1, c.PrimaryPhone.Phone);
            Assert.AreEqual(Extension1, c.PrimaryPhone.Extension);
        }
        /// <summary>
        /// Test AlternatePhone
        ///</summary>
        [TestMethod()]
        public void AlternatePhoneTest()
        {
            UserAccountContainer c = new UserAccountContainer();
            User u = new User();
            UserInfo ui = new UserInfo();
            UserPhone pP = new UserPhone();
            pP.Phone = Phone1;
            pP.Extension = Extension1;
            pP.PrimaryFlag = true;

            UserPhone pA = new UserPhone();
            pA.Phone = Phone2;
            pA.Extension = Extension2;
            pA.PrimaryFlag = false;

            c.User = u;

            ui.UserPhones.Add(pP);
            ui.UserPhones.Add(pA);
            u.UserInfoes.Add(ui);

            Assert.IsNotNull(c.AlternatePhone);
            Assert.AreEqual(Phone2, c.AlternatePhone.Phone);
            Assert.AreEqual(Extension2, c.AlternatePhone.Extension);
        }
        /// <summary>
        /// Test EMailTest
        ///</summary>
        [TestMethod()]
        public void EMailTest()
        {
            UserAccountContainer c = new UserAccountContainer();
            User u = new User();
            UserInfo ui = new UserInfo();
            UserPhone pP = new UserPhone();
            pP.Phone = Phone1;
            pP.Extension = Extension1;
            pP.PrimaryFlag = true;

            UserPhone pA = new UserPhone();
            pA.Phone = Phone2;
            pA.Extension = Extension2;
            pA.PrimaryFlag = false;

            c.User = u;

            ui.UserPhones.Add(pP);
            ui.UserPhones.Add(pA);
            u.UserInfoes.Add(ui);

            Assert.IsNotNull(c.AlternatePhone);
            Assert.AreEqual(Phone2, c.AlternatePhone.Phone);
            Assert.AreEqual(Extension2, c.AlternatePhone.Extension);
        }
        #endregion
    }

}
