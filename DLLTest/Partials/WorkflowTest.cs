using System;
using System.Collections.Generic;
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
    /// Unit tests for Workflow entity extension
    /// </summary>
    [TestClass]
    public class WorkflowTest
    {
        #region Constants & such
        private int _stepId_1 = 3;
        private int _stepId_2 = 8;
        private int _stepId_3 = 14;
        private int _workflowId_1 = 44;
        private string _stepName_1 = "this is my step name 1";
        private string _stepName_2 = "this is my step name 2";
        private string _stepName_3 = "this is my step name 3";
        private int _stepOrder_1 = 1;
        private int _stepOrder_2 = 2;
        private int _stepOrder_3 = 3;
        private bool _activeDefault_1 = true;
        private bool _activeDefault_2 = false;
        private bool _activeDefault_3 = true;
        private int _userId_1 = 33;
        private int _userId_2 = 1111;
        private int _userId_3 = 567890;
        private DateTime _DateTime_1 = new DateTime(2013, 12, 4);
        private DateTime _DateTime_2 = new DateTime(2010, 6, 7);

        private WorkflowStep aWorkflowStep1;
        private WorkflowStep aWorkflowStep2;
        private WorkflowStep aWorkflowStep3;
        private Workflow aWorkflow;
        private int _clientId = 222;
        private string _workflowName = "What a workflow name";
        private string _workflowDescription = "Tell me about it";

        private Client aClient = new Client();

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
            aWorkflowStep2 = new WorkflowStep
            {
                WorkflowStepId = _stepId_2,
                WorkflowId = _workflowId_1,
                StepName = _stepName_2,
                StepOrder = _stepOrder_2,
                ActiveDefault = _activeDefault_2,
                CreatedBy = _userId_1,
                CreatedDate = _DateTime_1,
                ModifiedBy = _userId_2,
                ModifiedDate = _DateTime_2,
                Workflow = aWorkflow
            };
            aWorkflowStep3 = new WorkflowStep
            {
                WorkflowStepId = _stepId_3,
                WorkflowId = _workflowId_1,
                StepName = _stepName_3,
                StepOrder = _stepOrder_3,
                ActiveDefault = _activeDefault_3,
                CreatedBy = _userId_1,
                CreatedDate = _DateTime_1,
                ModifiedBy = _userId_2,
                ModifiedDate = _DateTime_2,
                Workflow = aWorkflow
            };
            //
            // now initialize the workflow
            //
            aWorkflow = new Workflow
            {
                WorkflowId = _workflowId_1,
                ClientId  = _clientId,
                WorkflowName  = _workflowName,
                WorkflowDescription  = _workflowDescription,
                CreatedBy  = _userId_1,
                CreatedDate = _DateTime_1,
                ModifiedBy  = _userId_2,
                ModifiedDate  = _DateTime_2,

                Client = aClient,
                ApplicationWorkflows   = new List<ApplicationWorkflow>()

            };
            aWorkflow.WorkflowSteps.Add(aWorkflowStep1);
            aWorkflow.WorkflowSteps.Add(aWorkflowStep2);
            aWorkflow.WorkflowSteps.Add(aWorkflowStep3);

        }
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
        /// Test a copy with workflow steps
        /// </summary>
        [TestMethod]
        [Category("Workflow")]
        public void CopyTest()
        {
            //
            // Local Data
            //
            DateTime beforeTime = DateTime.Now;

            Workflow w = aWorkflow.Copy(_userId_3);
            //
            // Now check that the workflow data has bee copied
            // and new stuff created where appropriate.
            //
            Assert.AreEqual(0, w.WorkflowId, "WorkflowId was set to a value and it should not have been");
            Assert.AreEqual(_clientId, w.ClientId, "Client id was not set");
            Assert.IsTrue(w.WorkflowName.StartsWith(_workflowName), "Workflow name not properly copied");
            Assert.IsFalse(w.WorkflowName.Length == _workflowName.Length, "Workflow name does not have append text");
            Assert.AreEqual(_workflowDescription, w.WorkflowDescription, "Workflow description not copied correctly");
            Assert.AreEqual(_userId_3, w.CreatedBy, "Created by user id not set correctly");
            Assert.That(w.CreatedDate, Is.InRange(beforeTime, DateTime.Now), "Created date not set correctly");
            Assert.AreEqual(_userId_3, w.ModifiedBy, "Modified by user id not set correctly");
            Assert.That(w.ModifiedDate, Is.InRange(beforeTime, DateTime.Now), "Modified date not set correctly");
            Assert.IsNull(w.Client, "Client not set correctly");

            Assert.AreEqual(0, w.ApplicationWorkflows.Count, "Collection of applications were not initialized properly");
            Assert.AreEqual(3, w.WorkflowSteps.Count, "Workflow application steps not properly copied"); 
        }
        #endregion
    }
}
