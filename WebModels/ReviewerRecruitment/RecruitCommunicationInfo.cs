using System;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web model containing the communications log for this recruit
    /// </summary>
    public interface IRecruitCommunicationInfo
    {
        /// <summary>
        /// The user communication log identifier
        /// </summary>
        int UserCommunicationLogId { get; set; }
        /// <summary>
        /// The communication method id
        /// </summary>
        int CommunicationMethodId { get; set; }
        /// <summary>
        /// The communication method
        /// </summary>
        string CommunicationMethod { get; set; }
        /// <summary>
        /// The communication's comment
        /// </summary>
        string Comment { get; set; }
        /// <summary>
        /// The communication initiator last name
        /// </summary>
        string InitiatorLastName { get; set; }
        /// <summary>
        /// The communication initiator first name
        /// </summary>
        string InitiatorFirstName { get; set; }
        /// <summary>
        /// The date of the communication
        /// </summary>
        DateTime? Date { get; set; }
        /// <summary>
        /// Indicates if the comment is writable (updatable) by the reader.
        /// </summary>
        bool IsWritable { get;}
    }
    /// <summary>
    /// Web model containing the communications log for this recruit
    /// </summary>
    public class RecruitCommunicationInfo : IRecruitCommunicationInfo
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userCommunicationLogId">UserCommunicationLog entity identifier</param>
        /// <param name="communicationMethodId">CommumnicationMethod entity identifier</param>
        /// <param name="communicationMethod">CommunicationMethod name</param>
        /// <param name="comment">Communication comment</param>
        /// <param name="initiatorLastName">Commenter first name</param>
        /// <param name="initiatorFirstName">Commenter last name</param>
        /// <param name="date">Date comment was created or updated</param>
        /// <param name="isWritable">Indicates if the comment is writable</param>
        public RecruitCommunicationInfo(int userCommunicationLogId, int communicationMethodId, string communicationMethod,
                                        string comment, string initiatorLastName, string initiatorFirstName, DateTime? date,
                                        bool isWritable)
        {
            this.UserCommunicationLogId = userCommunicationLogId;
            this.CommunicationMethodId = communicationMethodId;
            this.CommunicationMethod = communicationMethod;
            this.Comment = comment;
            this.InitiatorLastName = initiatorLastName;
            this.InitiatorFirstName = initiatorFirstName;
            this.Date = date;
            this.IsWritable = isWritable;
        }
        public RecruitCommunicationInfo(int communicationMethodId, string comment)
        {
            this.CommunicationMethodId = communicationMethodId;
            this.Comment = comment;
        }
        #endregion
        /// <summary>
        /// The user communication log identifier
        /// </summary>
        public int UserCommunicationLogId { get; set; }
        /// <summary>
        /// The communication method id
        /// </summary>
        public int CommunicationMethodId { get; set; }
        /// <summary>
        /// The communication method
        /// </summary>
        public string CommunicationMethod { get; set; }
        /// <summary>
        /// The communication's comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// The communication initiator last name
        /// </summary>
        public string InitiatorLastName { get; set; }
        /// <summary>
        /// The communication initiator first name
        /// </summary>
        public string InitiatorFirstName { get; set; }
        /// <summary>
        /// The date of the communication
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Indicates if the comment is writable (updatable) by the reader.
        /// </summary>
        public bool IsWritable { get; private set; }
    }
}
