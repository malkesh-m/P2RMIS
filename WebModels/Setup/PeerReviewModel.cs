using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Peer review model container
    /// </summary>
    [Serializable]
    [XmlRoot("peer_review_data")]
    public class PeerReviewModelContainer
    {
        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        [XmlAttribute(AttributeName = "rows")]
        public int RowCount { get; set; }
        /// <summary>
        /// Gets or sets the peer review data.
        /// </summary>
        /// <value>
        /// The peer review data.
        /// </value>
        [XmlElement(ElementName = "row")]
        public List<PeerReviewModel> PeerReviewData { get; set; }
    }

    /// <summary>
    /// Peer review model
    /// </summary>
    [Serializable]
    public class PeerReviewModel
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        [XmlElement(DataType = "int",
        ElementName = "application_id")]
        public int ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "log_no")]
        public string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        [XmlElement(DataType = "int",
        ElementName = "panel_application_id")]
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "panel_name")]
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "panel_abrv")]
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [XmlElement(DataType = "dateTime",
        ElementName = "panel_start_date")]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [XmlElement(DataType = "dateTime",
        ElementName = "panel_end_date")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the meeting type.
        /// </summary>
        /// <value>
        /// The name of the meeting type.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "meeting_type")]
        public string MeetingTypeName { get; set; }
        /// <summary>
        /// Gets or sets the assignment release date.
        /// </summary>
        /// <value>
        /// The assignment release date.
        /// </value>
        [XmlElement(DataType = "date",
        ElementName = "assignment_release_date")]
        public DateTime? AssignmentReleaseDate { get; set; }
        /// <summary>
        /// Gets or sets the review status label.
        /// </summary>
        /// <value>
        /// The review status label.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "review_status")]
        public string ReviewStatusLabel { get; set; }
        /// <summary>
        /// Gets or sets the review status identifier.
        /// </summary>
        /// <value>
        /// The review status identifier.
        /// </value>
        [XmlElement(DataType = "int",
        ElementName = "review_status_id")]
        public int ReviewStatusId { get; set; }
        /// <summary>
        /// Gets or sets the average score.
        /// </summary>
        /// <value>
        /// The average score.
        /// </value>
        [XmlElement(DataType = "decimal",
        ElementName = "average_overall_score")]
        public decimal? AvgScore { get; set; }
        /// <summary>
        /// Gets or sets the st dev.
        /// </summary>
        /// <value>
        /// The st dev.
        /// </value>
        [XmlElement(DataType = "decimal",
        ElementName = "st_dev")]
        public decimal? StDev { get; set; }
        /// <summary>
        /// Gets or sets the screening teleconference start date.
        /// </summary>
        /// <value>
        /// The screening teleconference start date.
        /// </value>
        [XmlElement(DataType = "date",
        ElementName = "stm_start_date")]
        public DateTime? ScreeningTcDate { get; set; }
        /// <summary>
        /// Gets or sets the application's panel fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year associated with the panel.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "fy")]
        public string Year { get; set; }
        /// <summary>
        /// Gets or sets the application's receipt cycle.
        /// </summary>
        /// <value>
        /// The application's receipt cycle.
        /// </value>
        [XmlElement(DataType = "int",
        ElementName = "cycle")]
        public int ReceiptCycle { get; set; }
        /// <summary>
        /// Gets or sets the reviewers.
        /// </summary>
        /// <value>
        /// The reviewers.
        /// </value>
        [XmlElement(ElementName = "reviewers")]
        public PeerReviewReviewersModel Reviewers { get; set; } = new PeerReviewReviewersModel();

        
    }
    /// <summary>
    /// Reviewers model
    /// </summary>
    [Serializable]
    public class PeerReviewReviewersModel
    {
        /// <summary>
        /// Gets or sets the reviewers.
        /// </summary>
        /// <value>
        /// The reviewers.
        /// </value>
        [XmlElement(ElementName = "reviewer")]
        public List<PeerReviewReviewerModel> ReviewerList { get; set; } = new List<PeerReviewReviewerModel>();
    }
    /// <summary>
    /// Reviewer model
    /// </summary>
    [Serializable]
    public class PeerReviewReviewerModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "reviewer_first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "reviewer_last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the assignment label.
        /// </summary>
        /// <value>
        /// The assignment label.
        /// </value>
        [XmlElement(DataType = "string",
        ElementName = "assignment_label")]
        public string AssignmentLabel { get; set; }
        /// <summary>
        /// Gets or sets the client assignment type identifier.
        /// </summary>
        /// <value>
        /// The client assignment type identifier.
        /// </value>
        [XmlElement(DataType = "int",
        ElementName = "assignment_type_id")]
        public int? ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        [XmlElement(DataType = "int",
        ElementName = "assignment_sort_order")]
        public int? SortOrder { get; set; }
        /// <summary>
        /// Gets or sets the coi signed date.
        /// </summary>
        /// <value>
        /// The coi signed date.
        /// </value>
        [XmlElement(DataType = "dateTime",
        ElementName = "coi_signed_date")]
        public DateTime? CoiSignedDate { get; set; }
        /// <summary>
        /// Gets or sets the resolution date.
        /// </summary>
        /// <value>
        /// The resolution date.
        /// </value>
        [XmlElement(DataType = "dateTime",
        ElementName = "critique_submitted_date")]
        public DateTime? ResolutionDate { get; set; }
        /// <summary>
        /// Gets or sets the screening teleconference critique submission date.
        /// </summary>
        /// <value>
        /// The screening teleconference critique submission date.
        /// </value>
        [XmlElement(DataType = "dateTime",
        ElementName = "stm_critique_submitted_date")]
        public DateTime? ScreeningTcCritiqueDate { get; set; }
    }
}
