using NUnit.Framework;
using Sra.P2rmis.Bll.Builders;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;


namespace BLLTest.Builders
{
    /// <summary>
    /// Unit tests for TemplateStepModel builder class.
    /// </summary>
    [TestClass()]
    public class TemplateStepModelBuilderTests
    {
        #region Attributes
        private WorkflowStep _entityObject { get; set; }
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
            _entityObject = new WorkflowStep();
            PopulateEntity();
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
        /// Test builder should build a good model.  Make sure all fields that should be
        /// populated are populated with the correct value from the entity.
        /// </summary>
        [TestMethod()]
        [Category("Builders.TemplateModelBuilder")] 
        public void ATest()
        {
            //
            // Create the template builder & build the template model
            //
            TemplateStepModelBuilder builder = new TemplateStepModelBuilder(_entityObject);
            WorkflowStepModel model = builder.Build();
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Workflow_step_id, model.WorkflowStepId, "Workflow step ids do not match");
            Assert.AreEqual(Constants.Workflow_Id, model.WorkflowId, "Workflow ids do not match");
            Assert.AreEqual(Constants.Workflow_step_name, model.StepName, "Step names do not match");
            Assert.AreEqual(Constants.Step_order_1, model.StepOrder, "Step order do not match");
        }
        #endregion        
        #region Test Helpers
        /// <summary>
        /// Populate the entity's fields
        /// </summary>
        private void PopulateEntity()
        {
            this._entityObject.WorkflowId = Constants.Workflow_Id;
            this._entityObject.WorkflowStepId = Constants.Workflow_step_id;
            this._entityObject.ActiveDefault = Constants.Active;
            this._entityObject.StepName = Constants.Workflow_step_name;
            this._entityObject.StepOrder = Constants.Step_order_1;
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
