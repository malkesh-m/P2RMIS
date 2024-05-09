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
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.Dal.ResultModels.Reports;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Views.Report;

namespace BLLTest.Views
{
    /// <summary>
    /// Unit tests for ReportFiscalYearContainer class
    /// </summary>
    [TestClass()]
    public class ReportFiscalYearContainerTest
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
        /// Test ReportFiscalYearContainer when passed a null ReportFiscalYearResultModel
        ///</summary>
        [TestMethod()]
        public void NullConstructorTest()
        {
            // DELETE ME PLEASE !!
            //ReportFiscalYearContainer view = new ReportFiscalYearContainer(null);
            //Assert.IsNotNull(view, "Constructor is null");
            //Assert.IsNotNull(view.FiscalYearDescriptions, "fiscal year list is null, it should be 0 length list");
            //Assert.AreEqual(0, view.FiscalYearDescriptions.Count, "fiscal year list was not 0 and it should have been");
        }
        #endregion
        /// <summary>
        /// Test ReportFiscalYearContainer when passed a ReportFiscalYearResultModel with one
        /// entry.
        ///</summary>
        [TestMethod()]
        public void OneResultTest()
        {
            //string aValue = "2014";
            ////
            //// Create a mock & start recording
            ////
            //MockRepository mocks = new MockRepository();

            //IReportFiscalYearResultModel resultModel = mocks.DynamicMock<IReportFiscalYearResultModel>();
            //IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            //List<ReportFiscalYearModel> theList = new List<ReportFiscalYearModel>();
            //ReportFiscalYearModel m1 = new ReportFiscalYearModel { FiscalYear = aValue };
            //theList.Add(m1);
            //SetupResult.For(resultModel.ModelList).Return(theList);
            ////
            //// Finish the mocking & stop recording
            ////
            //mocks.ReplayAll();
            ////
            //// Now test
            ////
            //ReportFiscalYearContainer view = new ReportFiscalYearContainer(resultModel);
            //Assert.IsNotNull(view, "Constructor is null");
            //Assert.IsNotNull(view.FiscalYearDescriptions, "Fiscal year list is null, it should be 0 length list");
            //Assert.AreEqual(1, view.FiscalYearDescriptions.Count, "Fiscal year list was 0 and it should have been 1");
            ////
            //// Now check it's value
            ////
            //List<string> list = new List<string>(view.FiscalYearDescriptions);
            //Assert.AreEqual(aValue, list[0]);
        }
        /// <summary>
        /// Test ReportFiscalYearContainer when passed a ReportFiscalYearResultModel with multiple
        /// entries.
        ///</summary>
        [TestMethod()]
        public void MultipleResultTest()
        {
            //string aValue1 = "2014";
            //string aValue2 = "2014";
            //string aValue3 = "2014";
            //string aValue4 = "2014";
            //string aValue5 = "2014";
            ////
            //// Create a mock & start recording
            ////
            //MockRepository mocks = new MockRepository();

            //IReportFiscalYearResultModel resultModel = mocks.DynamicMock<IReportFiscalYearResultModel>();
            //IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            //List<ReportFiscalYearModel> theList = new List<ReportFiscalYearModel>();
            //ReportFiscalYearModel m1 = new ReportFiscalYearModel { FiscalYear = aValue1 };
            //theList.Add(m1);
            //ReportFiscalYearModel m2 = new ReportFiscalYearModel { FiscalYear = aValue2 };
            //theList.Add(m2);
            //ReportFiscalYearModel m3 = new ReportFiscalYearModel { FiscalYear = aValue3 };
            //theList.Add(m3);
            //ReportFiscalYearModel m4 = new ReportFiscalYearModel { FiscalYear = aValue4 };
            //theList.Add(m4);
            //ReportFiscalYearModel m5 = new ReportFiscalYearModel { FiscalYear = aValue5 };
            //theList.Add(m5);
            //SetupResult.For(resultModel.ModelList).Return(theList);
            ////
            //// Finish the mocking & stop recording
            ////
            //mocks.ReplayAll();
            ////
            //// Now test
            ////
            //ReportFiscalYearContainer view = new ReportFiscalYearContainer(resultModel);
            //Assert.IsNotNull(view, "Constructor is null");
            //Assert.IsNotNull(view.FiscalYearDescriptions, "Fiscal year list is null, it should be 0 length list");
            //Assert.AreEqual(5, view.FiscalYearDescriptions.Count, "Fiscal year list was 0 and it should have been 1");
            ////
            //// Now check it's value
            ////
            //List<string> list = new List<string>(view.FiscalYearDescriptions);
            //Assert.AreEqual(aValue1, list[0]);
            //Assert.AreEqual(aValue2, list[1]);
            //Assert.AreEqual(aValue3, list[2]);
            //Assert.AreEqual(aValue4, list[3]);
            //Assert.AreEqual(aValue5, list[4]);
        }
    }
}
