using System.Collections.Generic;
using System;
namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Model container for summary setup information
    /// </summary>
    public interface ISummarySetupModel
    {
        /// <summary>
        /// Collection of available templates for assignment
        /// </summary>
        IEnumerable<SummaryTemplate> AvailableSummaryTemplates { get; set; }
        /// <summary>
        /// Collection of currently assigned reviewer descriptions
        /// </summary>
        IEnumerable<SummaryStatementReviewerDescription> ReviewerDescriptions { get; set; }
        /// <summary>
        /// Award type associated with the mechanism
        /// </summary>
        string Award { get; set; }
        /// <summary>
        /// Client to which the mechanism is related
        /// </summary>
        string Client { get; set; }
        /// <summary>
        /// Fiscal Year to which the mechanism is related
        /// </summary>
        string FiscalYear { get; set; }
        /// <summary>
        /// User's name who last updated the mechanism summary setup
        /// </summary>
        string LastUpdatedByLastName { get; set; }

        /// <summary>
        /// User's name who last updated the mechanism summary setup
        /// </summary>
        string LastUpdatedByFirstName { get; set; }
        /// <summary>
        /// Date the mechanism summary setup was last modified
        /// </summary>
        DateTime? LastUpdateDate { get; set; }
        /// <summary>
        /// Program to which the mechanism is related
        /// </summary>
        string Program { get; set; }
        /// <summary>
        /// Currently assigned summary template Id
        /// </summary>
        int? SelectedStandardSummaryTemplateId { get; set; }
        /// <summary>
        /// Currently assigned summary template for expedited Id
        /// </summary>
        int? SelectedExpeditedSummaryTemplateId { get; set; }
        /// <summary>
        /// Client identifier
        /// </summary>
        int ClientId { get; set; }

    }

    /// <summary>
    /// Model container for summary setup information
    /// </summary>
    public class SummarySetupModel : ISummarySetupModel
    {
        #region Properties
        /// <summary>
        /// Collection of available templates for assignment
        /// </summary>
        public IEnumerable<SummaryTemplate> AvailableSummaryTemplates { get; set; }

        /// <summary>
        /// Collection of currently assigned reviewer descriptions
        /// </summary>
        public IEnumerable<SummaryStatementReviewerDescription> ReviewerDescriptions { get; set; }
        /// <summary>
        /// Currently assigned summary template Id
        /// </summary>
        public int? SelectedStandardSummaryTemplateId { get; set; }
        /// <summary>
        /// Currently assigned summary template for expedited Id
        /// </summary>
        public int? SelectedExpeditedSummaryTemplateId { get; set; }
        /// <summary>
        /// Client identifier
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// User's name who last updated the mechanism summary setup
        /// </summary>
        public string LastUpdatedByLastName { get; set; }

        /// <summary>
        /// User's name who last updated the mechanism summary setup
        /// </summary>
        public string LastUpdatedByFirstName { get; set; }
        /// <summary>
        /// Date the mechanism summary setup was last modified
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }
        /// <summary>
        /// Client to which the mechanism is related
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// Fiscal Year to which the mechanism is related
        /// </summary>
        public string FiscalYear { get; set; }

        /// <summary>
        /// Program to which the mechanism is related
        /// </summary>
        public string Program { get; set; }

        /// <summary>
        /// Award type associated with the mechanism
        /// </summary>
        public string Award { get; set; }
        #endregion
    }

    /// <summary>
    /// Individual summary template info
    /// </summary>
    public class SummaryTemplate
    {
        /// <summary>
        /// Identifier for a client summary template id
        /// </summary>
        public int ClientSummaryTemplateId { get; set; }

        /// <summary>
        /// Display name for a template
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Whether the template is intended for use by expedited applications
        /// </summary>
        public bool IsExpedited { get; set; }
    }

    /// <summary>
    /// Individual reviewer descriptions for summary statement
    /// </summary>
    public class SummaryStatementReviewerDescription
    {
        /// <summary>
        /// Identifier for a summary reviewer description
        /// </summary>
        public int SummaryReviewerDescriptionId { get; set; }

        /// <summary>
        /// Order in which the reviewer is assigned to the application
        /// </summary>
        public int AssignmentOrder { get; set; }

        /// <summary>
        /// Order in which the reviewer displays in a summary statement
        /// </summary>
        public int CustomOrder { get; set; }

        /// <summary>
        /// Label given to reviewer in a summary statement
        /// </summary>
        public string DisplayName { get; set; }
    }
}
