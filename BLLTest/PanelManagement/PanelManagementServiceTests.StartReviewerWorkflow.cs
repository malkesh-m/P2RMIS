using System;
using NUnit.Framework;
using Rhino.Mocks;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.PanelManagement
{
    /// <summary>
    /// Unit tests for PanelManagementService 
    /// </summary>

    public partial class PanelManagementServiceTests
    {
        /// <summary>
        /// Unit tests for PanelManagementService.StartReviewerWorkflow()
        /// </summary>
        #region StartReviewerWorkflow Tests

        /// <summary>
        /// Test with good parameters
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.StartReviewerWorkflow")]
        public void StartReviewerWorkflowSuccessfulTest()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 14;
            int panelApplicationId = 23;
            //
            // set the expectations
            //
            MockUnitOfWorkPanelApplicationReviewerAssignmentRepository();
            Expect.Call(thePanelApplicationReviewerAssignmentRepositoryMock.GetReviewerAssignmentForCritique(panelUserAssignmentId, panelApplicationId)).Return(null);

            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.StartReviewerWorkflow(panelUserAssignmentId, panelApplicationId, _goodUserId);
            //
            // Test the assertions
            //
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test with bad PanelUserAssignmentId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.StartReviewerWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.StartReviewerWorkflow detected an invalid parameter: panelUserAssignmentId was -12")]
        public void StartReviewerWorkflowWithNegativePanelUserAssignmentId()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = -12;
            int panelApplicationId = 23;
            //
            // Test
            //
            StartReviewerWorkflowFailureTest(panelUserAssignmentId, panelApplicationId, _goodUserId);
        }
        /// <summary>
        /// Test with bad PanelApplicationId
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementService.StartReviewerWorkflow")]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "PanelManagementService.StartReviewerWorkflow detected an invalid parameter: panelApplicationId was -4560")]
        public void StartReviewerWorkflowWithNegativePanelApplicationId()
        {
            //
            // Set up local data
            //
            int panelUserAssignmentId = 14;
            int panelApplicationId = -4560;
            //
            // Test
            //
            StartReviewerWorkflowFailureTest(panelUserAssignmentId, panelApplicationId, _goodUserId);
        }
        #region Helpers
        /// <summary>
        /// Test steps for an unsuccessful test for StartReviewerWorkflow
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="userId">User identifier of the currently logged-in user</param>
        private void StartReviewerWorkflowFailureTest(int panelUserAssignmentId, int panelApplicationId, int userId)
        {
            //
            // Execute the method under test
            //
            this.theTestPanelManagementService.StartReviewerWorkflow(panelUserAssignmentId, panelApplicationId, userId);
        }
        #endregion
        #endregion
    }
}
