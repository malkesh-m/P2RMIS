
namespace Sra.P2rmis.Bll.PanelManagement
{
    public enum ReviewerAssignmentStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Reviewer was successfully assigned
        /// </summary>
        Success = 1,
        /// <summary>
        /// Workflow was started & reviewer could not be unassigned
        /// </summary>
        ReviewerHasWorkflow = 2,
        /// <summary>
        /// Presentation position was currently occupied
        /// </summary>
        PositionOccupied = 3,
        /// <summary>
        /// Insufficient selection data was supplied to complete the assignment
        /// </summary>
        IncompleteAssignmentData = 4,
        /// <summary>
        /// Both COI type and comments are required for COI assignment
        /// </summary>
        MissingCoiTypeAndComments = 5,
        /// <summary>
        /// COI type is required for COI assignment
        /// </summary>
        MissingCoiType = 6,
        /// <summary>
        /// Comments is required for COI assignment
        /// </summary>
        MissingComments = 7
    }
}
