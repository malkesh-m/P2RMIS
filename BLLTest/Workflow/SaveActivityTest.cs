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
    public class SaveActivityTest: BLLBaseTest
    {
        #region Attributes
        private const string _content1 = "this is a long long string here ";
        private const int _contentId1 = 123456789;
        private const int _elementId = 65432;

        private int _stepId = 456;

        private SaveActivity theTestActivity;
        private IDictionary theValues;
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
            theTestActivity = new SaveActivity();
            theValues = new Hashtable(SaveActivity.ActivityArgumentCount);
            //
            // Initialize the necessary mocks
            //
            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            thePartialStepMock = theMock.PartialMock<ApplicationWorkflowStep>();

            theContentElementRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepElementContentRepository>();
            thePartialStepElementContentMock = theMock.PartialMock<ApplicationWorkflowStepElementContent>();
            thePartialStepElementMock = theMock.PartialMock<ApplicationWorkflowStepElement>();       
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

            theContentElementRepositoryMock = null;
            thePartialStepElementContentMock = null;
            thePartialStepElementMock = null;
        }
        //
        #endregion
        #endregion
        #region SetParameters Tests
        /// <summary>
        /// Test activity parameters - all parameters good
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_Parameters1Test()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // run the tests
            //
            theTestActivity.SetParameters(theValues);
            //
            // Verify expectations
            //
            Assert.AreEqual(_content1, theTestActivity.Content.Expression.ToString(), "Activity parameter Content was not initialized as expected");
            Assert.AreEqual(_contentId1.ToString(), theTestActivity.ContentId.Expression.ToString(), "Activity parameter ContentId was not initialized as expected");
            Assert.AreEqual(_elementId.ToString(), theTestActivity.ElementId.Expression.ToString(), "Activity parameter Element was not initialized as expected");
        }
        /// <summary>
        /// Test activity parameters - null content
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_Parameters2Test()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = null;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // run the tests
            //
            theTestActivity.SetParameters(theValues);
            //
            // Verify expectations
            //
            Assert.AreEqual("null", theTestActivity.Content.Expression.ToString(), "Activity parameter Content was not initialized as expected");
            Assert.AreEqual(_contentId1.ToString(), theTestActivity.ContentId.Expression.ToString(), "Activity parameter ContentId was not initialized as expected");
            Assert.AreEqual(_elementId.ToString(), theTestActivity.ElementId.Expression.ToString(), "Activity parameter Element was not initialized as expected");
        }
        /// <summary>
        /// Test activity parameters - string for contentId
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetParameters detected invalid arguments: list is null [False] list entries count [3]")]
        public void SaveActivity_Parameters3Test()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = "this is bad";
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // run the tests
            //
            theTestActivity.SetParameters(theValues);
        }
         /// <summary>
        /// Test activity parameters - string for contentId
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_Parameters4Test()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = 6;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // run the tests
            //
            theTestActivity.SetParameters(theValues);
            //
            // Verify expectations
            //
            Assert.AreEqual("null", theTestActivity.Content.Expression.ToString(), "Activity parameter Content was not initialized as expected");
            Assert.AreEqual(_contentId1.ToString(), theTestActivity.ContentId.Expression.ToString(), "Activity parameter ContentId was not initialized as expected");
        }
        #endregion
        #region SaveActivity Tests
        /// <summary>
        /// Test a good run
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")] 
        public void SaveActivity_SuccessfulTest()
        {
            //
            // Set up the local objects & configure
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _goodUserId;
            //
            // Setup the expectations
            //
            Expect.Call(theWorkMock.ApplicationWorkflowStepElementContentRepository).Return(theContentElementRepositoryMock);
            Expect.Call(theContentElementRepositoryMock.GetByID(_contentId1)).Return(thePartialStepElementContentMock);
            Expect.Call(delegate { thePartialStepElementContentMock.SaveModifiedContent(_content1, _goodUserId); } );
            Expect.Call(thePartialStepMock.IsCheckedOutByUser(_goodUserId)).Return(true);
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
            // Verify that it worked
            //
            Assert.AreEqual(WorkflowState.Default, outState, "Expected state not returned");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - no step
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SaveActivity detected invalid arguments: step is null [True]; userId [1111]; unit of work is null [False];  content is null [False]; contentId [123456789]")]
        public void SaveActivity_NoStepTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock = null;
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - bad content id
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SaveActivity detected invalid arguments: step is null [False]; userId [1111]; unit of work is null [False];  content is null [False]; contentId [-6]")]
        public void SaveActivity_BadContentIdTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = -6;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _goodUserId;
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - bad user id
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SaveActivity detected invalid arguments: step is null [False]; userId [0]; unit of work is null [False];  content is null [False]; contentId [123456789]")]
        public void SaveActivity_ZeroUserIdTest()
        {
            //
            // Set up the local objects
            //
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - bad user id
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SaveActivity detected invalid arguments: step is null [False]; userId [-4]; unit of work is null [False];  content is null [False]; contentId [123456789]")]
        public void SaveActivity_NegativeUserIdTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - bad user id
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SaveActivity detected invalid arguments: step is null [False]; userId [1111]; unit of work is null [True];  content is null [False]; contentId [123456789]")]
        public void SaveActivity_BadUnitOFWorkTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            theWorkMock = null;
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - null content for a content entry that exists
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_NullContentTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = null;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
            //
            // Set up expectations
            //
            Expect.Call(thePartialStepMock.IsCheckedOutByUser(_goodUserId)).Return(true);
            Expect.Call(theWorkMock.ApplicationWorkflowStepElementContentRepository).Return(theContentElementRepositoryMock);
            Expect.Call(delegate { theContentElementRepositoryMock.Delete(_contentId1); });
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test not checked out
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Save was not accomplished.  User [1111] did not have workflow step [step name here] Checked Out.")]
        public void SaveActivity_NotCheckedOutTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepMock.ApplicationWorkflowStepId = _goodUserId;
            thePartialStepMock.StepName = "step name here";
            //
            // Setup the expectations
            //
            Expect.Call(thePartialStepMock.IsCheckedOutByUser(_goodUserId)).Return(false);
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - empty content for a content entry that exists
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_EmptyContentTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = "     ";
            theValues[SaveActivity.SaveParameters.ContentId] = _contentId1;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
            //
            // Set up expectations
            //
            Expect.Call(thePartialStepMock.IsCheckedOutByUser(_goodUserId)).Return(true);
            Expect.Call(theWorkMock.ApplicationWorkflowStepElementContentRepository).Return(theContentElementRepositoryMock);
            Expect.Call(delegate { theContentElementRepositoryMock.Delete(_contentId1); });
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - no content for a content entry that does not exists
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_NoContentNoNewContentTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = "     ";
            theValues[SaveActivity.SaveParameters.ContentId] = 0;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
            //
            // Set up expectations
            //
            Expect.Call(thePartialStepMock.IsCheckedOutByUser(_goodUserId)).Return(true);
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - new content for a content entry that does not exists
        /// </summary>
        [TestMethod()]
        [Category("SaveActivity")]
        public void SaveActivity_NoContentAndNewContentTest()
        {
            //
            // Set up the local objects
            //
            theValues[SaveActivity.SaveParameters.Content] = _content1;
            theValues[SaveActivity.SaveParameters.ContentId] = 0;
            theValues[SaveActivity.SaveParameters.ElementId] = _elementId;
            //
            // Create any unique mocks & configure
            //
            thePartialStepElementContentMock.ApplicationWorkflowStepElementContentId = _contentId1;
            thePartialStepMock.ApplicationWorkflowStepId = _stepId;
            //
            // Set up expectations
            //
            Expect.Call(thePartialStepMock.IsCheckedOutByUser(_goodUserId)).Return(true);
            Expect.Call(thePartialStepMock.GetApplicationWorkflowStepElementById(_elementId)).Return(thePartialStepElementMock);
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
            // Verify that it worked
            //
            theMock.VerifyAll();
        }
        #endregion
    }
}
