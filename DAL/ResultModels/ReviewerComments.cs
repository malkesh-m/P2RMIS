
namespace Sra.P2rmis.Dal.ResultModels
{
    public class ReviewerComments : IReviewerComments
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        internal ReviewerComments() { }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationIDd { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int? PrgPartId { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Reviewers last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Reviewers first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Reviewers name prefix 
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// Panel identifier
        /// </summary>
        public int? PanelId { get; set; }
        /// <summary>
        /// Reviewer identifier
        /// </summary>
        public int ReviewerId { get; set; }
	    #endregion
	}
}
