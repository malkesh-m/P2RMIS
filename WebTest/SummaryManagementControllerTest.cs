using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest
{
    [TestClass]
    public partial class SummaryManagementControllerTest : WebBaseTest
    {
        #region  Constants
        private const int PROGRAM_1 = 47;
        private const string PROGRAM_1_NAME = "xxxxxxxxxxxxxxxxxxxxxxx xxxxxxxxxxxxxxxxxx xxxxxxxxxxxxxxxxxxxx";
        private const string PANEL_1 = "p1";
        private const int CYCLE_1 = 1;
        private const int FISCAL_YEAR_1 = 2010;
        private const string APPID_1 = "id1";
        private const int ID1 = 1234;
        private const int appCount = 43;

        private const string PROGRAM_2 = "YYYYY";
        private const string PROGRAM_2_NAME = "Yet another program name";
        private const string PANEL_2 = "p2";
        private const int CYCLE_2 = 2;
        private const int FISCAL_YEAR_2 = 2010;
        private const string APPID_2 = "id2";
        private const int ID2 = 456;


        private const string PROGRAM_3 = "WXYZ";
        private const string PROGRAM_3_NAME = "THIS IS THE program name";
        private const string PANEL_3 = "p6";
        private const int CYCLE_3 = 3;
        private const int FISCAL_YEAR_3 = 2000;
        private const string APPID_3 = "id3";
        private const int ID3 = 789;

        private List<IAvailableApplications> _la;
        private List<IApplicationsProgress> _lp;

        private SummaryStatementViewModel model;
        private ProgressViewModel modelp;

        private const string WHITE_SPACE = "    ";


        private const string PushButton = SummaryStatementController.CommandDraftConstants.PushSelected;

        private const string ProgressRoute = "Progress";
        private const string AvailableRoute = "Available";

        private string Log_1 = "log 1";
        private string Log_2 = "log 2";
        private string Log_3 = "log 3";
        private string Log_4 = "log 4";
        private string Log_5 = "log 5";
        private string Log_6 = "log 6";

        private const int _goodUserId = 14;

        private const int _goodAppWorkflowId = 12;
        private const int _noResultsAppWorkflowId = 23842;
        private const int _negativeAppWorkflowId = -123;
        private const int _zeroAppWorkflowId = 0;

        private const string action_1 = "Check-In";
        private const string action_2 = "Check-Out";
        private const string phase_1 = "Writing";
        private const string phase_2 = "Editing";
        private static DateTime transDate_1 = new DateTime(2014, 9, 10);
        private static DateTime transDate_2 = new DateTime(2014, 9, 15);
        private const string fName_1 = "Richard";
        private const string fName_2 = "Daniel";
        private const string lName_1 = "Labash";
        private const string lName_2 = "Coffey";
        private const int appId_1 = 9;

        private List<int> userIds = new List<int>(){1,2,3,4,5};
        private ISummaryGroup sg1 = new SummaryGroup() { ProgramAbbreviation = PROGRAM_1.ToString(), Cycle = CYCLE_1, NumberPanelApplications = appCount, PanelAbbreviation = PANEL_1, PanelId = ID1, Year = FISCAL_YEAR_1.ToString() };

        private IWorkflowTransactionModel wtm1 = new WorkflowTransactionModel() { Action = action_1, PhaseName = phase_1, TransactionDate = transDate_1, UserFirstName = fName_1, UserLastName = lName_1 };
        private IWorkflowTransactionModel wtm2 = new WorkflowTransactionModel() { Action = action_2, PhaseName = phase_2, TransactionDate = transDate_2, UserFirstName = fName_2, UserLastName = lName_2 };

        private IApplicationDetailModel adm1 = new ApplicationDetailModel() { ApplicationId =  appId_1 };

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
            _la = new List<IAvailableApplications>();
            model = new SummaryStatementViewModel
            {
                SelectedProgram = PROGRAM_1,
                SelectedFy = FISCAL_YEAR_1
            };

            _lp = new List<IApplicationsProgress>();
            modelp = new ProgressViewModel
            {
                SelectedProgram = PROGRAM_1,
                SelectedFy = FISCAL_YEAR_1
            };

            theMock = new MockRepository();
            summaryManagementServiceMock = theMock.DynamicMock<ISummaryManagementService>();
            theWorkflowServiceMock = theMock.DynamicMock<IWorkflowService>();
            summaryStatementControllerMock = theMock.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, theWorkflowServiceMock, null);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _la = null;
            model = null;

            summaryStatementControllerMock = null;
            theWorkflowServiceMock = null;
            summaryManagementServiceMock = null;
            theMock = null;
        }
        //
        #endregion
        #endregion
        #region ProgressMarkApplications Tests
        [TestMethod]
        public void ProgressMarkApplicationsTest()
        {
            //TODO: correct this test
            ////
            //// Do not really need to put anything into the program container
            ////
            //Container<ProgramModel> containerProgram = new Container<ProgramModel>();
            ////
            //// Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            //// that will be used.
            ////
            //MockRepository mocks = new MockRepository();

            ///// create data to be used in the test
            //NameValueCollection nv = new NameValueCollection();

            ///// data from the form collection
            //nv.Add("appIds", "1");
            //nv.Add("appIds", "2");
            //nv.Add("appIds", "3");

            ///// controller converts name value collection to collection of ints which is passed to the service layer mock
            //List<int> dan = new List<int>();
            //dan.Add(1);
            //dan.Add(2);
            //dan.Add(3);

            ////insert name value collection into the form collection to be passed to the mock controller action
            //FormCollection vp = new FormCollection(nv);
            ////
            //// SummaryService
            ////
            //ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            //SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, null, null);

            //// mock of the service layer call
            //Expect.Call(delegate { summaryManagementServiceMock.SetApplicationState(dan, ReviewStatu.CommandDraft, 0); });
            //Expect.Call(c.GetUserId()).Return(0);
            
            ////
            //// Finally turn off recording
            ////
            //mocks.ReplayAll();

            //var x = c.ProgressMarkApplications(vp, PriorityButton, ProgressRoute);
            //Assert.IsNotNull(x);

            //RedirectToRouteResult v = x as RedirectToRouteResult;

            //// test whether we are being directed to the correct route
            //Assert.AreEqual(ProgressRoute, v.RouteValues.Values.ElementAt(0));

            ////
            //// This verifies that all calls are made that we expect
            ////
            //mocks.VerifyAll();

        }
        
        /// <summary>
        /// Test the ProgressMarkApplications is called correctly
        /// </summary>
        [TestMethod]
        [Category("SummaryManagementController.StartApplication")]
        public void ProgressMarkApplications_StartApplicationTest()
        {
            //TODO: RDL test needs to be corrected
            ///// create data to be used in the test
            //NameValueCollection nv = new NameValueCollection();

            ///// data from the form collection
            //nv.Add("appIds", ID1.ToString());
            //nv.Add("appIds", ID2.ToString());
            //nv.Add("appIds", ID3.ToString());

            //List<string> theAppIds = new List<string>() { ID1.ToString(), ID2.ToString(), ID3.ToString() };

            ////insert name value collection into the form collection to be passed to the mock controller action
            //FormCollection vp = new FormCollection(nv);
            ////
            //// Set up the mocked objects
            ////
            //MockRepository mocks = new MockRepository();
            //ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            //SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, null, null);
            ////
            //// Set up the expectations & returns
            ////
            //Expect.Call(delegate { summaryManagementServiceMock.StartApplications(theAppIds, _goodUserId); }).IgnoreArguments();
            //Expect.Call(c.GetUserId()).Return(_goodUserId);
            ////
            //// Finally turn off recording
            ////
            //mocks.ReplayAll();
            ////
            //// Now execute the test
            ////
            //var result = c.ProgressMarkApplications(vp, PushButton, AvailableRoute);
            ////
            //// We know it work by the following assertions
            //// - got a result that is RedirectToRouteResult
            //// - is redirected to where we wanted it to go
            //// - the server method was called with the correct parameters
            ////
            //RedirectToRouteResult v = result as RedirectToRouteResult;
            //Assert.IsNotNull(result);
            //Assert.AreEqual(AvailableRoute, v.RouteValues.Values.ElementAt(0));
            //summaryManagementServiceMock.AssertWasCalled(q => q.StartApplications(Arg<List<string>>.Matches(a => ((a.Count == 3) && (a[0] == ID1.ToString()) && (a[1] == ID2.ToString()) && (a[2] == ID3.ToString()))), Arg<int>.Is.Equal(_goodUserId)));
        }
        #endregion
        #region AvailableApplications Tests

        [TestMethod]
        [Category("Dan")]
        public void AvailableApplicationsTest()
        {
            // setting up empty program model
            Container<ProgramModel> containerProgram = new Container<ProgramModel>();
            Container<ISummaryGroup> containerSummaryGroup = new Container<ISummaryGroup>();
            containerSummaryGroup.ModelList = new List<ISummaryGroup>() { sg1 };
            SummaryStatementViewModel ssViewModel = new SummaryStatementViewModel();

            // setup selected program and selected fy with some values
            ssViewModel.SelectedProgram = PROGRAM_1;
            ssViewModel.SelectedFy = FISCAL_YEAR_1;

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ICriteriaService criteriaServiceMock = mocks.DynamicMock<ICriteriaService>();
            SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, criteriaServiceMock, null, null);



            // setting up expected returns 
            Expect.Call(summaryManagementServiceMock.GetAllPanelSummaries(PROGRAM_1, FISCAL_YEAR_1, null, null, null)).Return(containerSummaryGroup);

            mocks.ReplayAll();
            var x = c.AvailableApplications(ssViewModel);
            Assert.IsNotNull(x);

            ViewResult v = x as ViewResult;

            Assert.AreEqual(v.ViewName, "Index");
            Assert.IsNotNull(v.Model);
            SummaryStatementViewModel m = v.Model as SummaryStatementViewModel;
            Assert.AreEqual(PROGRAM_1, m.SelectedProgram);
            Assert.AreEqual(FISCAL_YEAR_1, m.SelectedFy);
            Assert.IsNotNull(m.SummaryGroup, "No summary groups");
            Assert.AreEqual(1, m.SummaryGroup.Count());
        }

        /// <summary>
        /// Test AvailableApplications:
        ///   - test grouping for 2 groups; 3 in each group
        /// </summary>
        //[TestMethod]
        //public void AvailableApplications_MultipleEntriesInGroupTest()
        //{
        //    //
        //    // Do not really need to put anything into the program container
        //    //
        //    Container<ProgramModel> containerProgram = new Container<ProgramModel>();
        //    //
        //    // Set up the applications to be grouped
        //    //
        //    Container<IAvailableApplications> containerApplications = new Container<IAvailableApplications>();
        //    SetUpApplicationsForAvailableApplications_MultipleEntriesInGroup();
        //    containerApplications.ModelList = _la;
        //    //
        //    // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
        //    // that will be used.
        //    //
        //    MockRepository mocks = new MockRepository();
        //    //
        //    // CriteriaService
        //    //
        //    ICriteriaService criteriaServiceMock = mocks.DynamicMock<ICriteriaService>();
        //    SetupResult.For(criteriaServiceMock.GetProgramList(null)).IgnoreArguments().Return(containerProgram);
        //    //
        //    // SummaryService
        //    //
        //    ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
        //    SetupResult.For(summaryManagementServiceMock.GetSummaryApplications(PROGRAM_1, FISCAL_YEAR_1, null, null)).Return(containerApplications);
        //    //
        //    // Finally turn off recording
        //    //
        //    mocks.ReplayAll();

        //    SummaryStatementController c = new SummaryStatementController(summaryManagementServiceMock, criteriaServiceMock, null);

        //    var x = c.AvailableApplications(model);
        //    Assert.IsNotNull(x);

        //    ViewResult v = x as ViewResult;

        //    Assert.AreEqual(v.ViewName, "Index");
        //    Assert.IsNotNull(v.Model);
        //    SummaryStatementViewModel m = v.Model as SummaryStatementViewModel;
        //    Assert.AreEqual(PROGRAM_1, m.SelectedProgram);
        //    Assert.AreEqual(FISCAL_YEAR_1, m.SelectedFy);
        //    Assert.IsNotNull(m.Applications, "No dictionary of grouped applications");

            //Assert.AreEqual(2, m.Applications.Keys.Count(), "Incorrect number of keys returned");
            //Assert.IsTrue(m.Applications.Keys.Contains(KEY_1), "Did not find this key " + KEY_1);
            //Assert.AreEqual(3, m.Applications[KEY_1].Count(), "Incorrect number of group entries for the first group");
            //Assert.AreEqual(APPID_1, m.Applications[KEY_1][0].ApplicationId, "Incorrect application in group 1");

            //Assert.IsTrue(m.Applications.Keys.Contains(KEY_2), "Did not find this key " + KEY_2);
            //Assert.AreEqual(3, m.Applications[KEY_2].Count(), "Incorrect number of group entries for the second group");
            //Assert.AreEqual(APPID_2, m.Applications[KEY_2][0].ApplicationId, "Incorrect application in group 2");
            //
            // This verifies that all calls are made that we expect
            //
           // mocks.VerifyAll();
        //}
        /// <summary>
        /// Test AvailableApplications with a program as white space
        /// </summary>
        [TestMethod]
        public void AvailableApplications_WhiteSpaceProgramTest()
        {
            model.SelectedProgram = 0;
            //
            // Do not really need to put anything into the program container
            //
            Container<ProgramModel> containerProgram = new Container<ProgramModel>();
            //
            // Set up the applications to be grouped
            //
            Container<IAvailableApplications> containerApplications = new Container<IAvailableApplications>();
            SetUpApplicationsForAvailableApplications_MultipleEntriesInGroup();
            containerApplications.ModelList = _la;
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            //
            // CriteriaService
            //
            ICriteriaService criteriaServiceMock = mocks.DynamicMock<ICriteriaService>();

            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            SummaryStatementController c = new SummaryStatementController(null, criteriaServiceMock, null, null, null, null, null);

            var x = c.AvailableApplications(model);
            Assert.IsNotNull(x);

            ViewResult v = x as ViewResult;

            Assert.AreEqual(v.ViewName, "Index");
            Assert.IsNotNull(v.Model);
            SummaryStatementViewModel m = v.Model as SummaryStatementViewModel;
            Assert.AreEqual(WHITE_SPACE, m.SelectedProgram);
            Assert.AreEqual(FISCAL_YEAR_1, m.SelectedFy);

            //Assert.AreEqual(0, m.Applications.Keys.Count(), "Incorrect number of keys returned");
            //
            // This verifies that all calls are made that we expect
            //
            mocks.VerifyAll();
        }
       /// <summary>
        /// Test AvailableApplications with a null fiscal year
        /// </summary>
        [TestMethod]
        public void AvailableApplications_WhiteSpaceFiscalYearTest()
        {
            //
            // Set the appropriate parameter
            //
            model.SelectedFy = 0;

            //
            // Do not really need to put anything into the program container
            //
            Container<ProgramModel> containerProgram = new Container<ProgramModel>();
            //
            // Set up the applications to be grouped
            //
            Container<IAvailableApplications> containerApplications = new Container<IAvailableApplications>();
            SetUpApplicationsForAvailableApplications_MultipleEntriesInGroup();
            containerApplications.ModelList = _la;
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            //
            // CriteriaService
            //
            ICriteriaService criteriaServiceMock = mocks.DynamicMock<ICriteriaService>();
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            SummaryStatementController c = new SummaryStatementController(null, criteriaServiceMock, null, null, null, null, null);

            var x = c.AvailableApplications(model);
            Assert.IsNotNull(x);

            ViewResult v = x as ViewResult;

            Assert.AreEqual(v.ViewName, "Index");
            Assert.IsNotNull(v.Model);
            SummaryStatementViewModel m = v.Model as SummaryStatementViewModel;
            Assert.AreEqual(PROGRAM_1, m.SelectedProgram);
            Assert.AreEqual(WHITE_SPACE, m.SelectedFy);

            //Assert.AreEqual(0, m.Applications.Keys.Count(), "Incorrect number of keys returned");
            //
            // This verifies that all calls are made that we expect
            //
            mocks.VerifyAll();
        }
        
        /// <summary>
        /// Test AvailableApplications with a parameters as white space
        /// </summary>
        [TestMethod]
        public void AvailableApplications_WhiteSpaceParamTest()
        {
            //
            // Set the appropriate parameter
            //
            model.SelectedProgram = 0;
            model.SelectedFy =0;

            //
            // Do not really need to put anything into the program container
            //
            Container<ProgramModel> containerProgram = new Container<ProgramModel>();
            //
            // Set up the applications to be grouped
            //
            Container<IAvailableApplications> containerApplications = new Container<IAvailableApplications>();
            SetUpApplicationsForAvailableApplications_MultipleEntriesInGroup();
            containerApplications.ModelList = _la;
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            //
            // CriteriaService
            //
            ICriteriaService criteriaServiceMock = mocks.DynamicMock<ICriteriaService>();
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            SummaryStatementController c = new SummaryStatementController(null, criteriaServiceMock, null, null, null, null, null);

            var x = c.AvailableApplications(model);
            Assert.IsNotNull(x);

            ViewResult v = x as ViewResult;

            Assert.AreEqual(v.ViewName, "Index");
            Assert.IsNotNull(v.Model);
            SummaryStatementViewModel m = v.Model as SummaryStatementViewModel;
            Assert.AreEqual(WHITE_SPACE, m.SelectedProgram);
            Assert.AreEqual(WHITE_SPACE, m.SelectedFy);

            //Assert.AreEqual(0, m.Applications.Keys.Count(), "Incorrect number of keys returned");
            //
            // This verifies that all calls are made that we expect
            //
            mocks.VerifyAll();
        }
        #endregion
        #region AvailableProgressApplications Tests
        /// <summary>
        /// Test AvailableProgressApplications:
        /// </summary>
        [TestMethod]
        public void AvailableProgressApplicationsTest()
        {
            // setting up empty program model
            Container<ProgramModel> containerProgram = new Container<ProgramModel>();
            Container<ISummaryGroup> containerSummaryGroup = new Container<ISummaryGroup>();
            containerSummaryGroup.ModelList = new List<ISummaryGroup>() { sg1 };
            ProgressViewModel progressVM = new ProgressViewModel();

            // setup selected program and selected fy with some values
            progressVM.SelectedProgram = PROGRAM_1;
            progressVM.SelectedFy = FISCAL_YEAR_1;

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ICriteriaService criteriaServiceMock = mocks.DynamicMock<ICriteriaService>();
            SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, criteriaServiceMock, null, null);


            // setting up expected returns 
            Expect.Call(summaryManagementServiceMock.GetPanelSummaries(PROGRAM_1, FISCAL_YEAR_1, null, null, null, null)).Return(containerSummaryGroup);

            mocks.ReplayAll();

            var x = c.AvailableProgressApplications(progressVM);
            Assert.IsNotNull(x);

            ViewResult v = x as ViewResult;

            Assert.AreEqual(v.ViewName, "Progress");
            Assert.IsNotNull(v.Model);
            ProgressViewModel m = v.Model as ProgressViewModel;
            Assert.AreEqual(PROGRAM_1, m.SelectedProgram);
            Assert.AreEqual(FISCAL_YEAR_1, m.SelectedFy);
            Assert.IsNotNull(m.SummaryGroup, "No summary groups");
            Assert.AreEqual(1, m.SummaryGroup.Count());
        }
        #endregion
        #region Index Tests
        /// TBD:
        /// Unit tests for Index() need to be written.
        /// Pausing because the method uses Session which is not
        /// instantiated when the constructor is called.
        /// 
        #endregion
        #region Progress Tests
        /// TBD:
        /// Unit tests for Progress() need to be written.
        /// Pausing because the method uses Session which is not
        /// instantiated when the constructor is called.
        /// 
        #endregion
        #region WorkflowHistory Tests
        /// <summary>
        /// Test - Id that returns results from service
        /// </summary>
        [TestMethod]
        public void WorkflowHistoryTest_GoodId()
        {
            // setting up the expected container models
            Container<IWorkflowTransactionModel> containerWorkflowTransaction = new Container<IWorkflowTransactionModel>();
            containerWorkflowTransaction.ModelList = new List<IWorkflowTransactionModel>() { wtm1, wtm2 };
            IApplicationDetailModel appDetail = new ApplicationDetailModel();
            appDetail = adm1;

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ISummaryProcessingService summaryProcessingServiceMock = mocks.DynamicMock<ISummaryProcessingService>();
            SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, null, summaryProcessingServiceMock);

            // setting up expected returns 
            Expect.Call(summaryManagementServiceMock.GetWorkflowTransactionHistory(_goodAppWorkflowId)).Return(containerWorkflowTransaction);
            Expect.Call(summaryProcessingServiceMock.GetApplicationDetail(_goodAppWorkflowId)).Return(appDetail);

            mocks.ReplayAll();

            var x = c.WorkflowHistory(_goodAppWorkflowId);
            Assert.IsNotNull(x, "ActionResult was null and it should not have been");

            PartialViewResult v = x as PartialViewResult;

            Assert.AreEqual(v.ViewName, "_WorkflowHistory");

            Assert.IsNotNull(v.Model);
            WorkflowHistoryViewModel m = v.Model as WorkflowHistoryViewModel;
            Assert.IsNotNull(m.WorkflowTransactions, "WorkflowTransactions was null and it should not have been");
            Assert.IsNotNull(m.ApplicationDetail, "ApplicationDetail was null and it should not have been");
            Assert.AreEqual(2, m.WorkflowTransactions.Count());
        }
        /// <summary>
        /// Test - Negative Id
        /// </summary>
        [TestMethod]
        public void WorkflowHistoryTest_BadId()
        {
            // setting up the expected container models
            Container<IWorkflowTransactionModel> containerWorkflowTransaction = new Container<IWorkflowTransactionModel>();
            IApplicationDetailModel appDetail = new ApplicationDetailModel();

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ISummaryProcessingService summaryProcessingServiceMock = mocks.DynamicMock<ISummaryProcessingService>();
            SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, null, summaryProcessingServiceMock);

            // setting up expected returns 
            Expect.Call(summaryManagementServiceMock.GetWorkflowTransactionHistory(_negativeAppWorkflowId)).Return(containerWorkflowTransaction);
            Expect.Call(summaryProcessingServiceMock.GetApplicationDetail(_negativeAppWorkflowId)).Return(appDetail);

            mocks.ReplayAll();

            var x = c.WorkflowHistory(_negativeAppWorkflowId);
            Assert.IsNotNull(x, "ActionResult was null and it should not have been");

            PartialViewResult v = x as PartialViewResult;

            Assert.AreEqual(v.ViewName, "_WorkflowHistory");

            Assert.IsNotNull(v.Model);
            WorkflowHistoryViewModel m = v.Model as WorkflowHistoryViewModel;
            Assert.IsNotNull(m.WorkflowTransactions, "WorkflowTransactions was null and it should not have been");
            Assert.IsNotNull(m.ApplicationDetail, "ApplicationDetail was null and it should not have been");
            Assert.AreEqual(0, m.WorkflowTransactions.Count(), "Returned view model workflow transactions list count is not correct");
        }
        /// <summary>
        /// Test - Zero Id
        /// </summary>
        [TestMethod]
        public void WorkflowHistoryTest_ZeroId()
        {
            // setting up the expected container models
            Container<IWorkflowTransactionModel> containerWorkflowTransaction = new Container<IWorkflowTransactionModel>();
            IApplicationDetailModel appDetail = new ApplicationDetailModel();

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ISummaryProcessingService summaryProcessingServiceMock = mocks.DynamicMock<ISummaryProcessingService>();
            SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, null, summaryProcessingServiceMock);

            // setting up expected returns 
            Expect.Call(summaryManagementServiceMock.GetWorkflowTransactionHistory(_zeroAppWorkflowId)).Return(containerWorkflowTransaction);
            Expect.Call(summaryProcessingServiceMock.GetApplicationDetail(_zeroAppWorkflowId)).Return(appDetail);

            mocks.ReplayAll();

            var x = c.WorkflowHistory(_zeroAppWorkflowId);
            Assert.IsNotNull(x, "ActionResult was null and it should not have been");

            PartialViewResult v = x as PartialViewResult;

            Assert.AreEqual(v.ViewName, "_WorkflowHistory");

            Assert.IsNotNull(v.Model);
            WorkflowHistoryViewModel m = v.Model as WorkflowHistoryViewModel;
            Assert.IsNotNull(m.WorkflowTransactions, "WorkflowTransactions was null and it should not have been");
            Assert.IsNotNull(m.ApplicationDetail, "ApplicationDetail was null and it should not have been");
            Assert.AreEqual(0, m.WorkflowTransactions.Count(), "Returned view model workflow transactions list count is not correct");
        }
        /// <summary>
        /// Test - Good Id that returns no results from service
        /// </summary>
        [TestMethod]
        public void WorkflowHistoryTest_NoResultsId()
        {
            // setting up the expected container models
            Container<IWorkflowTransactionModel> containerWorkflowTransaction = new Container<IWorkflowTransactionModel>();
            IApplicationDetailModel appDetail = new ApplicationDetailModel();

            // setting up mock objects
            MockRepository mocks = new MockRepository();
            ISummaryManagementService summaryManagementServiceMock = mocks.DynamicMock<ISummaryManagementService>();
            ISummaryProcessingService summaryProcessingServiceMock = mocks.DynamicMock<ISummaryProcessingService>();
            SummaryStatementController c = mocks.PartialMock<SummaryStatementController>(summaryManagementServiceMock, null, null, summaryProcessingServiceMock);

            // setting up expected returns 
            Expect.Call(summaryManagementServiceMock.GetWorkflowTransactionHistory(_noResultsAppWorkflowId)).Return(containerWorkflowTransaction);
            Expect.Call(summaryProcessingServiceMock.GetApplicationDetail(_noResultsAppWorkflowId)).Return(appDetail);

            mocks.ReplayAll();

            var x = c.WorkflowHistory(_noResultsAppWorkflowId);
            Assert.IsNotNull(x, "ActionResult was null and it should not have been");

            PartialViewResult v = x as PartialViewResult;

            Assert.AreEqual(v.ViewName, "_WorkflowHistory");

            Assert.IsNotNull(v.Model);
            WorkflowHistoryViewModel m = v.Model as WorkflowHistoryViewModel;
            Assert.IsNotNull(m.WorkflowTransactions, "WorkflowTransactions was null and it should not have been");
            Assert.IsNotNull(m.ApplicationDetail, "ApplicationDetail was null and it should not have been");
            Assert.AreEqual(0, m.WorkflowTransactions.Count(), "Returned view model workflow transactions list count is not correct");
        }
        #endregion
        #region Test Helpers
        /// <summary>
        /// Sets up test data for AvailableApplications test
        /// </summary>
        private void SetUpApplicationsForAvailableApplications()
        {
            _la.Add(new AvailableApplications { ApplicationId = APPID_1, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_2, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_3, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_3, PanelAbbreviation = PANEL_3 });
        }
        /// <summary>
        /// Sets up test data for AvailableApplications test
        /// </summary>
        private void SetUpApplicationsForAvailableApplications_MultipleEntriesInGroup()
        {
            _la.Add(new AvailableApplications { ApplicationId = APPID_1, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_2, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_3, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_1, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_2, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2 });
            _la.Add(new AvailableApplications { ApplicationId = APPID_3, ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2 });
        }
        private void SetUpApplicationsForAvailableProgressApplications()
        {
            _lp.Add(new ApplicationsProgress {  ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1, LogNumber = Log_1 });
            _lp.Add(new ApplicationsProgress {  ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2, LogNumber = Log_2 });
            _lp.Add(new ApplicationsProgress {  ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_3, PanelAbbreviation = PANEL_3, LogNumber = Log_3 });
        }
        /// <summary>
        /// Sets up test data for AvailableApplications test
        /// </summary>
        private void SetUpApplicationsForAvailableProgressApplications_MultipleEntriesInGroup()
        {
            _lp.Add(new ApplicationsProgress { ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1, LogNumber = Log_1 });
            _lp.Add(new ApplicationsProgress { ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2, LogNumber = Log_2 });
            _lp.Add(new ApplicationsProgress { ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1, LogNumber = Log_3 });
            _lp.Add(new ApplicationsProgress { ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_1, PanelAbbreviation = PANEL_1, LogNumber = Log_4 });
            _lp.Add(new ApplicationsProgress { ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2, LogNumber = Log_5 });
            _lp.Add(new ApplicationsProgress { ProgramAbbreviation = PROGRAM_1.ToString(), FY = FISCAL_YEAR_1.ToString(), Cycle = CYCLE_2, PanelAbbreviation = PANEL_2, LogNumber = Log_6 });
        }

        #endregion
    }

}
