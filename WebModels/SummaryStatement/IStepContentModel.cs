namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public interface IStepContentModel
    {
        /// <summary>
        /// Unique identifier for a summary form element
        /// </summary>
        int ApplicationWorkflowStepElementId { get; set; }
        /// <summary>
        /// Unique identifier for a piece of summary form content
        /// </summary>
        int? ApplicationWorkflowStepContentId { get; set; }
        /// <summary>
        /// Display name for a summary form element
        /// </summary>
        string ElementName { get; set; }
        /// <summary>
        /// Instructions for users describing how the element should be worked on
        /// </summary>
        string ElementInstructions { get; set; }
        /// <summary>
        /// The order in which form elements should be arranged
        /// </summary>
        int ElementSortOrder { get; set; }
        /// <summary>
        /// Whether the element can be associated with a score
        /// </summary>
        bool ElementScoreFlag { get; set; }
        /// <summary>
        /// The score type of the element (if scored)
        /// </summary>
        string ElementScoreType { get; set; }
        /// <summary>
        /// Whether the element can be associated with text evaluation content
        /// </summary>
        bool ElementTextFlag { get; set; }
        /// <summary>
        /// true/false for if element is overall score
        /// </summary>
        bool ElementOverallFlag { get; set; }
        /// <summary>
        /// Text evaluation for an element
        /// </summary>
        string ElementContentText { get; set; }
        /// <summary>
        /// Pre-summary text evaluation for an element
        /// </summary>
        string ElementContentOriginalText { get; set; }
        /// <summary>
        /// Pre-meeting score provided for an evaluation element
        /// </summary>
        decimal? ElementContentScore { get; set; }
        /// <summary>
        /// The adjectival label for the pre-meeting score (if adjectival scored)
        /// </summary>
        string ElementContentAdjectivalLabel { get; set; }
        /// <summary>
        /// Meeting panel average score provided for an evaluation element
        /// </summary>
        decimal? ElementContentAverageScore { get; set; }
        /// <summary>
        /// Last name of the reviewer who reviewed the application
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// First name of the reviewer who reviewed the application
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// The order the reviewer was assigned to evaluate the application
        /// </summary>
        int? ReviewerAssignmentOrder { get; set; }
        /// <summary>
        /// The participation type the reviewer was assigned to the panel as
        /// </summary>
        string ReviewerAssignmentType { get; set; }
        /// <summary>
        /// The level of permission a standard user has to the element for a given step
        /// </summary>
        string AccessLevel { get; set; }

        /// <summary>
        /// High range that a score can occur
        /// </summary>
        decimal? ElementScaleHighValue { get; set; }

        /// <summary>
        /// Low range that a score can occur
        /// </summary>
        decimal? ElementScaleLowValue { get; set; }

        /// <summary>
        /// Standard deviation for the score
        /// </summary>
        decimal? ElementScoreStandardDeviation { get; set; }

        /// <summary>
        /// Whether the element represents a discussion note on the criteria
        /// </summary>
        bool DiscussionNoteFlag { get; set; }

        /// <summary>
        /// Whether the element represents the overview section
        /// </summary>
        bool IsOverview { get; set; }

        /// <summary>
        /// Content text without track changes and comment markup
        /// </summary>
        string ElementContentTextNoMarkup { get; set; }
    }
}
