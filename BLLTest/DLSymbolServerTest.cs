using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace BLLTest
{
    /// <summary>
    /// Unit tests for 
    /// </summary>
    [TestClass]
    public class DLSymbolServerTest
    {
        //TODO: RDL Delete this file
        //#region Overhead
        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //#region Additional test attributes
        //// 
        ////You can use the following additional attributes as you write your tests:
        ////
        ////Use ClassInitialize to run code before running the first test in the class
        ////[ClassInitialize()]
        ////public static void MyClassInitialize(TestContext testContext)
        ////{
        ////}
        ////
        ////Use ClassCleanup to run code after all tests in a class have run
        ////[ClassCleanup()]
        ////public static void MyClassCleanup()
        ////{
        ////}
        ////
        ////Use TestInitialize to run code before running each test
        ////[TestInitialize()]
        ////public void MyTestInitialize()
        ////{
        ////}
        ////
        ////Use TestCleanup to run code after each test has run
        ////[TestCleanup()]
        ////public void MyTestCleanup()
        ////{
        ////}
        ////
        //#endregion
        //#endregion
        //#region The Tests
        ///// <summary>
        ///// Test symbol is defined correctly
        ///// </summary>
        //[TestMethod]
        //[Category("DLSymbolServer")]
        //public void Triaged()
        //{
        //    Assert.AreEqual(DLSymbolServer.ReviewStatus.Triaged, ReviewStatu.Triaged);
        //}
        ///// <summary>
        ///// Test symbol is defined correctly
        ///// </summary>
        //[TestMethod]
        //[Category("DLSymbolServer")]
        //public void FullReview()
        //{
        //    Assert.AreEqual(DLSymbolServer.ReviewStatus.FullReview, ReviewStatu.FullReview);
        //}
        ///// <summary>
        ///// Test symbol is defined correctly
        ///// </summary>
        //[TestMethod]
        //[Category("DLSymbolServer")]
        //public void CommandDraft()
        //{
        //    Assert.AreEqual(DLSymbolServer.ReviewStatus.CommandDraft, ReviewStatu.CommandDraft);
        //}
        ///// <summary>
        ///// Test symbol is defined correctly
        ///// </summary>
        //[TestMethod]
        //[Category("DLSymbolServer")]
        //public void Qualifying()
        //{
        //    Assert.AreEqual(DLSymbolServer.ReviewStatus.Qualifying, ReviewStatu.Qualifying);
        //}
        //#endregion
    }
}
