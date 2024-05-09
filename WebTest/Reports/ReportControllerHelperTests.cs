using System.Collections.Generic;
using NUnit.Framework;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Reports;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace WebTest.Reports
{
    [TestClass()]
    public class ReportControllerHelperTests
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
        /// Test a good find
        /// </summary>
        [TestMethod()]
        public void FindReportListModelTest()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_1, list);

            Assert.IsNotNull(result, "Failed to find the report");
        }
        /// <summary>
        /// Test a null id
        /// </summary>
        [TestMethod()]
        public void FindReportListModelNullTest()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(null, list);

            Assert.IsNull(result, "Found a model but it should not have");
        }
        /// <summary>
        /// Test a report id that is not there
        /// </summary>
        [TestMethod()]
        public void FindReportListModelNonExistentIdTest()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(17, list);

            Assert.IsNull(result, "Found a model but it should not have");
        }
        /// <summary>
        /// Test null list
        /// </summary>
        [TestMethod()]
        public void FindReportListModelNullListTest()
        {
            List<IReportListModel> list = null;

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_1, list);

            Assert.IsNull(result, "Found a model but it should not have");
        }
        /// <summary>
        /// Test empty list
        /// </summary>
        [TestMethod()]
        public void FindReportListModelEmptyListTest()
        {
            List<IReportListModel> list = new List<IReportListModel>();

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_1, list);

            Assert.IsNull(result, "Found a model but it should not have");
        }
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        public void FindReportListModeMultiplelEntry_1_Test()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_1, list);

            Assert.IsNotNull(result, "Failed to find the report");
            Assert.AreEqual(REPORT_ID_1, result.ReportId, "Failed to find the correct report report");
        }
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        public void FindReportListModeMultiplelEntry_2_Test()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_2, list);

            Assert.IsNotNull(result, "Failed to find the report");
            Assert.AreEqual(REPORT_ID_2, result.ReportId, "Failed to find the correct report report");
        }
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        public void FindReportListModeMultiplelEntry_3_Test()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_3, list);

            Assert.IsNotNull(result, "Failed to find the report");
            Assert.AreEqual(REPORT_ID_3, result.ReportId, "Failed to find the correct report report");
        }
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        public void FindReportListModeMultiplelEntry_4_Test()
        {
            List<IReportListModel> list = new List<IReportListModel>();
            ReportListModel model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_1, REPORT_NAME_1, REPORT_FILENAME_1, REPORT_DESCRIPTION_1);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_1, REPORT_ID_2, REPORT_NAME_2, REPORT_FILENAME_2, REPORT_DESCRIPTION_2);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_3, REPORT_NAME_3, REPORT_FILENAME_3, REPORT_DESCRIPTION_3);
            list.Add(model);
            model = MakeModel(GROUP_ID_1, GROUP_NAME_1, GROUP_DESCRIPTION_1, SORT_ORDER_2, REPORT_ID_4, REPORT_NAME_4, REPORT_FILENAME_4, REPORT_DESCRIPTION_4);
            list.Add(model);

            IReportListModel result = ReportControllerHelpers.FindReportListModel(REPORT_ID_4, list);

            Assert.IsNotNull(result, "Failed to find the report");
            Assert.AreEqual(REPORT_ID_4, result.ReportId, "Failed to find the correct report report");
        }
        #endregion 
        #region Helpers
        /// <summary>
        /// Create a ReportListMode for testing
        /// NOTE: this could be re-factored.  It is a duplicate of a method in MenuBuilderTest.
        /// </summary>
        private ReportListModel MakeModel(int gId, string gName, string gDescription, int gOrder, int rId, string rName, string rFileName, string rDescritpion)
        {
            return new ReportListModel
            {
                ReportGroupId = gId,
                ReportGroupName = gName,
                ReportGroupDescription = gDescription,
                ReportGroupSortOrder = gOrder,
                ReportId = rId,
                ReportName = rName,
                ReportFileName = rFileName,
                ReportDescription = rDescritpion
            };
        }
        #endregion
    }
}
