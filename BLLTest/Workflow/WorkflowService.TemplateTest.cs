using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestMethod = NUnit.Framework.TestAttribute;
using Sra.P2rmis.CrossCuttingServices;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;


namespace BLLTest.Workflow
{
    public partial class WorkflowServiceTest
    {
        private const int GOOD_USER_ID = 5;
        private const int BAD_USER_ID = -9;
        private const int ZERO_USER_ID = 0;
        private const string GOOD_WORKFLOW_NAME = "A good workflow name here";
        private const string GOOD_WORKFLOW_DESCRIPTION = "A good workflow description";
        private const int GOOD_CLIENT_ID = 19;
        private const bool ACTIVE = true;
        private const bool NOT_ACTIVE = false;
        private const string FIRST_STEP_NAME = "first step name";
        private const string ANY_STEP_NAME = "a step name";
        private const string LAST_STEP_NAME = "last step name";
        private const int FIRST_STEP = 1;
        private const int MID_STEP = 5;
        private const int LAST_STEP = 10;
		#region Add Tests
        /// <summary>
        /// test the good path
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Templates")]
        public void AddTest()
        {
            //
            // create standard stuff
            //
            PopulateModel(this._templateModel);
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            IWorkflowTemplateRepository repositoryMock = t.Item3;
            //
            // Now we need to set this to be returned when it is called from the UnitOfWork
            //
            SetupResult.For(workMock.WorkflowTemplateRepository).Return(repositoryMock);
            //
            // The final object that needs to be constructed in the ResultModel,
            // which contains the actual program descriptions.  
            //
            EntityWorkflow entity = new EntityWorkflow();
            //
            // Set the repository to return the ReportClientProgramListResultModel when the DL is called
            //
            Expect.Call(delegate { repositoryMock.Add(entity); }).IgnoreArguments();
            Expect.Call(workMock.Save);
            //
            // Finally create the service.  Had to create a "test" service class & change the visibility
            // of the UnitOfWork to make this work with mocking
            //
            IWorkflowService serviceMock = mocks.StrictMock<TestWorkflowService>(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            bool result = serviceMock.Add(this._templateModel, GOOD_USER_ID);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsTrue(result, "Add did not return true");

            //
            // This verifies that all calls are made that we expect
            //
            mocks.VerifyAll();
        }
        /// <summary>
        /// test with a null parameter
        /// </summary>
        [TestMethod()]
        [Category("WorkflowService.Templates")]
        public void AddNullParameterTest()
        {
            //
            // create standard stuff
            //
            Tuple<MockRepository, IUnitOfWork, IWorkflowTemplateRepository> t = Create();
            MockRepository mocks = t.Item1;
            IUnitOfWork workMock = t.Item2;
            TestWorkflowService serviceMock = new TestWorkflowService(workMock);
            //
            // Finally turn off recording
            //
            mocks.ReplayAll();
            //
            // All the above are necessary to mock out the test
            //
            bool result = serviceMock.Add(null, GOOD_USER_ID);
            //
            // Always should get a container even if there are no programs
            //
            Assert.IsFalse(result, "Add did not return false");

            //
            // This verifies that all calls are made that we expect
            //
            mocks.VerifyAll();
        }
		#endregion
        #region Test Helpers
        /// <summary>
		/// Populate template with good values
		/// </summary>
		/// <param name="templateModel"></param>
        private void PopulateTemplateGood(WorkflowTemplateModel templateModel)
        {
            templateModel.WorkflowName = GOOD_WORKFLOW_NAME;
            templateModel.WorkflowDescription = GOOD_WORKFLOW_DESCRIPTION;
            templateModel.ClientId = GOOD_CLIENT_ID;
        }
        /// <summary>
        /// Create single step with good values
        /// </summary>
        /// <param name="templateModel">good template model created</param>
        private WorkflowStepModel PopulateTemplateGood(WorkflowTemplateModel templateModel, string stepName, int stepOrder, bool active)
        {
            return new WorkflowStepModel{
											StepName = stepName,
											StepOrder = stepOrder,
											ActiveDefault = active
										};
        }
		/// <summary>
		/// create date assertions
		/// </summary>
		/// <param name="workflow"></param>
		private void CheckCreateInfo(EntityWorkflow workflow)
        {
            Assert.AreEqual(workflow.CreatedBy, GOOD_USER_ID, "CreatedBy not correct user id") ;
            Assert.IsNotNull(workflow.CreatedDate, "CreateDate is supplied");
            Assert.LessOrEqual(workflow.CreatedDate, GlobalProperties.P2rmisDateToday, "CreateDate date not correct");
            Assert.GreaterOrEqual(workflow.CreatedDate, _nowDate, "CreateDate date not correct");
        }
		/// <summary>
		/// create date assertions
		/// </summary>
		/// <param name="workflow"></param>
		private void CheckModifiedByInfo(EntityWorkflow workflow)
        {
            Assert.AreEqual(workflow.ModifiedBy, GOOD_USER_ID, "ModifiedBy not correct user id") ;
            Assert.IsNotNull(workflow.ModifiedDate, "ModifiedDate is supplied");
            Assert.LessOrEqual(workflow.ModifiedDate, GlobalProperties.P2rmisDateToday, "ModifiedDate date not correct");
            Assert.GreaterOrEqual(workflow.ModifiedDate, _nowDate, "ModifiedDate date not correct");
        }
        /// <summary>
        /// create date assertions
        /// </summary>
        /// <param name="step"></param>
        private void CheckStepCreateInfo(WorkflowStep step)
        {
            Assert.AreEqual(step.CreatedBy, GOOD_USER_ID, "CreatedBy not correct user id");
            Assert.IsNotNull(step.CreatedDate, "CreateDate is supplied");
            Assert.LessOrEqual(step.CreatedDate, GlobalProperties.P2rmisDateToday, "CreateDate date not correct");
            Assert.GreaterOrEqual(step.CreatedDate, _nowDate, "CreateDate date not correct");
        }
        /// <summary>
        /// create date assertions
        /// </summary>
        /// <param name="step"></param>
        private void CheckStepkModifiedByInfo(WorkflowStep step)
        {
            Assert.AreEqual(step.ModifiedBy, GOOD_USER_ID, "ModifiedBy not correct user id");
            Assert.IsNotNull(step.ModifiedDate, "ModifiedDate is supplied");
            Assert.LessOrEqual(step.ModifiedDate, GlobalProperties.P2rmisDateToday, "ModifiedDate date not correct");
            Assert.GreaterOrEqual(step.ModifiedDate, _nowDate, "ModifiedDate date not correct");
        }
		/// <summary>
		/// Writes out the validation messages
		/// </summary>
		/// <param name="messages"></param>
		private void DumpMessages(List<string> messages)
        {
            foreach (string s in messages)
            {
                Console.WriteLine(s);
            }
		}
        #endregion
    }
}
