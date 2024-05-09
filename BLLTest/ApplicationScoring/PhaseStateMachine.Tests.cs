using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace BLLTest.ApplicationScoring
{
    [TestClass()]
    public class PhaseStateMachineTests
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
        #endregion
        #endregion
        #region Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase101_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, true, true, true);
            Assert.AreEqual(StateResult.Phase101, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase102_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, true, true, true);  //	2
            Assert.AreEqual(StateResult.Phase102, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase103_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, true, false, true);  //	3
            Assert.AreEqual(StateResult.Phase103, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase104_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, true, false, true);  //	4
            Assert.AreEqual(StateResult.Phase104, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase105_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, true, true, false);  //	5
            Assert.AreEqual(StateResult.Phase105, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase106_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, true, true, false);  //	6
            Assert.AreEqual(StateResult.Phase106, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase107_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, true, false, false);  //	7
            Assert.AreEqual(StateResult.Phase107, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase108_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, true, false, false);  //	8
            Assert.AreEqual(StateResult.Phase108, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase109_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, false, true, true);  //	9
            Assert.AreEqual(StateResult.Phase109, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase110_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, false, true, true);  //	10
            Assert.AreEqual(StateResult.Phase110, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase111_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, false, false, true);  //	11
            Assert.AreEqual(StateResult.Phase111, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase112_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, false, false, true);  //	12
            Assert.AreEqual(StateResult.Phase112, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase113_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, false, true, false);  //	13
            Assert.AreEqual(StateResult.Phase113, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase114_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, false, true, false);  //	14
            Assert.AreEqual(StateResult.Phase114, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase115_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, true, false, false, false);  //	15
            Assert.AreEqual(StateResult.Phase115, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase116_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(false, false, false, false, false);  //	16
            Assert.AreEqual(StateResult.Phase116, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase117_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, true, true, true);  //	17
            Assert.AreEqual(StateResult.Phase117, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase118_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, false, true, true);  //	18
            Assert.AreEqual(StateResult.Phase118, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase119_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, true, false, true);  //	19
            Assert.AreEqual(StateResult.Phase119, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase120_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, false, false, true);  //	20
            Assert.AreEqual(StateResult.Phase120, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase121_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, true, true, false);  //	21
            Assert.AreEqual(StateResult.Phase121, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase122_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, false, true, false);  //	22
            Assert.AreEqual(StateResult.Phase122, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase123_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, true, false, false);  //	23
            Assert.AreEqual(StateResult.Phase123, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase124_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, true, false, false, false);  //	24
            Assert.AreEqual(StateResult.Phase124, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase125_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, true, false, false);  //	25
            Assert.AreEqual(StateResult.Phase125, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase126_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, false, false, false);  //	26
            Assert.AreEqual(StateResult.Phase126, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase127_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, true, false, true);  //	27
            Assert.AreEqual(StateResult.Phase127, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase128_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, false, false, true);  //	28
            Assert.AreEqual(StateResult.Phase128, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase129_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, true, true, true);  //	29
            Assert.AreEqual(StateResult.Phase129, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase130_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, false, true, true);  //	30
            Assert.AreEqual(StateResult.Phase130, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase131_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, true, true, false);  //	31
            Assert.AreEqual(StateResult.Phase131, result);
        }
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PhaseStateMachine")]
        public void Phase132_Test()
        {
            //
            // Test & verify
            //
            var result = PhaseStateMachine.PhaseState(true, false, false, true, false);  //	32
            Assert.AreEqual(StateResult.Phase132, result);
        }
        #endregion
    }
}
