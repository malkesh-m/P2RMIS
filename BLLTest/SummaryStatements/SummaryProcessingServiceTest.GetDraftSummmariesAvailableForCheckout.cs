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
    /// Unit test for SummaryProcessingService.GetDraftSummmariesAvailableForCheckout().
    /// </summary>
    [TestClass()]
    public partial class SummaryProcessingServiceTest : BLLBaseTest
    {
        #region GetDraftSummmariesAvailableForCheckout Tests
        /// <summary>
        /// Test - non-empty results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void ReturningResulstsTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, null, null, null);
        }
        /// <summary>
        /// Test - cycle value of 0
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void CycleZeroTest()
        {
            GetDraftSummmariesAvailableForCheckout_FailTest(10, 47, 224, 0, null, null);
        }
        /// <summary>
        /// Test - cycle value of -1
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void CycleNegativeTest()
        {
            GetDraftSummmariesAvailableForCheckout_FailTest(10, 47, 224, -1, null, null);
        }
        /// <summary>
        /// Test - panel value of 0
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void PanelZeroTest()
        {
            GetDraftSummmariesAvailableForCheckout_FailTest(10, 47, 224, null, 0, null);
        }
        /// <summary>
        /// Test - panel value of -1
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void PanelNegativeTest()
        {
            GetDraftSummmariesAvailableForCheckout_FailTest(10, 47, 224, null, -1, null);
        }
        /// <summary>
        /// Test - empty award
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void AwardEmptyTest()
        {
            GetDraftSummmariesAvailableForCheckout_FailTest(10, 47, 224, null, null, 0);
        }
        /// <summary>
        /// Test - white space for award
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void AwardWhteSpaceTest()
        {
            GetDraftSummmariesAvailableForCheckout_FailTest(10, 47, 224, null, null, 0);
        }
        /// <summary>
        /// Test - supplying a value for cycle
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void CycleValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, 13, null, null);
        }
        /// <summary>
        /// Test - supplying a value for panel
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void PanelValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, null, 22, null);
        }
        #endregion
        /// <summary>
        /// Test -  supplying a value for award
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void AwardValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, null, null, 2155);
        }
        /// <summary>
        /// Test - supplying a value for cycle & panel
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void CyclePanelValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, 13, 44, null);
        }        /// <summary>
        /// Test - supplying a value for cycle; panel & award
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void CyclePanelAwardValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, 13, 33, 2155);
        }
        ///
        /// Test - supplying a valid value for cycle & award
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void CycleAwardValueTest()
        {
            //
            // Set up local data
            //
            ResultModel<ISummaryAssignedModel> resultModel = MakeResult();
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_SuccessTest(resultModel, 10, 47, 224, 13, null, 2155);
        }
        ///
        /// Test - zero user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void ZeroUserTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_FailTest(0, 47, 224, null, null, null);
        }
        ///
        /// Test - negative user id
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void NegativeoUserTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_FailTest(-70, 47, 224, null, null, null);
        }
        ///
        /// Test - empty program
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void EmptyProgramTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_FailTest(7, 0, 224, null, null, null);
        }
        ///
        /// Test - white space for program
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void WhiteSpaceProgramTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_FailTest(7, 0, 224, null, null, null);
        }
        ///
        /// Test - empty Fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void EmptyFiscalYearTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_FailTest(7, 47, 0, null, null, null);
        }
        ///
        /// Test - white space for Fiscal year
        /// </summary>
        [TestMethod()]
        [Category("SummaryProcessingServer.GetDraftSummmariesAvailableForCheckout")]
        public void WhiteSpaceFiscalYearTest()
        {
            //
            // Execute the test & test the assumptions
            //
            GetDraftSummmariesAvailableForCheckout_FailTest(7, 47, 0, null, null, null);
        }
        #region GetDraftSummmariesAvailableForCheckout Helpers
        private void GetDraftSummmariesAvailableForCheckout_SuccessTest(ResultModel<ISummaryAssignedModel> resultModel, int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetDraftSummmariesAvailableForCheckout(userId, program, fiscalYear, cycle, panelId, awardTypeId, true, true, true)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryAssignedModel> container = thetestSummaryProcessingService.GetDraftSummmariesAvailableForCheckout(userId, program, fiscalYear, cycle, panelId, awardTypeId, true, true, true);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private void GetDraftSummmariesAvailableForCheckout_FailTest(int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            //
            // set the expectations
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<ISummaryAssignedModel> container = thetestSummaryProcessingService.GetDraftSummmariesAvailableForCheckout(userId, program, fiscalYear, cycle, panelId, awardTypeId, true, true, true);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private ResultModel<ISummaryAssignedModel> MakeResult()
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
