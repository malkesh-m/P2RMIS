using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Web.Controllers;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestMethod = NUnit.Framework.TestAttribute;


namespace WebTest
{
    public partial class SummaryManagementControllerTest
    {
        #region ViewNotes Tests          
        /// <summary>
        /// Test the controller method ViewNotes with good value
        /// </summary>
        [TestMethod]
        [Category("SummaryManagementController.ViewNotes")]
        public void ProgressMarkApplications_ViewNotesTest()
        {
            string appId = "BC140001";
            int panelApplicationId = 5;
            Container<ISummaryCommentModel> container = new Container<ISummaryCommentModel>();
            List<ISummaryCommentModel> list = new List<ISummaryCommentModel>();
            list.Add(new SummaryCommentModel());
            list.Add(new SummaryCommentModel());
            list.Add(new SummaryCommentModel());
            container.ModelList = list;
            //
            // Set up the expectations
            //
            Expect.Call(summaryManagementServiceMock.GetApplicationSummaryComments(panelApplicationId)).Return(container);
            theMock.ReplayAll();
            //
            // Now execute the test
            //
            var result = summaryStatementControllerMock.ViewNotes(panelApplicationId);
            //
            // Verify the assertions
            //
            Assert.IsNotNull(result);
            PartialViewResult view = result as PartialViewResult;
            Assert.IsNotNull(view, "Partial view was not returned");
            Assert.AreEqual(view.ViewName, SummaryStatementController.ViewNames.Notes, "Unexpected view was returned");

            NotesViewModel m = view.Model as NotesViewModel;
            Assert.IsNotNull(m, "Unexpected model returned");
            Assert.AreEqual(list.Count, m.Notes.Count<ISummaryCommentModel>(), "Unexpected count of model list");
            theMock.VerifyAll();
        }
        #endregion
    }
}
