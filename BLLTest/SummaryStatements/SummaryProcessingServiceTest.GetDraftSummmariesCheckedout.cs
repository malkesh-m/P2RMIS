using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.SummaryStatements
{
    /// <summary>
    /// Unit test for SummaryProcessingService.GetDraftSummmariesChecedkout().
    /// </summary>
    [TestClass()]
    public partial class SummaryProcessingServiceTest : BLLBaseTest
    {
        #region GetDraftSummmariesCheckedout Tests
        /// <summary>
        /// Test - non-empty results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesCheckedout")]
        public void CheckedoutReturningResultsTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel =  MakeCheckedoutResult();

            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesCheckedout_SuccessTest(resultModel, 10);
        }
        ///
        /// Test - zero user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesCheckedout")]
        public void CheckedoutZeroUserTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummariesCheckedout_FailTest(0, true);
        }
        #endregion

        #region GetDraftSummmariesCheckedout Helpers
        private void GetDraftSummmariesCheckedout_SuccessTest(ResultModel<ISummaryAssignedModel> resultModel, int userId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetDraftSummmariesCheckedout(userId, true, true, true)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryAssignedModel> container = thetestSummaryProcessingService.GetDraftSummmariesCheckedout(userId, true, true, true);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private void GetDraftSummariesCheckedout_FailTest(int userId, bool hasManagePermission)
        {
            //
            // set the expectations
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryAssignedModel> container = thetestSummaryProcessingService.GetDraftSummmariesCheckedout(userId, true, true, true);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private ResultModel<ISummaryAssignedModel> MakeCheckedoutResult()
        {
            ResultModel<ISummaryAssignedModel> resultModel = new ResultModel<ISummaryAssignedModel>();
            List<ISummaryAssignedModel> list = new List<ISummaryAssignedModel>();
            list.Add(new SummaryAssignedModel { LogNumber = "BC12345" });
            list.Add(new SummaryAssignedModel { LogNumber = "BC12343" });
            list.Add(new SummaryAssignedModel { LogNumber = "BC1111" });

            resultModel.ModelList = list;

            return resultModel;
        }
        #endregion
    }
}
