using System;
using NUnit.Framework;
using Sra.P2rmis.Dal;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace DLLTest.Partials
{
    /// <summary>
    /// Unit tests for WorkflowStep entity extension
    /// </summary>
    [TestClass]
    public class WorkflowStepTest
    {
        #region Constants & such
        private int _stepId_1 = 3;
        private int _workflowId_1 = 44;
        private string _stepName_1 = "this is my step name";
        private int _stepOrder_1 = 6;
        private bool _activeDefault_1 = true;
        private int _userId_1 = 33;
        private int _userId_2 = 1111;
        private int _userId_3 = 567890;
        private DateTime _DateTime_1 = new DateTime(2013, 12, 4);
        private DateTime _DateTime_2 = new DateTime(2010, 6, 7);

        private WorkflowStep aWorkflowStep1;
        private Workflow aWorkflow = new Workflow();

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
            aWorkflowStep1 = new WorkflowStep
            {
                WorkflowStepId = _stepId_1,
                WorkflowId = _workflowId_1,
                StepName = _stepName_1,
                StepOrder = _stepOrder_1,
                ActiveDefault = _activeDefault_1,
                CreatedBy = _userId_1,
                CreatedDate = _DateTime_1,
                ModifiedBy = _userId_2,
                ModifiedDate = _DateTime_2,
                Workflow = aWorkflow
            };
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        public void MyTestCleanup()
        {
            aWorkflowStep1 = null;
        }
        //
        #endregion
        #endregion
        #region The Tests
        /// <summary>
        /// Test a good copy
        /// </summary>
        [TestMethod]
        [Category("WorkflowStep")]
        public void CopyTest()
        {
            //
            // Local Data
            //
            DateTime beforeTime = DateTime.Now;

            //
            // we have ourselves a workflow step so copy it.
            //
            WorkflowStep w = aWorkflowStep1.Copy(_userId_3);
            //
            // Now check to make sure copy did what it was supposed to.
            //
            Assert.AreEqual(0 , w.WorkflowId,"Have a workflow id but should not");
            Assert.AreEqual(0 , w.WorkflowStepId, "Have a workflow step id but should not");
            Assert.AreEqual(_stepName_1, w.StepName, "StepName not set correctly");
            Assert.AreEqual(_stepOrder_1, w.StepOrder, "Step order not copied correctly");
            Assert.AreEqual(_activeDefault_1, w.ActiveDefault, "ActiveDefault not copied correctly");
            Assert.AreEqual(_userId_3, w.CreatedBy, "Created by user id not set correctly");
            Assert.That(w.CreatedDate, Is.InRange(beforeTime, DateTime.Now), "Created Date not set correctly");
            Assert.AreEqual(_userId_3, w.ModifiedBy, "Modified by user id not set correctly");
            Assert.That(w.ModifiedDate, Is.InRange(beforeTime, DateTime.Now), "Modified date not set correctly");
            Assert.AreEqual(null, w.Workflow, "Workflow should not be set");

        }
        #endregion
    }
}
