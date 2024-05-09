namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public class ProgramMechanismSummaryStatementModel : IProgramMechanismSummaryStatementModel
    {
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// The name of the stored procedure.
        /// </value>
        public string StoredProcedureName { get; set; }
        /// <summary>
        /// Gets or sets the template location.
        /// </summary>
        /// <value>
        /// The template location.
        /// </value>
        public string TemplateLocation { get; set; }
    }
}