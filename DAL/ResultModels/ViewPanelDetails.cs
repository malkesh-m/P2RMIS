using System.Collections.Generic;
namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Model representing the results of a TODO:: document me
    /// </summary>
    public class ViewPanelDetails
    {
        
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int? PanelId { get; set; }
        public string ActiveApplication { get; set; }
        public string ApplicationId { get; set; }
        public int? Order { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PIInstitution { get; set; }
        public string ApplicationTitle { get; set; }
        public bool? Disapproved { get; set; }
        public bool? Triaged { get; set; }
        public string AwardShortDescription { get; set; }
        public int? ReviewTypeId { get; set; }
        public int? PossibleScores { get; set; }
        public int? ActualScores { get; set; }
        public decimal? AverageOE { get; set; }
        public int? CommentsCount { get; set; }
        public string COIs { get; set; }
        public string AssignmentSlots { get; set; }
        public string AssignmentTypes { get; set; }
        public string AssignmentNames { get; set; }
        public string AssignmentPartIds { get; set; }
        #endregion
    }
}
