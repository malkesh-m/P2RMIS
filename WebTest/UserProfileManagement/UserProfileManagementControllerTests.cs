using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Web.Controllers.UserProfileManagement;
using Sra.P2rmis.Web.UI.Models;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest.UserProfileManagement
{
    /// <summary>
    /// Unit test for PanelManagementController common method
    /// </summary>
    [TestClass()]
    public partial class UserProfileManagementControllerTests: WebBaseTest
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
            InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            CleanUpMocks();
        }
        #endregion

        #region The Tests
        /// <summary>
        /// Test for SetTabs
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementBaseController.SetTabs")]
        public void SetAllTabsTest()
        {
            var viewModel = new UserProfileManagementViewModel();

            var controllerMock = theMock.PartialMock<UserProfileManagementBaseController>();
            Expect.Call(controllerMock.HasPermission(Arg<string>.Is.Anything)).Return(true);
            theMock.ReplayAll();

            controllerMock.SetTabs(viewModel);
            
            Assert.IsTrue(viewModel.Tabs.Count == 4);
        }
        /// <summary>
        /// Test for SetTabs
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementBaseController.SetTabs")]
        public void SetFilteredTabsTest()
        {
            var viewModel = new UserProfileManagementViewModel();

            var controllerMock = theMock.PartialMock<UserProfileManagementBaseController>();
            Expect.Call(controllerMock.HasPermission(Arg<string>.Is.Null)).Return(true);
            Expect.Call(controllerMock.HasPermission(Arg<string>.Is.NotNull)).Return(false);
            theMock.ReplayAll();

            controllerMock.SetTabs(viewModel);
            
            Assert.IsTrue(viewModel.Tabs.Count == 2);
        }
        /// <summary>
        /// Test for HasPermission
        /// </summary>
        [TestMethod()]
        [Category("UserProfileManagementBaseController.HasPermission")]
        public void HasPermissionTest()
        {
             var controllerMock = theMock.PartialMock<UserProfileManagementBaseController>();
            theMock.ReplayAll();

            var result = controllerMock.HasPermission(null);
            
            Assert.IsTrue(result);
        }        
        #endregion

        #region Helpers
        #endregion
        #endregion
    }
}
