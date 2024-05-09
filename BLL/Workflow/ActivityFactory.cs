namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Factory creates the P2RMIS activity associated with summary statements.
    /// </summary>
    internal class ActivityFactory
    {
        #region Construction & Setup
        /// <summary>
        /// Private constructor - should not  be instances of the factory
        /// </summary>
        private ActivityFactory() { }
        #endregion
        /// <summary>
        /// Default activity that performs no actions.
        /// </summary>
        private readonly static P2rmisActivity _defaultActivity = new P2rmisActivity();
        /// <summary>
        /// Factory method to create P2RMIS activities.
        /// </summary>
        /// <param name="theActivity">Activity to create (P2rmisActions)</param>
        /// <returns>P2rmisActivity identified or default activity if factory does not identify parameter</returns>
        internal static P2rmisActivity CreateActivity(P2rmisActions theActivity)
        {
            P2rmisActivity result = _defaultActivity;
            //
            // Create the Activity based on the action specified.
            //
            switch (theActivity)
            {
                //
                // The summary statement check-in action
                //
                case P2rmisActions.Checkin:
                    {
                        result = new CheckinActivity();
                        break;
                    }
                //
                // The summary statement checkout action
                //
                case P2rmisActions.Checkout:
                    {
                        result = new CheckoutActivity();
                        break;
                    }
                //
                // The summary statement save action
                //
                case P2rmisActions.Save:
                    {
                        result = new SaveActivity();
                        break;
                    }
                //
                // The summary statement assign workflow step action
                //
                case P2rmisActions.AssignWorkflowStep:
                    {
                        result = new AssignWorkflowStepActivity();
                        break;
                    }
                //
                // The summary statement assign user action
                //
                case P2rmisActions.AssignUser:
                    {
                        result = new AssignUserActivity();
                        break;
                    }
                //
                // The reset application workflow to edit action
                //
                case P2rmisActions.ResetToEditApplicationWorkflow:
                    {
                        result = new ResetToEditWorkflowStepActivity();
                        break;
                    }
                //
                // Submit a critique - (check-in a critique without a workflow)
                //
                case P2rmisActions.Submit:
                    {
                        result = new SubmitActivity();
                        break;
                    }
                //
                // Checkin Activity when Deactivating a Client Review step
                //
                case P2rmisActions.DeactivateClientReviewCheckinActivity:
                    {
                        result = new DeactivateClientReviewCheckinActivity();
                        break;

                    }
                //
                // no know action was detected but return the default activity
                // which throws an exception if the Execute method is invoked.
                //
                default:
                    {
                        break;
                    }
            }
            return result;
        }
    }

}
