using System.Collections.Generic;
using System.Web;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Common.Interfaces;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Reports;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace WebTest
{
    [TestClass()]
    public class MenuBuilderTest
    {
        //
        // Some values
        //
        private const string REPORT_NAME_1 = "Report 1";
        private const string REPORT_NAME_2 = "Report 2";
        private const string REPORT_NAME_3 = "Report 3";
        private const string REPORT_NAME_4 = "Report 4";
        private const string REPORT_NAME_5 = "Report 5";

        private const string REPORT_FILENAME_1 = "Report Filename1";
        private const string REPORT_FILENAME_2 = "Report Filename2";
        private const string REPORT_FILENAME_3 = "Report Filename3";
        private const string REPORT_FILENAME_4 = "Report Filename4";
        private const string REPORT_FILENAME_5 = "Report Filename5";

        private const string REPORT_DESCRIPTION_1 = "Description 1";
        private const string REPORT_DESCRIPTION_2 = "Description 2";
        private const string REPORT_DESCRIPTION_3 = "Description 3";
        private const string REPORT_DESCRIPTION_4 = "Description 4";
        private const string REPORT_DESCRIPTION_5 = "Description 5";

        private const string GROUP_NAME_1 = "Group 1";
        private const string GROUP_NAME_2 = "Group 2";
        private const string GROUP_NAME_3 = "Group 3";
        private const string GROUP_NAME_4 = "Group 4";
        private const string GROUP_NAME_5 = "Group 5";

        private const string GROUP_DESCRIPTION_1 = "Group 1";
        private const string GROUP_DESCRIPTION_2 = "Group 2";
        private const string GROUP_DESCRIPTION_3 = "Group 3";
        private const string GROUP_DESCRIPTION_4 = "Group 4";
        private const string GROUP_DESCRIPTION_5 = "Group 5";

        private const int GROUP_ID_1 = 1;
        private const int GROUP_ID_2 = 2;
        private const int GROUP_ID_3 = 3;
        private const int GROUP_ID_4 = 4;
        private const int GROUP_ID_5 = 5;

        private const int REPORT_ID_1 = 11;
        private const int REPORT_ID_2 = 22;
        private const int REPORT_ID_3 = 33;
        private const int REPORT_ID_4 = 44;
        private const int REPORT_ID_5 = 55;

        private const int SORT_ORDER_1 = 1;
        private const int SORT_ORDER_2 = 2;
        private const int SORT_ORDER_3 = 3;

        private const string PERMISSION_1 = "View Reports";
        private const string PERMISSION_2 = "Not View Reports";

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
        #region constructor tests
        /// <summary>
        /// Test with an empty list
        /// </summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            IEnumerable<IReportListModel> reportList = new List<IReportListModel>();

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(0, tree.Count, "List of menu items is not 0");
        }
        /// <summary>
        /// Test for null parameter
        /// </summary>
        [TestMethod()]
        public void ConstructorNullParameterTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            mocks.ReplayAll();

            IEnumerable<IReportListModel> reportList = new List<IReportListModel>();

            MenuBuilder builder = new MenuBuilder(null, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(0, tree.Count, "List of menu items is not 0");
        }
        /// <summary>
        /// Test for null parameter
        /// </summary>
        [TestMethod()]
        public void ConstructorNullParametersTest()
        {
            MenuBuilder builder = new MenuBuilder(null, null);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(0, tree.Count, "List of menu items is not 0");
        }
        /// <summary>
        /// Test for null parameter
        /// </summary>
        [TestMethod()]
        public void ConstructorNullParameter2Test()
        {
            IEnumerable<IReportListModel> reportList = new List<IReportListModel>();

            MenuBuilder builder = new MenuBuilder(reportList, null);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(0, tree.Count, "List of menu items is not 0");
        }
        /// <summary>
        /// Test for null parameter
        /// </summary>
        [TestMethod()]
        public void ConstructorNullParameter3Test()
        {
            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, null);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(0, tree.Count, "List of menu items is not 0");
        }
        #endregion
        /// <summary>
        /// Test for 1 report group & 1 report
        /// </summary>
        [TestMethod()]
        public void ConstructorNOneParameterParameterTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();
            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 1 report group & 2 reports
        /// </summary>
        [TestMethod()]
        public void Build1Group_2ReportsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true).Repeat.Times(2);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.AreEqual(2, tree[0].Tree.Count, "Number of report menu entries is not correct");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);

            item = menu.Tree[1];
            CheckReportItem(REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 1 report group & 3 reports
        /// </summary>
        [TestMethod()]
        public void Build1Group_3ReportsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true).Repeat.Times(3);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.AreEqual(3, tree[0].Tree.Count, "Number of report menu entries is not correct");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);

            item = menu.Tree[1];
            CheckReportItem(REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, item as ReportMenuItem);

            item = menu.Tree[2];
            CheckReportItem(REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 1 report group & 4 reports
        /// </summary>
        [TestMethod()]
        public void Build1Group_4ReportsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true).Repeat.Times(4);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.AreEqual(4, tree[0].Tree.Count, "Number of report menu entries is not correct");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);

            item = menu.Tree[1];
            CheckReportItem(REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, item as ReportMenuItem);

            item = menu.Tree[2];
            CheckReportItem(REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, item as ReportMenuItem);

            item = menu.Tree[3];
            CheckReportItem(REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 1 report group & 5 reports
        /// </summary>
        [TestMethod()]
        public void Build1Group_5ReportsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true).Repeat.Times(5);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_5, REPORT_NAME_5, REPORT_FILENAME_5, REPORT_DESCRIPTION_5, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.AreEqual(5, tree[0].Tree.Count, "Number of report menu entries is not correct");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);

            item = menu.Tree[1];
            CheckReportItem(REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, item as ReportMenuItem);

            item = menu.Tree[2];
            CheckReportItem(REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, item as ReportMenuItem);

            item = menu.Tree[3];
            CheckReportItem(REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4, item as ReportMenuItem);

            item = menu.Tree[4];
            CheckReportItem(REPORT_ID_5, REPORT_NAME_5, REPORT_FILENAME_5, REPORT_DESCRIPTION_5, item as ReportMenuItem);
        }
        #region Multiple group tests
        /// <summary>
        /// Test for 2 report group & 2 reports, 1 each
        /// </summary>
        [TestMethod()]
        public void Build2Groups_1ReportsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true).Repeat.Times(2);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_2, GROUP_NAME_2, GROUP_DESCRIPTION_2, SORT_ORDER_2, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_1);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(2, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.IsNotNull(tree[1].Tree, "There is no report entry and there should be");
            Assert.AreEqual(1, tree[0].Tree.Count, "Number of report menu entries is not correct");
            Assert.AreEqual(1, tree[1].Tree.Count, "Number of report menu entries is not correct");
            //
            // First menu item 
            //
            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);

            menu = tree[1];
            CheckMenuItem(GROUP_ID_2, GROUP_NAME_2, GROUP_DESCRIPTION_2, SORT_ORDER_2, menu);

            item = menu.Tree[0];
            CheckReportItem(REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 2 report group & 4 reports, 21 each
        /// </summary>
        [TestMethod()]
        public void Build2Groups_2ReportsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true).Repeat.Times(4);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();
            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_1);
            reportList.Add(model);
            //
            // Now the second group
            //
            model = MakeModel(GROUP_ID_2, GROUP_NAME_2, GROUP_DESCRIPTION_2, SORT_ORDER_2, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_2, GROUP_NAME_2, GROUP_DESCRIPTION_2, SORT_ORDER_2, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4, PERMISSION_1);
            reportList.Add(model);


            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(2, tree.Count, "List of menu items (groups) is not 2");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.IsNotNull(tree[1].Tree, "There is no report entry and there should be");
            Assert.AreEqual(2, tree[0].Tree.Count, "Number of report menu entries is not correct");
            Assert.AreEqual(2, tree[1].Tree.Count, "Number of report menu entries is not correct");
            //
            // First menu item 
            //
            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);
            item = menu.Tree[1];
            CheckReportItem(REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, item as ReportMenuItem);
            //
            // second report group
            //
            menu = tree[1];
            CheckMenuItem(GROUP_ID_2, GROUP_NAME_2, GROUP_DESCRIPTION_2, SORT_ORDER_2, menu);
            item = menu.Tree[0];

            CheckReportItem(REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3, item as ReportMenuItem);
            item = menu.Tree[1];
            CheckReportItem(REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4, item as ReportMenuItem);
        }
        #endregion
        #region Permission Tests
        ///
        /// Things to test:
        ///   - 2 ReportListModel objects (same report) one with a permission that the user does not have
        ///   - 2 different ReportListModel objects one with a permission that the user does not have
        ///   - 2 different ReportListModel objects both with a permission that the user does not have
        ///   
        /// <summary>
        /// Test for 2 ReportListModel objects (same report) one with a permission that the user does not have.
        /// (This should result in one menu entry if the report has more than 1 permission)
        /// </summary>
        [TestMethod()]
        public void Permissions2SameReportList1GoodPermissionsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true);
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_2)).Return(false);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_2);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.AreEqual(1, tree[0].Tree.Count, "Number of report menu entries is not correct");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 2 different ReportListModel objects one with a permission that the user does not have
        /// </summary>
        [TestMethod()]
        public void Permissions2DifferentReportList1GoodPermissionsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(true);
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_2)).Return(false);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_2);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(1, tree.Count, "List of menu items (groups) is not 1");
            Assert.IsNotNull(tree[0].Tree, "There is no report entry and there should be");
            Assert.AreEqual(1, tree[0].Tree.Count, "Number of report menu entries is not correct");

            MenuItem menu = tree[0];
            CheckMenuItem(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, menu);

            MenuItem item = menu.Tree[0];
            CheckReportItem(REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, item as ReportMenuItem);
        }
        /// <summary>
        /// Test for 2 different ReportListModel objects both with a permission that the user does not have
        /// </summary>
        [TestMethod()]
        public void Permissions2DifferentReportListBadPermissionsTest()
        {
            //
            // Create a repository for the test's mocks.  Then we go through and create a mock for each object 
            // that will be used.
            //
            MockRepository mocks = new MockRepository();
            HttpSessionStateBase httpSessionStateBaseMock = mocks.DynamicMock<HttpSessionStateBase>();
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_1)).Return(false);
            Expect.Call(SecurityHelpers.CheckValidPermissionFromSession(httpSessionStateBaseMock, PERMISSION_2)).Return(false);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();

            List<IReportListModel> reportList = new List<IReportListModel>();
            ReportListModel model = MakeModel(1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1, PERMISSION_1);
            reportList.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2, PERMISSION_2);
            reportList.Add(model);

            MenuBuilder builder = new MenuBuilder(reportList, httpSessionStateBaseMock);
            List<MenuItem> tree = builder.Build();

            Assert.IsNotNull(tree, "Menu tree is null but it should not be");
            Assert.AreEqual(0, tree.Count, "List of menu items (groups) is not 1");
        }
        #endregion
        #endregion
        #region Helpers
        /// <summary>
        /// Create a ReportListMode for testing
        /// </summary>
        private ReportListModel MakeModel(int gId, string gName, string gDescription, int gOrder, int rId, string rName, string rFileName, string rDescritpion, string rPermission)
        {
            return new ReportListModel {
                                    ReportGroupId = gId,
                                    ReportGroupName =  gName,
                                    ReportGroupDescription = gDescription,
                                    ReportGroupSortOrder = gOrder,
                                    ReportId = rId,
                                    ReportName = rName, 
                                    ReportFileName  = rFileName,
                                    ReportDescription = rDescritpion,
                                    RequiredPermission = rPermission
                                        };
        }
        /// <summary>
        /// Checks the menu entry values
        /// </summary>
        private void CheckMenuItem(int gId, string gName, string gDescription, int gOrder, MenuItem menu)
        {
            Assert.AreEqual(gId, menu.Id);
            Assert.AreEqual(gDescription, menu.Description);
            Assert.AreEqual(gName, menu.Name);
            Assert.AreEqual(gOrder, menu.SortOrder);
        }
        /// <summary>
        /// Check the report entry values
        /// </summary>
        private void CheckReportItem(int rId, string rName, string rFileName, string rDescritpion, ReportMenuItem item)
        {
            Assert.AreEqual(rId, item.Id);
            Assert.AreEqual(rDescritpion, item.Description);
            Assert.AreEqual(rName, item.Name);
            Assert.AreEqual(-1, item.SortOrder);
            Assert.AreEqual(rFileName, item.ReportFileName);
        }

        // Use a fake HttpSessionStateBase, because it's hard to mock it with Rhino (or is it?)
        private class FakeSessionState : HttpSessionStateBase
        {
            Dictionary<string, object> items = new Dictionary<string, object>();
            public override object this[string name]
            {
                get { return items.ContainsKey(name) ? items[name] : null; }
                set { items[name] = value; }
            }
        }
        #endregion
    }

}
