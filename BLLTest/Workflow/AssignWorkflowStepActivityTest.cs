using System;
using System.Activities;
using System.Collections;
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
    /// Unit tests for SaveActivity
    /// </summary>
    [TestClass()]
    public class AssignWorkflowStepActivityTest: BLLBaseTest
    {
        #region Attributes
        private AssignWorkflowStepActivity theTestActivity;
        private IDictionary theValues;
        private int _targetStepId = 44;
        private int _stepId1 = 163;
        private int _worklogId = 34561;
        private int _templateId1 = 890876;
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
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //
            // This is the activity we are using
            //
            theTestActivity = new AssignWorkflowStepActivity();
            theValues = new Hashtable(4);
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
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theTestActivity = null;
            theValues = null;

            theMock = null;
            theWorkMock = null;
            thePartialStepMock = null;
            theWorkflowRepositoryMock = null;
            thePartialWorkflowMock = null;
            theWorkflowStepWorkLogRepositoryMock = null;
            theStepElementContentHistoryRepositoryMock = null;
        }
        //
        #endregion
        #endregion
        #region SetParameters Tests
        /// <summary>
        /// Test activity parameters - all parameters good
        /// </summary>
        [TestMethod()]
        [Category("AssignWorkflowStepActivity")]
        public void AssignActivity_Parameters1Test()
        {
            //
            // Set up the local objects
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
            //
            // run the tests
            //
            theTestActivity.SetParameters(theValues);
            //
            // Verify expectations
            //
            Assert.AreEqual(_targetStepId.ToString(), theTestActivity.TargetWorkflowId.Expression.ToString(), "AssignWorkflowStepActivity parameter targetStepId was not initialized as expected");
        }
        /// <summary>
        /// Test activity parameters - string for target id
        /// </summary>
        [TestMethod()]
        [Category("AssignWorkflowStepActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetParameters detected invalid arguments: list is null [False] list entries count [1]")]
        public void AssignActivity_Parameters2Test()
        {
            //
            // Set up the local objects
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = "this is bad";
            //
            // run the tests
            //
            theTestActivity.SetParameters(theValues);
            //
            // Verify expectations
            //
            Assert.Fail("Exception should have been thrown");
        }
        #endregion
        #region Execute Tests
        /// <summary>
        /// Test good rollback with a single content
        /// </summary>
        //[TestMethod()]
        //[Category("AssignWorkflowStepActivity.Execute")]
        public void Execute_SuccessfulTest()
        {	
	        //
	        // Set up local data
	        //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;

            ApplicationWorkflowStep targetStepMock = theMock.PartialMock<ApplicationWorkflowStep>();
             
            ApplicationWorkflowStepWorkLog worklogEntry = new ApplicationWorkflowStepWorkLog();
            worklogEntry.ApplicationWorkflowStepWorkLogId = _worklogId;

            System.Collections.Generic.List<ApplicationWorkflowStepElementContent> contentToCopy = new System.Collections.Generic.List<ApplicationWorkflowStepElementContent>();
            ApplicationWorkflowStepElementContent content1 = new ApplicationWorkflowStepElementContent();
            content1.ApplicationWorkflowStepElement = new ApplicationWorkflowStepElement();
            content1.ApplicationWorkflowStepElement.ApplicationTemplateElementId = _templateId1;

            contentToCopy.Add(content1);

            System.Collections.Generic.ICollection<ApplicationWorkflowStepElementContentHistory> contentHistory = new System.Collections.Generic.List<ApplicationWorkflowStepElementContentHistory>();
            //
            // Create any unique mocks & configure
            //
            thePartialStepMock.ApplicationWorkflowId = _stepId1;
            thePartialStepMock.ApplicationWorkflowStepId = __workflowStepId1;
            //
	        // Set up the expectations
	        //
            Expect.Call(thePartialStepMock.GetContentList(_goodUserId)).Return(contentToCopy);
            Expect.Call(thePartialStepMock.ApplicationWorkflow).Repeat.Times(2).Return(thePartialWorkflowMock);
            Expect.Call(thePartialStepMock.Promote(targetStepMock, _goodUserId)).Return(contentToCopy);
            Expect.Call(thePartialWorkflowMock.GetThisStep(_targetStepId)).Return(targetStepMock);
            Expect.Call(theWorkMock.ApplicationWorkflowStepWorkLogRepository).Return(theWorkflowStepWorkLogRepositoryMock);
            Expect.Call(theWorkflowStepWorkLogRepositoryMock.FindInCompleteWorkLogEntryByWorkflowStep(__workflowStepId1)).Return(worklogEntry);

            Expect.Call(thePartialStepMock.CreateHistory(_goodUserId, _worklogId)).Return(contentHistory);
            Expect.Call(theWorkMock.ApplicationWorkflowStepElementContentHistoryRepository).Return(theStepElementContentHistoryRepositoryMock);
            Expect.Call(delegate { theStepElementContentHistoryRepositoryMock.AddRange(contentHistory); });
            Expect.Call(delegate { thePartialWorkflowMock.ResetResolved(thePartialStepMock, targetStepMock); });
            Expect.Call(delegate { thePartialWorkflowMock.ReOpen(_goodUserId); });
            Expect.Call(delegate { targetStepMock.CopyContentFromOtherWorkflowStep(thePartialStepMock, _goodUserId); });
            //
            theMock.ReplayAll();
            //
            // Configure the activity and test the workflow
            //
            theTestActivity.UnitOfWork = new InArgument<IUnitOfWork>(ctx => (theWorkMock));
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => thePartialStepMock);
            theTestActivity.UserId = _goodUserId;
            theTestActivity.SetParameters(theValues);
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
        [Category("AssignWorkflowStepActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [True] userId [1111] UnitOfWork is null [False] TargetStepId [44]")]
        public void Execute_NullStepTest()
        {
            //
            // Set up local data
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
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
            theTestActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => null);
            theTestActivity.UserId = _goodUserId;
            theTestActivity.SetParameters(theValues);
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
        /// Test not setting step parameter
        /// </summary>
        [TestMethod()]
        [Category("AssignWorkflowStepActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [True] userId [1111] UnitOfWork is null [False] TargetStepId [44]")]
        public void Execute_BadStepTest()
        {
            //
            // Set up local data
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
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
            theTestActivity.UserId = _goodUserId;
            theTestActivity.SetParameters(theValues);
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
        [Category("AssignWorkflowStepActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [-4] UnitOfWork is null [False] TargetStepId [44]")]
        public void Execute_BadUserIdTest()
        {
            //
            // Set up local data
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
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
            theTestActivity.SetParameters(theValues);
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
        [Category("AssignWorkflowStepActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [0] UnitOfWork is null [False] TargetStepId [44]")]
        public void Execute_ZeroUserIdTest()
        {
            //
            // Set up local data
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
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
            theTestActivity.SetParameters(theValues);
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
        [Category("AssignWorkflowStepActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [1111] UnitOfWork is null [True] TargetStepId [44]")]
        public void Execute_NullUnitOfWorkTest()
        {
            //
            // Set up local data
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
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
            theTestActivity.SetParameters(theValues);
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
        [Category("AssignWorkflowStepActivity.Execute")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [False] userId [1111] UnitOfWork is null [True] TargetStepId [44]")]
        public void Execute_NotSetUnitOfWorkTest()
        {
            //
            // Set up local data
            //
            theValues[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = _targetStepId;
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
            theTestActivity.SetParameters(theValues);
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
