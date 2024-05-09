using System;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.LibraryService
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete UserTrainingDocument entities.
    /// </summary>
    class UserTrainingDocumentServiceAction : ServiceAction<UserTrainingDocument>
    {
        public UserTrainingDocumentServiceAction()
        {

        }

        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        public void Populate(int trainingDocumentId, DateTime? reviewedDate, bool? reviewed)
        {
            this.TrainingDocumentId = trainingDocumentId;
            this.ReviewedDate = reviewedDate;
            this.Reviewed = reviewed;
        }

//        public int? UserTrainingDocumentId { get; private set; }
        public int TrainingDocumentId { get; private set; }
        public DateTime? ReviewedDate { get; private set; }
        public bool? Reviewed { get; private set; }

        #region Required Overrides
        /// <summary>
        /// Populate the UserTrainingDocument entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">UserTrainingDocument entity</param>
        protected override void Populate(UserTrainingDocument entity)
        {
            entity.Populate(this.TrainingDocumentId, this.ReviewedDate.Value, this.UserId);
        }
        /// <summary>
        /// Indicates if the doument is to be marked as reviewed.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                return this.Reviewed ?? false;
            }
        }
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete
        {
            get { return (!this.Reviewed.HasValue || !this.Reviewed.Value) && EntityIdentifier != 0; }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Optional pre-add precessing logic.  Set the reviewed time to current time.
        protected override void PreAdd()
        {
            if (!this.ReviewedDate.HasValue)
            {
                this.ReviewedDate = DateTime.Now;
            }
        }
        /// <summary>
        /// Optional post-delete precessing logic.  Set the review time returned to null
        protected override void PostDelete(UserTrainingDocument entity)
        {
            base.PostDelete(entity);

            this.ReviewedDate = null;
        }

        #endregion
        /// <summary>
        /// Optional pre-modify precessing logic.  Set the reviewed time to current time.
        protected override void PreModify(UserTrainingDocument entity)
        {
            if (!this.ReviewedDate.HasValue)
            {
                this.ReviewedDate = DateTime.Now;
            }
        }

    }
}
