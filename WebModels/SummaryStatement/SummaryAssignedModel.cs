namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a user's workflow step assignments
    /// </summary>
    public class SummaryAssignedModel : SummaryStatementModel, ISummaryAssignedModel
    {
        /// <summary>
        /// checked-out user First Name
        /// </summary>
        public string CheckedoutUserFirstName { get; set; }
        /// <summary>
        /// checked-out user Last Name
        /// </summary>
        public string CheckedoutUserLastName { get; set; }
        /// <summary>
        /// checked out user Last Name
        /// </summary>
        public int CheckedoutUserId { get; set; }
        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is client review step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is client review step type; otherwise, <c>false</c>.
        /// </value>
        public bool IsClientReviewStepType { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is editing step type; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditingStepType { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is writing step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is writing step type; otherwise, <c>false</c>.
        /// </value>
        public bool IsWritingStepType { get; set; } = false;
    }
}
