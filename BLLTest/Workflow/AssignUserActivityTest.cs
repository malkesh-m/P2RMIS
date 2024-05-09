using System;
using System.Activities;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit tests for AssignUserActivity
    /// </summary>
    [TestClass()]
    public class AssignUserActivityTest : BLLBaseTest
    {
        #region Attributes
        private AssignUserActivity theTestActivity;
        private int _stepId1 = 163;
        private int _worklogId = 34561;
        private int __workflowStepId1 = 44326;
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
            //
            // This is the activity we are using
            //
            theTestActivity = new AssignUserActivity();
            //
            // Initialize the necessary mocks
            //
            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            thePartialStepMock = theMock.PartialMock<ApplicationWorkflowStep>();
            theWorkflowRepositoryMock = theMock.DynamicMock<IApplicationWorkflowRepository>();
            thePartialWorkflowMock = theMock.PartialMock<ApplicationWorkflow>();
            theWorkflowStepWorkLogRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
            theStepElementContentHistoryRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepElementContentHistoryRepository>();
            thePartialStepWorkLogMock = theMock.PartialMock<ApplicationWorkflowStepWorkLog>();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theTestActivity = null;

            theMock = null;
            theWorkMock = null;
            thePartialStepMock = null;
            theWorkflowRepositoryMock = null;
            thePartialWorkflowMock = null;
            theWorkflowStepWorkLogRepositoryMock = null;
            theStepElementContentHistoryRepositoryMock = null;
            thePartialStepWorkLogMock = null;
        }
        //
        #endregion
        #endregion
        #region Execute Tests
        /// <summary>
        /// Test good rollback with a single content
        /// </summary>
        //[TestMethod()]
        //[Category("AssignUserActivity.Execute")]
        public void Execute_SuccessfulTest()
        {
            ApplicationWorkflowStepWorkLog worklogEntry = new ApplicationWorkflowStepWorkLog();
            worklogEntry.ApplicationWorkflowStepWorkLogId = _worklogId;

            System.Collections.Generic.ICollection<ApplicationWorkflowStepElementContentHistory> contentHistory = new System.Collections.Generic.List<ApplicationWorkflowStepElementContentHistory>();
            //
            // Create any unique mocks & configure
            //
            thePartialStepMock.ApplicationWorkflowId = _stepId1;
            thePartialStepMock.ApplicationWorkflowStepId = __workflowStepId1;
            //
            // Set up the expectations
            //
            Expect.Call(theWorkMock.ApplicationWorkflowStepWorkLogRepository).Return(theWorkflowStepWorkLogRepositoryMock);
            Expect.Call(theWorkflowStepWorkLogRepositoryMock.FindInCompleteWorkLogEntryByWorkflowStep(__workflowStepId1)).Return(worklogEntry); //yes

            Expect.Call(thePartialStepMock.CreateHistory(_goodUserId, _worklogId)).Return(contentHistory);
            Expect.Call(theWorkMock.ApplicationWorkflowStepElementContentHistoryRepository).Return(theStepElementContentHistoryRepositoryMock);
            Expect.Call(delegate { theStepElementContentHistoryRepositoryMock.AddRange(contentHistory); });
            Expect.Call(delegate { thePartialStepWorkLogMock.Complete(_goodUserId); });
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => thePartialStepMock);
            theTestActivity.UserId = _goodUserId;
            //
            // Run the test
            //
            var workflowResults = WorkflowInvoker.Invoke(theTestActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify
            //
            theMock.VerifyAll();
            Assert.AreEqual(WorkflowState.Default, outState, "Expected state not returned");
        }
        /// <summary>
        /// Test null step parameter
        /// </summary>
        [TestMethod()]
        [Category("AssignUserActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignUserActivity.Execute() detected invalid arguments: ApplicationWorkflowStep is null [True] userId [1111] UnitOfWork is null? [False]")]
        public void Execute_NullStepTest()
        {
            // Create any unique mocks & configure
            //
            //
            // Set up the expectations
            //
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => null);
            theTestActivity.UserId = _goodUserId;
            //
            // Run the test
            //
            var workflowResults = WorkflowInvoker.Invoke(theTestActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify
            //
            Assert.Fail("Test case should have thrown an exception");
        }
        /// <summary>
        /// Test bad user id
        /// </summary>
        [TestMethod()]
        [Category("AssignUserActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignUserActivity.Execute() detected invalid arguments: ApplicationWorkflowStep is null [False] userId [-4] UnitOfWork is null? [False]")]
        public void Execute_BadUserIdTest()
        {
            //
            // Create any unique mocks & configure
            //
            //
            // Set up the expectations
            //
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => thePartialStepMock);
            theTestActivity.UserId = _badUserId;
            //
            // Run the test
            //
            var workflowResults = WorkflowInvoker.Invoke(theTestActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify
            //
            Assert.Fail("Test case should have thrown an exception");
        }
        /// <summary>
        /// Test zero user id
        /// </summary>
        [TestMethod()]
        [Category("AssignUserActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignUserActivity.Execute() detected invalid arguments: ApplicationWorkflowStep is null [False] userId [0] UnitOfWork is null? [False]")]
        public void Execute_ZeroUserIdTest()
        {
            //
            // Create any unique mocks & configure
            //
            //
            // Set up the expectations
            //
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => thePartialStepMock);
            theTestActivity.UserId = 0;
            //
            // Run the test
            //
            var workflowResults = WorkflowInvoker.Invoke(theTestActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify
            //
            Assert.Fail("Test case should have thrown an exception");
        }
        /// <summary>
        /// Test null unit of work
        /// </summary>
        [TestMethod()]
        [Category("AssignUserActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignUserActivity.Execute() detected invalid arguments: ApplicationWorkflowStep is null [False] userId [1111] UnitOfWork is null? [True]")]
        public void Execute_NullUnitOfWorkTest()
        {
            //
            // Create any unique mocks & configure
            //
            //
            // Set up the expectations
            //
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (null));
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => (thePartialStepMock));
            theTestActivity.UserId = _goodUserId;
            //
            // Run the test
            //
            var workflowResults = WorkflowInvoker.Invoke(theTestActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify
            //
            Assert.Fail("Test case should have thrown an exception");
        }
        /// <summary>
        /// Test bad unit of work
        /// </summary>
        [TestMethod()]
        [Category("AssignUserActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignUserActivity.Execute() detected invalid arguments: ApplicationWorkflowStep is null [False] userId [1111] UnitOfWork is null? [True]")]
        public void Execute_NotSetUnitOfWorkTest()
        {
            //
            // Create any unique mocks & configure
            //
            //
            // Set up the expectations
            //
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => (thePartialStepMock));
            theTestActivity.UserId = _goodUserId;
            //
            // Run the test
            //
            var workflowResults = WorkflowInvoker.Invoke(theTestActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify
            //
            Assert.Fail("Test case should have thrown an exception");
        }
        #endregion
    }
}
