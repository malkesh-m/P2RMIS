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
using Sra.P2rmis.Dal.ResultModels.Reports;

namespace DLLTest.ResultModels
{
    /// <summary>
    /// Unit tests for the data layer result model returning fiscal years for one or more programs.
    /// </summary>
    [TestClass()]
    public class ReportFiscalYearResultModelTest
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
            // DELETE ME PLEASE !!
            //ReportFiscalYearResultModel model = new ReportFiscalYearResultModel();
            //Assert.IsNotNull(model.ModelList, "Constructor did not create a ModelList");
        }
        #endregion
    }
}
