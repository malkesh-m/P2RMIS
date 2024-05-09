using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest
{
    /// <summary>
    /// Unit tests for SummaryManagementService.GetCompletedProgressApplications
    /// </summary>
    [TestClass()]
    public partial class SummaryManagementServiceTest: BLLBaseTest
    {
        #region Attributes
        private int _panelId = 54;
        private int _cycle = 3;
        #endregion
        #region The Tests
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodId_1Test()
        {
            //
            // Set up local data
            //
            IApplicationsProgress ap1 = new ApplicationsProgress { };
            IApplicationsProgress ap2 = new ApplicationsProgress { };
            IApplicationsProgress ap3 = new ApplicationsProgress { };
            IApplicationsProgress ap4 = new ApplicationsProgress { };

            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            theList.Add(ap1);
            theList.Add(ap2);
            theList.Add(ap3);
            theList.Add(ap4);
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, null, null);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodId_2Test()
        {
            //
            // Set up local data
            //
            IApplicationsProgress ap1 = new ApplicationsProgress { };
            IApplicationsProgress ap2 = new ApplicationsProgress { };
            IApplicationsProgress ap3 = new ApplicationsProgress { };
            IApplicationsProgress ap4 = new ApplicationsProgress { };

            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            theList.Add(ap1);
            theList.Add(ap2);
            theList.Add(ap3);
            theList.Add(ap4);
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, null, 347);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodId_3Test()
        {
            //
            // Set up local data
            //
            IApplicationsProgress ap1 = new ApplicationsProgress { };
            IApplicationsProgress ap2 = new ApplicationsProgress { };
            IApplicationsProgress ap3 = new ApplicationsProgress { };
            IApplicationsProgress ap4 = new ApplicationsProgress { };

            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            theList.Add(ap1);
            theList.Add(ap2);
            theList.Add(ap3);
            theList.Add(ap4);
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 0, null);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodId_4Test()
        {
            //
            // Set up local data
            //
            IApplicationsProgress ap1 = new ApplicationsProgress { };
            IApplicationsProgress ap2 = new ApplicationsProgress { };
            IApplicationsProgress ap3 = new ApplicationsProgress { };
            IApplicationsProgress ap4 = new ApplicationsProgress { };

            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            theList.Add(ap1);
            theList.Add(ap2);
            theList.Add(ap3);
            theList.Add(ap4);
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 0, 112244);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodId_5Test()
        {
            //
            // Set up local data
            //
            IApplicationsProgress ap1 = new ApplicationsProgress { };
            IApplicationsProgress ap2 = new ApplicationsProgress { };
            IApplicationsProgress ap3 = new ApplicationsProgress { };
            IApplicationsProgress ap4 = new ApplicationsProgress { };

            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            theList.Add(ap1);
            theList.Add(ap2);
            theList.Add(ap3);
            theList.Add(ap4);
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 353, null);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodId_6Test()
        {
            //
            // Set up local data
            //
            IApplicationsProgress ap1 = new ApplicationsProgress { };
            IApplicationsProgress ap2 = new ApplicationsProgress { };
            IApplicationsProgress ap3 = new ApplicationsProgress { };
            IApplicationsProgress ap4 = new ApplicationsProgress { };

            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            theList.Add(ap1);
            theList.Add(ap2);
            theList.Add(ap3);
            theList.Add(ap4);
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 353, 5555);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodIdNoResults_1Test()
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, null, null);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodIdNoResults_2Test()
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, null, 347);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodIdNoResults_3Test()
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 0, null);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodIdNoResults_4Test()
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 0, 112244);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodIdNoResults_5Test()
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 353, null);
        }
        /// <summary>
        /// Test - we get the results of the service call
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_GoodIdNoResults_6Test()
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            List<IApplicationsProgress> theList = new List<IApplicationsProgress>();
            result.ModelList = theList;
            GetCompletedProgressApplicationsSuccess(result, _panelId, _cycle, 353, 5555);
        }
        /// <summary>
        /// Test - negative panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_NegativeIdT_1Test()
        {
            GetCompletedProgressApplicationsFail(-1, _cycle, null, null);
        }
        /// <summary>
        /// Test - negative panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_NegativeIdT_2Test()
        {
            GetCompletedProgressApplicationsFail(-1, _cycle, null, 456);
        }
        /// <summary>
        /// Test - negative panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_NegativeIdT_3Test()
        {
            GetCompletedProgressApplicationsFail(-1, _cycle, 0, null);
        }
        /// <summary>
        /// Test - negative panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_NegativeIdT_4Test()
        {
            GetCompletedProgressApplicationsFail(-1, _cycle, 0, 32);
        }
        /// <summary>
        /// Test - negative panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_NegativeIdT_5Test()
        {
            GetCompletedProgressApplicationsFail(-1, _cycle, 353, null);
        }
        /// <summary>
        /// Test - negative panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_NegativeIdT_6Test()
        {
            GetCompletedProgressApplicationsFail(-1, _cycle, 353, 7890);
        }
        #endregion
        /// <summary>
        /// Test - zero panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_ZeroIdT_1Test()
        {
            GetCompletedProgressApplicationsFail(0, Cycle1, null, null);
        }
        /// <summary>
        /// Test - zero panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_ZeroIdT_2Test()
        {
            GetCompletedProgressApplicationsFail(0, Cycle1, null, 456);
        }
        /// <summary>
        /// Test - zero panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_ZeroIdT_3Test()
        {
            GetCompletedProgressApplicationsFail(0, Cycle1, 0, null);
        }
        /// <summary>
        /// Test - zero panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_ZeroIdT_4Test()
        {
            GetCompletedProgressApplicationsFail(0, Cycle1, 0, 32);
        }
        /// <summary>
        /// Test - zero panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_ZeroIdT_5Test()
        {
            GetCompletedProgressApplicationsFail(0, Cycle1, 353, null);
        }
        /// <summary>
        /// Test - zero panel Id
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementService.GetCompletedProgressApplications")]
        public void GetCompletedProgressApplications_ZeroIdT_6Test()
        {
            GetCompletedProgressApplicationsFail(0, Cycle1, 353, 7890);
        }
        #region Helpers
        private void GetCompletedProgressApplicationsFail(int panelId, int cycle, int? awardTypeId, int? userId)
        {
            // Set expectations
            theMock.ReplayAll();
            //
            //
            // Test
            //
            var z = testService.GetCompletedProgressApplications(panelId, cycle, awardTypeId, userId);
            //
            // Verify
            //
            Assert.IsNotNull(z, "Service did not return a object");
            Assert.IsNotNull(z.ModelList, "Model list was null but it should not be");
            Assert.AreEqual(0, z.ModelList.Count<IApplicationsProgress>(), "Model list did not match expected count");
            theMock.VerifyAll();
        }
        private void GetCompletedProgressApplicationsSuccess(ResultModel<IApplicationsProgress> resultToReturn, int panelId, int cycle,  int? awardTypeId, int? userId)
        {
            //
            // Set expectations
            //
            Expect.Call(theWorkMock.SummaryManagementRepository).Return(theSummaryManagementRepositoryMock);
            Expect.Call(theSummaryManagementRepositoryMock.GetCompletedProgressApplications(panelId, cycle, awardTypeId, userId)).Return(resultToReturn);
            theMock.ReplayAll();
            //
            // Test
            //
            var z = testService.GetCompletedProgressApplications(panelId, cycle, awardTypeId, userId);
            //
            // Verify
            //
            Assert.IsNotNull(z, "Service did not return a object");
            Assert.IsNotNull(z.ModelList, "Model list was null but it should not be");
            Assert.AreEqual(resultToReturn.ModelList.Count<IApplicationsProgress>(), z.ModelList.Count<IApplicationsProgress>(), "Model list did not match expected count");
            theMock.VerifyAll();
        }
        #endregion
    }
}
