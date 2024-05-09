using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.ApplicationScoring
{
    [TestClass()]
    public partial class ApplicationScoringTests : BLLBaseTest
    {
        /* These need updated if we want to use them
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
        #region DeterminePhaseState Tests
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        //public void Phase1()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase01, result, "Should not be here - should throw exception");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase2()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase02, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase3()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase03, result, "Should not be here - should throw exception");
        }

        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase4()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase04, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase5()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase05, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase6()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase06, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase7()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase07, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase8()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase08, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase9()
        {
            //
            // Set up local data
            //

            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase09, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase10()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase10, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase11()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase11, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase12()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase12, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase13()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase13, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase14()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase14, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase15()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase15, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase16()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase16, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        ////[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        //public void Phase17()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    bool assignedToApplication = true;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase17, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase18()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase18, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase19()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase19, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase20()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase20, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase21()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase21, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase22()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase22, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase23()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase23, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase24()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase24, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase25()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase25, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase26()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase26, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        ////[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        //public void Phase27()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase27, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase28()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase28, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase29()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase29, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase30()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Preliminary;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotApplicable;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase30, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase31()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = true;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase31, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        //[TestMethod()]
        //[Category("PhaseStatus")]
        //public void Phase32()
        //{
        //    //
        //    // Set up local data
        //    //
        //    int phase = StepType.Indexes.Preliminary;
        //    bool ReOpened = false;
        //    ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
        //    ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
        //    bool assignedToApplication = false;
        //    //
        //    // Test
        //    //
        //    var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
        //    //
        //    // Verify
        //    //
        //    Assert.AreEqual(StateResult.Phase32, result, "result was not expected enum value");
        //}
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase33()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase33, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase34()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase34, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase35()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase35, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase36()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase36, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase37()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase37, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase38()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase38, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase39()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase39, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase40()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase40, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase41()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase41, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase42()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase42, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase43()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase43, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase44()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase44, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase45()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase45, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase46()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase46, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase47()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase47, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase48()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase48, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase49()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase49, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase50()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase50, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase51()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase51, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase52()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase52, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase53()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase53, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase54()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase54, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase55()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase55, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase56()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase56, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase57()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase57, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase58()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase58, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase59()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase59, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase60()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase60, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase61()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase61, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase62()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase62, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase63()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase63, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        //[ExpectedException(typeof(StateException), ExpectedMessage = "This should not happen")]
        public void Phase64()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Revised;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase64, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase65()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase65, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase66()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase66, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase67()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase67, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase68()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase68, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase69()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase69, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase70()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase70, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase71()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase71, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase72()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase72, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase73()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase73, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase74()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase74, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase75()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase75, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase76()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase76, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase77()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase77, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase78()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase78, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase79()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase79, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase80()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = true;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase80, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase81()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase81, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase82()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase82, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase83()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase83, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase84()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase84, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase85()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase85, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase86()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase86, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase87()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase87, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase88()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase88, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase89()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase89, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase90()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase90, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase91()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase91, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase92()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase92, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase93()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase93, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase94()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase94, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase95()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = true;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase95, result, "result was not expected enum value");
        }
        /// <summary>
        /// Test DeterminePhaseState
        /// </summary>
        [TestMethod()]
        [Category("PhaseStatus")]
        public void Phase96()
        {
            //
            // Set up local data
            //
            int phase = StepType.Indexes.Final;
            bool ReOpened = false;
            ApplicationScoringService.CritiqueSubmittal applicationCritiqueState = ApplicationScoringService.CritiqueSubmittal.Submitted;
            ApplicationScoringService.CritiqueSubmittal phaseCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            ApplicationScoringService.CritiqueSubmittal reviewersPreviousCritiqueState = ApplicationScoringService.CritiqueSubmittal.NotSubmitted;
            bool assignedToApplication = false;
            //
            // Test
            //
            var result = ApplicationScoringService.DeterminePhaseState(phase, ReOpened, applicationCritiqueState, phaseCritiqueState, reviewersPreviousCritiqueState, assignedToApplication);
            //
            // Verify
            //
            Assert.AreEqual(StateResult.Phase96, result, "result was not expected enum value");
        }
        #endregion
          */
    }
}
