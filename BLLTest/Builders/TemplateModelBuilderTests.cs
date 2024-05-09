using System.Collections.Generic;
using System.Linq;
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
    /// Unit tests for TemplateModel builder class.
    /// </summary>
    [TestClass()]
    public class TemplateModelBuilderTests
    {
        #region Attributes
        private EntityWorkflow _entityObject { get; set; }
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
            _entityObject = new EntityWorkflow();
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
            TemplateModelBuilder builder = new TemplateModelBuilder(_entityObject);
            WorkflowTemplateModel model = builder.Build();
            //
            // Make sure it did what it was supposed to.
            //
            Assert.AreEqual(Constants.Client_Id, model.ClientId, "Client ids do not match");
            Assert.AreEqual(Constants.Workflow_Description_1, model.WorkflowDescription, "Workflow descriptions do not match");
            Assert.AreEqual(Constants.Workflow_Name_1, model.WorkflowName, "Workflow name do not match");
            Assert.AreEqual(Constants.Workflow_Id, model.WorkflowId, "Workflow ids do not match");

            Assert.AreEqual(Constants.Count, model.Steps.Count(), "Wrong number of workflow steps");
        }
        #endregion
        #region Test Helpers
        /// <summary>
        /// Populate the entity's fields
        /// </summary>
        private void PopulateEntity()
        {
            this._entityObject.WorkflowId = Constants.Workflow_Id;
            this._entityObject.ClientId = Constants.Client_Id;
            this._entityObject.WorkflowName = Constants.Workflow_Name_1;
            this._entityObject.WorkflowDescription = Constants.Workflow_Description_1;

            this._entityObject.WorkflowSteps = new List<WorkflowStep>();

            WorkflowStep step0 = new WorkflowStep();

            for (int i = 0; i < Constants.Count; i++)
            {
                this._entityObject.WorkflowSteps.Add(new WorkflowStep());
            }
        }
        #endregion
        public class Constants
        {
            public const string Workflow_Name_1 = "workflow name 1";
            public const string Workflow_Description_1 = "workflow description 1";
            public const int Workflow_Id = 22;
            public const int Client_Id = 19;
            public const int Count = 4;
        }
    }
}
