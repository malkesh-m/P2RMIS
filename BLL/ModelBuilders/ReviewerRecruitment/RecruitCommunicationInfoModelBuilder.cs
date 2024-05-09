using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.Bll.ReviewerRecruitment;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    internal class RecruitCommunicationInfoModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="userId">The recruit's user identifier</param>
        public RecruitCommunicationInfoModelBuilder(IUnitOfWork unitOfWork, int requestorUserId, int userId)
            : base(unitOfWork)
        {
            this.RequestorUserId = requestorUserId;
            this.RecruitId = userId;
            this.Results = new Container<IRecruitCommunicationInfo>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// User entity identifier of person requesting the log
        /// </summary>
        protected int RequestorUserId { get; private set; }
        /// <summary>
        /// Recruit's User entity identifier
        /// </summary>
        protected int RecruitId { get; private set; }
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IRecruitCommunicationInfo> Results { get; private set; }
        #endregion
        #region Builder
        public override void BuildContainer()
        {
            //
            // Retrieve any UserProfile changes for this user
            //
            IEnumerable<UserCommunicationLog> log = RetrieveCommunicationLog();
            //
            // Now build the results & put them in the container
            //
            BuildResults(log);
        }
        /// <summary>
        /// Retrieve all the UserProfile changes for any users assigned to the client's
        /// panels.
        /// </summary>
        /// <returns>Enumeration of UserInfoChangeLog entities</returns>
        private IEnumerable<UserCommunicationLog> RetrieveCommunicationLog()
        {
            User user = this.GetThisUser(RecruitId);
            //
            // Get all the use communication logs for this user.
            //
            IEnumerable<UserCommunicationLog> results = user.UserCommunicationLogs.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UserCommunicationLogId);

            return results;
        }
        /// <summary>
        /// Construct a list of UserProfie changes.  Entries have been "de-duped" meaning
        /// changes to individual fields that were made by separate users are marked for display
        /// on once.
        /// Duplicated changes are included but are not marked for display.  The duplicate records
        /// indicate which UserInfoChangeLog entities should be marked as "reviewed".  This prevents
        /// the marking as "reviewed" changes that were not represented in the original display.
        /// </summary>
        /// <param name="changes">Enumeration of UserInfoChangeLog entities for the specified Client</param>
        private void BuildResults(IEnumerable<UserCommunicationLog> logs)
        {
            //
            // For each of the UserInfoChangeLog identified create a WorkList item and add it to a list
            //
            List<IRecruitCommunicationInfo> list = new List<IRecruitCommunicationInfo>();
            foreach (var l in logs)
            {
                IRecruitCommunicationInfo logEntry = new RecruitCommunicationInfo(
                    l.UserCommunicationLogId, 
                    l.CommunicationMethodId, 
                    l.CommunicationMethod.MethodName,
                    l.Comment, 
                    InitiatedByLastName(l.CreatedBy), 
                    InitiatedByFirstName(l.CreatedBy), 
                    l.CreatedDate,
                    l.CreatedBy.HasValue ? IsWritable(l.CreatedBy.Value, l.Comment) : false);
                list.Add(logEntry);
            }
            ReplaceInvitationSentByName(list);
            Results.ModelList = list;
        }
        /// <summary>
        /// Replace the CreatedBy name for the initial communication to "System".
        /// </summary>
        /// <remarks>Really Really do not like doing this.  Really what should happen is that
        /// there should be a flag on the UserCommunicationLog entity that indicates if it should have
        /// the name replaced.
        /// </remarks>
        /// <param name="list">List ofIRecruitCommunicationInfo models </param>
        protected void ReplaceInvitationSentByName(List<IRecruitCommunicationInfo> list)
        {
            foreach(IRecruitCommunicationInfo item in list)
            {
                if (item.Comment == ReviewerRecruitmentService.Constants.Invitation)
                {
                    item.InitiatorLastName = "System";
                    item.InitiatorLastName = string.Empty;
                }
            }
        }
        /// <summary>
        /// Determines if the requestor can update the comment.
        /// </summary>
        /// <param name="userEntityId">Comment creator User entity identifier</param>
        /// <returns>True if the requestor can edit the comment; false otherwise</returns>
        protected bool IsWritable(int userEntityId, string comment)
        {
            return (comment != ReviewerRecruitmentService.Constants.Invitation) && (userEntityId == this.RequestorUserId);
        }
        /// <summary>
        /// Retrieve the last name of the indicated user
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Last name of the person who initiated the communication</returns>
        private string InitiatedByLastName(int? userId)
        {
            //
            // Check to see if the User entity id exists.  (It should).  If it 
            // does not then just return the empty string
            //
            return (userId.HasValue) ? this.GetThisUser(userId.Value).LastName() : string.Empty;
        }
        /// <summary>
        /// Retrieve the first name of the indicated
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>First name of the person who initiated the communication</returns>
        private string InitiatedByFirstName(int? userId)
        {
            //
            // Check to see if the User entity id exists.  (It should).  If it 
            // does not then just return the empty string
            //
            return (userId.HasValue) ? this.GetThisUser(userId.Value).FirstName() : string.Empty;
        }
        #endregion
    }
}
