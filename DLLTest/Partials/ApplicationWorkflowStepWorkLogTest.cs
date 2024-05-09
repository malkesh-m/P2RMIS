using System;
using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for ApplicationWorkflowStepWorkLog.Initialize()
    /// </summary>
    [TestClass()]
    public class ApplicationWorkflowStepWorkLogTest
    {
        #region Attributes
        private int _workflowStepId = 11;
        private int _userId = 44;
        private int _modifiedUserId = 45;

        private int _badWorkflowStepId = -1;
        private int _zeroWorkflowStepId = 0;
        private int _badUserId = -11;
        private int _zeroUserId = 0;

        private ApplicationWorkflowStepWorkLog entity;

        #endregion
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
            entity = new ApplicationWorkflowStepWorkLog();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            entity = null;
        }
        //
        #endregion
        #endregion
        #region The Tests
        /// <summary>
        /// Test initialize
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")] 
        public void InitialzeTest()
        {
            // setup local data
            DateTime beforeTime = DateTime.Now;

            //
            // run the test
            //
            entity.Initialize(_workflowStepId, _userId);
            //
            // this is what should be set
            //
            Assert.AreEqual(_workflowStepId, entity.ApplicationWorkflowStepId);
            Assert.AreEqual(_userId, entity.UserId);
            Assert.That(entity.CheckOutDate, Is.InRange(beforeTime, DateTime.Now));
            Assert.That(entity.CreatedDate, Is.InRange(beforeTime, DateTime.Now));
            Assert.AreEqual(_userId, entity.CreatedBy);
            Assert.That(entity.ModifiedDate, Is.InRange(beforeTime, DateTime.Now));
            Assert.AreEqual(_userId, entity.ModifiedBy);
            //
            // this should not change
            //
            Assert.AreEqual(0, entity.ApplicationWorkflowStepWorkLogId);
            Assert.AreEqual(null,  entity.CheckInDate);
            Assert.AreEqual(null, entity.ApplicationWorkflowStep);
            Assert.IsNotNull(entity.ApplicationWorkflowStepElementContentHistories1);
            Assert.AreEqual(0, entity.ApplicationWorkflowStepElementContentHistories1.Count());
            Assert.AreEqual(null, entity.User);
        }
        /// <summary>
        /// Test initialize with an invalid workflow id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [-1] and userId is [44]")]
        public void InvalidWorkflowStepIdTest()
        {
            //
            // run the test
            //
            entity.Initialize(_badWorkflowStepId, _userId);
            Assert.Fail("Initialize should have thrown an exception");
        }
        /// <summary>
        /// Test initialize with an invalid user id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [11] and userId is [-11]")]
        public void InvalidUserIdTest()
        {
            //
            // run the test
            //
            entity.Initialize(_workflowStepId, _badUserId);
            Assert.Fail("Initialize should have thrown an exception");
        }
        /// <summary>
        /// Test initialize with workflowId zero
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [0] and userId is [44]")]
        public void ZeroWorkflowStepIdTest()
        {
            //
            // run the test
            //
            entity.Initialize(_zeroWorkflowStepId, _userId);
            Assert.Fail("Initialize should have thrown an exception");
        }
        /// <summary>
        /// Test initialize with a zero  user id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [11] and userId is [0]")]
        public void ZeroUserIdTest()
        {
            //
            // run the test
            //
            entity.Initialize(_workflowStepId, _zeroUserId);
            Assert.Fail("Initialize should have thrown an exception");
        }
        /// <summary>
        /// Test initialize with both parameters invalid
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [-1] and userId is [-11]")]
        public void TwoBadParametersTest()
        {
            //
            // run the test
            //
            entity.Initialize(_badWorkflowStepId, _badUserId);
            Assert.Fail("Initialize should have thrown an exception");
        }
        /// <summary>
        /// Test initialize with both parameters zero
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Initialize")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Initialize received an invalid parameter workflowStepId is [0] and userId is [0]")]
        public void TwoZeroParametersTest()
        {
            //
            // run the test
            //
            entity.Initialize(_zeroWorkflowStepId, _zeroUserId);
            Assert.Fail("Initialize should have thrown an exception");
        }
        #endregion
        /// <summary>
        /// Test Complete
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Complete")]
        public void CompleteTest()
        {
            //
            // Local data
            //
            DateTime beforeTime = DateTime.Now;

            //
            // run the test
            //
            entity.Complete(_userId, _modifiedUserId);
            //
            // this is what should be set
            //
            Assert.AreEqual(_userId, entity.UserId);
            Assert.That(entity.CheckInDate, Is.InRange(beforeTime, DateTime.Now));
            Assert.That(entity.ModifiedDate, Is.InRange(beforeTime, DateTime.Now));
            Assert.AreEqual(_modifiedUserId, entity.ModifiedBy);
            //
            // this should not change
            //
            Assert.AreEqual(0, entity.ApplicationWorkflowStepWorkLogId);
            Assert.AreEqual(null, entity.ApplicationWorkflowStep);
            Assert.IsNotNull(entity.ApplicationWorkflowStepElementContentHistories1);
            Assert.AreEqual(0, entity.ApplicationWorkflowStepElementContentHistories1.Count());
            Assert.AreEqual(null, entity.User);
            Assert.AreEqual(0, entity.ApplicationWorkflowStepId);
            Assert.AreEqual("1/1/0001 12:00:00 AM", entity.CheckOutDate.ToString());
            //Assert.AreEqual("1/1/0001 12:00:00 AM", entity.CreatedDate.ToString());
            //Assert.AreEqual(0, entity.CreatedBy);
        }
        /// <summary>
        /// Test Complete with zero for a user id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Complete")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Complete received an invalid parameter: checkinUserId is [0] and modifiedUserId is [0]")]
        public void Complete_ZeroParameterTest()
        {
            //
            // run the test
            //
            entity.Complete(_zeroUserId);
            Assert.Fail("Complete should have thrown an exception"); 
        }
        /// <summary>
        /// Test Complete with a negative user id
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflowStepWorkLog.Complete")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "ApplicationWorkflowStepWorkLog.Complete received an invalid parameter: checkinUserId is [-11] and modifiedUserId is [-11]")]
        public void Complete_NegativeParameterTest()
        {
            //
            // run the test
            //
            entity.Complete(_badUserId);
            Assert.Fail("Complete should have thrown an exception");
        }
    }
}
