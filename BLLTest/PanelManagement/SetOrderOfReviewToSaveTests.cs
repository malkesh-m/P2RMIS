using System;
using NUnit.Framework;
using Sra.P2rmis.Bll.PanelManagement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.PanelManagement
{
    /// <summary>
    /// Unit tests for 
    /// </summary>
    [TestClass()]
    public class SetOrderOfReviewToSaveTests: BLLBaseTest
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
        #region SetOrderOfReviewToSave Tests
        /// <summary>
        /// Test valid combination
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        public void SetOrderOfReviewToSaveOrderAndNotTriagedTest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave("3", 2, false);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should return.
        }
        /// <summary>
        /// Test valid combination
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        public void SetOrderOfReviewToSaveNoOrderAndTriagedTest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave("3", null, true);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should return.
        }
        /// <summary>
        /// Test invalid log number - null
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetOrderOfReviewToSave.Validate()  Invalid LogNumber detected; null, empty string or whitespace")]
        public void SetOrderOfReviewToSaveNullLogNumberTest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave(null, null, true);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should throw an exception.
        }
        /// <summary>
        /// Test invalid log number - null
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetOrderOfReviewToSave.Validate()  Invalid LogNumber detected; null, empty string or whitespace")]
        public void SetOrderOfReviewToSaveEmptyLogNumberTest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave(string.Empty, null, true);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should throw an exception.
        }
        /// <summary>
        /// Test invalid log number - null
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetOrderOfReviewToSave.Validate()  Invalid LogNumber detected; null, empty string or whitespace")]
        public void SetOrderOfReviewToSaveWhitespaceLogNumberTest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave("       ", null, true);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should throw an exception.
        }
        /// <summary>
        /// Test invalid combination
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetOrderOfReviewToSave.Validate()  Invalid combination detected NewOrder:  IsTriaged: False")]
        public void SetOrderOfReviewToSaveInvalidCombinationATest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave("6", null, false);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should throw an exception.
        }
        /// <summary>
        /// Test invalid combination
        /// </summary>
        [TestMethod()]
        [Category("SetOrderOfReviewToSave")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SetOrderOfReviewToSave.Validate()  Invalid combination detected NewOrder: 19 IsTriaged: True")]
        public void SetOrderOfReviewToSaveInvalidCombinationBTest()
        {
            //
            // Set up local data
            //
            SetOrderOfReviewToSave order = new SetOrderOfReviewToSave("6", 19, true);
            //
            // Test
            //
            order.Validate();
            //
            // Nothing to validate, it should throw an exception.
        }
        #endregion
    }
		
}
