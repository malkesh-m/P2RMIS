using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
		
namespace BLLTest.Reports
{

    /// <summary>
    /// Unit tests ReportViewerService 
    /// </summary>
    [TestClass()]
    public class ReportViewerServiceTest: BLLBaseTest
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
        #region Tests

        /// <summary>
        /// Test creating log entry for a completed workflow
        /// </summary>
        [TestMethod()]
        [Category("ReportViewerService")]
        public void CompletedWorkflowTestTest()
        {
            int id = 6;
            int applicationWorkflowStepId = 100;
            bool completedIs = true;

            ApplicationWorkflow applicationworkflowEntity = theMock.PartialMock<ApplicationWorkflow>();
            ApplicationWorkflowStep applicationWorkflowStepEntity = new ApplicationWorkflowStep { ApplicationWorkflowStepId = applicationWorkflowStepId};
            MockUnitOfWorkApplicationWorkflowRepository();
            MockUnitOfWorkApplicationSummaryLogRepository();
            Expect.Call(theWorkflowRepositoryMock.GetByID(id)).Return(applicationworkflowEntity);
            Expect.Call(delegate { theApplicationSummaryLogRepositoryMock.Add(null); }).IgnoreArguments();
            Expect.Call(applicationworkflowEntity.IsComplete()).Return(completedIs);
            Expect.Call(applicationworkflowEntity.LastStep()).Return(applicationWorkflowStepEntity);
            MockUnitOfWorkSave();

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestReportViewerService.LogReportInfo(_goodUserId, id);
            //
            // Test the assertions
            //
            IList<object[]> argsPerCall = theApplicationSummaryLogRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));
            ApplicationSummaryLog applicationSummaryLogEntityCreated = argsPerCall[0][0] as ApplicationSummaryLog;
            Assert.AreEqual(applicationWorkflowStepId, applicationSummaryLogEntityCreated.ApplicationWorkflowStepId, "ApplicationWorkflowStepId was not as expected");
            Assert.AreEqual(_goodUserId, applicationSummaryLogEntityCreated.UserId, "UserId was not as expected");
            Assert.AreEqual(completedIs, applicationSummaryLogEntityCreated.CompletedFlag, "CompletedFlag was not as expected");
            Assert.AreEqual(_goodUserId, applicationSummaryLogEntityCreated.CreatedBy, "CreatedBy was not as expected");
            Assert.IsNotNull(applicationSummaryLogEntityCreated.CreatedDate, "CreatedDate was not as expected");
            Assert.AreEqual(_goodUserId, applicationSummaryLogEntityCreated.ModifiedBy, "ModifiedBy was not as expected");
            Assert.IsNotNull(applicationSummaryLogEntityCreated.ModifiedDate, "ModifiedDate was not as expected");

            theWorkMock.AssertWasCalled(x => x.Save());
        }
        /// <summary>
        /// Test creating log entry for a incomplete workflow
        /// </summary>
        [TestMethod()]
        [Category("ReportViewerService")]
        public void InCompletWorkflowTest()
        {
            int id = 6;
            int applicationWorkflowStepId = 22;
            bool completedIs = false;

            ApplicationWorkflow applicationworkflowEntity = theMock.PartialMock<ApplicationWorkflow>();
            ApplicationWorkflowStep applicationWorkflowStepEntity = new ApplicationWorkflowStep { ApplicationWorkflowStepId = applicationWorkflowStepId };
            MockUnitOfWorkApplicationWorkflowRepository();
            MockUnitOfWorkApplicationSummaryLogRepository();
            Expect.Call(theWorkflowRepositoryMock.GetByID(id)).Return(applicationworkflowEntity);
            Expect.Call(delegate { theApplicationSummaryLogRepositoryMock.Add(null); }).IgnoreArguments();
            Expect.Call(applicationworkflowEntity.IsComplete()).Return(completedIs);
            Expect.Call(applicationworkflowEntity.CurrentStep()).Return(applicationWorkflowStepEntity);
            MockUnitOfWorkSave();

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestReportViewerService.LogReportInfo(_goodUserId, id);
            //
            // Test the assertions
            //
            IList<object[]> argsPerCall = theApplicationSummaryLogRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));
            ApplicationSummaryLog applicationSummaryLogEntityCreated = argsPerCall[0][0] as ApplicationSummaryLog;
            Assert.AreEqual(applicationWorkflowStepId, applicationSummaryLogEntityCreated.ApplicationWorkflowStepId, "ApplicationWorkflowStepId was not as expected");
            Assert.AreEqual(_goodUserId, applicationSummaryLogEntityCreated.UserId, "UserId was not as expected");
            Assert.AreEqual(completedIs, applicationSummaryLogEntityCreated.CompletedFlag, "CompletedFlag was not as expected");
            Assert.AreEqual(_goodUserId, applicationSummaryLogEntityCreated.CreatedBy, "CreatedBy was not as expected");
            Assert.IsNotNull(applicationSummaryLogEntityCreated.CreatedDate, "CreatedDate was not as expected");
            Assert.AreEqual(_goodUserId, applicationSummaryLogEntityCreated.ModifiedBy, "ModifiedBy was not as expected");
            Assert.IsNotNull(applicationSummaryLogEntityCreated.ModifiedDate, "ModifiedDate was not as expected");

            theWorkMock.AssertWasCalled(x => x.Save());
        }		
	    #endregion
    }		
}
