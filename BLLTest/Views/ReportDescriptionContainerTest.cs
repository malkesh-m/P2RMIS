using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using Sra.P2rmis.Bll.Views.Report;
using Sra.P2rmis.Dal.ResultModels.Reports;
using Sra.P2rmis.Dal.Interfaces;

namespace BLLTest.Views
{
    /// <summary>
    /// Unit tests for the data layer result model returning fiscal years for one or more programs.
    /// </summary>
    [TestClass()]
    public class ReportDescriptionContainerTest
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
        #region Tests
        /// <summary>
        /// Test checking if constructor populated the program list by default
        /// </summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            ReportDescriptionContainer container = new ReportDescriptionContainer(null);
            Assert.IsNotNull(container.ProgramDescriptions, "Constructor did not create a ProgramDescriptions list");
            Assert.AreEqual(0, container.ProgramDescriptions.Count, "report group list is not 0 and it should have been");
        }
        /// <summary>
        /// Test ReportDescriptionContainer when passed a ReportDescriptionResultModel with one
        /// entry.
        ///</summary>
        [TestMethod()]
        public void OneResultTest()
        {
            //
            // Create a mock & start recording
            //
            MockRepository mocks = new MockRepository();

            IReportDescriptionResultModel resultModel = mocks.DynamicMock<IReportDescriptionResultModel>();
            IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            List<ReportDescriptionModel> theList = new List<ReportDescriptionModel>();
            ReportDescriptionModel m1 = new ReportDescriptionModel { ReportGroupId = 1, GroupName = "group name", Description = "description", SortOrder = 1 };
            theList.Add(m1);
            SetupResult.For(resultModel.ModelList).Return(theList);
            //
            // Finish the mocking & stop recording
            //
            mocks.ReplayAll();
            //
            // Now test
            //
            ReportDescriptionContainer view = new ReportDescriptionContainer(resultModel);
            Assert.IsNotNull(view, "Constructor is null");
            Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, it should be 0 length list");
            Assert.AreEqual(1, view.ProgramDescriptions.Count, "Program list was  0 and it should have been 1");
            //
            // Now check it's value
            //
            List<Tuple<int, string, string, int>> list = new List<Tuple<int, string, string, int>>(view.ProgramDescriptions);
            Assert.AreEqual(1, list[0].Item1);
            Assert.AreEqual("group name", list[0].Item2);
            Assert.AreEqual("description", list[0].Item3);
            Assert.AreEqual(1, list[0].Item4);
        }
        /// <summary>
        /// Test ReportDescriptionContainer when passed a ReportDescriptionResultModel with one
        /// entry.
        ///</summary>
        [TestMethod()]
        public void MultipleResultTest()
        {
            //
            // Create a mock & start recording
            //
            MockRepository mocks = new MockRepository();

            IReportDescriptionResultModel resultModel = mocks.DynamicMock<IReportDescriptionResultModel>();
            IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            List<ReportDescriptionModel> theList = new List<ReportDescriptionModel>();
            ReportDescriptionModel m1 = new ReportDescriptionModel { ReportGroupId = 1, GroupName = "group name1", Description = "description1", SortOrder = 1 };
            ReportDescriptionModel m2 = new ReportDescriptionModel { ReportGroupId = 2, GroupName = "group name2", Description = "description2", SortOrder = 2 };
            ReportDescriptionModel m3 = new ReportDescriptionModel { ReportGroupId = 3, GroupName = "group name3", Description = "description3", SortOrder = 3 };
            ReportDescriptionModel m4 = new ReportDescriptionModel { ReportGroupId = 4, GroupName = "group name4", Description = "description4", SortOrder = 4 };
            ReportDescriptionModel m5 = new ReportDescriptionModel { ReportGroupId = 5, GroupName = "group name5", Description = "description5", SortOrder = 5 };
            theList.Add(m1);
            theList.Add(m2);
            theList.Add(m3);
            theList.Add(m4);
            theList.Add(m5);
            SetupResult.For(resultModel.ModelList).Return(theList);
            //
            // Finish the mocking & stop recording
            //
            mocks.ReplayAll();
            //
            // Now test
            //
            ReportDescriptionContainer view = new ReportDescriptionContainer(resultModel);
            Assert.IsNotNull(view, "Constructor returned null");
            Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, there should be a list");
            Assert.AreEqual(5, view.ProgramDescriptions.Count, "Program list was not correct length");
            //
            // Now check it's value
            //
            List<Tuple<int, string, string, int>> list = new List<Tuple<int, string, string, int>>(view.ProgramDescriptions);
            Assert.AreEqual(1, list[0].Item1);
            Assert.AreEqual("group name1", list[0].Item2);
            Assert.AreEqual("description1", list[0].Item3);
            Assert.AreEqual(1, list[0].Item4);
        }
        /// <summary>
        /// Test ReportDescriptionContainer when passed a ReportDescriptionResultModel with one
        /// entry.  This is to support the request for a specific report group description
        ///</summary>
        [TestMethod()]
        public void SpecificDescriptionTest()
        {
            string reportGroupDescription = "this is my report group description";
            //
            // Create a mock & start recording
            //
            MockRepository mocks = new MockRepository();

            IReportDescriptionResultModel resultModel = mocks.DynamicMock<IReportDescriptionResultModel>();
            IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            List<ReportDescriptionModel> theList = new List<ReportDescriptionModel>();
            ReportDescriptionModel m1 = new ReportDescriptionModel { ReportGroupId = 1, GroupName = "group name", Description = reportGroupDescription, SortOrder = 1 };
            theList.Add(m1);
            SetupResult.For(resultModel.ModelList).Return(theList);
            //
            // Finish the mocking & stop recording
            //
            mocks.ReplayAll();
            //
            // Now test
            //
            ReportDescriptionContainer view = new ReportDescriptionContainer(resultModel);
            Assert.IsNotNull(view, "Constructor is null");
            Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, it should be 0 length list");
            Assert.AreEqual(1, view.ProgramDescriptions.Count, "Program list was  0 and it should have been 1");
            //
            // Now check it's value
            //
            Assert.AreEqual(reportGroupDescription, view.GetSpecificDescription, "Did not return the specific description");
        }
        /// <summary>
        /// Test ReportDescriptionContainer when passed a ReportDescriptionResultModel with one
        /// entry.  This is to support the request for a specific report group description
        ///</summary>
        [TestMethod()]
        public void SpecificDescriptionTestAgain()
        {
            string reportGroupDescription = "this is my report group description";
            //
            // Create a mock & start recording
            //
            MockRepository mocks = new MockRepository();

            IReportDescriptionResultModel resultModel = mocks.DynamicMock<IReportDescriptionResultModel>();
            IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            List<ReportDescriptionModel> theList = new List<ReportDescriptionModel>();
            ReportDescriptionModel m1 = new ReportDescriptionModel { ReportGroupId = 1, GroupName = "group name", Description = reportGroupDescription, SortOrder = 1 };
            ReportDescriptionModel m2 = new ReportDescriptionModel { ReportGroupId = 2, GroupName = "group name", Description = "some other string", SortOrder = 2 };
            ReportDescriptionModel m3 = new ReportDescriptionModel { ReportGroupId = 3, GroupName = "group name", Description = "yet another string", SortOrder = 3 };
            theList.Add(m1);
            theList.Add(m2);
            theList.Add(m3); 
            SetupResult.For(resultModel.ModelList).Return(theList);
            //
            // Finish the mocking & stop recording
            //
            mocks.ReplayAll();
            //
            // Now test
            //
            ReportDescriptionContainer view = new ReportDescriptionContainer(resultModel);
            Assert.IsNotNull(view, "Constructor is null");
            Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, it should be 0 length list");
            Assert.AreEqual(3, view.ProgramDescriptions.Count, "Program list was not 3");
            //
            // Now check it's value
            //
            Assert.AreEqual(reportGroupDescription, view.GetSpecificDescription, "Did not return the specific description");
        }

        #endregion 
    }
}
