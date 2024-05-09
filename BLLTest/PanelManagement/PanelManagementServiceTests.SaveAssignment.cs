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
    /// Unit tests for PanelManagementService - SaveAssiignment methods 
    /// </summary>
    public partial class PanelManagementServiceTests
    {
        int? aPresentationOrder;
        int aPanelApplicationId;
        PanelApplication aPanelApplication;
        #region CalculatePresentationOrder Tests
        /// <summary>
        /// Test - specified presentation order
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServiceT.SaveAssignment")]
        public void CalculatePresentationOrderWithValueTest()
        {
            //
            // Set up local data
            //
            aPanelApplicationId = 234;
            aPresentationOrder = 3;
            aPanelApplication = new PanelApplication();
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 1 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 2 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = aPresentationOrder });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 5 });
            //
            // Set up expectations
            //
            theMock.ReplayAll();
            //
            // Test
            //
            int result = this.theTestPanelManagementService.CalculatePresentationOrder(aPresentationOrder, aPanelApplicationId);
            //
            // Verify
            //
            Assert.AreEqual(aPresentationOrder, result, "CalculatePresentationOrder returned an incorrect value");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test null presentation order
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServiceT.SaveAssignment")]
        public void CalculatePresentationOrderNullValueTest()
        {
            //
            // Set up local data
            //
            aPanelApplicationId = 234;
            aPresentationOrder = null;
            aPanelApplication = new PanelApplication();
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 1 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 2 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = aPresentationOrder });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 5 });

            //
            // Set up expectations
            //
            MockPanelApplicationRepository();
            Expect.Call(this.thePanelApplicationRepositoryMock.GetByID(aPanelApplicationId)).Return(aPanelApplication);
            theMock.ReplayAll();
            //
            // Test
            //
            int result = this.theTestPanelManagementService.CalculatePresentationOrder(aPresentationOrder, aPanelApplicationId);
            //
            // Verify
            //
            Assert.AreEqual(0, result, "CalculatePresentationOrder returned an incorrect value");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test all slots taken & at maximum
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServiceT.SaveAssignment")]
        public void CalculatePresentationOrderAllGoneTest()
        {
            //
            // Set up local data
            //
            aPanelApplicationId = 234;
            aPresentationOrder = null;
            aPanelApplication = new PanelApplication();
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 1 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 2 });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = aPresentationOrder });
            aPanelApplication.PanelApplicationReviewerAssignments.Add(new PanelApplicationReviewerAssignment { SortOrder = 20 });

            //
            // Set up expectations
            //
            MockPanelApplicationRepository();
            Expect.Call(this.thePanelApplicationRepositoryMock.GetByID(aPanelApplicationId)).Return(aPanelApplication);
            theMock.ReplayAll();
            //
            // Test
            //
            int result = this.theTestPanelManagementService.CalculatePresentationOrder(null, aPanelApplicationId);
            //
            // Verify
            //
            Assert.AreEqual(0, result, "CalculatePresentationOrder returned an incorrect value");
            theMock.VerifyAll();
        }
        #endregion
        #region SaveAssignment Tests
        /// <summary>
        /// Test 
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementServiceT.SaveAssignment")]
        public void SaveAssignmentTest()
        {
            ////
            //// Set up local data
            ////
            //int? aClientExpertiseRatingId = 1;
            //int? aPresentationOrder = 1;
            //int? aClientAssignmentTypeId = 1;
            //int aPanelUserAssignmentId = 1;
            //int reviewerUserId = 0;
            //int? aClientCoiTypeId = 0;
            //string aComment = "";
            ////
            //// Set expectations
            ////
            //Expect.Call(theTestPanelManagementService.AssignOrUnassignReviewer(aClientExpertiseRatingId, aPresentationOrder, aClientAssignmentTypeId, aPanelApplicationId, aPanelUserAssignmentId, _goodUserId)).Return(true);
            //Expect.Call( delegate {theTestPanelManagementService.SaveReviewerExpertise(aPanelApplicationId, aPanelUserAssignmentId, aClientExpertiseRatingId, _goodUserId); });
            //MockUnitOfWorkSave();
            //theMock.ReplayAll();

            ////
            //// Test
            //var result = this.theTestPanelManagementService.SaveAssignment(reviewerUserId, aClientExpertiseRatingId, aClientCoiTypeId, aPresentationOrder, aClientAssignmentTypeId, aPanelApplicationId, aPanelUserAssignmentId, aComment, _goodUserId);

            ////
            //// Verify
            ////

        }
		
        
        #endregion
    }
}
