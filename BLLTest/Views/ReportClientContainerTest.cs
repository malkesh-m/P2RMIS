using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views.Report;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace BLLTest.Views
{
    /// <summary>
    /// Unit tests for ReportClientContainerTest class
    /// </summary>
    [TestClass()]
    public class ReportClientContainerTest
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
        /// Test ReportClientContainer when passed a null ReportClientListResultModel
        ///</summary>
        [TestMethod()]
        public void NullConstructorTest()
        {
            ReportClientContainer view = new ReportClientContainer(null);
            Assert.IsNotNull(view, "Constructor is null");
            Assert.IsNotNull(view.ClientList, "Client list is null, it should be 0 length list");
            Assert.AreEqual(0, view.ClientList.Count, "Client list was not 0 and it should have been");
        }
        /// <summary>
        /// Test ReportPorgramContainer when passed a ReportClientListResultModel with one
        /// entry.
        ///</summary>
        [TestMethod()]
        public void OneResutlTest()
        {
            //
            // Create a mock & start recording
            //
            MockRepository mocks = new MockRepository();

            IReportClientListResultModel resultModel = mocks.DynamicMock<IReportClientListResultModel>();
            IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            List<ReportClientModel> theList = new List<ReportClientModel>();
            ReportClientModel m1 = new ReportClientModel { ClientIdentifier = 19, ClientAbbreviation = "USAMRMC", ClientDescription = "USAMRMC/CDMRP" };
            theList.Add(m1);
            SetupResult.For(resultModel.ClientList).Return(theList);
            //
            // Finish the mocking & stop recording
            //
            mocks.ReplayAll();
            //
            // Now test
            //
            ReportClientContainer view = new ReportClientContainer(resultModel);
            Assert.IsNotNull(view, "Constructor is null");
            Assert.IsNotNull(view.ClientList, "Client list is null, it should be 1 length list");
            Assert.AreEqual(1, view.ClientList.Count, "Client list was  not equal to 1 and it should have been.");
            //
            // Now check it's value
            //
            List<Tuple<int, string, string>> list = new List<Tuple<int, string, string>>(view.ClientList);
            Assert.AreEqual(19, list[0].Item1, "Client ID should have been 19");
            Assert.AreEqual("USAMRMC", list[0].Item2, "Description should have been USAMRMC");
            Assert.AreEqual("USAMRMC/CDMRP", list[0].Item3, "Description should have been USAMRMC/CDMRP");
        }
        /// <summary>
        /// Test ReportClientContainer when passed a ReportClientListResultModel with multiple
        /// entry.
        ///</summary>
        [TestMethod()]
        public void MultipleResutlTest()
        {
            //
            // Create a mock & start recording
            //
            MockRepository mocks = new MockRepository();

            IReportClientListResultModel resultModel = mocks.DynamicMock<IReportClientListResultModel>();
            IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

            List<ReportClientModel> theList = new List<ReportClientModel>();
            ReportClientModel m1 = new ReportClientModel { ClientIdentifier = 19, ClientAbbreviation = "USAMRMC", ClientDescription = "USAMRMC/CDMRP" };
            theList.Add(m1);
            ReportClientModel m2 = new ReportClientModel { ClientIdentifier = 9, ClientAbbreviation = "CPRIT", ClientDescription = "Cancer Prevention and Research Institute of Texas" };
            theList.Add(m2);
            ReportClientModel m3 = new ReportClientModel { ClientIdentifier = 1, ClientAbbreviation = "SRA", ClientDescription = "SRA International" };
            theList.Add(m3);
            
   
            SetupResult.For(resultModel.ClientList).Return(theList);
            //
            // Finish the mocking & stop recording
            //
            mocks.ReplayAll();
            //
            // Now test
            //
            ReportClientContainer view = new ReportClientContainer(resultModel);
            Assert.IsNotNull(view, "Constructor is null");
            Assert.IsNotNull(view.ClientList, "Client list is null, it should be 3 length list");
            Assert.AreEqual(3, view.ClientList.Count, "Client list should have been 3");
            //
            // Now check it's value
            //
            List<Tuple<int, string, string>> list = new List<Tuple<int, string, string>>(view.ClientList);

            Assert.AreEqual("USAMRMC/CDMRP", list[0].Item3);
            Assert.AreEqual("Cancer Prevention and Research Institute of Texas", list[1].Item3);
            Assert.AreEqual("SRA International", list[2].Item3);

            Assert.AreEqual("USAMRMC", list[0].Item2);
            Assert.AreEqual("CPRIT", list[1].Item2);
            Assert.AreEqual("SRA", list[2].Item2);

            Assert.AreEqual(19, list[0].Item1);
            Assert.AreEqual(9, list[1].Item1);
            Assert.AreEqual(1, list[2].Item1);
        }

        #endregion
    }
}
