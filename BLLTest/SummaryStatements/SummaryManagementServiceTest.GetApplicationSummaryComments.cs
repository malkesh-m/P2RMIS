using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using Sra.P2rmis.CrossCuttingServices;

namespace BLLTest.SummaryStatements
{
    /// <summary>
    /// Unit test for SummaryManagementServer.SummaryCommentService.
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest : BLLBaseTest
    {
        #region Attributes - unique to SummaryManagementServiceTest.GetApplicationSummaryComments

        private string APPLICATION_ID_1 = "5";
        private string BAD_EMPTY_APP_ID = String.Empty;
        private int PANEL_APPLICATION_ID = 5;

        private int COMMENT_ID_1 = 10;
        private int COMMENT_ID_2 = 20;
        private int COMMENT_ID_3 = 30;

        private string USER_NAME_1 = "Smith, John";
        private string USER_NAME_2 = "Jones, Barbara";
        private string USER_NAME_3 = "Brown, Robert";

        private DateTime DATE_TIME_1 = GlobalProperties.P2rmisDateTimeNow;
        private DateTime DATE_TIME_2 = GlobalProperties.P2rmisDateTimeNow.AddDays(-1);
        private DateTime DATE_TIME_3 = GlobalProperties.P2rmisDateTimeNow.AddDays(-2);

        #endregion

        #region Overhead - unique to SummaryManagementServiceTest.GetApplicationSummaryComments
        #endregion

        #region The GetApplicationSummaryComments Tests
        /// <summary>
        /// Test - non-empty results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetApplicationSummaryComments")]
        public void GetApplicationSummaryComments_GoodGetTest()
        {
            //
            // Set up local data
            string goodAppId = APPLICATION_ID_1;
            //
            int goodPanelApplicationId = PANEL_APPLICATION_ID;

            ResultModel<ISummaryCommentModel> resultModel = new ResultModel<ISummaryCommentModel>();
            List<ISummaryCommentModel> list = new List<ISummaryCommentModel>();

            list.Add(new SummaryCommentModel { CommentID = COMMENT_ID_1, FirstName = USER_NAME_1, LastName = USER_NAME_2, CommentDate = DATE_TIME_1, Comment = "Comment COMMENT_ID_1" });
            list.Add(new SummaryCommentModel { CommentID = COMMENT_ID_2, FirstName = USER_NAME_2, LastName = USER_NAME_3, CommentDate = DATE_TIME_2, Comment = "Comment for COMMENT_ID_2" });
            list.Add(new SummaryCommentModel { CommentID = COMMENT_ID_3, FirstName = USER_NAME_3, LastName = USER_NAME_1, CommentDate = DATE_TIME_3, Comment = "Comment for COMMENT_ID_3" });
            resultModel.ModelList = list;



            GetApplicationSummaryCommentsSuccessTest(resultModel, goodPanelApplicationId);
        }

        /// <summary>
        /// Test - no comments returned from
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetApplicationSummaryComments")]
        public void GetApplicationSummaryComments_EmptyGoodGetTest()
        {
            //
            // Set up local data
            int goodAppId = PANEL_APPLICATION_ID;
            //

            ResultModel<ISummaryCommentModel> resultModel = new ResultModel<ISummaryCommentModel>();
            List<ISummaryCommentModel> list = new List<ISummaryCommentModel>();

            resultModel.ModelList = list;



            GetApplicationSummaryCommentsEmptySuccessTest(resultModel, goodAppId);
        }
 
        #endregion

        #region GetApplicationSummaryComments Helpers

        private void GetApplicationSummaryCommentsSuccessTest(ResultModel<ISummaryCommentModel> resultModel, int panelApplicationId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetApplicationSummaryComments(panelApplicationId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryCommentModel> container = testService.GetApplicationSummaryComments(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }

        private void GetApplicationSummaryCommentsSuccess_invalidAppId(ResultModel<ISummaryCommentModel> resultModel, int invalidPanelApplicationId)
        {
            //
            // Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryCommentModel> container = testService.GetApplicationSummaryComments(invalidPanelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.SummaryManagementRepository);
        }

        private void GetApplicationSummaryCommentsEmptySuccessTest(ResultModel<ISummaryCommentModel> resultModel, int panelApplicationId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetApplicationSummaryComments(panelApplicationId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryCommentModel> container = testService.GetApplicationSummaryComments(panelApplicationId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }

        #endregion

    }
}

