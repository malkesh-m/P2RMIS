namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for displaying the content for an application workflow step.
    /// </summary>
    public class StepContentModel : IStepContentModel
    {
        /// <summary>
        /// Unique identifier for a summary form element
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; set; }
        /// <summary>
        /// Unique identifier for a piece of summary form content
        /// </summary>
        public int? ApplicationWorkflowStepContentId { get; set; }
        /// <summary>
        /// Display name for a summary form element
        /// </summary>
        public string ElementName { get; set; }
        /// <summary>
        /// The order in which form elements should be arranged
        /// </summary>
        public int ElementSortOrder { get; set; }
        /// <summary>
        /// Instructions for users describing how the element should be worked on
        /// </summary>
        public string ElementInstructions { get; set; }
        /// <summary>
        /// Whether the element can be associated with a score evaluation
        /// </summary>
        public bool ElementScoreFlag { get; set; }
        /// <summary>
        /// The score type of the element (if scored)
        /// </summary>
        public string ElementScoreType { get; set; }
        /// <summary>
        /// Whether the element can be associated with a text evaluation
        /// </summary>
        public bool ElementTextFlag { get; set; }
        /// <summary>
        /// true/false for if element is overall score
        /// </summary>
        public bool ElementOverallFlag { get; set; }
        /// <summary>
        /// Text evaluation for an element
        /// </summary>
        public string  ElementContentText { get; set; }
        /// <summary>
        /// Pre-summary text evaluation for an element
        /// </summary>
        public string ElementContentOriginalText { get; set; }
        /// <summary>
        /// Pre-meeting score provided for an evaluation element
        /// </summary>
        public decimal? ElementContentScore { get; set; }
        /// <summary>
        /// The adjectival label for the pre-meeting score (if adjectival scored)
        /// </summary>
        public string ElementContentAdjectivalLabel { get; set; }
        /// <summary>
        /// Meeting panel average score provided for an evaluation element
        /// </summary>
        public decimal? ElementContentAverageScore { get; set; }
        /// <summary>
        /// Last name of the reviewer who reviewed the application
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// First name of the reviewer who reviewed the application
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// The order the reviewer was assigned to evaluate the application
        /// </summary>
        public int? ReviewerAssignmentOrder { get; set; }
        /// <summary>
        /// The participation type the reviewer was assigned to the panel as
        /// </summary>
        public string ReviewerAssignmentType { get; set; }
        /// <summary>
        /// The level of permission a standard user has to the element for a given step
        /// </summary>
        public string AccessLevel { get; set; }
        /// <summary>
        /// High range that a score can occur
        /// </summary>
        public decimal? ElementScaleHighValue { get; set; }
        /// <summary>
        /// Low range that a score can occur
        /// </summary>
        public decimal? ElementScaleLowValue { get; set; }
        /// <summary>
        /// Standard deviation for the score
        /// </summary>
        public decimal? ElementScoreStandardDeviation { get; set; }
        /// <summary>
        /// Whether the element represents a discussion note on the criteria
        /// </summary>
        public bool DiscussionNoteFlag { get; set; }
        /// <summary>
        /// Whether the element represents the overview section
        /// </summary>
        public bool IsOverview { get; set; }
        /// <summary>
        /// Content text without track changes and comment markup
        /// </summary>
        public string ElementContentTextNoMarkup { get; set; }
    }
}

