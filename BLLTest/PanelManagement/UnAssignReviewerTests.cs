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
    /// Unit tests for UnAssignReviewer 
    /// </summary>
    [TestClass()]
    public partial class UnAssignReviewerTests : BLLBaseTest
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
        #region UnAssignReviewer Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.UnAssignReviewer")]
        public void UnAssignReviewerSuccessfulTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 14;
            int panelApplicationId = 23;
            //
            // Test
            //
            UnAssignReviewerSuccessTest(panelUserAssignmentId, panelApplicationId, _goodUserId);
        }
        /// <summary>
        /// Test invalid panelUserAssignmentId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.UnAssignReviewer")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.UnAssignReviewer detected an invalid parameter: panelUserAssignmentId was -12")]
        public void UnAssignReviewerNegativePanelUserAssignmentIdTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = -12;
            int panelApplicationId = 23;
            //
            // Test
            //
            UnAssignReviewerFailureTest(panelUserAssignmentId, panelApplicationId, _goodUserId);
        }
        /// <summary>
        /// Test invalid panelApplicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.UnAssignReviewer")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.UnAssignReviewer detected an invalid parameter: panelApplicationId was -4560")]
        public void UnAssignReviewerNegativePanelApplicationIdTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 14;
            int panelApplicationId = -4560;
            //
            // Test
            //
            UnAssignReviewerFailureTest(panelUserAssignmentId, panelApplicationId, _goodUserId);
        }
        #region Helpers
        /// <summary>
        /// Test steps for a successful test for UnAssignReviewer
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="userId">User identifier of the currently logged-in user</param>
        private void UnAssignReviewerSuccessTest(int panelUserAssignmentId, int panelApplicationId, int userId)
        {
            //
            // set the expectations
            //
            List<ApplicationWorkflow> resultModel1 = new List<ApplicationWorkflow>();
            PanelApplicationReviewerAssignment resultModel2 = new PanelApplicationReviewerAssignment();

            Expect.Call(theWorkMock.ApplicationWorkflowRepository).Return(theWorkflowRepositoryMock);
            Expect.Call(theWorkMock.PanelApplicationReviewerAssignmentRepository).Return(thePanelApplicationReviewerAssignmentRepositoryMock);
            Expect.Call(theWorkflowRepositoryMock.GetPreMeetingWorkflows(panelUserAssignmentId, panelApplicationId)).Return(resultModel1);
            Expect.Call(thePanelApplicationReviewerAssignmentRepositoryMock.GetReviewerAssignment(panelUserAssignmentId, panelApplicationId)).Return(resultModel2);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.UnAssignReviewer(panelUserAssignmentId, panelApplicationId, userId);
            //
            // Test the assertions
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test steps for an unsuccessful test for UnAssignReviewer
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="userId">User identifier of the currently logged-in user</param>
        private void UnAssignReviewerFailureTest(int panelUserAssignmentId, int panelApplicationId, int userId)
        {
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.UnAssignReviewer(panelUserAssignmentId, panelApplicationId, userId);
        }
        #endregion
        #endregion
    }
}