namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// States of the workflow state machine.  This is the following transations
    /// that a workflow step will go through:
    /// 
    ///         STATE                
    ///           NOT_STARTED
    ///              --------------> O  (Activity)
    ///                              |
    ///           STARTED            |
    ///              <-------------- |
    ///                              |
    ///           STARTED            |
    ///              --------------> |
    ///                              |
    ///           COMPLETED          |
    ///              <-------------- |
    ///              
    ///   An activity could also return the following:
    ///           Any state
    ///              --------------> O  (Activity)
    ///                              |
    ///           FAIL               |
    ///              <-------------- |
    ///    in which case the workflow step is not moved to the next step.
    ///    
    /// </summary>
    public enum WorkflowState
    {
        Default = 0,
        /// <summary>
        /// Workflow step has not been started
        /// </summary>
        NotStarted = 1,
        /// <summary>
        /// Workflow step has been started 
        /// </summary>
        Started = 2,
        /// <summary>
        /// Workflow step has been completed.
        /// </summary>
        Complete = 3,
        /// <summary>
        /// A failure occurred while processing the activity.  
        /// Step should not be advanced to the next step.
        /// </summary>
        Fail = 4
    }
}
