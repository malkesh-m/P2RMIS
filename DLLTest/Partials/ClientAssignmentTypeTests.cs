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
    /// Unit tests for ClientAssignmentType entity object extensions
    /// </summary>
    [TestClass()]
    public class ClientAssignmentTypeTests : DllBaseTest
    {
        ClientAssignmentType entity;
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
            entity = new ClientAssignmentType();
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
        [Category("ClientAssignmentType")]
        public void IsReaderTrueTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Reader };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotEditorTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Editor };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotWriterTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Writer };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotSrmTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.SRM };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotClientTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Client };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotSrTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.SR };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotCrTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.CR };
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
        [Category("ClientAssignmentType")]
        public void IsReaderNotCoiTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.COI };
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
        [Category("ClientAssignmentType")]
        public void IsCoiTrueTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.COI };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotEditorTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Editor };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotWriterTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Writer };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotSrmTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.SRM };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotClientTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Client };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotSrTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.SR };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotCrTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.CR };
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
        [Category("ClientAssignmentType")]
        public void IsCoiNotReaderTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentType = new AssignmentType { AssignmentTypeId = AssignmentType.Reader };
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
        #region NullableAssignmentTypeId Tests
        /// <summary>
        /// Test NullableAssignmentTypeId with null AssignmentTypeId
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void NullableAssignmentTypeIdNullAssignmentTypeIdTest()
        {
            //
            // Verify
            //
            Assert.IsNull(entity.NullableAssignmentTypeId, "Null value not returned for NullableAssignmentTypeId");
        }
        /// <summary>
        /// Test NullableAssignmentTypeId with AssignmentTypeId
        /// </summary>
        [TestMethod()]
        [Category("AssignmentType")]
        public void NullableAssignmentTypeIdAssignmentTypeIdTest()
        {
            //
            // Set up local data
            //
            entity.AssignmentTypeId = 6;
            //
            // Verify
            //
            Assert.IsNotNull(entity.NullableAssignmentTypeId, "Null value returned for NullableAssignmentTypeId");
        }
        #endregion
    }
		
}
