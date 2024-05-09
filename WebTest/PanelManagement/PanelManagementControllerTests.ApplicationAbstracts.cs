using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Web.Controllers.PanelManagement;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.PanelManagement;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest.PanelManagement
{
    /// <summary>
    /// Unit test for SummaryStatementController Assignment methods.
    /// </summary>
    public partial class PanelManagementControllerTests
    {
        #region The Tests
        /// <summary>
        /// Test for SRO
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementController.Assignments")]
        public void IndexForSroGoodTest()
        {
            //
            // Set up local data
            //
            int? sessionProgramYearId = null;
            int sessionPanelId = 5;
            
            Container<IApplicationInformationModel> container = BuildContainer<IApplicationInformationModel, ApplicationInformationModel>(7);
            Container<IPanelSignificationsModel> container2 = BuildContainer<IPanelSignificationsModel, PanelSignificationsModel>(3);
            Assert.IsNotNull(container2);
            //
            // Set the expectations
            //
            MockGetUserId(this._goodId1);
            Expect.Call(this.thePanelManagementListServiceMock.ListApplicationInformation(sessionPanelId)).Return(container);
            MockGetSelectProgramPanelPermission(false);
            MockListPanelSignifications(this._goodId1, container2);
            MockSetPanelSession(sessionPanelId);
            MockGetPanelSession(sessionPanelId);
            theMock.ReplayAll();
            //
            // Test
            //
            var result = panelManagementControllerMock.Index(sessionProgramYearId, sessionPanelId);
            //
            // Verify the assertions
            //
            Assert.IsNotNull(result);
            ViewResult view = result as ViewResult;
            Assert.IsNotNull(view, "View result was not returned");
            Assert.AreEqual(view.ViewName, PanelManagementController.ViewNames.ApplicationAbstracts, "Unexpected view was returned ");

            ViewApplicationViewModel model = view.Model as ViewApplicationViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(sessionPanelId, model.SelectedPanel, "Selected panel was not as expected");
            Assert.AreEqual(container.ModelList.Count(), model.Applications.Count(), "Model list's of application information did not contain the correct number of entries");
            //
            // needs an assertion to check if the panel list has been set but do not have that method yet.
            //
            Assert.IsNotNull(model.Panels, "Panel list should not be null");
            Assert.AreEqual(container2.ModelList.Count(), model.Panels.Count(), "Model list's of panels did not contain the correct number of entries");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test for SRM
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementController.Assignments")]
        public void IndexForSrmGoodTest()
        {
            //
            // Set up local data
            //
            int sessionProgramYearId = 2;
            int sessionPanelId = 5;

            Container<IApplicationInformationModel> container = BuildContainer<IApplicationInformationModel, ApplicationInformationModel>(7);
            Container<IPanelSignificationsModel> container2 = BuildContainer<IPanelSignificationsModel, PanelSignificationsModel>(3);
            Container<IProgramYearModel> container3 = BuildContainer<IProgramYearModel, ProgramYearModel>(2);
            Assert.IsNotNull(container2);
            //
            // Set the expectations
            //
            MockGetUserId(this._goodId1);
            Expect.Call(this.thePanelManagementListServiceMock.ListApplicationInformation(sessionPanelId)).Return(container);
            MockGetSelectProgramPanelPermission(true);
            MockListPanelSignifications(this._goodId1, sessionProgramYearId, container2);
            MockListProgramYears(this._goodId1, container3);
            MockSetProgramYearSession(sessionProgramYearId);
            MockGetProgramYearSession(sessionProgramYearId);
            MockSetPanelSession(sessionPanelId);
            MockGetPanelSession(sessionPanelId);
            theMock.ReplayAll();
            //
            // Test
            //
            var result = panelManagementControllerMock.Index(sessionProgramYearId, sessionPanelId);
            //
            // Verify the assertions
            //
            Assert.IsNotNull(result);
            ViewResult view = result as ViewResult;
            Assert.IsNotNull(view, "View result was not returned");
            Assert.AreEqual(view.ViewName, PanelManagementController.ViewNames.ApplicationAbstracts, "Unexpected view was returned ");

            ViewApplicationViewModel model = view.Model as ViewApplicationViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(sessionPanelId, model.SelectedPanel, "Selected panel was not as expected");
            Assert.AreEqual(container.ModelList.Count(), model.Applications.Count(), "Model list's of application information did not contain the correct number of entries");
            //
            // needs an assertion to check if the panel list has been set but do not have that method yet.
            //
            Assert.IsNotNull(model.Panels, "Panel list should not be null");
            Assert.AreEqual(container2.ModelList.Count(), model.Panels.Count(), "Model list's of panels did not contain the correct number of entries");
            theMock.VerifyAll();
        }
        /// <summary>
        /// Test to ensure Elmah is called when an exception occurs.
        /// </summary>
        [TestMethod()]
        [Category("PanelManagementController.Assignments")]
        [ExpectedException(typeof(ElmahTestException))]
        public void ElmahCatchTest()
        {
            int sessionProgramYearId = 2;
            int sessionPanelId = 5;
            ForceExceptionFromGetUserId(panelManagementControllerMock);
            ElmahErrorTest(panelManagementControllerMock);

            theMock.ReplayAll();
            //
            // Test
            //
            var result = panelManagementControllerMock.Index(sessionProgramYearId, sessionPanelId);
        }
        #endregion
    }
}
