using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ManageWorkflowViewModel : SSFilterMenuViewModel
    {
        #region Constants
        /// <summary>
        /// the ids for the Manage workflow screens
        /// </summary>
        public class ViewIds
        {
            public const string AwardIds = "awardIds";
            public const string PriorityOneWorkflows = "PriorityOneWorkflows";
            public const string PriorityTwoWorkflows = "PriorityTwoWorkflows";
            public const string NoPriorityWorkflows = "NoPriorityWorkflows";
        }
	    #endregion
        #region Constructors

        /// <summary>
        /// View model for displaying the available applications
        /// </summary>
        public ManageWorkflowViewModel()
            : base()
        {
            //
            // Allocate a dictionary so view can display nothing.
            //
            this.HideAwardCriteria = true;
            this.HidePanelCriteria = true;
            this.HideUserSearchCriteria = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// the list of workflows
        /// </summary>
        public List<KeyValuePair<int, string>> WorkflowDropDown { get; set; }
        /// <summary>
        /// variable for priority 1 workflows
        /// </summary>
        public List<int> PriorityOneWorkflows { get; set; }
        /// <summary>
        /// variable for priority 2 workflows
        /// </summary>
        public List<int> PriorityTwoWorkflows { get; set; }
        /// <summary>
        /// variable for no priority workflow
        /// </summary>
        public List<int> NoPriorityWorkflows { get; set; }
        /// <summary>
        /// variable for the program year text
        /// </summary>
        public string ProgramYear { get; set; }
        /// <summary>
        /// variable for the program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        #endregion

    }
}