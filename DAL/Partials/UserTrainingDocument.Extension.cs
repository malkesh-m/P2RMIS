using System;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's User object.
    /// </summary>
    public partial class UserTrainingDocument : IStandardDateFields
    {
        /// <summary>
        /// Extension to populate a UserTrainingDocument
        /// </summary>
        /// <param name="trainingDocumentId">TrainingDocument entity identifier</param>
        /// <param name="reviewDate">Review date</param>
        /// <param name="userId">Identifier for a user</param>
        public UserTrainingDocument Populate(int trainingDocumentId, DateTime reviewDate, int userId)
        {
            this.UserId = userId;
            this.TrainingDocumentId = trainingDocumentId;
            this.ReviewDate = reviewDate;
            return this;
        } 
    }
}
