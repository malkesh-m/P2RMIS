using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.PanelManagement;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
    
namespace BLLTest.PanelManagement
{
    /// <summary>
    /// Unit tests for PanelManagementService 
    /// </summary>
    [TestClass()]
    public partial class PanelManagementServiceTests: BLLBaseTest
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
        //
        #endregion
        #endregion
        #region SetOrderOfRevew Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        public void SetOrderOfReviewSuccessfulOneEntryTest()
        {
            //
            // Set up local data
            //
            int sessionPanelId = 444;
            int newOrder = 2;
            bool isTriaged = false;
            string logNumber = "XX9876";

            SessionPanel sessionPanel = new SessionPanel();
            PanelApplication panelApplication = new PanelApplication();
            panelApplication.Application = new Sra.P2rmis.Dal.Application { LogNumber = logNumber };
            sessionPanel.PanelApplications.Add(panelApplication);

            SetOrderOfReviewToSave item = new SetOrderOfReviewToSave(logNumber, newOrder, isTriaged);
            List<SetOrderOfReviewToSave> collection = new List<SetOrderOfReviewToSave>();
            collection.Add(item);
            //
            // Test
            //
            ListSetOrderOfReviewSuccessTest(sessionPanelId, collection, _goodUserId, sessionPanel);
        }
        /// <summary>
        /// Test invalid sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.SetOrderOfReview detected an invalid parameter: sessionPanelId was 0")]
        public void SetOrderOfReviewZeroSessionPanelIdTest()
        {
            //
            // Set up local data
            //
            string logNumber = "ZZ133";
            int sessionPanelId = 0;
            int newOrder = 2;
            bool isTriaged = false;

            SetOrderOfReviewToSave item = new SetOrderOfReviewToSave(logNumber, newOrder, isTriaged);
            List<SetOrderOfReviewToSave> collection = new List<SetOrderOfReviewToSave>();
            collection.Add(item);
            //
            // Test
            //
            ListSetOrderOfReviewFailureTest(sessionPanelId, collection, _goodUserId);
        }
        /// <summary>
        /// Test invalid sessionPanelId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.SetOrderOfReview detected an invalid parameter: sessionPanelId was -4560")]
        public void SetOrderOfReviewNegativeSessionPanelIdTest()
        {
            //
            // Set up local data
            //
            string logNumber = "ZZ133";
            int sessionPanelId = -4560;
            int newOrder = 2;
            bool isTriaged = false;

            SetOrderOfReviewToSave item = new SetOrderOfReviewToSave(logNumber, newOrder, isTriaged);
            List<SetOrderOfReviewToSave> collection = new List<SetOrderOfReviewToSave>();
            collection.Add(item);
            //
            // Test
            //
            ListSetOrderOfReviewFailureTest(sessionPanelId, collection, _goodUserId);
        }
        /// <summary>
        /// Test invalid userId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.SetOrderOfReview detected an invalid parameter: userId was 0")]
        public void SetOrderOfReviewZeroUserIdTest()
        {
            //
            // Set up local data
            //
            string logNumber = "ZZ133";
            int sessionPanelId = 4560;
            int newOrder = 2;
            bool isTriaged = false;

            SetOrderOfReviewToSave item = new SetOrderOfReviewToSave(logNumber, newOrder, isTriaged);
            List<SetOrderOfReviewToSave> collection = new List<SetOrderOfReviewToSave>();
            collection.Add(item);
            //
            // Test
            //
            ListSetOrderOfReviewFailureTest(sessionPanelId, collection, 0);
        }
        /// <summary>
        /// Test invalid userId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.SetOrderOfReview detected an invalid parameter: userId was -45789")]
        public void SetOrderOfReviewNegativebUserIdTest()
        {
            //
            // Set up local data
            //
            string logNumber = "ZZ133";
            int sessionPanelId = 4560;
            int newOrder = 2;
            bool isTriaged = false;

            SetOrderOfReviewToSave item = new SetOrderOfReviewToSave(logNumber, newOrder, isTriaged);
            List<SetOrderOfReviewToSave> collection = new List<SetOrderOfReviewToSave>();
            collection.Add(item);
            //
            // Test
            //
            ListSetOrderOfReviewFailureTest(sessionPanelId, collection, -45789);
        }
        /// <summary>
        /// Test invalid collection
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.SetOrderOfReview detected an invalid parameter: collection was null")]
        public void SetOrderOfReviewNullCollectionTest()
        {
            //
            // Set up local data
            //
            int sessionPanelId = 4560;
            //
            // Test
            //
            ListSetOrderOfReviewFailureTest(sessionPanelId, null, _goodUserId);
        }
        /// <summary>
        /// Test invalid collection entry
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServer.SetOrderOfReview")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetOrderOfReviewToSave.Validate()  Invalid LogNumber detected; null, empty string or whitespace")]
        public void SetOrderOfReviewInvalidCollectionEntryTest()
        {
            //
            // Set up local data
            //
            string logNumber = "";
            int sessionPanelId = 444;
            int newOrder = 2;
            bool isTriaged = false;

            SetOrderOfReviewToSave item = new SetOrderOfReviewToSave(logNumber, newOrder, isTriaged);
            List<SetOrderOfReviewToSave> collection = new List<SetOrderOfReviewToSave>();
            collection.Add(item);

            ListSetOrderOfReviewFailureTest(sessionPanelId, collection, _goodUserId);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for SetOrderOfReview
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void ListSetOrderOfReviewSuccessTest(int sessionPanelId, ICollection<SetOrderOfReviewToSave> collection, int userId, SessionPanel sessionPanel)
        {
            //
            // set the expectations
            //
            MockUnitOfWorkSessionPanelRepository();
            Expect.Call(theSessionPanelRepositoryMock.GetByID(sessionPanelId)).Return(sessionPanel);
            MockUnitOfWorkSave();
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.SetOrderOfReview(sessionPanelId, collection, userId);
            //
            // Test the assertions
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for an unsuccessful test for SetOrderOfReview
        /// </summary>
        /// <param name="resultModel">ResultModel to return</param>
        /// <param name="sessionPanelId">SessionUPanelId to test</param>
        private void ListSetOrderOfReviewFailureTest(int sessionPanelId, ICollection<SetOrderOfReviewToSave> collection, int userId)
        {
            //
            // set the expectations
            //
            //MockUnitOfWorkSessionPanelRepository();
            //Expect.Call(theSessionPanelRepositoryMock.GetByID(sessionPanelId)).Return(sessionPanel);
            //MockUnitOfWorkSave();
            //theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.SetOrderOfReview(sessionPanelId, collection, userId);
            //
            // Test the assertions
            //
            //theMock.VerifyAll();
        }
        #endregion
        #endregion
        #region GetProgramYear Tests
        /// <summary>
        /// Test successful return from GetProgramYear repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetProgramYear")]
        public void GetProgramYearSuccessfulTest()
        {
            //
            // Set up local data
            //
            IProgramYearModel resultModel = new ProgramYearModel { FY = "2013", ProgramAbbreviation = "BCP-1" };
            int currentPanelId = 25;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SessionPanelRepository).Return(theSessionPanelRepositoryMock);
            Expect.Call(theSessionPanelRepositoryMock.GetProgramYear(currentPanelId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.GetProgramYear(currentPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test zero session panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetProgramYear")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetProgramYear detected an invalid parameter: sessionPanelId was 0")]
        public void GetProgramYearZeroSessionPaneldTest()
        {
            //
            // Test
            //
            GetProgramYearFailTest(0);
        }
        /// <summary>
        /// Test negative session panel identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetProgramYear")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetProgramYear detected an invalid parameter: sessionPanelId was -25")]
        public void GetProgramYearNegativeSessionPanelIdTest()
        {
            //
            // Test
            //
            GetProgramYearFailTest(-25);
        }
        #region Critique Tests
        /// <summary>
        /// Test successful return from GetApplicationCritiqueDetails repository call.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetApplicationCritiqueDetails")]
        public void GetApplicationCritiqueDetailsSuccessfulTest()
        {
            //
            // Set up local data
            //
            IApplicationCritiqueDetailsModel resultModel = new ApplicationCritiqueDetailsModel { MeetingDescription = "Panel BCRP June Meeting", ProgramYear = "2014" };
            int applicationWorkflowStepId = 25;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.PanelManagementRepository).Return(thePanelManagementRepositoryMock);
            Expect.Call(thePanelManagementRepositoryMock.GetApplicationCritiqueDetails(applicationWorkflowStepId)).Return(resultModel);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.GetApplicationCritiqueDetails(applicationWorkflowStepId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test zero application workflow step identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetApplicationCritiqueDetails")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetApplicationCritiqueDetails detected an invalid parameter: applicationWorkflowStepId was 0")]
        public void GetApplicationCritiqueDetailsWorkflowStepldTest()
        {
            //
            // Test
            //
            GetApplicationCritiqueDetailsFailTest(0);
        }

        /// <summary>
        /// Test negative application workflow step identifier
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.GetApplicationCritiqueDetails")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.GetApplicationCritiqueDetails detected an invalid parameter: applicationWorkflowStepId was -25")]
        public void GetApplicationCritiqueDetailsNegativeWorkflowStepIdTest()
        {
            //
            // Test
            //
            GetApplicationCritiqueDetailsFailTest(-25);
        }


        #endregion
        #region Helpers
        /// <summary>
        /// Test steps for a failure test for GetProgramYear
        /// </summary>
        /// <param name="sessionPanelId">the sessionPanelId to test</param>
        private void GetProgramYearFailTest(int sessionPanelId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.GetProgramYear(sessionPanelId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
        }
        /// <summary>
        /// Test steps for a failure test for GetApplicationCritiqueDetails
        /// </summary>
        /// <param name="applicationWorkflowStepId">the applicationWorkflowId to test</param>
        private void GetApplicationCritiqueDetailsFailTest(int applicationWorkflowStepId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = this.theTestPanelManagementService.GetApplicationCritiqueDetails(applicationWorkflowStepId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
        }

        #endregion
        #endregion
    }
}