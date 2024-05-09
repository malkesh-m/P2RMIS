using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using EntityApplication = Sra.P2rmis.Dal.PanelApplication;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace BLLTest
{
    /// <summary>
    /// Unit test for SummaryManagementService.
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest
    {
        #region Attributes
        private EntityApplication _app_1;
        private EntityApplication _app_2;
        private EntityApplication _app_3;
        private const int STATUS_ID_1 = 1;
        private const int STATUS_ID_2 = 2;
        private List<int> _theList;
        private List<string> _theLogList;
        private int _reviewId;
        private EntityApplication _anApplication = new EntityApplication { };
        private EntityApplication _anApplication_2 = new EntityApplication { };
        #endregion

        #region Helpers
        /// <summary>
        /// Initialization for this test file
        /// </summary>
        private void InitCommandDraft()
        {
            this._app_1 = new EntityApplication();
            this._app_2 = new EntityApplication();
            this._app_3 = new EntityApplication();
            this._theList = new List<int>();
            _reviewId = STATUS_ID_1;
            //this._anApplication.LogNumber = A_LOG_NUMBER_1;

            ReviewStatu rStat = new ReviewStatu{ ReviewStatusId = STATUS_ID_1};
            ApplicationReviewStatu stat = new ApplicationReviewStatu();
            stat.ReviewStatu = rStat;

            this._anApplication_2.ApplicationReviewStatus.Add(stat);
        }
        private void CleanUpCommandDraft()
        {
            this._app_1 = null;
            this._app_2 = null;
            this._app_3 = null;
            this._theList = null;
            this._reviewId = -1;
            this._anApplication.ApplicationReviewStatus.Clear();
        }
        private void InitCommadDraftForLogNumbers()
        {
            this._theLogList = new List<string>();
        }
        private void CleanUpCommandDraftForLogNumbers()
        {
            this._theLogList = null;
        }
        #endregion

        #region GetApplicationDetail Tests
        /// <summary>
        /// Test a good find
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetApplicationDetail")]
        public void GetApplicationDetail_Preview_GoodTest()
        {
            int panelApplicationId = 7;
            GetApplicationDetailPreviewSuccessTest(panelApplicationId);  // any good id that will return something
        }
        /// <summary>
        /// Test - no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetApplicationDetail")]
        public void GetApplicationDetail_Preview_GoodGetZeroTest()
        {
            int panelApplicationId = 7;
            GetApplicationDetailPreviewSuccessTest(panelApplicationId);  // any good id that will return something
        }
        #region Helpers
        private void GetApplicationDetailPreviewSuccessTest(int panelApplicationId)
        {
            //
            // Set up local data
            //
            IApplicationDetailModel resultModel = new ApplicationDetailModel();
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetPreviewApplicationInfoDetail(panelApplicationId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            IApplicationDetailModel container = testService.GetPreviewApplicationInfoDetail(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theMock.VerifyAll();
        }
        private void GetApplicationDetailPreviewFailTest(int panelApplicationId)
        {
            //
            // Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            IApplicationDetailModel container = testService.GetPreviewApplicationInfoDetail(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            theWorkMock.AssertWasNotCalled(s => s.SummaryManagementRepository);
        }
        #endregion
        #endregion
        #region GetApplicationStepContent Tests
        ///// <summary>
        ///// Test a good find
        ///// </summary>
        //[TestMethod()]
        //[Category("SummaryManagementService.GetApplicationStepContent")]
        //public void GetApplicationStepContent_GoodTest()
        //{
        //    int applicationWorkflowId = 7;
        //    GetApplicationStepContentPreviewSuccessTest(applicationWorkflowId);
        //}
        /// <summary>
        /// Test - no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetApplicationStepContent")]
        public void GetApplicationStepContent_GoodGetZeroTest()
        {
            int panelApplicationId = 7;
            //
            // Set up local data
            //
            ResultModel<IStepContentModel> resultModel = new ResultModel<IStepContentModel>();
            List<IStepContentModel> list = new List<IStepContentModel>();
            resultModel.ModelList = list;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetPreviewApplicationStepContent(panelApplicationId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = testService.GetPreviewApplicationStepContent(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        #region Helpers
        private void GetApplicationStepContentPreviewSuccessTest(int panelApplicationId)
        {
            //
            // Set up local data
            //
            ResultModel<IStepContentModel> resultModel = new ResultModel<IStepContentModel>();
            List<IStepContentModel> list = new List<IStepContentModel>();
            list.Add(new StepContentModel { ApplicationWorkflowStepContentId = 1 });
            list.Add(new StepContentModel { ApplicationWorkflowStepContentId = 2 });
            resultModel.ModelList = list;
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetApplicationStepContent(panelApplicationId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = testService.GetPreviewApplicationStepContent(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(2, container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private void GetApplicationStepContentPreviewFailTest(int panelApplicationId)
        {
            //
            //// Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var container = testService.GetPreviewApplicationStepContent(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.SummaryManagementRepository);
        }
        #endregion
        #endregion
    }
}
