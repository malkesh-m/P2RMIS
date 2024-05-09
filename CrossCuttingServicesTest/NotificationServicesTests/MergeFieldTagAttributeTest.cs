using NUnit.Framework;
using Sra.P2rmis.CrossCuttingServices;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace NotificationServicesTest
{
    
    
    /// <summary>
    ///This is a test class for MergeFieldTagAttributeTest and is intended
    ///to contain all MergeFieldTagAttributeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MergeFieldTagAttributeTest
    {


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


        /// <summary>
        ///A test for MergeFieldTagAttribute Constructor
        ///</summary>
        [TestMethod()]
        public void MergeFieldTagAttributeConstructorTest()
        {
            string tag = "test";
            MergeFieldTagAttribute target = new MergeFieldTagAttribute(tag);
            Assert.AreEqual(target.Tag, MergeFieldTagAttribute.MergeFieldOpeningDelimeter + tag + MergeFieldTagAttribute.MergeFieldClosingDelimiter);
        }

        /// <summary>
        ///A test for Tag
        ///</summary>
        [TestMethod()]
        public void TagTest()
        {
            string tag = "test";
            MergeFieldTagAttribute target = new MergeFieldTagAttribute(tag);
            string actual = target.Tag;
            Assert.AreEqual(actual, MergeFieldTagAttribute.MergeFieldOpeningDelimeter + tag + MergeFieldTagAttribute.MergeFieldClosingDelimiter);
        }
    }
}
