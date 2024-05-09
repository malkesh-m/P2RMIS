using System;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest
{
    [TestClass]
    public partial class SummaryManagementControllerTest
    {
        #region MyRegion
        /// <summary>
        /// Test - that the service layer is invoked
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementController.Preview")] 
        public void PreviewTest()
        {
            int panelApplicationId = 7; 
            //
            // Do not really need to put anything into the program container since sorting and ordering is tested with the ControllerHelpers
            //
            Container<IStepContentModel> stepContentContainer = new Container<IStepContentModel>();
            IApplicationDetailModel applicationDetail = new ApplicationDetailModel();
            //
            // Set the expectations
            //
            Expect.Call(summaryManagementServiceMock.GetPreviewApplicationStepContent(panelApplicationId)).Return(stepContentContainer);
            Expect.Call(summaryManagementServiceMock.GetPreviewApplicationInfoDetail(panelApplicationId)).Return(applicationDetail);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var x = summaryStatementControllerMock.Preview(panelApplicationId);

            Assert.IsNotNull(x);
            PartialViewResult v = x as PartialViewResult;
            Assert.IsNotNull(v, "Partial view was not returned");
            Assert.AreEqual(v.ViewName, SummaryStatementController.ViewNames.Preview, "Unexpected view was returned");

            Assert.IsNotNull(v.Model);
            EditSummaryStatementViewModel m = v.Model as EditSummaryStatementViewModel;
            Assert.IsNotNull(m, "Unexpected model returned");
            Assert.IsNotNull(m.Criteria, "criteria was null and it should not be");
            Assert.IsNotNull(m.ApplicationDetails, "ApplicationDetails was null and it should not have been");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test - ensure Elmah is invoked for exceptions.
        /// </summary>
        [TestMethod()]
        [Category("SummaryManagementController.Preview")]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: context")]
        public void Preview2Test()
        {
            int panelApplicationId = 7;
            //
            // Do not really need to put anything into the program container since sorting and ordering is tested with the ControllerHelpers
            //
            Container<IStepContentModel> stepContentContainer = new Container<IStepContentModel>();
            IApplicationDetailModel applicationDetail = new ApplicationDetailModel();

            //
            // Set the expectations
            //
            Expect.Call(summaryManagementServiceMock.GetPreviewApplicationStepContent(panelApplicationId)).Return(null);
            Expect.Call(summaryManagementServiceMock.GetPreviewApplicationInfoDetail(panelApplicationId)).Return(applicationDetail);
            theMock.ReplayAll();
            //
            // Execute the method under test
            //
            var x = summaryStatementControllerMock.Preview(panelApplicationId);
        }
        #endregion
    }
}
