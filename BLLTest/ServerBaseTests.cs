using System;
using NUnit.Framework;
using Sra.P2rmis.Bll;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
		

namespace BLLTest
{
    /// <summary>
    /// Unit tests for ServerBase 
    /// </summary>
    [TestClass()]
    public class ServerBaseTests: BLLBaseTest
    {
        ServerBaseTestService theServerBaseTestService;
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
            theServerBaseTestService = new ServerBaseTestService();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            theServerBaseTestService = null;
            CleanUpMocks();
        }
        //
        #endregion
        #endregion
        #region ValidateNullableInteger Tests
        /// <summary>
        /// Test for a null value
        /// </summary>
        [TestMethod()]
        [Category("ServerBase")]
        public void NullValueTest()
        {
            //
            // Set up local data
            //
            this.theServerBaseTestService.ValidateNullableInt(null, "caller", "parameter name");
            //
            // If we get here it is valid 
            //
        }
        /// <summary>
        /// Test for a negative value
        /// </summary>
        [TestMethod()]
        [Category("ServerBase")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "caller detected an invalid parameter: parameter name was -3")]
        public void NegativeValueTest()
        {
            //
            // Set up local data
            //
            this.theServerBaseTestService.ValidateNullableInt(-3, "caller", "parameter name");
            //
            // If we get here it is not valid 
            //
            Assert.Fail("Should have thrown an exception");
        }
        /// <summary>
        /// Test for a zero value
        /// </summary>
        [TestMethod()]
        [Category("ServerBase")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "caller detected an invalid parameter: parameter name was 0")]
        public void ZeroValueTest()
        {
            //
            // Set up local data
            //
            this.theServerBaseTestService.ValidateNullableInt(0, "caller", "parameter name");
            //
            // If we get here it is not valid 
            //
            Assert.Fail("Should have thrown an exception");
        }
        /// <summary>
        /// Test for a valid value
        /// </summary>
        [TestMethod()]
        [Category("ServerBase")]
        public void ValueTest()
        {
            //
            // Set up local data
            //
            this.theServerBaseTestService.ValidateNullableInt(10, "caller", "parameter name");
            //
            // If we get here it is valid 
            //
        }
        #endregion

        /// <summary>
        /// Test wrapper to test protected methods
        /// </summary>
        public class ServerBaseTestService : ServerBase
        {
            public void ValidateNullableInt(int? value, string caller, string paramter)
            {
                base.ValidateNullableInteger(value, caller, paramter);
            }
        }
    }
		
}
