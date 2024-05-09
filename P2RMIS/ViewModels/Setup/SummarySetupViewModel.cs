using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for summary setup page
    /// </summary>
    public class SummarySetupViewModel
    {
        #region Properties
        /// <summary>
        /// Collection of available templates for assignment
        /// </summary>
        public List<KeyValuePair<int, string>> AvailableStandardSummaryTemplates { get; set; }
        /// <summary>
        /// Collection of available templates for assignment
        /// </summary>
        public List<KeyValuePair<int, string>> AvailableExpeditedSummaryTemplates { get; set; }
        /// <summary>
        /// Currently assigned summary template Id for standard applications
        /// </summary>
        public int? SelectedStandardSummaryTemplateId { get; set; }
        /// <summary>
        /// Currently assigned summary template Id for expedited applications
        /// </summary>
        public int? SelectedExpeditedSummaryTemplateId { get; set; }
        /// <summary>
        /// User's name who last updated the mechanism summary setup
        /// </summary>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// Date the mechanism summary setup was last modified
        /// </summary>
        public string LastUpdateDate { get; set; }
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
        /// <summary>
        /// Identifier for a program mechanism
        /// </summary>
        public int ProgramMechanismId { get; set; }
        /// <summary>
        /// Available assignment types in which reviewer description can be associated
        /// </summary>
        public List<KeyValuePair<int, string>> AvailableAssignmentTypes { get; set; }
        /// <summary>
        /// Collection of custom reviewer descriptions associated with the summary template
        /// </summary>
        public List<ReviewerDescriptionViewModel> ReviewerDescriptions { get; set; }
        /// <summary>
        /// Whether the template was modified
        /// </summary>
        public bool IsTemplateEdit { get; set; }
        /// <summary>
        /// Whether the description was modified
        /// </summary>
        public bool IsDescriptionEdit { get; set; }
        /// <summary>
        /// Available values for assignment and display order
        /// </summary>
        public List<int> AssignmentOrderDropdown => Enumerable.Range(1, 15).ToList();
        #endregion
        #region Methods and Helpers
        internal void Populate(ISummarySetupModel summaryData, int programMechanismId)
        {
            SelectedStandardSummaryTemplateId = summaryData.SelectedStandardSummaryTemplateId;
            SelectedExpeditedSummaryTemplateId = summaryData.SelectedExpeditedSummaryTemplateId;
            LastUpdatedBy = ViewHelpers.ConstructNameWithSpace(summaryData.LastUpdatedByFirstName, summaryData.LastUpdatedByLastName);
            LastUpdateDate = ViewHelpers.FormatDate(summaryData.LastUpdateDate);
            Client = summaryData.Client;
            Program = summaryData.Program;
            Award = summaryData.Award;
            FiscalYear = summaryData.FiscalYear;
            AvailableStandardSummaryTemplates = ExtractAvailableTemplateDropdown(summaryData.AvailableSummaryTemplates, false);
            AvailableExpeditedSummaryTemplates = ExtractAvailableTemplateDropdown(summaryData.AvailableSummaryTemplates, true);
            ProgramMechanismId = programMechanismId;
            ReviewerDescriptions = new List<ReviewerDescriptionViewModel>();

            PopulateReviewerDescriptions(summaryData.ReviewerDescriptions);
        }

        private List<KeyValuePair<int, string>> ExtractAvailableTemplateDropdown(IEnumerable<SummaryTemplate> templates, bool expedited)
        {
            return templates.Where(x => x.IsExpedited == expedited)
                            .OrderBy(o => o.TemplateName)
                            .Select(x => new KeyValuePair<int, string>(x.ClientSummaryTemplateId, x.TemplateName)).ToList();
        }

        private void PopulateReviewerDescriptions (IEnumerable<SummaryStatementReviewerDescription> descriptions)
        {
            descriptions.OrderBy(o => o.AssignmentOrder).ToList().ForEach(x => ReviewerDescriptions.Add(
                new ReviewerDescriptionViewModel(x.SummaryReviewerDescriptionId, x.AssignmentOrder, x.CustomOrder, x.DisplayName)));
        }
        #endregion
    }
    #region Sub view model
    /// <summary>
    /// Model for a reviewer description
    /// </summary>
    public class ReviewerDescriptionViewModel
    {
        #region Constructor
        internal ReviewerDescriptionViewModel (int reviewerDescriptionId, int assignmentOrder, int displayOrder, string displayLabel)
        {
            ReviewerDescriptionId = reviewerDescriptionId;
            AssignmentOrder = assignmentOrder;
            DisplayOrder = displayOrder;
            DisplayName = displayLabel;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReviewerDescriptionViewModel()
        { }
        #endregion
        #region Properties
        /// <summary>
        /// Unique identifier for a reviewer description
        /// </summary>
        public int ReviewerDescriptionId { get; set; }

        /// <summary>
        /// Order in which the reviewer was assigned to present/review
        /// </summary>
        public int AssignmentOrder { get; set; }

        /// <summary>
        /// Order in which the reviewer should display on the summary statement
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Name/label which displays on the summary statement for the reviewer
        /// </summary>
        public string DisplayName { get; set; }
        #endregion
    }
    #endregion
}