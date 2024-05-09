using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest
{
    /// <summary>
    /// Unit test for SummaryProcessingServer class.
    /// </summary>
    [TestClass()]
    public partial class SummaryProcessingServiceTest: BLLBaseTest
    {
        private const int ApplicationWorkflowId1 = 4;
        private const int ApplicationWorkflowIdNoResults = 995;
        private const int NegativeApplicationWorkflowId = -1;
        private const int ZeroApplicationWorkflowId = 0;
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
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
            thetestSummaryProcessingService = theMock.PartialMock<SummaryProcessingServiceTestService>(theWorkMock);

        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            thetestSummaryProcessingService = null;
            theSummaryManagementRepositoryMock = null;
            theWorkMock = null;
            theMock = null;
        }
        //
        #endregion
        #endregion
        #region GetSingleWorkflowProgress Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetSingleWorkflowProgress")]
        public void GetSingleWorkflowProgress_GoodTest()
        {
            //
            // Instantiate objects for test
            //
            ResultModel<ApplicationWorkflowStepModel> resultModel = new ResultModel<ApplicationWorkflowStepModel>();
            List<ApplicationWorkflowStepModel> list = new List<ApplicationWorkflowStepModel>();
            list.Add(new ApplicationWorkflowStepModel { ApplicationWorkflowStepId = 1 });
            list.Add(new ApplicationWorkflowStepModel { ApplicationWorkflowStepId = 12 });
            list.Add(new ApplicationWorkflowStepModel { ApplicationWorkflowStepId = 13 });
            list.Add(new ApplicationWorkflowStepModel { ApplicationWorkflowStepId = 14 });
            resultModel.ModelList = list;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            IApplicationWorkflowRepository repositoryMock = mocks.DynamicMock<IApplicationWorkflowRepository>();
            IWorkflowService serviceMock = mocks.PartialMock<TestWorkflowService>(unitOfWorkMock);
            //
            // Expected calls and setting up the returns from calls
            //
            SetupResult.For(unitOfWorkMock.ApplicationWorkflowRepository).Return(repositoryMock);
            Expect.Call(repositoryMock.GetSingleWorkflowProgress(ApplicationWorkflowId1)).Return(resultModel);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ApplicationWorkflowStepModel> container = serviceMock.GetSingleWorkflowProgress(ApplicationWorkflowId1);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(4, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test - no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetSingleWorkflowProgress")]
        public void GetSingleWorkflowProgress_GoodGetZeroTest()
        {
            //
            // Instantiate objects for test
            //
            ResultModel<ApplicationWorkflowStepModel> resultModel = new ResultModel<ApplicationWorkflowStepModel>();
            List<ApplicationWorkflowStepModel> list = new List<ApplicationWorkflowStepModel>();
            resultModel.ModelList = list;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            IApplicationWorkflowRepository repositoryMock = mocks.DynamicMock<IApplicationWorkflowRepository>();
            IWorkflowService serviceMock = mocks.PartialMock<TestWorkflowService>(unitOfWorkMock);
            //
            // Expected calls and setting up the returns from calls
            //
            SetupResult.For(unitOfWorkMock.ApplicationWorkflowRepository).Return(repositoryMock);
            Expect.Call(repositoryMock.GetSingleWorkflowProgress(ApplicationWorkflowIdNoResults)).Return(resultModel);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ApplicationWorkflowStepModel> container = serviceMock.GetSingleWorkflowProgress(ApplicationWorkflowIdNoResults);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test negative application workflow id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetSingleWorkflowProgress")]
        public void GetSingleWorkflowProgress_NegativeIdTest()
        {
            //
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            IWorkflowService serviceMock = mocks.PartialMock<TestWorkflowService>(unitOfWorkMock);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ApplicationWorkflowStepModel> container = serviceMock.GetSingleWorkflowProgress(NegativeApplicationWorkflowId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test zero application workflow id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetSingleWorkflowProgress")]
        public void GetSingleWorkflowProgress_ZeroIdTest()
        {
            //
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            IWorkflowService serviceMock = mocks.PartialMock<TestWorkflowService>(unitOfWorkMock);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ApplicationWorkflowStepModel> container = serviceMock.GetSingleWorkflowProgress(ZeroApplicationWorkflowId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        #endregion
        #region GetApplicationDetail Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationDetail")]
        public void GetApplicationDetail_GoodTest()
        {
            GetApplicationDetailSuccessTest(ApplicationWorkflowId1);
        }
        /// <summary>
        /// Test - no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationDetail")]
        public void GetApplicationDetail_GoodGetZeroTest()
        {
            GetApplicationDetailSuccessTest(ApplicationWorkflowIdNoResults);
        }
        /// <summary>
        /// Test negative application workflow id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationDetail")]
        public void GetApplicationDetail_NegativeIdTest()
        {
            GetApplicationDetailFailTest(NegativeApplicationWorkflowId);
        }
        /// <summary>
        /// Test zero application workflow id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationDetail")]
        public void GetApplicationDetail_ZeroIdTest()
        {
            GetApplicationDetailFailTest(ZeroApplicationWorkflowId);
        }
        #region Helpers
        private void GetApplicationDetailSuccessTest(int workflowId)
        {
            //
            // Set up local data
            //
            IApplicationDetailModel resultModel = new ApplicationDetailModel();
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetApplicationDetail(workflowId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            IApplicationDetailModel container = thetestSummaryProcessingService.GetApplicationDetail(workflowId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theMock.VerifyAll();
        }
        private void GetApplicationDetailFailTest(int workflowId)
        {
            //
            // Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            IApplicationDetailModel container = thetestSummaryProcessingService.GetApplicationDetail(workflowId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theWorkMock.AssertWasNotCalled(s => s.SummaryManagementRepository);
        }
        #endregion
        #endregion
        #region GetApplicationStepContent Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationStepContent")]
        public void GetApplicationStepContent_GoodTest()
        {
            GetApplicationStepContentSuccessTest(ApplicationWorkflowId1);
        }
        /// <summary>
        /// Test - no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationStepContent")]
        public void GetApplicationStepContent_GoodGetZeroTest()
        {
            //
            // Set up local data
            //
            ResultModel<IStepContentModel> resultModel = new ResultModel<IStepContentModel>();
            List<IStepContentModel> list = new List<IStepContentModel>();
            resultModel.ModelList = list;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetApplicationStepContent(ApplicationWorkflowIdNoResults)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IStepContentModel> container = thetestSummaryProcessingService.GetApplicationStepContent(ApplicationWorkflowIdNoResults);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test negative application workflow id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationStepContent")]
        public void GetApplicationStepContent_NegativeIdTest()
        {
             GetApplicationStepContentFailTest(NegativeApplicationWorkflowId);
        }
        /// <summary>
        /// Test zero application workflow id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetApplicationStepContent")]
        public void GetApplicationStepContent_ZeroIdTest()
        {
            GetApplicationStepContentFailTest(ZeroApplicationWorkflowId);
        }
        #region Helpers
        private void GetApplicationStepContentSuccessTest(int workflowId)
        {
            //
            // Set up local data
            //
            ResultModel<IStepContentModel> resultModel = new ResultModel<IStepContentModel>();
            List<IStepContentModel> list = new List<IStepContentModel>();
            list.Add(new StepContentModel { ApplicationWorkflowStepContentId = 1 });
            list.Add(new StepContentModel { ApplicationWorkflowStepContentId = 2 });
            resultModel.ModelList = list;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetApplicationStepContent(workflowId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IStepContentModel> container = thetestSummaryProcessingService.GetApplicationStepContent(workflowId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(2, container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private void GetApplicationStepContentFailTest(int workflowId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IStepContentModel> container = thetestSummaryProcessingService.GetApplicationStepContent(workflowId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.SummaryManagementRepository);
        }
        #endregion
        #endregion
        #region GetActiveApplicationWorkflowStepTests
        //TODO: GetActiveApplicationWorkflowStepTests Here
        #endregion
        #region Helpers
        public class TestSummaryProcessingService : SummaryProcessingService
        {
            public TestSummaryProcessingService(IUnitOfWork unit)
            {
                UnitOfWork = unit;
            }
        }
        public class TestWorkflowService : WorkflowService
        {
            public TestWorkflowService(IUnitOfWork unit)
            {
                UnitOfWork = unit;
            }
        }
        #endregion
    }
}
