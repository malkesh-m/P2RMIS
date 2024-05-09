namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Activities supported by the P2RMIS workflow
    /// </summary>
    public enum P2rmisActions
	{
        /// <summary>
        /// The default activity.  In this case nothing is executed within the activity
        /// </summary>
	    Default = 0,
        /// <summary>
        /// The Summary Statement Checkout button performs this activity.
        /// </summary>
        Checkout = 1,
        /// <summary>
        /// The Summary Statement Save button performs this activity.
        /// </summary>
        Save = 2,
        /// <summary>
        /// The Summary Statement Check-in button performs this activity.
        /// </summary>
        Checkin = 3,
        /// <summary>
        /// The Summary Statement Assign Workflow Step button performs this activity.
        /// </summary>
        AssignWorkflowStep = 4,
        /// <summary>
        /// The Summary Statement Assign User button performs this activity.
        /// </summary>
        AssignUser = 5,
        /// <summary>
        /// The PanelManagement reset to edit action performs this activity
        /// </summary>
        ResetToEditApplicationWorkflow = 6,
        //<summary>
        // Submit a critique (Submit is intended for critiques that were not checked out.  Only difference is the work log
        // is not updated.  The work log became OBE with the implementation of soft deletes.
        //</summary>
        Submit = 7,
        /// <summary>
        /// Check-in a summary statement when the workflow is going from Priority 1 to no Priority 1
        /// </summary>
        DeactivateClientReviewCheckinActivity = 8
	} 

}
