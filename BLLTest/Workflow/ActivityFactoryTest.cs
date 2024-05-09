using System;
using NUnit.Framework;
using Sra.P2rmis.Bll.Workflow;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;

namespace BLLTest.Workflow
{
    /// <summary>
    /// Unit tests for Activity Factory
    /// </summary>
    [TestClass()]
    public class ActivityFactoryTest
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
        #region The Tests
        /// <summary>
        /// Test creating a CheckoutActivity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void CheckoutActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.Checkout);

            Assert.AreEqual(typeof(CheckoutActivity), theActivity.GetType(), "CheckoutActivityTest returned an activity that is not correct type");
        }
        /// <summary>
        /// Test creating a DefaultActivity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void DefaultActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.Default);

            Assert.AreEqual(typeof(P2rmisActivity), theActivity.GetType(), "Returned activity is not correct type");
        }
        /// <summary>
        /// Test creating a CheckinActivity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void CheckinActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.Checkin);
            Assert.AreEqual(typeof(CheckinActivity), theActivity.GetType(), "CheckinActivityTest returned an activity that is not correct type");
        }
        /// <summary>
        /// Test creating a SaveActivity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void SaveActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.Save);
            Assert.AreEqual(typeof(SaveActivity), theActivity.GetType(), "Returned activity is not correct type");
        }
        /// <summary>
        /// Test creating an AssignWorkflowStep activity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void AssignWorkflowStepActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.AssignWorkflowStep);
            Assert.AreEqual(typeof(AssignWorkflowStepActivity), theActivity.GetType(), "Returned activity is not correct type");
        }
        /// <summary>
        /// Test creating an AssignUser activity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void AssignUserActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.AssignUser);
            Assert.AreEqual(typeof(AssignUserActivity), theActivity.GetType(), "Returned activity is not correct type");
        }
        /// <summary>
        /// Test creating an ResetToEdit activity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void ResetToEditActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.ResetToEditApplicationWorkflow);
            Assert.AreEqual(typeof(ResetToEditWorkflowStepActivity), theActivity.GetType(), "Returned activity is not correct type");
        }
        /// <summary>
        /// Test creating an Deactivate activity
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void DeactivateClientReviewCheckinActivityTest()
        {
            P2rmisActivity theActivity = ActivityFactory.CreateActivity(P2rmisActions.DeactivateClientReviewCheckinActivity);
            Assert.AreEqual(typeof(DeactivateClientReviewCheckinActivity), theActivity.GetType(), "Returned activity is not correct type");
        }
        /// <summary>
        /// Test to make sure all activities have a test
        /// </summary>
        [TestMethod()]
        [Category("ActivityFactory")]
        public void NumberTest()
        {
            //
            // The magic number is 7 because that was the number of entries in P2rmisActions when all entries had a unit test.
            // This is merely a reminder that if a new entry to P2rmisActions to add a unit test.
            //
            Assert.AreEqual(9, Enum.GetValues(typeof(P2rmisActions)).Length, "Factory may need additional unit tests");
        }
        #endregion
   }
}
