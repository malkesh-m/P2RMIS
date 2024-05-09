using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for AssignmentType entity object extensions
    /// </summary>
    [TestClass()]
    public class AssignmentTypeTests : DllBaseTest
    {
        AssignmentType entity;
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
            entity = new AssignmentType();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            entity = null;
            CleanUpMocks();
        }
        #endregion
        #endregion
        #region IsReader Tests
        /// <summary>
        /// Test IsReader (application is reader type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderTrueTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Reader };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.IsTrue(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is reader type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotEditorTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Editor };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Writer type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotWriterTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Writer };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Srm type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotSrmTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.SRM };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Client type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotClientTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Client };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Sr type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotSrTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.SR };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Cr type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotCrTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.CR };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Coi type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsReaderNotCoiTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.COI };
            //
            // Test
            //
            bool result = entity.IsReader;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        #endregion
        #region IsCoi Tests
        /// <summary>
        /// Test IsReader (application is reader type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiTrueTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.COI };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.IsTrue(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is reader type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotEditorTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Editor };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Writer type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotWriterTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Writer };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Srm type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotSrmTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.SRM };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Client type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotClientTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Client };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Sr type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotSrTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.SR };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Cr type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotCrTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.CR };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        /// <summary>
        /// Test IsReader (application is Reader type)
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void IsCoiNotReaderTest()
        {
            //
            // Set up local data
            //
            entity = new AssignmentType { AssignmentTypeId = AssignmentType.Reader };
            //
            // Test
            //
            bool result = entity.IsCoi;
            //
            // Verify
            //
            Assert.False(result, "Unexpected value returned");
        }
        #endregion
        #region Default Tests
        /// <summary>
        /// Test  entity's default set up
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void DefaultTest()
        {
            //
            // Verify
            //
            Assert.NotNull(AssignmentType.Default, "AssignmentType Default not initialized");
        }
        #endregion
    }
}
