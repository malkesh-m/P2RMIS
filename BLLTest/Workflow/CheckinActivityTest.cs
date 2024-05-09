using System;
using System.Activities;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit tests for CheckinActivity
    /// </summary>
    [TestClass()]
    public class CheckinActivityTest
    {
        #region Attributes
        private const int _workflowId = 22;
        private const int _stepOrder = 3;
        private const int _workflowStepId = 333;
        private const int _goodUserId = 10;
        private const int _zeroUserId = 0;
        private const bool _shouldNotRemoveMarkup = false;
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
        #region The Tests
        /// <summary>
        /// Test Checkin Activity
        /// </summary>
        [TestMethod()]
        [Category("CheckinActivity")] 
        public void CheckinActivity_Test()
        {
            //
            // Set up local objects
            //
            CheckinActivity testActivity = new CheckinActivity();
            //
            // Create the mocks & configure as necessary
            //
            MockRepository mocks = new MockRepository();
            ApplicationWorkflowStep mockStep = mocks.PartialMock<ApplicationWorkflowStep>();
            mockStep.ApplicationWorkflowId = _workflowId;
            mockStep.StepOrder = _stepOrder;
            mockStep.ApplicationWorkflowStepId = _workflowStepId;
            IUnitOfWork workMock = mocks.DynamicMock<IUnitOfWork>();
            IApplicationWorkflowStepElementContentRepository elementContentRepositoryMock = mocks.DynamicMock<IApplicationWorkflowStepElementContentRepository>();
            IApplicationWorkflowRepository workflowRepositoryMock = mocks.DynamicMock<IApplicationWorkflowRepository>();
            ApplicationWorkflowStep mockNextStep = mocks.PartialMock<ApplicationWorkflowStep>();
            IApplicationWorkflowStepWorkLogRepository logMock = mocks.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
            ApplicationWorkflowStepWorkLog mockWorkLog = mocks.DynamicMock<ApplicationWorkflowStepWorkLog>();
            //
            // Setup the expectations
            //
            Expect.Call(workMock.ApplicationWorkflowStepElementContentRepository).Return(elementContentRepositoryMock);
            Expect.Call(delegate { elementContentRepositoryMock.Add(null); }).IgnoreArguments();
            Expect.Call(workMock.ApplicationWorkflowRepository).Return(workflowRepositoryMock);
            Expect.Call(workflowRepositoryMock.GetNextStep(_workflowId, _stepOrder)).Return(mockNextStep);
            Expect.Call(workMock.ApplicationWorkflowStepWorkLogRepository).Return(logMock);
            Expect.Call(logMock.FindInCompleteWorkLogEntryByWorkflowStep(_workflowStepId)).Return(mockWorkLog);
            Expect.Call(delegate { mockWorkLog.Complete(_goodUserId); });
            Expect.Call(mockNextStep.ShouldRemoveMarkup()).Return(_shouldNotRemoveMarkup);
            mocks.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (workMock));
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => mockStep);
            testActivity.UserId = _goodUserId;
            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify that it worked
            //
            Assert.AreEqual(WorkflowState.Complete, outState, "Expected state not returned");
            mocks.VerifyAll();
        }
        /// <summary>
        /// Test a null unit of work
        /// </summary>
        [TestMethod()]
        [Category("CheckinActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckinActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [10] UnitOfWork is null [True]")]
        public void CheckinActivity_NullUnitOfWorkTest()
        {
            //
            // Set up the local objects
            //
            CheckinActivity testActivity = new CheckinActivity();
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            step.ApplicationWorkflowStepId = 22;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IApplicationWorkflowStepWorkLogRepository logMock = mocks.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
            IUnitOfWork workMock = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = _goodUserId;
            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            //
            // Verify
            //
            Assert.Fail("CheckinActivity_NullUnitOfWorkTest should have thrown an exception");
        }
        /// <summary>
        /// Test a null workflow step
        /// </summary>
        [TestMethod()]
        [Category("CheckinActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckinActivity detected invalid arguments: ApplicationWorkflowStep is null [True] userId [10] UnitOfWork is null [False]")]
        public void CheckinActivity_NullWorkflowStepTest()
        {
            //
            // Set up the local objects
            //
            CheckinActivity testActivity = new CheckinActivity();
            //ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            //step.ApplicationWorkflowStepId = 22;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork workMock = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            //testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = _goodUserId;
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (workMock));
            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            //
            // Verify
            //
            Assert.Fail("CheckinActivity_NullUnitOfWorkTest should have thrown an exception");
        }
        /// <summary>
        /// Test a null workflow step
        /// </summary>
        [TestMethod()]
        [Category("CheckinActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckinActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [0] UnitOfWork is null [False]")]
        public void CheckinActivity_ZeroUserIdTest()
        {
            //
            // Set up the local objects
            //
            CheckinActivity testActivity = new CheckinActivity();
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            step.ApplicationWorkflowStepId = 22;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork workMock = mocks.DynamicMock<IUnitOfWork>();
            mocks.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = _zeroUserId;
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (workMock));
            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            //
            // Verify
            //
            Assert.Fail("CheckinActivity_ZeroUserIdTTest should have thrown an exception");
        }
        #endregion
    }
}
