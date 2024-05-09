using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;


namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit test for WorkflowService class.
    /// </summary>
    [TestClass()]
    public partial class WorkflowServiceTest: BLLBaseTest
    {
        WorkflowTemplateModel _templateModel;
        DateTime _nowDate;
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
            // Make a model
            //
            _templateModel = new WorkflowTemplateModel();

            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theWorkflowRepositoryMock = theMock.DynamicMock<IApplicationWorkflowRepository>();
            theWorkflowStepRepositoryMock = theMock.DynamicMock<IApplicationWorkflowStepRepository>();
            thePartialWorkflowMock = theMock.PartialMock<ApplicationWorkflow>();
            theTestWorkflowService = theMock.PartialMock<WorkflowTestService>(theWorkMock);
            ExecuteInitialize();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _templateModel = null;

            theTestWorkflowService = theMock.PartialMock<WorkflowTestService>(theWorkMock);
            thePartialWorkflowMock = null;
            theWorkflowStepRepositoryMock = null;
            theWorkflowRepositoryMock = null;
            theWorkMock = null;
            theMock = null;
            ExecuteCleanUp();
        }
        //
        #endregion
        #endregion
        #region The Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService")] 
        public void ATest()
        {
            //Assert.AreEqual(1, 2);
        }
        #endregion
        /// <summary>
        /// For some reason the workMock was always null when instantiated in MyTestInitialize().
        /// </summary>
        /// <returns>Tupple containing the objects</returns>
        private Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> Create()
        {
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            //
            // Start of with a UnitOfWork mock
            //
            IUnitOfWork workMock = mocks.DynamicMock<IUnitOfWork>();
            //
            // Create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowTemplateRepository repositoryMock = mocks.DynamicMock<IWorkflowTemplateRepository>();
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> tupple = Tuple.Create(mocks, workMock, repositoryMock);
            return tupple;
        }

        public class TestWorkflowService : WorkflowService
        {
            public TestWorkflowService(IUnitOfWork unit)
            {
                UnitOfWork = unit;
            }
        }
        #region Test Helpers
        /// <summary>
        /// Populate the entity's fields
        /// </summary>
        private void PopulateModel(WorkflowTemplateModel model)
        {
            model.WorkflowId = Constants.Workflow_Id;
            model.ClientId = Constants.Client_Id;
            model.WorkflowName = Constants.Workflow_Name_1;
            model.WorkflowDescription = Constants.Workflow_Description_1;

            model.Steps = new List<WorkflowStepModel>();

            for (int i = 0; i < Constants.Count; i++)
            {
                model.Steps.Add(new WorkflowStepModel { WorkflowStepId = i, StepName = "name", StepOrder = i, ActiveDefault = true });
            }
        }
        #endregion
        public class Constants
        {
            public const string Workflow_Name_1 = "workflow name 1";
            public const string Workflow_Description_1 = "workflow description 1";
            public const int Workflow_Id = 22;
            public const int Client_Id = 19;
            public const int Count = 4;
            public const int User2 = 199;
        }
        #region GetActiveWorkflowStep Tests
        /// <summary>
        /// Test retrieval of the workflow step
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetActiveApplicationWorkflowStep")]
        public void GetActiveApplicationWorkflowStep_GoodTest()
        {
            int valueToReturn = 56798;
            int workflowId = 445566770;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock);
            Expect.Call(theWorkflowRepositoryMock.GetActiveApplicationWorkflowStep(workflowId)).Return(valueToReturn);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            int result = theTestWorkflowService.GetActiveApplicationWorkflowStep(workflowId);
            //
            // Test the assertions
            //
            Assert.AreEqual(valueToReturn, result, "Unexpected workflow step id returned");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test zero application workflow id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetActiveApplicationWorkflowStep")]
        [ExpectedException(typeof(System.ArgumentException), ExpectedMessage = "WorkflowService.GetActiveApplicationWorkflowStep() detected an invalid argument.  applicationWorkflowId parameter is less than or equal to 0")]
        public void GetActiveApplicationWorkflowStep_ZeroTest()
        {
            GetActiveApplicationWorkflowStepFailTest(0);
        }
        /// <summary>
        /// Test a negative application workflow id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetActiveApplicationWorkflowStep")]
        [ExpectedException(typeof(System.ArgumentException), ExpectedMessage = "WorkflowService.GetActiveApplicationWorkflowStep() detected an invalid argument.  applicationWorkflowId parameter is less than or equal to 0")]
        public void GetActiveApplicationWorkflowStep_NegativeTest()
        {
            GetActiveApplicationWorkflowStepFailTest(-5);
        }
        #region Helpers
        private void GetActiveApplicationWorkflowStepFailTest(int workflowId)
        {
            //
            // set the expectations
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            int result = theTestWorkflowService.GetActiveApplicationWorkflowStep(workflowId);
        }
        #endregion
        #endregion

    }
}
