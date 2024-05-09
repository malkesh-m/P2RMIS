using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.SummaryStatement;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace WebTest
{
    /// <summary>
    /// Unit test for SummaryStatementController Assignment methods.
    /// </summary>
    /// <remarks>
    /// Eventually the class should be renamed to SummaryStatementControllerTests to reflects the correct class name.
    /// </remarks>
    [TestClass]
    public partial class SummaryManagementControllerTest
    {
        #region Attributes
        string[] workflowIds = new string[3] { "10", "11", "12" };
        string[] assigneeIds = new string[3] { "110", "111", "112" };
        string[] appIds = new string[3] { "210", "211", "212" };

        string[] targetWorkflowStepIds = new string[3] { "310", "311", "312" };
        string[] currentWorkflowStepIds = new string[3] { "410", "411", "412" };
        string[] matchingCurrentWorkflowStepIds = new string[3] { "310", "311", "312" };
        #endregion
        /// <summary>
        /// Test that building of the workflow parameter collection build all entries when
        /// they are different
        /// </summary>
        // Uncomment this test when UI complete [TestMethod()]
        [Category("SummaryManagementController.Assignment")]
        public void AssignAppsToUsersBuildWorkflowStepCollectionTest()
        {
            ///
            // Use this data
            //
            var formCollection = CreateTestingFormCollection(workflowIds, assigneeIds, appIds, targetWorkflowStepIds, currentWorkflowStepIds);
            //
            // This is the return from GetAssignedUsers
            //
            var assignedUserContainer = new Container<IUserApplicationModel>();
            //
            // Set the expectation
            //
            if (theWorkflowServiceMock == null) Console.WriteLine("mock is null");
            Expect.Call(delegate { theWorkflowServiceMock.ExecuteAssignWorkflow(null, _goodUserId); }).IgnoreArguments();
            Expect.Call(delegate { theWorkflowServiceMock.ExecuteAssignUser(null); }).IgnoreArguments();
            Expect.Call(summaryManagementServiceMock.GetAssignedUsers(null)).IgnoreArguments().Return(assignedUserContainer);
            Expect.Call(summaryStatementControllerMock.GetUserId()).Return(_goodUserId);
            theMock.ReplayAll();
            //
            // Run the test
            //
            summaryStatementControllerMock.AssignAppsToUsers(formCollection);
            //
            // Now verify
            //
            IList<object[]> argsPerCall = theWorkflowServiceMock.GetArgumentsForCallsMadeOn(x => x.ExecuteAssignWorkflow(null, _goodUserId));

            Assert.AreEqual(_goodUserId, (int)argsPerCall[0][1], "User id not as expected");
            var anArgument = argsPerCall[0][0] as ICollection<AssignWorkflowStep>;
            Assert.IsNotNull(anArgument, "AssignWorkflowStep argument was not as expected was null");
            Assert.AreEqual(targetWorkflowStepIds.Length, anArgument.Count, "Invalid number of assignment values passed.");
            //
            // This test is only concerned with the functionality for building the AssignmentWorkflow parameters.  Consequently 
            // the other expectations are not verified.
            //
        }
        /// <summary>
        /// Test that building of the workflow parameter collection build all entries when
        /// they are different
        /// </summary>
        // Uncomment this test when UI complete [TestMethod()]
        [Category("SummaryManagementController.Assignment")]
        public void AssignAppsToUsersBuildWorkflowStepCollectionAllMatchTest()
        {
            ///
            // Use this data
            //
            var formCollection = CreateTestingFormCollection(workflowIds, assigneeIds, appIds, targetWorkflowStepIds, matchingCurrentWorkflowStepIds);
            //
            // This is the return from GetAssignedUsers
            //
            var assignedUserContainer = new Container<IUserApplicationModel>();
            //
            // Set the expectation
            //
            Expect.Call(delegate { theWorkflowServiceMock.ExecuteAssignWorkflow(null, _goodUserId); }).IgnoreArguments();
            Expect.Call(delegate { theWorkflowServiceMock.ExecuteAssignUser(null); }).IgnoreArguments();
            Expect.Call(summaryManagementServiceMock.GetAssignedUsers(null)).IgnoreArguments().Return(assignedUserContainer);
            Expect.Call(summaryStatementControllerMock.GetUserId()).Return(_goodUserId);
            theMock.ReplayAll();
            //
            // Run the test
            //
            summaryStatementControllerMock.AssignAppsToUsers(formCollection);
            //
            // Now verify
            //
            IList<object[]> argsPerCall = theWorkflowServiceMock.GetArgumentsForCallsMadeOn(x => x.ExecuteAssignWorkflow(null, _goodUserId));

            Assert.AreEqual(_goodUserId, (int)argsPerCall[0][1], "User id not as expected");
            var anArgument = argsPerCall[0][0] as ICollection<AssignWorkflowStep>;
            Assert.IsNotNull(anArgument, "AssignWorkflowStep argument was not as expected was null");
            Assert.AreEqual(0, anArgument.Count, "Invalid number of assignment values passed.");
            //
            // This test is only concerned with the functionality for building the AssignmentWorkflow parameters.  Consequently 
            // the other expectations are not verified.
            //
        }

        /// <summary>
        /// Helper method to construct a NameValueCollection for controller testing
        /// </summary>
        /// <param name="key">Key to use</param>
        /// <param name="values">Array of values to use</param>
        /// <returns>NameValueCollection</returns>
        private NameValueCollection CreateAFormCollection(string key, string[]values)
        {
            var formValueCollection = new NameValueCollection();

            foreach (var aValue in values)
            {
                formValueCollection.Add(key, aValue);
            }

            return formValueCollection;
        }
        /// <summary>
        /// Create a FormCollection for AssignAppsToUsers testing
        /// </summary>
        /// <param name="workflowIds">Workflow identifiers</param>
        /// <param name="assigneeIds">Assignee identifiers</param>
        /// <param name="appIds">Application identifiers</param>
        /// <param name="targetWorkflowStepIds">Target workflow identifiers</param>
        /// <param name="currentWorkflowStepIds">Current workflow identifiers</param>
        /// <returns>FormCollection for testing</returns>
        private FormCollection CreateTestingFormCollection(string[] workflowIds, string[] assigneeIds, string[] appIds, string[] targetWorkflowStepIds, string[] currentWorkflowStepIds)
        {
            var formCollection = new FormCollection();
            formCollection.Add(CreateAFormCollection(AssignmentUpdateViewModel.Labels.FormWorkflowIds, workflowIds));
            formCollection.Add(CreateAFormCollection(AssignmentUpdateViewModel.Labels.FormUserIds, assigneeIds));
            formCollection.Add(CreateAFormCollection(AssignmentUpdateViewModel.Labels.FormApplicationIds, appIds));
            formCollection.Add(CreateAFormCollection(AssignmentUpdateViewModel.Labels.FormTargetWorkflowStepIds, targetWorkflowStepIds));
            formCollection.Add(CreateAFormCollection(AssignmentUpdateViewModel.Labels.FormCurrentWorkflowStepIds, currentWorkflowStepIds));
            return formCollection;
        }

    }
}
