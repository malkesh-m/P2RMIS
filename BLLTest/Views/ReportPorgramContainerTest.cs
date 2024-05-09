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
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Views.Report;
namespace BLLTest.Views
{
    // DELETE ME PLEASE !!
    /// <summary>
    /// Unit tests for ReportPorgramContainerTest class
    /// </summary>
    //[TestClass()]
    //public class ReportPorgramContainerTest
    //{
    //    #region Overhead
    //    private TestContext testContextInstance;

    //    /// <summary>
    //    ///Gets or sets the test context which provides
    //    ///information about and functionality for the current test run.
    //    ///</summary>
    //    public TestContext TestContext
    //    {
    //        get
    //        {
    //            return testContextInstance;
    //        }
    //        set
    //        {
    //            testContextInstance = value;
    //        }
    //    }

    //    #region Additional test attributes
    //    // 
    //    //You can use the following additional attributes as you write your tests:
    //    //
    //    //Use ClassInitialize to run code before running the first test in the class
    //    //[ClassInitialize()]
    //    //public static void MyClassInitialize(TestContext testContext)
    //    //{
    //    //}
    //    //
    //    //Use ClassCleanup to run code after all tests in a class have run
    //    //[ClassCleanup()]
    //    //public static void MyClassCleanup()
    //    //{
    //    //}
    //    //
    //    //Use TestInitialize to run code before running each test
    //    //[TestInitialize()]
    //    //public void MyTestInitialize()
    //    //{
    //    //}
    //    //
    //    //Use TestCleanup to run code after each test has run
    //    //[TestCleanup()]
    //    //public void MyTestCleanup()
    //    //{
    //    //}
    //    //
    //    #endregion
    //    #endregion
    //    #region Tests
    //    /// <summary>
    //    /// Test ReportPorgramContainer when passed a null ReportClientProgramListResultModel
    //    ///</summary>
    //    [TestMethod()]
    //    public void NullConstructorTest()
    //    {
    //        ReportPorgramContainer view = new ReportPorgramContainer(null);
    //        Assert.IsNotNull(view, "Constructor is null");
    //        Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, it should be 0 length list");
    //        Assert.AreEqual(0, view.ProgramDescriptions.Count, "Program list was not 0 and it should have been");
    //    }
    //    /// <summary>
    //    /// Test ReportPorgramContainer when passed a ReportClientProgramListResultModel with one
    //    /// entry.
    //    ///</summary>
    //    [TestMethod()]
    //    public void OneResutlTest()
    //    {
    //        //
    //        // Create a mock & start recording
    //        //
    //        MockRepository mocks = new MockRepository();

    //        IReportClientProgramListResultModel resultModel = mocks.DynamicMock<IReportClientProgramListResultModel>();
    //        IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

    //        List<ReportClientProgramModel> theList = new List<ReportClientProgramModel>();
    //        ReportClientProgramModel m1 = new ReportClientProgramModel { ClientProgramDescription = "description1" };
    //        theList.Add(m1);
    //        SetupResult.For(resultModel.ProgramList).Return(theList);
    //        //
    //        // Finish the mocking & stop recording
    //        //
    //        mocks.ReplayAll();
    //        //
    //        // Now test
    //        //
    //        ReportPorgramContainer view = new ReportPorgramContainer(resultModel);
    //        Assert.IsNotNull(view, "Constructor is null");
    //        Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, it should be 0 length list");
    //        Assert.AreEqual(1, view.ProgramDescriptions.Count, "Program list was  0 and it should have been 1");
    //        //
    //        // Now check it's value
    //        //
    //        List<Tuple<string, string>> list = new List<Tuple<string, string>>(view.ProgramDescriptions);
    //        Assert.AreEqual("description1", list[0].Item2);
    //    }
    //    /// <summary>
    //    /// Test ReportPorgramContainer when passed a ReportClientProgramListResultModel with multiple
    //    /// entry.
    //    ///</summary>
    //    [TestMethod()]
    //    public void MultipleResutlTest()
    //    {
    //        //
    //        // Create a mock & start recording
    //        //
    //        MockRepository mocks = new MockRepository();

    //        IReportClientProgramListResultModel resultModel = mocks.DynamicMock<IReportClientProgramListResultModel>();
    //        IReportRepository reportRepositoryMock = mocks.DynamicMock<IReportRepository>();

    //        List<ReportClientProgramModel> theList = new List<ReportClientProgramModel>();
    //        ReportClientProgramModel m1 = new ReportClientProgramModel { ClientProgramAbbreviation = "abbreviation1", ClientProgramDescription = "description1" };
    //        theList.Add(m1);
    //        ReportClientProgramModel m2 = new ReportClientProgramModel { ClientProgramAbbreviation = "abbreviation2", ClientProgramDescription = "description2" };
    //        theList.Add(m2);
    //        ReportClientProgramModel m3 = new ReportClientProgramModel { ClientProgramAbbreviation = "abbreviation3", ClientProgramDescription = "description3" };
    //        theList.Add(m3);
    //        ReportClientProgramModel m4 = new ReportClientProgramModel { ClientProgramAbbreviation = "abbreviation4", ClientProgramDescription = "description4" };
    //        theList.Add(m4);
    //        ReportClientProgramModel m5 = new ReportClientProgramModel { ClientProgramAbbreviation = "abbreviation5", ClientProgramDescription = "description5" };
    //        theList.Add(m5);
    //        SetupResult.For(resultModel.ProgramList).Return(theList);
    //        //
    //        // Finish the mocking & stop recording
    //        //
    //        mocks.ReplayAll();
    //        //
    //        // Now test
    //        //
    //        ReportPorgramContainer view = new ReportPorgramContainer(resultModel);
    //        Assert.IsNotNull(view, "Constructor is null");
    //        Assert.IsNotNull(view.ProgramDescriptions, "Program list is null, it should be 0 length list");
    //        Assert.AreEqual(5, view.ProgramDescriptions.Count, "Program list was  0 and it should have been 1");
    //        //
    //        // Now check it's value
    //        //
    //        List<Tuple<string, string>> list = new List<Tuple<string, string>>(view.ProgramDescriptions);

    //        Assert.AreEqual("description1", list[0].Item2);
    //        Assert.AreEqual("description2", list[1].Item2);
    //        Assert.AreEqual("description3", list[2].Item2);
    //        Assert.AreEqual("description4", list[3].Item2);
    //        Assert.AreEqual("description5", list[4].Item2);

    //        Assert.AreEqual("abbreviation1", list[0].Item1);
    //        Assert.AreEqual("abbreviation2", list[1].Item1);
    //        Assert.AreEqual("abbreviation3", list[2].Item1);
    //        Assert.AreEqual("abbreviation4", list[3].Item1);
    //        Assert.AreEqual("abbreviation5", list[4].Item1);

    //    }

    //    #endregion
    //}
}
