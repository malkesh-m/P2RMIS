using System;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's User object.
    /// </summary>
    public partial class TrainingDocument
    {
        /// <summary>
        /// Returns a Training document's review time for a specific user
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>DateTime the user reviewed the document or null if not reviewed.</returns>
        public DateTime? UserTrainingDocumentReviewDateTime(int userId)
        {
            //
            // There can be multiple UserTrainingDocuments associated with this TrainingDocument.  But there can only be a 
            // single UserTrainingDocument entity for this TrainingDocument for a specific user.
            //
            var userTrainingDocumentEntity = this.UserTrainingDocuments.FirstOrDefault(x => x.UserId == userId);
            return (userTrainingDocumentEntity == null) ? (DateTime?)null : userTrainingDocumentEntity.ReviewDate;
        }
        public int? UserTrainingDocumentId(int userId)
        {
            //
            // There can be multiple UserTrainingDocuments associated with this TrainingDocument.  But there can only be a 
            // single UserTrainingDocument entity for this TrainingDocument for a specific user.
            //
            var userTrainingDocumentEntity = this.UserTrainingDocuments.FirstOrDefault(x => x.UserId == userId);
            return (userTrainingDocumentEntity == null) ? (int?)null :  (int?)userTrainingDocumentEntity.UserTrainingDocumentId;
        }
    }
}
