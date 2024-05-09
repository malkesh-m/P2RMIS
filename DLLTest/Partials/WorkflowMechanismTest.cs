using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for WorkflowMechanism entity extensions
    /// </summary>
    [TestClass()]
    public class WorkflowMechanismTest: DllBaseTest
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

        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

        }
        //
        #endregion
        #endregion
        #region Exists Tests
        /// <summary>
        /// Test Exists = Yes it does
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Exist")]
        public void ExistYesTest()
        {
            WorkflowMechanism entity = new WorkflowMechanism();
            bool result = WorkflowMechanism.Exists(entity);

            Assert.IsTrue(result, "WorkflowMechanism does not exist but it should have");
        }   
        /// <summary>
        /// Test Exists = No it does not
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Exist")]
        public void ExistNoTest()
        {
            bool result = WorkflowMechanism.Exists(null);

            Assert.False(result, "WorkflowMechanism exists but it should not");
        }
        #endregion
        #region Changed() Tests
        /// <summary>
        /// Test WorkflowMechanism did not changed
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Changed")]
        public void NotChangedTest()
        {
            var entity = new WorkflowMechanism();
            entity.Populate(10, 44, 12, 10);

            bool result = entity.Changed(44, 12);

            Assert.False(result, "WorkflowMechanism changed but it should not");
        }
        /// <summary>
        /// Test WorkflowMechanism changed
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Changed")]
        public void ChangedTest()
        {
            var entity = new WorkflowMechanism();
            entity.Populate(10, 44, 12, 20);

            bool result = entity.Changed(9, 11);

            Assert.True(result, "WorkflowMechanism did not changed but it should have");
        }
        #endregion
        #region Populate tests
        /// <summary>
        /// Test WorkflowMechanism Populate
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Populate")]
        public void PopulateMechanismTest()
        {
            int mId = 10;
            int uId = 77;

            var entity = new WorkflowMechanism();
            entity.Populate(mId, 44, 123, uId);

            Assert.AreEqual(mId, entity.MechanismId, "MechanismId did not properly set up");
            Assert.AreEqual(uId, entity.ModifiedBy, "Modified by was not property set");
        }
        /// <summary>
        /// Test WorkflowMechanism Populate
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Populate")]
        public void PopulateWorkflowTest()
        {
            int wId = 110;
            int uId = 77;

            var entity = new WorkflowMechanism();
            entity.Populate(10, wId, 124, uId);

            Assert.AreEqual(wId, entity.WorkflowId, "WorkflowId did not properly set up");
            Assert.AreEqual(uId, entity.ModifiedBy, "Modified by was not property set");
        }
        /// <summary>
        /// Test WorkflowMechanism Populate
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Populate")]
        public void PopulateMechanismWorkflowTest()
        {
            int mId = 10;
            int wId = 110;
            int rId = 765;
            int uId = 77;

            var entity = new WorkflowMechanism();
            entity.Populate(mId, wId, rId, uId);

            Assert.AreEqual(wId, entity.WorkflowId, "WorkflowId did not properly set up");
            Assert.AreEqual(mId, entity.MechanismId, "MechanismId did not properly set up");
            Assert.AreEqual(rId, entity.ReviewStatusId, "ReviewStatusId did not properly set up");
            Assert.AreEqual(uId, entity.ModifiedBy, "Modified by was not property set");
        }
        #endregion
        #region Create tests
        /// <summary>
        /// Test WorkflowMechanism Populate
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Create")]
        public void CreateMechanismTest()
        {
            int mId = 10;
            int uId = 77;

            var entity = new WorkflowMechanism();
            entity.Create(mId, 44, 123, uId);

            Assert.AreEqual(mId, entity.MechanismId, "MechanismId did not properly set up");
            Assert.AreEqual(uId, entity.ModifiedBy, "Modified by was not property set");
            Assert.AreEqual(uId, entity.CreatedBy, "Created by was not property set");
        }
        /// <summary>
        /// Test WorkflowMechanism Populate
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Create")]
        public void CreateWorkflowTest()
        {
            int wId = 110;
            int uId = 77;

            var entity = new WorkflowMechanism();
            entity.Create(10, wId, 124, uId);

            Assert.AreEqual(wId, entity.WorkflowId, "WorkflowId did not properly set up");
            Assert.AreEqual(uId, entity.ModifiedBy, "Modified by was not property set");
            Assert.AreEqual(uId, entity.CreatedBy, "Created by was not property set");
        }
        /// <summary>
        /// Test WorkflowMechanism Populate
        /// </summary>
        [TestMethod()]
        [Category("WorkflowMechanism.Create")]
        public void CreateMechanismWorkflowTest()
        {
            int mId = 10;
            int wId = 110;
            int rId = 765;
            int uId = 77;

            var entity = new WorkflowMechanism();
            entity.Create(mId, wId, rId, uId);

            Assert.AreEqual(wId, entity.WorkflowId, "WorkflowId did not properly set up");
            Assert.AreEqual(mId, entity.MechanismId, "MechanismId did not properly set up");
            Assert.AreEqual(rId, entity.ReviewStatusId, "ReviewStatusId did not properly set up");
            Assert.AreEqual(uId, entity.ModifiedBy, "Modified by was not property set");
            Assert.AreEqual(uId, entity.CreatedBy, "Created by was not property set");
        }
        #endregion
    }
}
