using NUnit.Framework;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;

namespace BLLTest
{
    /// <summary>
    /// Unit tests for the Report Service.
    /// </summary>
    [TestClass()]
    public class ReportServiceTest
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
    }

    public class TestReportService : ReportService
    {
        public TestReportService(IUnitOfWork unit) 
        {
            UnitOfWork = unit;
        }
    }
    public class TestCriteriaService : CriteriaService
    {
        public TestCriteriaService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }
    }

}
