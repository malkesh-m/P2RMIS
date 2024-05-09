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
    /// Unit test for SummaryManagementServer class.
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest: BLLBaseTest
    {
        #region Attributes
        private int _badId = -75;
        private int _noResultsId = 9538;
        #endregion
        #region Overhead

        // TestContext moved to seperate file in the partial class

        #region Additional test attributes

        // ClassInitialize, Cleanup and Initialize moved to a seperate file in the partial class
        #endregion
        #endregion
        #region The GetWorkflowTransactionHistory Tests
        /// <summary>
        /// Test - results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowTransactionHistory")]
        public void GetWorkflowTransactionHistory_GoodGetTest()
        {
            //
            // Set up local data
            //
            //ResultModel<IWorkflowTransactionModel> resultModel = new ResultModel<IWorkflowTransactionModel>();
            //List<IWorkflowTransactionModel> list = new List<IWorkflowTransactionModel>();
            //list.Add(new WorkflowTransactionModel { Action = _actionCheckIn, PhaseName = _stepName1 });
            //list.Add(new WorkflowTransactionModel { Action = _actionCheckOut, PhaseName = _stepName1 });
            //list.Add(new WorkflowTransactionModel { Action = _actionCheckIn, PhaseName = _stepName2 });
            //resultModel.ModelList = list;

            //GetWorkflowTransactionHistorySuccessTest(resultModel, _goodId);
        }
        /// <summary>
        /// Test - no results returned from repository
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowTransactionHistory")]
        public void GetWorkflowTransactionHistory_GoodGetZeroTest()
        {
            //
            // Set up local data
            //
            ResultModel<IWorkflowTransactionModel> resultModel = new ResultModel<IWorkflowTransactionModel>();
            List<IWorkflowTransactionModel> list = new List<IWorkflowTransactionModel>();
            resultModel.ModelList = list;
            GetWorkflowTransactionHistorySuccessTest(resultModel, _noResultsId);
        }
        /// <summary>
        /// Test negative application workflow id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowTransactionHistory")]
        public void GetWorkflowTransactionHistory_NegativeIdTest()
        {
            GetWorkflowTransactionHistoryFailTest(_badId);
        }
        /// <summary>
        /// Test zero application workflow id test
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementServer.GetWorkflowTransactionHistory")]
        public void GetWorkflowTransactionHistory_ZeroIdTest()
        {
            GetWorkflowTransactionHistoryFailTest(0);
        }
        #endregion
        #region Helpers
        private void GetWorkflowTransactionHistorySuccessTest(ResultModel<IWorkflowTransactionModel> resultModel, int workflowId)
        {
            //
            // set the expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetWorkflowTransactionHistory(workflowId)).Return(resultModel);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IWorkflowTransactionModel> container = testService.GetWorkflowTransactionHistory(workflowId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(resultModel.ModelList.Count(), container.ModelList.Count(), "Container ModelList count is not correct");
            theMock.VerifyAll();
        }
        private void GetWorkflowTransactionHistoryFailTest(int workflowId)
        {
            //
            // Set up local data
            //
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            Container<IWorkflowTransactionModel> container = testService.GetWorkflowTransactionHistory(workflowId);
            //
            // Test the assertions
            //
            Assert.IsNotNull(container, "Returned container is null but should not be");
            Assert.IsNotNull(container.ModelList, "Container ModelList is null but should not be");
            Assert.AreEqual(0, container.ModelList.Count(), "Container ModelList count is not correct");
            theWorkMock.AssertWasNotCalled(s => s.SummaryManagementRepository);
        }
        #endregion
    }
}
