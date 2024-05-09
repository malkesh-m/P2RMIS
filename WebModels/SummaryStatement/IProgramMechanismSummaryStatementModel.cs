namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public interface IProgramMechanismSummaryStatementModel
    {
        /// <summary>
        /// Gets or sets the template location.
        /// </summary>
        /// <value>
        /// The template location.
        /// </value>
        string TemplateLocation { get; set; }
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// The name of the stored procedure.
        /// </value>
        string StoredProcedureName { get; set; }
    }
}
