using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Peer review result model interface
    /// </summary>
    public interface IPeerReviewResultModel
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        int ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        DateTime? StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        DateTime? EndDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the meeting type.
        /// </summary>
        /// <value>
        /// The name of the meeting type.
        /// </value>
        string MeetingTypeName { get; set; }
        /// <summary>
        /// Gets or sets the assignment release date.
        /// </summary>
        /// <value>
        /// The assignment release date.
        /// </value>
        DateTime? AssignmentReleaseDate { get; set; }
        /// <summary>
        /// Gets or sets the review status identifier.
        /// </summary>
        /// <value>
        /// The review status identifier.
        /// </value>
        int ReviewStatusId { get; set; }
        /// <summary>
        /// Gets or sets the review status label.
        /// </summary>
        /// <value>
        /// The review status label.
        /// </value>
        string ReviewStatusLabel { get; set; }
        /// <summary>
        /// Gets or sets the average score.
        /// </summary>
        /// <value>
        /// The average score.
        /// </value>
        decimal? AvgScore { get; set; }
        /// <summary>
        /// Gets or sets the st dev.
        /// </summary>
        /// <value>
        /// The st dev.
        /// </value>
        decimal? StDev { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }
        /// <summary>
        /// Gets or sets the client assignment type identifier.
        /// </summary>
        /// <value>
        /// The client assignment type identifier.
        /// </value>
        int? ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the assignment label.
        /// </summary>
        /// <value>
        /// The assignment label.
        /// </value>
        string AssignmentLabel { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        int? SortOrder { get; set; }
        /// <summary>
        /// Gets or sets the coi signed date.
        /// </summary>
        /// <value>
        /// The coi signed date.
        /// </value>
        DateTime? CoiSignedDate { get; set; }
        /// <summary>
        /// Gets or sets the resolution date.
        /// </summary>
        /// <value>
        /// The resolution date.
        /// </value>
        DateTime? ResolutionDate { get; set; }
        /// <summary>Gets or sets the date the screening tc occurred.</summary>
        /// <value>The screening teleconference date.</value>
        DateTime? ScreeningTcDate { get; set; }

        /// <summary>Gets or sets the screening tc critique submission date.</summary>
        /// <value>The screening teleconference critique date.</value>
        DateTime? ScreeningTcCritiqueDate { get; set; }

        /// <summary>Fiscal year of application's panel.</summary>
        string Year { get; set; }
        /// <summary>Receipt cycle of application.</summary>
        int ReceiptCycle { get; set; }
    }
    /// <summary>
    /// Peer review result model
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Dal.ResultModels.IPeerReviewResultModel" />
    public class PeerReviewResultModel : IPeerReviewResultModel
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        public string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the meeting type.
        /// </summary>
        /// <value>
        /// The name of the meeting type.
        /// </value>
        public string MeetingTypeName { get; set; }
        /// <summary>
        /// Gets or sets the assignment release date.
        /// </summary>
        /// <value>
        /// The assignment release date.
        /// </value>
        public DateTime? AssignmentReleaseDate { get; set; }
        /// <summary>
        /// Gets or sets the review status identifier.
        /// </summary>
        /// <value>
        /// The review status identifier.
        /// </value>
        public int ReviewStatusId { get; set; }
        /// <summary>
        /// Gets or sets the review status label.
        /// </summary>
        /// <value>
        /// The review status label.
        /// </value>
        public string ReviewStatusLabel { get; set; }
        /// <summary>
        /// Gets or sets the average score.
        /// </summary>
        /// <value>
        /// The average score.
        /// </value>
        public decimal? AvgScore { get; set; }
        /// <summary>
        /// Gets or sets the st dev.
        /// </summary>
        /// <value>
        /// The st dev.
        /// </value>
        public decimal? StDev { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the client assignment type identifier.
        /// </summary>
        /// <value>
        /// The client assignment type identifier.
        /// </value>
        public int? ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the assignment label.
        /// </summary>
        /// <value>
        /// The assignment label.
        /// </value>
        public string AssignmentLabel { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int? SortOrder { get; set; }
        /// <summary>
        /// Gets or sets the coi signed date.
        /// </summary>
        /// <value>
        /// The coi signed date.
        /// </value>
        public DateTime? CoiSignedDate { get; set; }
        /// <summary>
        /// Gets or sets the resolution date.
        /// </summary>
        /// <value>
        /// The resolution date.
        /// </value>
        public DateTime? ResolutionDate { get; set; }

        /// <summary>Gets or sets the date the screening tc occurred.</summary>
        /// <value>The screening teleconference date.</value>
        public DateTime? ScreeningTcDate { get; set; }

        /// <summary>Gets or sets the screening tc critique submission date.</summary>
        /// <value>The screening teleconference critique date.</value>
        public DateTime? ScreeningTcCritiqueDate { get; set; }
        /// <summary>Fiscal year of application's panel.</summary>
        public string Year { get; set; }
        /// <summary>Receipt cycle of application.</summary>
        public int ReceiptCycle { get; set; }
    }
}
