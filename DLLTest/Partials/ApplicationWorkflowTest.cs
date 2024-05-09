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
    /// Unit tests for ApplicationWorkflow entity extensions
    /// </summary>
    [TestClass()]
    public class ApplicationWorkflowTest
    {
        #region Attributes etc.
        private ApplicationWorkflow _workflow;
        private static int _so1 = 1;
        private static int _so2 = 2;
        private static int _so3 = 3;

        private static int _sid1 = 100;
        private static int _sid2 = 200;
        private static int _sid3 = 300;

        private ApplicationWorkflowStep _step1 = new ApplicationWorkflowStep { Active = true, Resolution = false, StepOrder = _so1, ApplicationWorkflowStepId = _sid1 };
        private ApplicationWorkflowStep _step2 = new ApplicationWorkflowStep { Active = true, Resolution = false, StepOrder = _so2, ApplicationWorkflowStepId = _sid2 };
        private ApplicationWorkflowStep _step3 = new ApplicationWorkflowStep { Active = true, Resolution = false, StepOrder = _so3, ApplicationWorkflowStepId = _sid3 };

        private int _userId = 11;
        private int _stepId1 = 7010;
        private int _stepId2 = 7020;
        private int _stepId3 = 7030;
        private int _stepId4 = 7040;
        private int _stepId5 = 7050;
        private int _stepId6 = 7060;
        private int _stepId7 = 7070;
        private int _stepId8 = 7080;
        private int _stepId9 = 7009;
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
            this._workflow = new ApplicationWorkflow();
            this._workflow.ApplicationWorkflowSteps.Add(_step1);
            this._workflow.ApplicationWorkflowSteps.Add(_step2);
            this._workflow.ApplicationWorkflowSteps.Add(_step3);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            this._workflow = null;
        }
        //
        #endregion
        #endregion
        #region The CurrentStep Tests
        /// <summary>
        /// Test CurrentStep where first entry
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.CurrentStep")]
        public void CurrentStep_At1Test()
        {
            var x = this._workflow.CurrentStep();

            Assert.IsNotNull(x, "CurrentStep did not return a step and it should have");
            Assert.AreEqual(_so1, x.StepOrder, "CurrentStep did not return correct step");
        }
        /// <summary>
        /// Test CurrentStep where second entry
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.CurrentStep")]
        public void CurrentStep_At2Test()
        {
            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep { StepOrder = _so1, Resolution = true, Active = true };
            this._workflow.ApplicationWorkflowSteps.Clear();
            this._workflow.ApplicationWorkflowSteps.Add(s1);
            this._workflow.ApplicationWorkflowSteps.Add(_step2);
            this._workflow.ApplicationWorkflowSteps.Add(_step3);
            var x = this._workflow.CurrentStep();

            Assert.IsNotNull(x, "CurrentStep did not return a step and it should have");
            Assert.AreEqual(_so2, x.StepOrder, "CurrentStep did not return correct step");
        }
        /// <summary>
        /// Test CurrentStep where third entry
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.CurrentStep")]
        public void CurrentStep_At3Test()
        {
            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep { StepOrder = _so1, Resolution = true, Active = true };
            ApplicationWorkflowStep s2 = new ApplicationWorkflowStep { StepOrder = _so2, Resolution = true, Active = true };
            this._workflow.ApplicationWorkflowSteps.Clear();
            this._workflow.ApplicationWorkflowSteps.Add(s1);
            this._workflow.ApplicationWorkflowSteps.Add(s2);
            this._workflow.ApplicationWorkflowSteps.Add(_step3);
            var x = this._workflow.CurrentStep();

            Assert.IsNotNull(x, "CurrentStep did not return a step and it should have");
            Assert.AreEqual(_so3, x.StepOrder, "CurrentStep did not return correct step");
        }
        /// <summary>
        /// Test CurrentStep where nothing returned
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.CurrentStep")]
        public void CurrentStep_NoneTest()
        {
            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep { StepOrder = _so1, Resolution = true, Active = true };
            ApplicationWorkflowStep s2 = new ApplicationWorkflowStep { StepOrder = _so2, Resolution = true, Active = true };
            ApplicationWorkflowStep s3 = new ApplicationWorkflowStep { StepOrder = _so3, Resolution = true, Active = true };
            this._workflow.ApplicationWorkflowSteps.Clear();
            this._workflow.ApplicationWorkflowSteps.Add(s1);
            this._workflow.ApplicationWorkflowSteps.Add(s2);
            this._workflow.ApplicationWorkflowSteps.Add(s3);
            var x = this._workflow.CurrentStep();

            Assert.IsNull(x, "CurrentStep did  return a step and it should not have");
        }        
        #endregion
        #region The IsComplete Tests
        /// <summary>
        /// Test IsComplete on a complete workflow
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.IsComplete")]
        public void IsComplete_YesTest()
        {
            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep { StepOrder = _so1, Resolution = true, Active = true };
            ApplicationWorkflowStep s2 = new ApplicationWorkflowStep { StepOrder = _so2, Resolution = true, Active = true };
            ApplicationWorkflowStep s3 = new ApplicationWorkflowStep { StepOrder = _so3, Resolution = true, Active = true };
            this._workflow.ApplicationWorkflowSteps.Clear();
            this._workflow.ApplicationWorkflowSteps.Add(s1);
            this._workflow.ApplicationWorkflowSteps.Add(s2);
            this._workflow.ApplicationWorkflowSteps.Add(s3);

            var x = this._workflow.IsComplete();

            Assert.IsTrue(x, "IsComplete did not find a completed workflow and should have");
        }
        /// <summary>
        /// Test IsComplete on a complete workflow
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.IsComplete")]
        public void IsComplete_NoTest()
        {
            var x = this._workflow.IsComplete();

            Assert.IsFalse(x, "IsComplete did find a completed workflow and should not have");
        }
        /// <summary>
        /// Test IsComplete on a incomplete workflow with inactive entry
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.IsComplete")]
        public void IsComplete_NotActiveTest()
        {
            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep { StepOrder = _so1, Resolution = true,  Active = true };
            ApplicationWorkflowStep s2 = new ApplicationWorkflowStep { StepOrder = _so2, Resolution = false, Active = false };
            ApplicationWorkflowStep s3 = new ApplicationWorkflowStep { StepOrder = _so3, Resolution = true,  Active = true };
            this._workflow.ApplicationWorkflowSteps.Clear();
            this._workflow.ApplicationWorkflowSteps.Add(s1);
            this._workflow.ApplicationWorkflowSteps.Add(s2);
            this._workflow.ApplicationWorkflowSteps.Add(s3);

            var x = this._workflow.IsComplete();

            Assert.IsTrue(x, "IsComplete did not find a completed workflow and should have");
        }
        /// <summary>
        /// Test IsComplete on an un-complete workflow with an inactive entry
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.IsComplete")]
        public void IsComplete_NotActiveGoodTest()
        {
            ApplicationWorkflowStep s1 = new ApplicationWorkflowStep { StepOrder = _so1, Resolution = true,  Active = true  };
            ApplicationWorkflowStep s2 = new ApplicationWorkflowStep { StepOrder = _so2, Resolution = false, Active = false };
            ApplicationWorkflowStep s3 = new ApplicationWorkflowStep { StepOrder = _so3, Resolution = false, Active = true  };
            this._workflow.ApplicationWorkflowSteps.Clear();
            this._workflow.ApplicationWorkflowSteps.Add(s1);
            this._workflow.ApplicationWorkflowSteps.Add(s2);
            this._workflow.ApplicationWorkflowSteps.Add(s3);

            var x = this._workflow.IsComplete();

            Assert.IsFalse(x, "IsComplete did find a completed workflow and should not have");
        }
        #endregion
        #region Complete Tests
        /// <summary>
        /// Test Complete
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.Complete")]
        public void Complete_Test()
        {
            //
            // Set up for the test
            //

            DateTime beforeTime = DateTime.Now;
            ApplicationWorkflow wf = new ApplicationWorkflow();
            //
            // Test the expectations
            //
            Assert.AreEqual(0, wf.ApplicationWorkflowId, "Complete - ApplicationWorkflowId not initialized as expected");
            Assert.AreEqual(0, wf.WorkflowId, "Complete - WorkflowId and should not initialized as expected");
            Assert.AreEqual(0, wf.ApplicationId, "Complete - ApplicationId and should not initialized as expected");
            Assert.AreEqual(0, wf.ApplicationTemplateId, "Complete - ApplicationTemplateId and should not initialized as expected");
            //Assert.AreEqual(0, wf.WorkflowPermissionSchemeId, "Complete - WorkflowPermissionSchemeId and should not initialized as expected");
            Assert.AreEqual(null, wf.ApplicationWorkflowName, "Complete - ApplicationWorkflowName and should not initialized as expected");
            Assert.AreEqual(null, wf.DateAssigned, "Complete - DateAssigned and should not initialized as expected");
            //Assert.AreEqual(0, wf.CreatedBy, "Complete - CreatedBy and should not initialized as expected");
            //Assert.AreEqual(_noDate, wf.CreatedDate, "Complete - CreatedDate and should not initialized as expected");
            Assert.AreEqual(null, wf.Application, "Complete - Application and should not initialized as expected");
            Assert.AreEqual(null, wf.ApplicationTemplate, "Complete - ApplicationTemplate and should not initialized as expected");
            Assert.AreEqual(null, wf.Workflow, "Complete - Workflow and should not initialized as expected");
            Assert.IsNotNull(wf.ApplicationWorkflowSteps, "Complete - ApplicationWorkflowSteps and should not initialized as expected");
            Assert.AreEqual(0, wf.ApplicationWorkflowSteps.Count(), "Complete - ApplicationWorkflowSteps and should not initialized as expected");
            Assert.AreEqual(null, wf.WorkflowPermissionScheme, "Complete - WorkflowPermissionScheme and should not initialized as expected");
            //
            // Test
            //
            wf.Complete(_userId);
            //
            // these should change
            //
            Assert.That(wf.DateClosed, Is.InRange(beforeTime, DateTime.Now), "Complete did not set DateClosed");
            Assert.That(wf.ModifiedDate, Is.InRange(beforeTime, DateTime.Now), "Complete did not set ModifiedDate");
            Assert.AreEqual(_userId, wf.ModifiedBy, "Complete did not set ModifiedBy");
            //
            // these should not
            //
            Assert.AreEqual(0, wf.ApplicationWorkflowId,  "Complete set ApplicationWorkflowId and should not have");
            Assert.AreEqual(0, wf.WorkflowId,  "Complete set WorkflowId and should not have");
            Assert.AreEqual(0, wf.ApplicationId,  "Complete set ApplicationId and should not have");
            Assert.AreEqual(0, wf.ApplicationTemplateId,  "Complete set ApplicationTemplateId and should not have");
            //Assert.AreEqual(0, wf.WorkflowPermissionSchemeId,  "Complete set WorkflowPermissionSchemeId and should not have");
            Assert.AreEqual(null, wf.ApplicationWorkflowName,  "Complete set ApplicationWorkflowName and should not have");
            Assert.AreEqual(null, wf.DateAssigned, "Complete set DateAssigned and should not have");
            //Assert.AreEqual(0, wf.CreatedBy,  "Complete set CreatedBy and should not have");
            //Assert.AreEqual(_noDate, wf.CreatedDate, "Complete set CreatedDate and should not have");
            Assert.AreEqual(null, wf.Application,  "Complete set Application and should not have");
            Assert.AreEqual(null, wf.ApplicationTemplate,  "Complete set ApplicationTemplate and should not have");
            Assert.AreEqual(null, wf.Workflow,  "Complete set Workflow and should not have");
            Assert.IsNotNull(wf.ApplicationWorkflowSteps,  "Complete set ApplicationWorkflowSteps and should not have");
            Assert.AreEqual(0, wf.ApplicationWorkflowSteps.Count(),  "Complete set ApplicationWorkflowSteps and should not have");
            Assert.AreEqual(null, wf.WorkflowPermissionScheme,  "Complete set WorkflowPermissionScheme and should not have");
        }        
        #endregion
        #region GetThisStep Tests
        /// <summary>
        /// Test get the first step
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.GetThisStep")] 
        public void GetThisStep_FirstTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, ApplicationWorkflowStepId = _stepId1 };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, ApplicationWorkflowStepId = _stepId2 };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, ApplicationWorkflowStepId = _stepId3 };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, ApplicationWorkflowStepId = _stepId4 };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, ApplicationWorkflowStepId = _stepId5 };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, ApplicationWorkflowStepId = _stepId6 };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, ApplicationWorkflowStepId = _stepId7 };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, ApplicationWorkflowStepId = _stepId8 };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9, ApplicationWorkflowStepId = _stepId9 };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);
            //
            // Test
            //
            var s = workflow.GetThisStep(_stepId1);
            //
            // Verify
            //
            Assert.AreSame(step1, s, "GetThisStep did not return correct step");
        }
        /// <summary>
        /// Test get a middle step
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.GetThisStep")]
        public void GetThisStep_MidTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, ApplicationWorkflowStepId = _stepId1 };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, ApplicationWorkflowStepId = _stepId2 };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, ApplicationWorkflowStepId = _stepId3 };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, ApplicationWorkflowStepId = _stepId4 };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, ApplicationWorkflowStepId = _stepId5 };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, ApplicationWorkflowStepId = _stepId6 };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, ApplicationWorkflowStepId = _stepId7 };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, ApplicationWorkflowStepId = _stepId8 };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9, ApplicationWorkflowStepId = _stepId9 };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);
            //
            // Test
            //
            var s = workflow.GetThisStep(_stepId5);
            //
            // Verify
            //
            Assert.AreSame(step5, s, "GetThisStep did not return correct step");
        }
        /// <summary>
        /// Test get a last step
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.GetThisStep")]
        public void GetThisStep_LastTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, ApplicationWorkflowStepId = _stepId1 };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, ApplicationWorkflowStepId = _stepId2 };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, ApplicationWorkflowStepId = _stepId3 };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, ApplicationWorkflowStepId = _stepId4 };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, ApplicationWorkflowStepId = _stepId5 };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, ApplicationWorkflowStepId = _stepId6 };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, ApplicationWorkflowStepId = _stepId7 };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, ApplicationWorkflowStepId = _stepId8 };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9, ApplicationWorkflowStepId = _stepId9 };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);
            //
            // Test
            //
            var s = workflow.GetThisStep(_stepId9);
            //
            // Verify
            //
            Assert.AreSame(step9, s, "GetThisStep did not return correct step");
        }
        /// <summary>
        /// Test get step that does not exist
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.GetThisStep")]
        public void GetThisStep_NonStepTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, ApplicationWorkflowStepId = _stepId1 };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, ApplicationWorkflowStepId = _stepId2 };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, ApplicationWorkflowStepId = _stepId3 };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, ApplicationWorkflowStepId = _stepId4 };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, ApplicationWorkflowStepId = _stepId5 };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, ApplicationWorkflowStepId = _stepId6 };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, ApplicationWorkflowStepId = _stepId7 };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, ApplicationWorkflowStepId = _stepId8 };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9, ApplicationWorkflowStepId = _stepId9 };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);
            //
            // Test
            //
            var s = workflow.GetThisStep(17);
            //
            // Verify
            //
            Assert.IsNull(s, "GetThisStep did not return null");
        }
        #endregion
        #region ResetResolved Tests
        /// <summary>
        /// Test Reset resolved with entries incomplete current step
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.ResetResolved")]
        public void ResetResolved_InCompleteCurrentTest()
        {	
	        //
	        // Set up local data
	        //
            ApplicationWorkflow workflow = new ApplicationWorkflow();


            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, Resolution = true };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, Resolution = true };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, Resolution = true };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, Resolution = true };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, Resolution = true };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, Resolution = true };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7};
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8};
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9};

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);

            ApplicationWorkflowStep currentStep = step7;
            ApplicationWorkflowStep targetStep = step3;
	        //
	        // Test
	        //
            workflow.ResetResolved(currentStep, targetStep);
	        //
	        // Verify
	        //
            Assert.IsTrue(step1.Resolution, "Resolution of step 1 is not as it should be");
            Assert.IsTrue(step2.Resolution, "Resolution of step 2 is not as it should be");
            Assert.IsFalse(step3.Resolution, "Resolution of step 3 is not as it should be");
            Assert.IsFalse(step4.Resolution, "Resolution of step 4 is not as it should be");
            Assert.IsFalse(step5.Resolution, "Resolution of step 5 is not as it should be");
            Assert.IsFalse(step6.Resolution, "Resolution of step 6 is not as it should be");
            Assert.IsFalse(step7.Resolution, "Resolution of step 7 is not as it should be");
            Assert.IsFalse(step8.Resolution, "Resolution of step 8 is not as it should be");
            Assert.IsFalse(step9.Resolution, "Resolution of step 9 is not as it should be");
        }
        /// <summary>
        /// Test Reset resolved with entries complete current step
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.ResetResolved")]
        public void ResetResolved_CompleteCurrentTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, Resolution = true };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, Resolution = true };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, Resolution = true };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, Resolution = true };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, Resolution = true };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, Resolution = true };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, Resolution = true };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8 };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9 };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);

            ApplicationWorkflowStep currentStep = step7;
            ApplicationWorkflowStep targetStep = step3;
            //
            // Test
            //
            workflow.ResetResolved(currentStep, targetStep);
            //
            // Verify
            //
            Assert.IsTrue(step1.Resolution, "Resolution of step 1 is not as it should be");
            Assert.IsTrue(step2.Resolution, "Resolution of step 2 is not as it should be");
            Assert.IsFalse(step3.Resolution, "Resolution of step 3 is not as it should be");
            Assert.IsFalse(step4.Resolution, "Resolution of step 4 is not as it should be");
            Assert.IsFalse(step5.Resolution, "Resolution of step 5 is not as it should be");
            Assert.IsFalse(step6.Resolution, "Resolution of step 6 is not as it should be");
            Assert.IsFalse(step7.Resolution, "Resolution of step 7 is not as it should be");
            Assert.IsFalse(step8.Resolution, "Resolution of step 8 is not as it should be");
            Assert.IsFalse(step9.Resolution, "Resolution of step 9 is not as it should be");
        }
        /// <summary>
        /// Test Reset resolved with entries complete current step
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.ResetResolved")]
        public void ResetResolved_LastTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, Resolution = true };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, Resolution = true };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, Resolution = true };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, Resolution = true };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, Resolution = true };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, Resolution = true };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, Resolution = true };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, Resolution = true };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9};

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);

            ApplicationWorkflowStep currentStep = step9;
            ApplicationWorkflowStep targetStep = step3;
            //
            // Test
            //
            workflow.ResetResolved(currentStep, targetStep);
            //
            // Verify
            //
            Assert.IsTrue(step1.Resolution, "Resolution of step 1 is not as it should be");
            Assert.IsTrue(step2.Resolution, "Resolution of step 2 is not as it should be");
            Assert.IsFalse(step3.Resolution, "Resolution of step 3 is not as it should be");
            Assert.IsFalse(step4.Resolution, "Resolution of step 4 is not as it should be");
            Assert.IsFalse(step5.Resolution, "Resolution of step 5 is not as it should be");
            Assert.IsFalse(step6.Resolution, "Resolution of step 6 is not as it should be");
            Assert.IsFalse(step7.Resolution, "Resolution of step 7 is not as it should be");
            Assert.IsFalse(step8.Resolution, "Resolution of step 8 is not as it should be");
            Assert.IsFalse(step9.Resolution, "Resolution of step 9 is not as it should be");
        }
        /// <summary>
        /// Test Reset resolved - all steps but last complete
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.ResetResolved")]
        public void ResetResolved_LastInCompleteTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, Resolution = true };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, Resolution = true };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, Resolution = true };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, Resolution = true };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, Resolution = true };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, Resolution = true };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, Resolution = true };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, Resolution = true };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9};

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);

            ApplicationWorkflowStep currentStep = step9;
            ApplicationWorkflowStep targetStep = step3;
            //
            // Test
            //
            workflow.ResetResolved(currentStep, targetStep);
            //
            // Verify
            //
            Assert.IsTrue(step1.Resolution, "Resolution of step 1 is not as it should be");
            Assert.IsTrue(step2.Resolution, "Resolution of step 2 is not as it should be");
            Assert.IsFalse(step3.Resolution, "Resolution of step 3 is not as it should be");
            Assert.IsFalse(step4.Resolution, "Resolution of step 4 is not as it should be");
            Assert.IsFalse(step5.Resolution, "Resolution of step 5 is not as it should be");
            Assert.IsFalse(step6.Resolution, "Resolution of step 6 is not as it should be");
            Assert.IsFalse(step7.Resolution, "Resolution of step 7 is not as it should be");
            Assert.IsFalse(step8.Resolution, "Resolution of step 8 is not as it should be");
            Assert.IsFalse(step9.Resolution, "Resolution of step 9 is not as it should be");
        }
        /// <summary>
        /// Test Reset resolved - all steps complete
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.ResetResolved")]
        public void ResetResolved_LastCompleteTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, Resolution = true };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2, Resolution = true };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3, Resolution = true };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4, Resolution = true };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5, Resolution = true };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6, Resolution = true };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7, Resolution = true };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8, Resolution = true };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9, Resolution = true };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);

            ApplicationWorkflowStep currentStep = step9;
            ApplicationWorkflowStep targetStep = step3;
            //
            // Test
            //
            workflow.ResetResolved(currentStep, targetStep);
            //
            // Verify
            //
            Assert.IsTrue(step1.Resolution, "Resolution of step 1 is not as it should be");
            Assert.IsTrue(step2.Resolution, "Resolution of step 2 is not as it should be");
            Assert.IsFalse(step3.Resolution, "Resolution of step 3 is not as it should be");
            Assert.IsFalse(step4.Resolution, "Resolution of step 4 is not as it should be");
            Assert.IsFalse(step5.Resolution, "Resolution of step 5 is not as it should be");
            Assert.IsFalse(step6.Resolution, "Resolution of step 6 is not as it should be");
            Assert.IsFalse(step7.Resolution, "Resolution of step 7 is not as it should be");
            Assert.IsFalse(step8.Resolution, "Resolution of step 8 is not as it should be");
            Assert.IsFalse(step9.Resolution, "Resolution of step 9 is not as it should be");
        }
        /// <summary>
        /// Test Reset resolved - reset only first
        /// </summary>
        [TestMethod()]
        [Category("ApplicationWorkflow.ResetResolved")]
        public void ResetResolved_ResetTwoTest()
        {
            //
            // Set up local data
            //
            ApplicationWorkflow workflow = new ApplicationWorkflow();

            ApplicationWorkflowStep step1 = new ApplicationWorkflowStep { StepOrder = 1, Resolution = true };
            ApplicationWorkflowStep step2 = new ApplicationWorkflowStep { StepOrder = 2 };
            ApplicationWorkflowStep step3 = new ApplicationWorkflowStep { StepOrder = 3 };
            ApplicationWorkflowStep step4 = new ApplicationWorkflowStep { StepOrder = 4 };
            ApplicationWorkflowStep step5 = new ApplicationWorkflowStep { StepOrder = 5 };
            ApplicationWorkflowStep step6 = new ApplicationWorkflowStep { StepOrder = 6 };
            ApplicationWorkflowStep step7 = new ApplicationWorkflowStep { StepOrder = 7 };
            ApplicationWorkflowStep step8 = new ApplicationWorkflowStep { StepOrder = 8 };
            ApplicationWorkflowStep step9 = new ApplicationWorkflowStep { StepOrder = 9 };

            workflow.ApplicationWorkflowSteps.Add(step1);
            workflow.ApplicationWorkflowSteps.Add(step2);
            workflow.ApplicationWorkflowSteps.Add(step3);
            workflow.ApplicationWorkflowSteps.Add(step4);
            workflow.ApplicationWorkflowSteps.Add(step5);
            workflow.ApplicationWorkflowSteps.Add(step6);
            workflow.ApplicationWorkflowSteps.Add(step7);
            workflow.ApplicationWorkflowSteps.Add(step8);
            workflow.ApplicationWorkflowSteps.Add(step9);

            ApplicationWorkflowStep currentStep = step2;
            ApplicationWorkflowStep targetStep = step1;
            //
            // Test
            //
            workflow.ResetResolved(currentStep, targetStep);
            //
            // Verify
            //
            Assert.IsFalse(step1.Resolution, "Resolution of step 1 is not as it should be");
            Assert.IsFalse(step2.Resolution, "Resolution of step 2 is not as it should be");
            Assert.IsFalse(step3.Resolution, "Resolution of step 3 is not as it should be");
            Assert.IsFalse(step4.Resolution, "Resolution of step 4 is not as it should be");
            Assert.IsFalse(step5.Resolution, "Resolution of step 5 is not as it should be");
            Assert.IsFalse(step6.Resolution, "Resolution of step 6 is not as it should be");
            Assert.IsFalse(step7.Resolution, "Resolution of step 7 is not as it should be");
            Assert.IsFalse(step8.Resolution, "Resolution of step 8 is not as it should be");
            Assert.IsFalse(step9.Resolution, "Resolution of step 9 is not as it should be");
        }
        #endregion
    }
}
