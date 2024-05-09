using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserCommunicationLog object. 
    /// </summary>
    public partial class UserCommunicationLog: IStandardDateFields
    {
        /// <summary>
        /// Populates the UserCommunicationLog entity for CRUD operations
        /// </summary>
        /// <param name="userId">User entity identifier of user receiving the communication</param>
        /// <param name="communicationMethodId">CommunicationMethod entity identifier</param>
        /// <param name="comment">Comment</param>
        public void Populate(int userId, int communicationMethodId, string comment)
        {
            this.UserId = userId;
            this.CommunicationMethodId = communicationMethodId;
            this.Comment = comment;
        }
    }
}
