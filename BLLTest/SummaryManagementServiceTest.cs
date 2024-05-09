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


namespace BLLTest
{
    /// <summary>
    /// Unit test for SummaryManagementServer class.
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest
    {
        private const int Program1 = 47;
        private const int ProgramBlank = 0;
        private const int FY1 = 224;
        private const int FYBlank = 0;
        private const int Cycle1 = 2;
        private int? CycleNull = null;
        private const int PanelId1 = 2;
        private const int PanelIdNoResults = 5;
        private const int NegativePanelId = -1;
        private const int ZeroPanelId = 0;
        private int? PanelIdNull = null;
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
            testService = theMock.PartialMock<SummaryManagementServiceTestService>(theWorkMock);
            theSummaryManagementRepositoryMock = theMock.DynamicMock<ISummaryManagementRepository>();
            theApplicationRepositoryMock = theMock.DynamicMock<IApplicationRepository>();
            theApplicationReviewStatusRepositoryMock = theMock.DynamicMock<IApplicationReviewStatusRepository>();

            InitCommandDraft();
            InitCommadDraftForLogNumbers();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theApplicationReviewStatusRepositoryMock = null;
            theApplicationRepositoryMock = null;
            theSummaryManagementRepositoryMock = null;
            testService = null;
            theWorkMock = null;
            theMock = new MockRepository();

            CleanUpCommandDraft();
            CleanUpCommandDraftForLogNumbers();
        }
        //
        #endregion
        #endregion
        #region GetAllPanelSummaries Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_GoodTest()
        {
           GetAllPanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test for empty result from data layer
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_NoResultTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();
            SummaryGroup resultModel = new SummaryGroup();
            result.ModelList = theList;
            //
            // Set the expectations
            //
            Expect.Call(theSummaryManagementRepositoryMock.GetAllPanelSummaries(Program1, FY1, Cycle1, PanelId1, _anAwardId)).Return(result);
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            theMock.ReplayAll();
            //
            // Run the method under test
            //
            Container<ISummaryGroup> container = testService.GetAllPanelSummaries(Program1, FY1, Cycle1, PanelId1, _anAwardId);
            //
            // the assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");

            theMock.VerifyAll();
        }
        /// <summary>
        /// Test a null cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_NullCycleTest()
        {
            GetAllPanelSummariesSuccessTest(Program1, FY1, null, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a negative cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_NegativeCycleTest()
        {
            GetAllPanelSummariesFailTest(Program1, FY1, -834444566, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a null panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_NullPanelIdTest()
        {
            GetAllPanelSummariesSuccessTest(Program1, FY1, Cycle1, null, _anAwardId);
        }
        /// <summary>
        /// Test a negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_NegativePanelIdTest()
        {
            GetAllPanelSummariesFailTest(Program1, FY1, Cycle1, -567890036, _anAwardId);
        }

        /// <summary>
        /// Test a null award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_NullAwardIdTest()
        {
            GetAllPanelSummariesSuccessTest(Program1, FY1, Cycle1, _panelId, null);
        }
        /// <summary>
        /// Test a blank award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_EmptyAwardIdTest()
        {
            GetAllPanelSummariesFailTest(Program1, FY1, Cycle1, _panelId, 0);
        }

        /// <summary>
        /// Test for blank program
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_BlankProgramTest()
        {
            GetAllPanelSummariesFailTest(ProgramBlank, FY1, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test for blank (white space) fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetAllPanelSummaries_BlankFyTest()
        {
            GetAllPanelSummariesFailTest(Program1, FYBlank, Cycle1, PanelId1, _anAwardId);
        }
        #region GetAllPanelSummaries Helpers
        private void GetAllPanelSummariesSuccessTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId)
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();
            SummaryGroup resultModel = new SummaryGroup();
            theList.Add(resultModel);
            result.ModelList = theList;
            //
            // Set the expectations
            //
            Expect.Call(theSummaryManagementRepositoryMock.GetAllPanelSummaries(program, fiscalYear, cycleId, panelId, awardId)).Return(result);
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            theMock.ReplayAll();
            //
            // Run the method under test
            //
            Container<ISummaryGroup> container = testService.GetAllPanelSummaries(program, fiscalYear, cycleId, panelId, awardId);
            //
            // the assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(1, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");

            theMock.VerifyAll();
        }
        private void GetAllPanelSummariesFailTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId)
        {
            theMock.ReplayAll();
            //
            // The method under test
            //
            Container<ISummaryGroup> container = testService.GetAllPanelSummaries(program, fiscalYear, cycleId, panelId, awardId);
            //
            // This is how we know it works
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            theMock.VerifyAll();
        }
        #endregion
        #endregion
        #region GetSummaryApplications Tests
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_NegativePanelId_1Test()
        {
            GetSummaryApplicationsFailTest(NegativePanelId, Cycle1, 2122);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_NegativePanelId_2Test()
        {
            GetSummaryApplicationsFailTest(NegativePanelId, Cycle1, null);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_NegativePanelId_3Test()
        {
            GetSummaryApplicationsFailTest(NegativePanelId, Cycle1, 0);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_ZeroPanelId_1Test()
        {
            GetSummaryApplicationsFailTest(0, Cycle1, 2122);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_ZeroPanelId_2Test()
        {
            GetSummaryApplicationsFailTest(0, Cycle1, null);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_ZeroPanelId_3Test()
        {
            GetSummaryApplicationsFailTest(0, Cycle1, 0);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_GoodPanelId__NullTest()
        {
            //
            // Set up local data
            //
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            List<IAvailableApplications> theList = new List<IAvailableApplications>();
            IAvailableApplications resultModel = new AvailableApplications();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetSummaryApplicationsSuccessTest(result, PanelId1, Cycle1, null);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_GoodPanelId__EmptyTest()
        {
            //
            // Set up local data
            //
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            List<IAvailableApplications> theList = new List<IAvailableApplications>();
            IAvailableApplications resultModel = new AvailableApplications();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetSummaryApplicationsSuccessTest(result, PanelId1, Cycle1, 0);
        } 
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_GoodPanelId__AValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            List<IAvailableApplications> theList = new List<IAvailableApplications>();
            IAvailableApplications resultModel = new AvailableApplications();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetSummaryApplicationsSuccessTest(result, PanelId1, Cycle1, 353);
        }
        /// <summary>
        /// Test for good panel id but no results from the DL
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_NoResults__NullTest()
        {
            //
            // Set up local data
            //
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            List<IAvailableApplications> theList = new List<IAvailableApplications>();
            IAvailableApplications resultModel = new AvailableApplications();
            result.ModelList = theList;

            GetSummaryApplicationsSuccessTest(result, PanelId1, Cycle1, null);
        }
        /// <summary>
        /// Test for good panel id but no results from the DL
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_NoResults__EmptyTest()
        {
            //
            // Set up local data
            //
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            List<IAvailableApplications> theList = new List<IAvailableApplications>();
            IAvailableApplications resultModel = new AvailableApplications();
            result.ModelList = theList;

            GetSummaryApplicationsSuccessTest(result, PanelId1, Cycle1, 0);
        }
        /// <summary>
        /// Test for good panel id but no results from the DL
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetSummaryApplications_NoResults__AValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            List<IAvailableApplications> theList = new List<IAvailableApplications>();
            IAvailableApplications resultModel = new AvailableApplications();
            result.ModelList = theList;

            GetSummaryApplicationsSuccessTest(result, PanelId1, Cycle1, 353);
        }
        #region GetSummaryApplications Helpers
        private void GetSummaryApplicationsSuccessTest(ResultModel<IAvailableApplications> resultToReturn, int panelId, int cycle, int? awardTypeId)
        {
            //
            // Set expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetSummaryApplications(panelId, cycle, awardTypeId)).Return(resultToReturn);
            theMock.ReplayAll();
            //
            // Execute the test
            //
            Container<IAvailableApplications> container = testService.GetSummaryApplications(panelId, cycle, awardTypeId);
            //
            // Check the assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(resultToReturn.ModelList.Count<IAvailableApplications>(), container.ModelList.Count<IAvailableApplications>(), "Container list not correct count");
            theMock.VerifyAll();
        }
        private void GetSummaryApplicationsFailTest(int panelId, int cycle, int? awardTypeId)
        {
            theMock.ReplayAll();
            //
            // execute the test
            //
            Container<IAvailableApplications> container = testService.GetSummaryApplications(panelId, cycle, awardTypeId);
            //
            // test the assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<IAvailableApplications>(), "Container list not correct count");
            theMock.VerifyAll();
        } 
        #endregion
        #endregion
        #region GetPanelSummaries Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_GoodTest()
        {
            GetPanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelId1, _anAwardId, _goodUserId);
        }
        /// <summary>
        /// Test a good find but no results returned
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_ZeroReturnTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();
            SummaryGroup resultModel = new SummaryGroup();
            result.ModelList = theList;
            //
            // Set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetPanelSummaries(Program1, FY1, Cycle1, PanelId1, _anAwardId, _goodUserId)).Return(result);
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetPanelSummaries(Program1, FY1, Cycle1, PanelId1, _anAwardId, _goodUserId);
            //
            // Test the expectations
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");

            theMock.VerifyAll();
        }
        /// <summary>
        /// Test a null cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NullCycleTest()
        {
            GetPanelSummariesSuccessTest(Program1, FY1, null, PanelId1, _anAwardId, _goodUserId);
        }
        /// <summary>
        /// Test a negative cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NegativeCycleTest()
        {
            GetPanelSummariesFailTest(Program1, FY1, -8, PanelId1, _anAwardId, _goodUserId);
        }
        /// <summary>
        /// Test a null panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NullPanelIdTest()
        {
            GetPanelSummariesSuccessTest(Program1, FY1, Cycle1, null, _anAwardId, _goodUserId);
        }
        /// <summary>
        /// Test a negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NegativePanelIdTest()
        {
            GetPanelSummariesFailTest(Program1, FY1, Cycle1, -9876, _anAwardId, _goodUserId);
        }
        /// <summary>
        /// Test for null program
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_BlankProgramTest()
        {
            GetPanelSummariesFailTest(ProgramBlank, FY1, Cycle1, PanelId1, _anAwardId, _goodUserId);
        }
        /// <summary>
        /// Test for blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_BlankFyTest()
        {
            GetPanelSummariesFailTest(Program1, FYBlank, Cycle1, PanelId1, _anAwardId, _goodUserId);
        }

        /// <summary>
        /// Test for null award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NullAwardIdTest()
        {
            GetPanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelId1, null, _goodUserId);
        }
        /// <summary>
        /// Test for empty award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_EmptyAwardIdTest()
        {
            GetPanelSummariesFailTest(Program1, FY1, Cycle1, PanelId1, 352, _goodUserId);
        }
        /// <summary>
        /// Test for null user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NullUserIdTest()
        {
            GetPanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelId1, _anAwardId, null);
        }
        /// <summary>
        /// Test for empty user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplication")]
        public void GetPanelSummaries_NegativeUserIdTest()
        {
            GetPanelSummariesFailTest(Program1, FY1, Cycle1, PanelId1, _anAwardId, -12345);
        }
        #region GetPanelSummaries Helpers
        private void GetPanelSummariesSuccessTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, int? userId)
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();

            SummaryGroup resultModel = new SummaryGroup();
            theList.Add(resultModel);

            result.ModelList = theList;
            //
            // Set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetPanelSummaries(program, fiscalYear, cycleId, panelId, awardId, userId)).Return(result);
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetPanelSummaries(program, fiscalYear, cycleId, panelId, awardId, userId);
            //
            // Test the expectations
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(1, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");

            theMock.VerifyAll();
        }
        private void GetPanelSummariesFailTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, int? userId)
        {
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetPanelSummaries(ProgramBlank, FYBlank, Cycle1, PanelId1, _anAwardId, _goodUserId);
            //
            // Check assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            theMock.VerifyAll();
        } 
        #endregion
        #endregion
        #region Helpers
        public class TestSummaryManagementService : SummaryManagementService
        {
            public TestSummaryManagementService(IUnitOfWork unit)
            {
                UnitOfWork = unit;
            }
        }
        public class TestSummaryProcessingService : SummaryProcessingService
        {
            public TestSummaryProcessingService(IUnitOfWork unit)
            {
                UnitOfWork = unit;
            }
        }

        #endregion
        #region GetAllCompletePanelSummaries Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_GoodTest()
        {
            GetAllCompletePanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_EmptyReturnFromRepositoryTest()
        {
            //
            // Set up the local data
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();

            SummaryGroup resultModel = new SummaryGroup();

            result.ModelList = theList;
            //
            // Set up expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetAllCompletePanelSummaries(Program1, FY1, Cycle1, PanelId1, _anAwardId)).Return(result);
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetAllCompletePanelSummaries(Program1, FY1, Cycle1, PanelId1, _anAwardId);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test a null cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_NullCycleTest()
        {
            GetAllCompletePanelSummariesSuccessTest(Program1, FY1, CycleNull, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a negative cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_NegativeCycleTest()
        {
            GetAllCompletePanelSummariesFailTest(Program1, FY1, -6, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a null panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_NullPanelIdTest()
        {
            GetAllCompletePanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelIdNull, _anAwardId);
        }
        /// <summary>
        /// Test a negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_NegativePanelIdTest()
        {
            GetAllCompletePanelSummariesFailTest(Program1, FY1, Cycle1, -7, _anAwardId);
        }
        /// <summary>
        /// Test for blank program
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_BlankProgramTest()
        {
            GetAllCompletePanelSummariesFailTest(ProgramBlank, FY1, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test for blank fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_BlankFyTest()
        {
            GetAllCompletePanelSummariesFailTest(Program1, FYBlank, Cycle1, PanelId1, _anAwardId);
        }
  
        /// <summary>
        /// Test a null award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_NullAwardIdTest()
        {
            GetAllCompletePanelSummariesSuccessTest(Program1, FY1, Cycle1, PanelId1, null);
        }
        /// <summary>
        /// Test a empty award
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetAllCompletePanelSummaries")]
        public void GetAllCompletePanelSummaries_EmptyAwardIdTest()
        {
            GetAllCompletePanelSummariesFailTest(Program1, FY1, Cycle1, PanelId1, 0);
        }
        #region GetAllCompletePanelSummaries Helpers
        /// <summary>
        /// base bad parameter test
        /// </summary>
        private void GetAllCompletePanelSummariesFailTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId)
        {
            //
            // No local data
            //
            //
            // No expectations
            //
            //
            theMock.ReplayAll();

            var container = testService.GetAllCompletePanelSummaries(program, fiscalYear, cycleId, panelId, awardId);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            theMock.VerifyAll();
        }
        /// <summary>
        /// base bad parameter test
        /// </summary>
        private void GetAllCompletePanelSummariesSuccessTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId)
        {
            //
            // Set up the local data
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();

            SummaryGroup resultModel = new SummaryGroup();
            theList.Add(resultModel);

            result.ModelList = theList;
            //
            // Set up expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetAllCompletePanelSummaries(program, fiscalYear, cycleId, panelId, awardId)).Return(result);
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetAllCompletePanelSummaries(program, fiscalYear, cycleId, panelId, awardId);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(1, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }
        #endregion
        #endregion
        #region GetPanelSummariesPhases Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_GoodTest()
        {
            GetPanelSummariesPhasesSuccessTest(Program1, FY1, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test for null fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_ZeroCycleTest()
        {
            GetPanelSummariesPhasesFailTest(Program1, FY1, 0, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a null cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_NullCycleTest()
        {
            GetPanelSummariesPhasesSuccessTest(Program1, FY1, null, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a negative cycle value
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_NegativeCycleTest()
        {
            GetPanelSummariesPhasesFailTest(Program1, FY1, -3, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test a null panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_NullPanelIdTest()
        {
            GetPanelSummariesPhasesSuccessTest(Program1, FY1, Cycle1, null, _anAwardId);
        }
        /// <summary>
        /// Test a negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_NegativePanelIdTest()
        {
            GetPanelSummariesPhasesFailTest(Program1, FY1, Cycle1, -8, _anAwardId);
        }
        /// <summary>
        /// Test for blank (white space or empty) program
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_BlankProgramTest()
        {
            GetPanelSummariesPhasesFailTest(ProgramBlank, FY1, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test for  blank (white space or empty) fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_BlankFyTest()
        {
            GetPanelSummariesPhasesFailTest(Program1, FYBlank, Cycle1, PanelId1, _anAwardId);
        }
        /// <summary>
        /// Test for null award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_NullAwardIdTest()
        {
            GetPanelSummariesPhasesSuccessTest(Program1, FY1, Cycle1, PanelId1, null);
        }
        /// <summary>
        /// Test for blank (white space or empty) award id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_EmptyAwardIdTest()
        {
            GetPanelSummariesPhasesFailTest(Program1, FY1, Cycle1, PanelId1, 0);
        }
        #region GetPanelSummariesPhases Helpers
        private void GetPanelSummariesPhasesFailTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId)
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository theMock = new MockRepository();
            //
            // Start of with a UnitOfWork mock
            //
            IUnitOfWork theWorkMock = theMock.DynamicMock<IUnitOfWork>();
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            ISummaryManagementService testService = theMock.PartialMock<TestSummaryManagementService>(theWorkMock);
            //
            // Finally turn off recording
            //
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetPanelSummariesPhases(program, fiscalYear, cycleId, panelId, awardId);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test for negative user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetSummaryApplicationPhases")]
        public void GetPanelSummariesPhases_EmptyReturnTest()
        {
            //
            // Now we need to set this to be returned when it is called from the UnitOfWork
            //
            SetupResult.For(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            //
            // The final object that needs to be constructed in the ResultModel,
            // which contains the actual program descriptions.  It only needs one entry because we are
            // not testing the ReportClientProgramListResultModel class, just the fact that we get it
            // back.
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();

            SummaryGroup resultModel = new SummaryGroup();
            result.ModelList = theList;
            //
            // Set the repository to return the ReportClientProgramListResultModel when the DL is called
            //
            Expect.Call(theSummaryManagementRepositoryMock.GetPanelSummariesPhases(Program1, FY1, Cycle1, PanelId1, _anAwardId)).Return(result);
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetPanelSummariesPhases(Program1, FY1, Cycle1, PanelId1, _anAwardId);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }
        private void GetPanelSummariesPhasesSuccessTest(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId)
        {
            //
            // Now we need to set this to be returned when it is called from the UnitOfWork
            //
            SetupResult.For(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            //
            // The final object that needs to be constructed in the ResultModel,
            // which contains the actual program descriptions.  It only needs one entry because we are
            // not testing the ReportClientProgramListResultModel class, just the fact that we get it
            // back.
            //
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            List<ISummaryGroup> theList = new List<ISummaryGroup>();

            SummaryGroup resultModel = new SummaryGroup();
            theList.Add(resultModel);

            result.ModelList = theList;
            //
            // Set the repository to return the ReportClientProgramListResultModel when the DL is called
            //
            Expect.Call(theSummaryManagementRepositoryMock.GetPanelSummariesPhases(program, fiscalYear, cycleId, panelId, awardId)).Return(result);
            theMock.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            Container<ISummaryGroup> container = testService.GetPanelSummariesPhases(program, fiscalYear, cycleId, panelId, awardId);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(1, container.ModelList.Count<ISummaryGroup>(), "Container list not correct count");
            //
            // This verifies that all calls are made that we expect
            //
            theMock.VerifyAll();
        }
        #endregion
        #endregion
        #region GetPhaseCounts Tests
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_NegativePanelId_1Test()
        {
            GetPhaseCountsFailTest(NegativePanelId, _cycle, 3538 , 5);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_NegativePanelId_2Test()
        {
            GetPhaseCountsFailTest(NegativePanelId, _cycle, null, null);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_NegativePanelId_3Test()
        {
            GetPhaseCountsFailTest(NegativePanelId, _cycle, 0, null);
        }
        /// <summary>
        /// Test for negative panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_NegativePanelId_4Test()
        {
            GetPhaseCountsFailTest(NegativePanelId, _cycle, 3538, null);
        }
        /// <summary>
        /// Test for zero panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_ZeroPanelId_1Test()
        {
            GetPhaseCountsFailTest(0, _cycle, 3538, 5);
        }
        /// <summary>
        /// Test for zero panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_ZeroPanelId_2Test()
        {
            GetPhaseCountsFailTest(0, _cycle, null, null);
        }
        /// <summary>
        /// Test for zero panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_ZeroPanelId_3Test()
        {
            GetPhaseCountsFailTest(0, _cycle, 0, null);
        }
        /// <summary>
        /// Test for zero panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_ZeroPanelId_4Test()
        {
            GetPhaseCountsFailTest(0, _cycle, 3538, null);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelId_1Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            IPhaseCountModel resultModel = new PhaseCountModel();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, 521, 5);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelId_2Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            IPhaseCountModel resultModel = new PhaseCountModel();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, null, 5);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelId_3Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            IPhaseCountModel resultModel = new PhaseCountModel();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, 0, 5);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelId_4Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            IPhaseCountModel resultModel = new PhaseCountModel();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, null, null);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelId_5Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            IPhaseCountModel resultModel = new PhaseCountModel();
            theList.Add(resultModel);
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, 0, null);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelIdNoResults_1Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, 521, 5);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelIdNoResults_2Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, null, 5);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelIdNoResults_3Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, 0, 5);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelIdNoResults_4Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, null, null);
        }
        /// <summary>
        /// Test for good panel id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetPhaseCounts")]
        public void GetPhaseCounts_GoodPanelIdNoResults_5Test()
        {
            //
            // Set up local data
            //
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            List<IPhaseCountModel> theList = new List<IPhaseCountModel>();
            result.ModelList = theList;

            GetPhaseCountsSuccessTest(result, 2, _cycle, 0, null);
        }
        #region GetPhaseCounts Helpers
        private void GetPhaseCountsSuccessTest(ResultModel<IPhaseCountModel> resultToReturn, int panelId, int cycle, int? awardTypeId, int? userId)
        {
            //
            // Set the Expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetPhaseCounts(panelId, cycle, awardTypeId, userId)).Return(resultToReturn);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IPhaseCountModel> container = testService.GetPhaseCounts(PanelId1, cycle, awardTypeId, userId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(resultToReturn.ModelList.Count(), container.ModelList.Count<IPhaseCountModel>(), "Container list not correct count");
            theMock.VerifyAll();
        }
        private void GetPhaseCountsFailTest(int panelId, int cycle, int? awardTypeId, int? userId)
        {
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IPhaseCountModel> container = testService.GetPhaseCounts(NegativePanelId, cycle, 0, null);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Container is null and it should not be");
            Assert.IsNotNull(container.ModelList, "Container model list not instantiated");
            Assert.AreEqual(0, container.ModelList.Count<IPhaseCountModel>(), "Container list not correct count");
            theMock.VerifyAll();
        }
        #endregion
        #endregion

    }
}
