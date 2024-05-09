namespace Sra.P2rmis.Web.Common
{
    public class Message: ITest
    {
        public string TheMessage
        {
            get { return "This came from the dependent object"; }
        }
        /// <summary>
        /// Message when the scoring hasn't been set up
        /// </summary>
        public static string ScoringNotSetupMessage
        {
            get { return "Scoring hasn't been set up so applications cannot be released."; }
        }
        /// <summary>
        /// Message when the reviewer has workflow(s) associated
        /// </summary>
        public static string ReviewerHasWorkflowMessage
        {
            get { return "Workflow was started & reviewer could not be unassigned. To proceed, the associated workflow/critiques data will be deleted."; }
        }
        /// <summary>
        /// Message when the presentation position was currently occupied
        /// </summary>
        public static string PositionOccupiedMessage
        {
            get { return "Presentation position was currently occupied.";  }
        }
        /// <summary>
        /// Message when insufficient selection data was supplied
        /// </summary>
        public static string IncompleteAssignmentDataMessage
        {
            get { return "Insufficient selection data was supplied to complete the assignment."; }
        }
        /// <summary>
        /// Message when both COI type and comments are missing
        /// </summary>
        public static string MissingCoiTypeAndCommentsMessage
        {
            get { return "Both COI type and comments are required for COI assignment."; }
        }
        /// <summary>
        /// Message when COI type is missing
        /// </summary>
        public static string MissingCoiTypeMessage
        {
            get { return "COI type is required for COI assignment."; }
        }
        /// <summary>
        /// Message when Comments is missing
        /// </summary>
        public static string MissingCommentsMessage
        {
            get { return "Comments is required for COI assignment."; }
        }
        /// <summary>
        /// Message when a reviewer is assigned to COI
        /// </summary>
        public static string AssignReviewerToCoiMessage
        {
            get { return "The reviewer will be assigned as a COI."; }
        }
        /// <summary>
        /// Message when a reviewer is assigned to COI
        /// </summary>
        public static string AssignSelfToCoiMessage
        {
            get { return "You have declared yourself as a Conflict of Interest."; }
        }
    }
}
