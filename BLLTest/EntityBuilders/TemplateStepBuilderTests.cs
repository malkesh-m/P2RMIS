using System;
using NUnit.Framework;
using Sra.P2rmis.Bll.EntityBuilders;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using Sra.P2rmis.CrossCuttingServices;

namespace BLLTest.EntityBuilders
{
    /// <summary>
    /// Unit tests for Template builder class. (builds entity objects)
    /// </summary>
    [TestClass()]
    public class TemplateStepBuilderTests
    {
        #region Attributes
        private WorkflowStepModel _model { get; set; }
        private int _user = 10;
        DateTime _nowDate = GlobalProperties.P2rmisDateToday;
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
            _model = new WorkflowStepModel()
;            PopulateModel();
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
        /// Test Ste[ builder should build a good entity from a model.  Make sure all fields that should be
        /// populated are populated with the correct value from the model.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateStepBuilder")]
        public void BuildTest()
        {
            //
            // Create the template builder & build the template entity
            //
            TemplateStepBuilder builder = new TemplateStepBuilder(this._model, this._user);
            WorkflowStep  entityObject = builder.Build();
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, this._model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, this._model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, this._model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, this._model.StepOrder, "Step order do not match");
            Assert.IsTrue(builder.IsDirty, "Dirty flag not set");
            Assert.AreEqual(this._user, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");
        }
        /// <summary>
        /// Test Step builder should build a good entity from a model.  Make sure all fields that should be
        /// populated are populated with the correct value from the model.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateStepBuilder")]
        public void UpdateTest()
        {
            //
            // Create the template builder & build the template entity
            //
            WorkflowStep oldEntityObject = new WorkflowStep{
                WorkflowId = Constants.Workflow_Id,
                WorkflowStepId = Constants.Workflow_step_id,
                StepName = "this will change",
                StepOrder = 9999,
                ActiveDefault = false,
                CreatedBy = 789,
                CreatedDate = this._date2
            };

            TemplateStepBuilder builder = new TemplateStepBuilder(this._model, this._user);
            WorkflowStep entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, this._model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, this._model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, this._model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, this._model.StepOrder, "Step order do not match");
            Assert.IsTrue(builder.IsDirty, "Dirty flag not set");
            Assert.AreEqual(789, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._date2, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");
        }
        /// <summary>
        /// Test Step builder should build a good entity from a model.  Make sure no fields were changed
        /// when no modifications were made.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateStepBuilder")]
        public void NoUpdateTest()
        {
            //
            // Create the template builder & build the template entity
            //
            WorkflowStep oldEntityObject = new WorkflowStep
            {
                WorkflowId = Constants.Workflow_Id,
                WorkflowStepId = Constants.Workflow_step_id,
                StepName = Constants.Workflow_step_name,
                StepOrder = Constants.Step_order_1,
                ActiveDefault = Constants.Active,
                CreatedBy = this._user,
                CreatedDate = this._nowDate
            };

            TemplateStepBuilder builder = new TemplateStepBuilder(this._model, this._user);
            WorkflowStep entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, this._model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, this._model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, this._model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, this._model.StepOrder, "Step order do not match");
            Assert.IsFalse(builder.IsDirty, "Dirty flag not set");
            Assert.AreEqual(this._user, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(0, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(oldEntityObject.ModifiedDate, entityObject.ModifiedDate, "Modified date not correct");
        }
        /// <summary>
        /// Test Step builder should build a good entity from a model.  change the StepName
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateStepBuilder")]
        public void UpdateStepNameTest()
        {
            //
            // Create the template builder & build the template entity
            //
            WorkflowStep oldEntityObject = new WorkflowStep
            {
                WorkflowId = Constants.Workflow_Id,
                WorkflowStepId = Constants.Workflow_step_id,
                StepName = "change me",
                StepOrder = Constants.Step_order_1,
                ActiveDefault = Constants.Active,
                CreatedBy = this._user,
                CreatedDate = this._nowDate
            };

            TemplateStepBuilder builder = new TemplateStepBuilder(this._model, this._user);
            WorkflowStep entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, this._model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, this._model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, this._model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, this._model.StepOrder, "Step order do not match");
            Assert.IsTrue(builder.IsDirty, "Dirty flag not set");
            Assert.AreEqual(this._user, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");
        }
        /// <summary>
        /// Test Step builder should build a good entity from a model.  Change the StepName
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateStepBuilder")]
        public void UpdateStepOrderTest()
        {
            //
            // Create the template builder & build the template entity
            //
            WorkflowStep oldEntityObject = new WorkflowStep
            {
                WorkflowId = Constants.Workflow_Id,
                WorkflowStepId = Constants.Workflow_step_id,
                StepName = Constants.Workflow_step_name,
                StepOrder = 3245,
                ActiveDefault = Constants.Active,
                CreatedBy = this._user,
                CreatedDate = this._nowDate
            };

            TemplateStepBuilder builder = new TemplateStepBuilder(this._model, this._user);
            WorkflowStep entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, this._model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, this._model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, this._model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, this._model.StepOrder, "Step order do not match");
            Assert.IsTrue(builder.IsDirty, "Dirty flag not set");
            Assert.AreEqual(this._user, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");
        }
        /// <summary>
        /// Test Step builder should build a good entity from a model.  Change the ActiveDefault
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateStepBuilder")]
        public void UpdateActiveTest()
        {
            //
            // Create the template builder & build the template entity
            //
            WorkflowStep oldEntityObject = new WorkflowStep
            {
                WorkflowId = Constants.Workflow_Id,
                WorkflowStepId = Constants.Workflow_step_id,
                StepName = Constants.Workflow_step_name,
                StepOrder = Constants.Step_order_1,
                ActiveDefault = false,
                CreatedBy = this._user,
                CreatedDate = this._nowDate
            };

            TemplateStepBuilder builder = new TemplateStepBuilder(this._model, this._user);
            WorkflowStep entityObject = builder.Update(oldEntityObject);
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, this._model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, this._model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, this._model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, this._model.StepOrder, "Step order do not match");
            Assert.IsTrue(builder.IsDirty, "Dirty flag not set");
            Assert.AreEqual(this._user, entityObject.CreatedBy, "Created by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.CreatedDate, "Created Date not correct");
            Assert.AreEqual(this._user, entityObject.ModifiedBy, "Modified by user is not set");
            Assert.AreEqual(this._nowDate, entityObject.ModifiedDate, "Modified date not correct");
        }
        #endregion
        #region Test Helpers
        /// <summary>
        /// Populate the entity's fields
        /// </summary>
        private void PopulateModel()
        {
            this._model.WorkflowId = Constants.Workflow_Id;
            this._model.WorkflowStepId = Constants.Workflow_step_id;
            this._model.ActiveDefault = Constants.Active;
            this._model.StepName = Constants.Workflow_step_name;
            this._model.StepOrder = Constants.Step_order_1;
        }
        #endregion
        public class Constants
        {
            public const int Workflow_Id = 22;
            public const int Workflow_step_id = 89;
            public const string Workflow_step_name = "my little step name";
            public const int Step_order_1 = 1;
            public const bool Active = true;
        }
    }
}
