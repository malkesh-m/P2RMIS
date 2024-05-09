using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for PanelApplicationSummary extensions
    /// </summary>
    [TestClass()]
    public class PanelApplicationSummaryTests : DllBaseTest
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            InitializeMocks();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            CleanUpMocks();
        }
        //
        #endregion
        #endregion
        #region MyRegion
        private int _panelApplicationId = 44;
        private string _summaryText = "lets put something here if you please";
        #endregion
        #region The Tests
        /// <summary>
        /// Test Populate
        /// </summary>
        [TestMethod()]
        [Category("PanelApplicationSummary")]
        public void PopulateTest()
        {
            //
            // Set up local data
            //
            PanelApplicationSummary entity = new PanelApplicationSummary();
            //
            // Test
            //
            entity.Populate(_panelApplicationId, _summaryText);
            //
            // Verify
            //
            Assert.AreEqual(_panelApplicationId, entity.PanelApplicationId, "PanelApplicationId was not set but should have been");
            Assert.AreEqual(_summaryText, entity.SummaryText, "SummaryText was not set but should have been");
            Assert.IsNull(entity.CreatedBy, "CreatedBy was  set but should not have been");
            Assert.IsNull(entity.CreatedDate, "CreatedDate was  set but should not have been");
            Assert.IsNull(entity.ModifiedBy, "ModifiedBy was  set but should not have been");
            Assert.IsNull(entity.ModifiedDate, "ModifiedDate was  set but should not have been");
            Assert.IsNull(entity.DeletedBy, "DeletedBy was  set but should not have been");
            Assert.IsNull(entity.DeletedDate, "DeletedDate was  set but should not have been");
            Assert.IsNull(entity.PanelApplication, "PanelApplication was  set but should not have been");
            Assert.AreEqual(_notSet, entity.PanelApplicationSummaryId, "PanelApplicationSummaryId was set but should not have been");
        }
		
        #endregion
    }
}
