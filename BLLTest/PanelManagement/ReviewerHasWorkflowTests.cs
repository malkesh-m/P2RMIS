using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.PanelManagement
{
    /// <summary>
    /// Unit tests for ReviewerHasWorkflow
    /// </summary>
    [TestClass()]
    public partial class ReviewerHasWorkflowTests : BLLBaseTest
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
        #region ReviewerHasWorkflow Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ReviewerHasWorkflow")]
        public void ReviewerHasWorkflowSuccessfulTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 14;
            int panelApplicationId = 23;
            //
            // Test
            //
            ReviewerHasWorkflowSuccessTest(panelUserAssignmentId, panelApplicationId);
        }
        /// <summary>
        /// Test invalid panelUserAssignmentId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ReviewerHasWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ReviewerHasWorkflow detected an invalid parameter: panelUserAssignmentId was -4")]
        public void ReviewerHasWorkflowNegativePanelUserAssignmentIdTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = -4;
            int panelApplicationId = 23;
            //
            // Test
            //
            ReviewerHasWorkflowFailureTest(panelUserAssignmentId, panelApplicationId);
        }
        /// <summary>
        /// Test invalid panelApplicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.ReviewerHasWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.ReviewerHasWorkflow detected an invalid parameter: panelApplicationId was -4560")]
        public void ReviewerHasWorkflowNegativePanelApplicationIdTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 25;
            int panelApplicationId = -4560;
            //
            // Test
            //
            ReviewerHasWorkflowFailureTest(panelUserAssignmentId, panelApplicationId);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for ReviewerHasWorkflow
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        private void ReviewerHasWorkflowSuccessTest(int panelUserAssignmentId, int panelApplicationId)
        {
            //
            // set the expectations
            //
            List<ApplicationWorkflow> resultModel = new List<ApplicationWorkflow>();
            resultModel.Add(new ApplicationWorkflow());

            Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock);
            Expect.Call(theWorkflowRepositoryMock.ReviewerHasWorkflow(panelUserAssignmentId, panelApplicationId)).Return(true);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.ReviewerHasWorkflow(panelUserAssignmentId, panelApplicationId);
            //
            // Test the assertions
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for an unsuccessful test for ReviewerHasWorkflow
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        private void ReviewerHasWorkflowFailureTest(int panelUserAssignmentId, int panelApplicationId)
        {
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.ReviewerHasWorkflow(panelUserAssignmentId, panelApplicationId);
        }
        #endregion
        #endregion
    }
}