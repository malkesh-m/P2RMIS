using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace BLLTest.SummaryStatements
{
    /// <summary>
    /// Unit test for SummaryProcessingtServer class.
    /// </summary>
    [TestClass()]
    public partial class SummaryProcessingServiceTest
    {
        #region Attributes
        private int _userId = 11;
        private int _zeroUserId = 0;
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
            theMock = new MockRepository();
            theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
            testService = theMock.PartialMock<SummaryManagementServiceTestService>(theWorkMock);
            thetestSummaryProcessingService = theMock.PartialMock<SummaryProcessingServiceTestService>(theWorkMock);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            thetestSummaryProcessingService = null;
            testService = null;
            theSummaryManagementRepositoryMock = null;
            theWorkMock = null;
            theMock = null;
        }
        //
        #endregion
        #endregion
        #region The GetAssignedSummaries Tests
        /// <summary>
        /// Test - results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetAssignedSummaries")]
        public void GetAssignedSummaries_GoodGetTest()
        {
            //
            // Instantiate objects for test
            //
            ResultModel<ISummaryAssignedModel> resultModel = new ResultModel<ISummaryAssignedModel>();
            List<ISummaryAssignedModel> list = new List<ISummaryAssignedModel>();
            list.Add(new SummaryAssignedModel { ApplicationWorkflowId = 1 });
            list.Add(new SummaryAssignedModel { ApplicationWorkflowId = 12 });
            list.Add(new SummaryAssignedModel { ApplicationWorkflowId = 13 });
            list.Add(new SummaryAssignedModel { ApplicationWorkflowId = 14 });
            resultModel.ModelList = list;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryManagementRepository repositoryMock = mocks.DynamicMock<ISummaryManagementRepository>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            //
            // Expected calls and setting up the returns from calls
            //
            SetupResult.For(unitOfWorkMock.SummaryManagementRepository).Return(repositoryMock);
            Expect.Call(repositoryMock.GetAssignedSummaries(this._userId)).Return(resultModel);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ISummaryAssignedModel> container = serviceMock.GetAssignedSummaries(this._userId);
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
        [Category("SummaryManagementServer.GetAssignedSummaries")]
        public void GetAssignedSummaries_GoodGetZeroTest()
        {
            //
            // Instantiate objects for test
            //
            ResultModel<ISummaryAssignedModel> resultModel = new ResultModel<ISummaryAssignedModel>();
            List<ISummaryAssignedModel> list = new List<ISummaryAssignedModel>();
            resultModel.ModelList = list;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryManagementRepository repositoryMock = mocks.DynamicMock<ISummaryManagementRepository>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            //
            // Expected calls and setting up the returns from calls
            //
            SetupResult.For(unitOfWorkMock.SummaryManagementRepository).Return(repositoryMock);
            Expect.Call(repositoryMock.GetAssignedSummaries(this._userId)).Return(resultModel);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ISummaryAssignedModel> container = serviceMock.GetAssignedSummaries(this._userId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test negative user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetAssignedSummaries")]
        public void GetAssignedSummaries_NegativeUserIdTest()
        {
            //
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ISummaryAssignedModel> container = serviceMock.GetAssignedSummaries(_badUserId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test zero user id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetAssignedSummaries")]
        public void GetAssignedSummaries_ZeroIdTest()
        {
            //
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<ISummaryAssignedModel> container = serviceMock.GetAssignedSummaries(this._zeroUserId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        #endregion
        #region The GetWorkflowProgress Tests
        /// <summary>
        /// Test - results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowProgress")]
        public void GetWorkflowProgress_GoodGetTest()
        {
            //
            // Instantiate objects for test
            //
            ResultModel<IWorkflowProgress> resultModel = new ResultModel<IWorkflowProgress>();
            List<IWorkflowProgress> list = new List<IWorkflowProgress>();
            list.Add(new WorkflowProgress { ApplicationWorkflowId = 1 });
            list.Add(new WorkflowProgress { ApplicationWorkflowId = 12 });
            list.Add(new WorkflowProgress { ApplicationWorkflowId = 13 });
            list.Add(new WorkflowProgress { ApplicationWorkflowId = 14 });
            resultModel.ModelList = list;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryManagementRepository repositoryMock = mocks.DynamicMock<ISummaryManagementRepository>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            //
            // Expected calls and setting up the returns from calls
            //
            SetupResult.For(unitOfWorkMock.SummaryManagementRepository).Return(repositoryMock);
            Expect.Call(repositoryMock.GetWorkflowProgress(this._userId)).Return(resultModel);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<IWorkflowProgress> container = serviceMock.GetWorkflowProgress(this._userId);
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
        [Category("SummaryManagementServer.GetWorkflowProgress")]
        public void GetWorkflowProgress_GoodGetZeroTest()
        {
            //
            // Instantiate objects for test
            //
            ResultModel<IWorkflowProgress> resultModel = new ResultModel<IWorkflowProgress>();
            List<IWorkflowProgress> list = new List<IWorkflowProgress>();
            resultModel.ModelList = list;
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryManagementRepository repositoryMock = mocks.DynamicMock<ISummaryManagementRepository>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            //
            // Expected calls and setting up the returns from calls
            //
            SetupResult.For(unitOfWorkMock.SummaryManagementRepository).Return(repositoryMock);
            Expect.Call(repositoryMock.GetWorkflowProgress(this._userId)).Return(resultModel);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<IWorkflowProgress> container = serviceMock.GetWorkflowProgress(this._userId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test negative user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowProgress")]
        public void GetWorkflowProgress_NegativeUserIdTest()
        {
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<IWorkflowProgress> container = serviceMock.GetWorkflowProgress(_badUserId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        /// <summary>
        /// Test zero user id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowProgress")]
        public void GetWorkflowProgress_ZeroIdTest()
        {
            //
            // Create the mocks
            //
            MockRepository mocks = new MockRepository();
            IUnitOfWork unitOfWorkMock = mocks.DynamicMock<IUnitOfWork>();
            ISummaryProcessingService serviceMock = mocks.PartialMock<TestSummaryProcessingService>(unitOfWorkMock);
            mocks.ReplayAll();
            //
            // Test & verify
            Container<IWorkflowProgress> container = serviceMock.GetWorkflowProgress(this._zeroUserId);
            //
            mocks.VerifyAll();
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
        }
        #endregion
    }
    public class TestSummaryProcessingService : SummaryProcessingService
    {
        public TestSummaryProcessingService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }

}
