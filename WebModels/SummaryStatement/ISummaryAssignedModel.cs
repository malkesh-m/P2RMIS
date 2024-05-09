namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a summary statement
    /// </summary>
    public interface ISummaryAssignedModel : ISummaryStatementModel
    {
        /// <summary>
        /// checked-out user First Name
        /// </summary>
        string CheckedoutUserFirstName { get; set; }
        /// <summary>
        /// checked-out user Last Name
        /// </summary>
        string CheckedoutUserLastName { get; set; }
        /// <summary>
        /// checked out user Last Name
        /// </summary>
        int CheckedoutUserId { get; set; }
        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is client review step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is client review step type; otherwise, <c>false</c>.
        /// </value>
        bool IsClientReviewStepType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is editing step type; otherwise, <c>false</c>.
        /// </value>
        bool IsEditingStepType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is writing step type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is writing step type; otherwise, <c>false</c>.
        /// </value>
        bool IsWritingStepType { get; set; }
    }
}
