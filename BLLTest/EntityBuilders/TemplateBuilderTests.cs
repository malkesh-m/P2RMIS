using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sra.P2rmis.Bll.EntityBuilders;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;
using Sra.P2rmis.CrossCuttingServices;

namespace BLLTest.EntityBuilders
{
    /// <summary>
    /// Unit tests for Template builder class. (builds entity objects)
    /// </summary>
    [TestClass()]
    public class TemplateBuilderTests
    {
        #region Attributes
        private WorkflowTemplateModel _model { get; set; }
        private int _user = 10;
        DateTime  _nowDate = GlobalProperties.P2rmisDateToday;
        private DateTime _date2 = new DateTime(1999, 12, 12);
        private DateTime _date3 = new DateTime(2010, 1, 1);
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
            _model = new WorkflowTemplateModel();
            PopulateModel();
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
        /// Test builder should build a good entity from a model.  Make sure all fields that should be
        /// populated are populated with the correct value from the model.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void BuildTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow  entityObject = builder.Build();
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_Id, entityObject.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Client_Id, entityObject.ClientId, "Client ids do not match");
            Assert.AreEqual(Constants.Workflow_Name_1, entityObject.WorkflowName, "Workflow name do not match");
            Assert.AreEqual(Constants.Workflow_Description_1, entityObject.WorkflowDescription, "Workflow descriptions do not match");
            Assert.AreEqual(this._user, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");

            Assert.AreEqual(Constants.Count, entityObject.WorkflowSteps.Count(), "Wrong number of workflow steps");
        }
        /// <summary>
        /// Test builder should update a good entity.  Make sure all fields that should be
        /// populated are populated with the correct value from the model.  Let's try to change
        /// all fields.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void UpdateTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow oldEntityObject = new EntityWorkflow { 
                                                            WorkflowId = Constants.Workflow_Id,
                                                            ClientId = 1345,
                                                            WorkflowName = "this should be changed",
                                                            WorkflowDescription = "Description to change",
                                                            CreatedBy = Constants.User2,
                                                            CreatedDate = _date2,
                                                            ModifiedBy = 200,
                                                            ModifiedDate = _date3
                                                                };
            //
            // Now add the steps
            //
            oldEntityObject.WorkflowSteps = new List<WorkflowStep>();

            for (int i = 0; i < Constants.Count; i++)
            {
                oldEntityObject.WorkflowSteps.Add(new WorkflowStep { WorkflowStepId = i, StepName = "name", StepOrder = 1, ActiveDefault = true });
            }

            EntityWorkflow entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_Id, entityObject.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(1345, entityObject.ClientId, "Client ids changed");
            Assert.AreEqual(Constants.Workflow_Name_1, entityObject.WorkflowName, "Workflow name do not match");
            Assert.AreEqual(Constants.Workflow_Description_1, entityObject.WorkflowDescription, "Workflow descriptions do not match");
            Assert.AreEqual(Constants.User2, entityObject.CreatedBy, "Created by user changed");
            Assert.AreEqual(_date2, entityObject.CreatedDate, "Created Date changed");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");

            Assert.AreEqual(Constants.Count, entityObject.WorkflowSteps.Count(), "Wrong number of workflow steps");
        }
        /// <summary>
        /// Test builder should update a good entity.  Only a single step fields are changed.  Check to see that it is dirty and
        /// the modified fields are set.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void UpdateOnlyStepChangedTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow oldEntityObject = new EntityWorkflow
            {
                WorkflowId = Constants.Workflow_Id,
                ClientId = Constants.Client_Id,
                WorkflowName = Constants.Workflow_Name_1,
                WorkflowDescription = Constants.Workflow_Description_1,
                CreatedBy = Constants.User2,
                CreatedDate = _date2,
                ModifiedBy = 200,
                ModifiedDate = _date3
            };
            //
            // Now add the steps
            //
            oldEntityObject.WorkflowSteps = new List<WorkflowStep>();

            for (int i = 0; i < Constants.Count; i++)
            {
                oldEntityObject.WorkflowSteps.Add(new WorkflowStep { WorkflowStepId = i, StepName = "name", StepOrder = 1, ActiveDefault = true });
            }
            WorkflowStep s = oldEntityObject.WorkflowSteps.ElementAt(0);
            s.StepName = "I changed this";

            EntityWorkflow entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.IsTrue(builder.IsDirty, "dirty flag not dirty");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");

            Assert.AreEqual(Constants.Count, entityObject.WorkflowSteps.Count(), "Wrong number of workflow steps");
        }
        /// <summary>
        /// Test builder error test - check workflow step does not contain white space
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForWorkflownwmeTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            entityObject.WorkflowName = string.Empty;
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.EMPTY_TEMPLATE_NAME, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - check workflow step does not contain white space
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForWorkflownwmeNullTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            entityObject.WorkflowName = null;
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.EMPTY_TEMPLATE_NAME, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - check workflow step does not contain white space
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForWorkflowDeescriptionTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            entityObject.WorkflowDescription = string.Empty;
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.EMPTY_DESCRIPTION, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - check workflow step does not contain white space
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForWorkflowDescriptionNullTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            entityObject.WorkflowDescription = null;
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.EMPTY_DESCRIPTION, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - too few steps
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForTooFewStepsTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            entityObject.WorkflowSteps.Clear();
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.STEP_COUNT, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - too few steps
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForOneStepTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            WorkflowStep step = entityObject.WorkflowSteps.ElementAt(0);
            entityObject.WorkflowSteps.Clear();
            entityObject.WorkflowSteps.Add(step);
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.STEP_COUNT, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - too few steps
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForInactiveFirstStepTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            WorkflowStep step = entityObject.WorkflowSteps.ElementAt(0);
            step.ActiveDefault = false;
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.FIRST_STEP_NOT_ACTIVE, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - too few steps
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForInactiveFLastStepTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            WorkflowStep step = entityObject.WorkflowSteps.ElementAt(3);
            step.ActiveDefault = false;
            List<string> l = builder.Validate(entityObject);
            Assert.AreEqual(1, l.Count, "found extra errors");
            Assert.AreEqual(TemplateBuilder.Messages.LAST_STEP_NOT_ACTIVE, l[0], "wrong message");
        }
        /// <summary>
        /// Test builder error test - too few steps
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateBuilder")]
        public void ValidateForNullTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateBuilder builder = new TemplateBuilder(_model, _user);
            EntityWorkflow entityObject = builder.Build();
            //
            // now make the error
            //
            List<string> l = builder.Validate(null);
            Assert.IsNotNull(l, "Nothing returned");
            Assert.AreEqual(0, l.Count, "There was an error");
        }

        #endregion
        #region Test Helpers
        /// <summary>
        /// Populate the entity's fields
        /// </summary>
        private void PopulateModel()
        {
            this._model.WorkflowId = Constants.Workflow_Id;
            this._model.ClientId = Constants.Client_Id;
            this._model.WorkflowName = Constants.Workflow_Name_1;
            this._model.WorkflowDescription = Constants.Workflow_Description_1;

            this._model.Steps = new List<WorkflowStepModel>();

            for (int i = 0; i < Constants.Count; i++)
            {
                this._model.Steps.Add(new WorkflowStepModel { WorkflowStepId = i, StepName = "name", StepOrder = i, ActiveDefault = true });
            }
        }
        private void CreateWorkflowSteps()
        {

        }
        #endregion
        public class Constants
        {
            public const string Workflow_Name_1 = "workflow name 1";
            public const string Workflow_Description_1 = "workflow description 1";
            public const int Workflow_Id = 22;
            public const int Client_Id = 19;
            public const int Count = 4;
            public const int User2 = 199;
        }

    }
}
