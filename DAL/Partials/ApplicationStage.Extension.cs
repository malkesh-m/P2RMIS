using System;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationStage. 
    /// </summary>
    public partial class ApplicationStage: IStandardDateFields
    {
        /// <summary>
        /// Checks if the application stage can be made visible.
        /// </summary>
        /// <param name="releaseDateTime">DateTime to use for the Release Date/Time</param>
        /// <param name="userId">User entity identifier</param>
        public void SetActive(DateTime releaseDateTime, int userId)
        {
            //
            // Check the review stage and if it is Asynchronous then
            // we can release the application.
            //
            if (this.ReviewStageId == ReviewStage.Asynchronous)
            {
                this.AssignmentVisibilityFlag = true;
                this.AssignmentReleaseDate = releaseDateTime;
                Helper.UpdateModifiedFields(this, userId);
            }
        }
        /// <summary>
        /// Checks if the application is released.
        /// </summary>
        public bool IsReleased()
        {
            return (this.AssignmentVisibilityFlag == true);
        }

        /// <summary>
        /// Populates the specified review status identifier.
        /// </summary>
        /// <param name="reviewStatusId">The review status identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <remarks>This will be removed once applicationId is refactored out of the table</remarks>
        public void Populate(int reviewStageId, int stageOrder, bool activeFlag, bool assignmentVisibilityFlag, DateTime? assignmentReleaseDate)
        {
            this.ReviewStageId = reviewStageId;
            this.StageOrder = stageOrder;
            this.ActiveFlag = activeFlag;
            this.AssignmentVisibilityFlag = assignmentVisibilityFlag;
            this.AssignmentReleaseDate = assignmentReleaseDate;
        }
    }
}
