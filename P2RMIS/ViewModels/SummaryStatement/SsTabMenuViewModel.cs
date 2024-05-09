using System.Collections.Generic;
using Sra.P2rmis.Web.ViewModels.Shared;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    /// <summary>
    /// The view model for the tabbed list on the summary statement pages.
    /// </summary>
    public class SsTabMenuViewModel : TabMenuViewModel
    {
        #region Constructor
        public SsTabMenuViewModel() : base()
        {
            TabNames = new string[] 
            {
                "Set Review Priorities", "Manage Workflow", "Staged SS",
                "Overall Progress", "Available Draft Summary Statements", "My Draft Summary Statements",
                "Deliverables"
            };
            TabLinks = new string[]
            {
                "/SummaryStatementReview/RequestReview", "/SummaryStatement/ManageWorkflow", "/SummaryStatement/Index",
                "/SummaryStatement/Progress", "/SummaryStatementProcessing/Index", "/SummaryStatementProcessing/Assignments",
                "/SummaryStatement/Completed"
            };
            TabRequiredPermissions = new string[]
            {
                Permissions.SummaryStatement.Manage, Permissions.SummaryStatement.Manage, Permissions.SummaryStatement.Manage,
                Permissions.SummaryStatement.Manage, Permissions.SummaryStatement.ProcessOrReview, Permissions.SummaryStatement.ProcessOrReview,
                Permissions.SummaryStatement.Manage
            };
            SetTabs();
        }
        #endregion
    }
}