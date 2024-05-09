using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Crud operations for ApplicationBudget entities.
    /// </summary>
    public class ApplicationBudgetServiceAction : ServiceAction<ApplicationBudget>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationBudgetServiceAction() { }
        /// <summary>
        /// Populates the ServiceAction's local variables
        /// </summary>
        /// <param name="applicationId">Containing Application entity identifier</param>
        /// <param name="comment">The comment</param>
        internal void Populate(int applicationId, string comment, int? userId)
        {
            this.ApplicationId = applicationId;
            this.Comment = comment;
            if (!string.IsNullOrEmpty(comment))
            {
                this.CommentModifiedBy = userId;
                this.CommentModifiedDate = CrossCuttingServices.GlobalProperties.P2rmisDateTimeNow;
            }

        }
        #endregion
        #region Properties
        /// <summary>
        /// Application (to link the ApplicationBudget entity to) entity identifier
        /// </summary>
        protected int ApplicationId { get; set; }
        /// <summary>
        /// The comment
        /// </summary>
        protected string Comment { get; set; }


        /// <summary>
        /// The comment modified by
        /// </summary>
        public Nullable<int> CommentModifiedBy { get; set; }
        public Nullable<System.DateTime> CommentModifiedDate { get; set; }

        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the ApplicationBudget entity.
        /// </summary>
        /// <param name="entity">ApplicationBudget entity</param>
        protected override void Populate(ApplicationBudget entity)
        {
            entity.ApplicationId = this.ApplicationId;
            entity.Comments = this.Comment;
            entity.CommentModifiedBy = this.CommentModifiedBy;
            entity.CommentModifiedDate = this.CommentModifiedDate;
        }
        /// <summary>
        /// Indicates if the ApplicationBudget has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        #endregion
    }
}
