using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;


namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit tests for CheckoutActivity
    /// </summary>
    [TestClass()]
    public class CheckoutActivityTest: BLLBaseTest
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
            //
            // Create the mocks
            //
            theMock = new MockRepository();
            theWorkflowStepWorkLogRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
            theApplicationWorkflowStepAssignmentRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepAssignmentRepository>(); 
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theApplicationWorkflowStepAssignmentRepositoryMock = null;
            theSummaryManagementRepositoryMock = null;
            theWorkMock = null;
            theWorkflowStepWorkLogRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepWorkLogRepository>();
            theMock = null;
        }
        //
        #endregion
        #endregion
        #region Activity Tests
        /// <summary>
        /// Test successful check out when GetStepAssignment() returns null.
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")] 
        public void CheckoutActivity_SuccessfulAddTest()
        {
            //
            // Set up the local objects
            //
            CheckoutActivity testActivity = new CheckoutActivity();
            ApplicationWorkflowSummaryStatement ss = new ApplicationWorkflowSummaryStatement
            {
                ApplicationWorkflowSummaryStatementId = 20,
                DocumentFile = Encoding.ASCII.GetBytes("the")
            };
            ApplicationWorkflow workflow = new ApplicationWorkflow {ApplicationWorkflowId = 21, ApplicationWorkflowSummaryStatements = new [] {ss}.ToList()};
            ApplicationWorkflowStep step = new ApplicationWorkflowStep {ApplicationWorkflowStepId = 22, ApplicationWorkflow = workflow};
            //
            // Setup the expectations
            Expect.Call(theWorkMock.ApplicationWorkflowStepWorkLogRepository).Return(theWorkflowStepWorkLogRepositoryMock);
            Expect.Call(delegate { theWorkflowStepWorkLogRepositoryMock.Add(null); }).IgnoreArguments();
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.IsSsCheckedOut(step.ApplicationWorkflowId)).Return(false);
            Expect.Call(theWorkMock.ApplicationWorkflowStepAssignmentRepository).Return(theApplicationWorkflowStepAssignmentRepositoryMock);
            Expect.Call(theApplicationWorkflowStepAssignmentRepositoryMock.GetStepAssignment(step.ApplicationWorkflowStepId)).Return(null);
            Expect.Call(theWorkMock.ApplicationWorkflowStepAssignmentRepository).Return(theApplicationWorkflowStepAssignmentRepositoryMock);
            Expect.Call(delegate { theApplicationWorkflowStepAssignmentRepositoryMock.Add(null); }).IgnoreArguments();

            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = _goodUserId;

            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            bool wasCheckedOut = (bool)workflowResults[CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString()];
            //
            // Verify that it worked
            //
            theWorkflowStepWorkLogRepositoryMock.AssertWasCalled(x => x.Add(Arg<ApplicationWorkflowStepWorkLog>.Is.Anything));
            theApplicationWorkflowStepAssignmentRepositoryMock.AssertWasCalled(x => x.Add(Arg<ApplicationWorkflowStepAssignment>.Is.Anything));

            theSummaryManagementRepositoryMock.AssertWasCalled(x => x.IsSsCheckedOut(step.ApplicationWorkflowId));
            theApplicationWorkflowStepAssignmentRepositoryMock.AssertWasCalled(x => x.GetStepAssignment(step.ApplicationWorkflowStepId));
            theWorkMock.AssertWasCalled(x => { var ignored = x.ApplicationWorkflowStepAssignmentRepository; });
            theWorkMock.AssertWasCalled(x => { var ignored = x.ApplicationWorkflowStepWorkLogRepository; });
            theWorkMock.AssertWasCalled(x => { var ignored = x.SummaryManagementRepository; });

            Assert.AreEqual(WorkflowState.Started, outState, "Expected state not returned");
            IList<object[]> argsPerCall = theApplicationWorkflowStepAssignmentRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Add(null));
            //
            // now check that the user was set
            //
            ApplicationWorkflowStepAssignment argumentPassed = argsPerCall[0][0] as ApplicationWorkflowStepAssignment;
            Assert.AreEqual(_goodUserId, argumentPassed.UserId, "UserId was not as expected");
            Assert.IsFalse(wasCheckedOut, "Checked out indication not set");
        }
        /// <summary>
        /// Test successful check out hen GetStepAssignment() returns an assignment.
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        public void CheckoutActivity_SuccessfulUpdateTest()
        {
            //
            // Set up the local objects
            //
            CheckoutActivity testActivity = new CheckoutActivity();
            ApplicationWorkflowSummaryStatement ss = new ApplicationWorkflowSummaryStatement
            {
                ApplicationWorkflowSummaryStatementId = 20,
                DocumentFile = Encoding.ASCII.GetBytes("the")
            };
            ApplicationWorkflow workflow = new ApplicationWorkflow { ApplicationWorkflowId = 21, ApplicationWorkflowSummaryStatements = new[] { ss }.ToList() };
            ApplicationWorkflowStep step = new ApplicationWorkflowStep { ApplicationWorkflowStepId = 22, ApplicationWorkflow = workflow };
            ApplicationWorkflowStepAssignment assignment = new ApplicationWorkflowStepAssignment();
            //
            // Setup the expectations
            Expect.Call(theWorkMock.ApplicationWorkflowStepWorkLogRepository).Return(theWorkflowStepWorkLogRepositoryMock);
            Expect.Call(delegate { theWorkflowStepWorkLogRepositoryMock.Add(null); }).IgnoreArguments();
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.IsSsCheckedOut(step.ApplicationWorkflowId)).Return(false);
            Expect.Call(theWorkMock.ApplicationWorkflowStepAssignmentRepository).Return(theApplicationWorkflowStepAssignmentRepositoryMock);
            Expect.Call(theApplicationWorkflowStepAssignmentRepositoryMock.GetStepAssignment(step.ApplicationWorkflowStepId)).Return(assignment);
            Expect.Call(theWorkMock.ApplicationWorkflowStepAssignmentRepository).Return(theApplicationWorkflowStepAssignmentRepositoryMock);
            Expect.Call(delegate { theApplicationWorkflowStepAssignmentRepositoryMock.Update(assignment); });

            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = _goodUserId;

            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            bool wasCheckedOut = (bool)workflowResults[CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString()];
            //
            // Verify that it worked
            //
            theWorkflowStepWorkLogRepositoryMock.AssertWasCalled(x => x.Add(Arg<ApplicationWorkflowStepWorkLog>.Is.Anything));
            theApplicationWorkflowStepAssignmentRepositoryMock.AssertWasCalled(x => x.Update(Arg<ApplicationWorkflowStepAssignment>.Is.Anything));
            theSummaryManagementRepositoryMock.AssertWasCalled(x => x.IsSsCheckedOut(step.ApplicationWorkflowId));
            theApplicationWorkflowStepAssignmentRepositoryMock.AssertWasCalled(x => x.GetStepAssignment(step.ApplicationWorkflowStepId));
            theWorkMock.AssertWasCalled(x => { var ignored = x.ApplicationWorkflowStepAssignmentRepository; });
            theWorkMock.AssertWasCalled(x => { var ignored = x.ApplicationWorkflowStepWorkLogRepository; });
            theWorkMock.AssertWasCalled(x => { var ignored = x.SummaryManagementRepository; });
            Assert.AreEqual(WorkflowState.Started, outState, "Expected state not returned");

            IList<object[]> argsPerCall = theApplicationWorkflowStepAssignmentRepositoryMock.GetArgumentsForCallsMadeOn(x => x.Update(assignment));
            //
            // now check that the user was set
            //
            ApplicationWorkflowStepAssignment argumentPassed = argsPerCall[0][0] as ApplicationWorkflowStepAssignment;
            Assert.AreEqual(_goodUserId, argumentPassed.UserId, "UserId was not as expected");
            Assert.IsFalse(wasCheckedOut, "Checked out indication not set");
        }
        /// <summary>
        /// Test check out when summary statement is already checked out.
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        public void CheckoutActivity_AlreadyCheckedOutTest()
        {
            //
            // Set up the local objects
            //
            CheckoutActivity testActivity = new CheckoutActivity();
            ApplicationWorkflowStep step = new ApplicationWorkflowStep();
            step.ApplicationWorkflowStepId = 22;
            //
            // Setup the expectations
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.IsSsCheckedOut(step.ApplicationWorkflowId)).Return(true);
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = _goodUserId;

            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            bool wasCheckedOut = (bool)workflowResults[CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString()];
            //
            // Verify that it worked
            //
            theSummaryManagementRepositoryMock.AssertWasCalled(x => x.IsSsCheckedOut(step.ApplicationWorkflowId));
            Assert.AreEqual(WorkflowState.Default, outState, "Expected state not returned");
            Assert.IsTrue(wasCheckedOut, "Checked out indication not set");
        }

        /// <summary>
        /// Test no ApplicationWorkflowStep
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckoutActivity detected invalid arguments: ApplicationWorkflowStep is null [True] userId [1111] UnitOfWork is null [False]")]
        public void CheckoutActivity_NoApplicationWorkflowStepTest()
        {
            CheckoutActivityFailTest(theWorkMock, _goodUserId, null);
        }
        /// <summary>
        /// Test a negative user id
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckoutActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [-4] UnitOfWork is null [False]")]
        public void CheckoutActivity_NegativeUserIdTest()
        {
            CheckoutActivityFailTest(theWorkMock, _badUserId, new ApplicationWorkflowStep());
        }
        /// <summary>
        /// Test a zero user id
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckoutActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [0] UnitOfWork is null [False]")]
        public void CheckoutActivity_ZeroUserIdTest()
        {
            CheckoutActivityFailTest(theWorkMock, _UserIdZero, new ApplicationWorkflowStep());
        }
        /// <summary>
        /// Test - no unit of work
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "CheckoutActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [1111] UnitOfWork is null [True]")]
        public void CheckoutActivity_NullUnitOfWorkTest()
        {
            CheckoutActivityFailTest(null, _goodUserId, new ApplicationWorkflowStep());
        }

        #endregion
        #region GetResults Tests
        /// <summary>
        /// Test - getting the results back to the caller
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        public void GetResultSuccessTest()
        {
            IDictionary<string, object> results = new Dictionary<string, object>();;
            IDictionary resultsList = new Hashtable();

            results.Add(CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString(), true);
            CheckoutActivity testActivity = new CheckoutActivity();

            IDictionary d =  testActivity.GetResults(results, resultsList);

            Assert.AreEqual(1, d.Count, "Incorrect number of results returned");
            Assert.IsTrue((bool)d[CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString()], "Result value was not as expected");
        }
        /// <summary>
        /// Test - invalid parameter.  
        /// Note:
        /// The activity supplies the results so not test is supplied.
        /// </summary>
        [TestMethod()]
        [Category("CheckoutActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignUserActivity.GetResults() detected invalid arguments: list is null? [True]")]
        public void GetResultFailTest()
        {
            IDictionary<string, object> results = new Dictionary<string, object>(); ;
            CheckoutActivity testActivity = new CheckoutActivity();

            IDictionary d = testActivity.GetResults(results, null);
        }
        #endregion
        #region Helpers
        private void CheckoutActivityFailTest(IUnitOfWork workMock, int userId, ApplicationWorkflowStep step)
        {
            //
            // Set up the local objects
            //
            CheckoutActivity testActivity = new CheckoutActivity();
            //
            // Create the mocks
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            testActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (workMock));
            testActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => step);
            testActivity.UserId = userId;
            var workflowResults = WorkflowInvoker.Invoke(testActivity);
            WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
            //
            // Verify that it worked
            //
            theWorkflowStepWorkLogRepositoryMock.AssertWasCalled(x => x.Add(Arg<ApplicationWorkflowStepWorkLog>.Is.Anything));
            Assert.AreEqual(WorkflowState.Started, outState, "Expected state not returned");
        }       
        #endregion
    }
}
