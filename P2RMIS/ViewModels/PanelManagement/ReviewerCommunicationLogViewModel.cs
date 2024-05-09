using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;

    namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for reviewer communication log
    /// </summary>
    public class ReviewerCommunicationLogViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerCommunicationLogViewModel"/> class.
        /// </summary>
        public ReviewerCommunicationLogViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerCommunicationLogViewModel"/> class.
        /// </summary>
        /// <param name="logModel">The log model.</param>
        /// <param name="methods">The methods.</param>
        public ReviewerCommunicationLogViewModel(IUserCommunicationLogModel logModel, List<IListEntry> methods)
        {
            UserId = logModel.RecruitContactInformation.UserId;
            PhoneNumber = string.IsNullOrEmpty(logModel.RecruitContactInformation.PhoneExtension) ? logModel.RecruitContactInformation.Phone :
                string.Format("{0}x{1}", logModel.RecruitContactInformation.Phone, logModel.RecruitContactInformation.PhoneExtension);
            FaxNumber = logModel.RecruitContactInformation.Fax;
            EmailAddress = logModel.RecruitContactInformation.Email;
            Logs = logModel.RecruitCommunicationLog.Cast<IRecruitCommunicationInfo>().ToList().ConvertAll(x => new Log(x));
            Methods = methods;
        }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; private set; }
        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; private set; }
        /// <summary>
        /// Gets the fax number.
        /// </summary>
        /// <value>
        /// The fax number.
        /// </value>
        public string FaxNumber { get; private set; }
        /// <summary>
        /// Gets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; private set; }
        /// <summary>
        /// Gets the method identifier.
        /// </summary>
        /// <value>
        /// The method identifier.
        /// </value>
        public int? MethodId { get; private set; }
        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <value>
        /// The methods.
        /// </value>
        public List<IListEntry> Methods { get; private set; }
        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public List<Log> Logs { get; private set; }
        /// <summary>
        /// Communication log
        /// </summary>
        public class Log
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Log"/> class.
            /// </summary>
            public Log() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Log"/> class.
            /// </summary>
            /// <param name="info">The information.</param>
            public Log(IRecruitCommunicationInfo info)
            {
                LogId = info.UserCommunicationLogId;
                Initiator = string.IsNullOrEmpty(info.InitiatorLastName) ? Invariables.Labels.PanelManagement.Communication.System : 
                        ViewHelpers.ConstructName(info.InitiatorLastName, info.InitiatorFirstName);
                Method = info.CommunicationMethod;
                MethodId = info.CommunicationMethodId;
                LogDate = ViewHelpers.FormatDate(info.Date);
                Comment = info.Comment;
                IsWritable = info.IsWritable;
            }
            /// <summary>
            /// Gets the log identifier.
            /// </summary>
            /// <value>
            /// The log identifier.
            /// </value>
            public int LogId { get; set; }
            /// <summary>
            /// Gets the initiator.
            /// </summary>
            /// <value>
            /// The initiator.
            /// </value>
            public string Initiator { get; set; }
            /// <summary>
            /// Gets the method.
            /// </summary>
            /// <value>
            /// The method.
            /// </value>
            public string Method { get; set; }
            /// <summary>
            /// Gets the method identifier.
            /// </summary>
            /// <value>
            /// The method identifier.
            /// </value>
            public int MethodId { get; set; }
            /// <summary>
            /// Gets the log date.
            /// </summary>
            /// <value>
            /// The log date.
            /// </value>
            public string LogDate { get; set; }
            /// <summary>
            /// Gets the comment.
            /// </summary>
            /// <value>
            /// The comment.
            /// </value>
            public string Comment { get; set; }
            /// <summary>
            /// Gets the recruit communication information.
            /// </summary>
            /// <returns></returns>
            public IRecruitCommunicationInfo GetRecruitCommuncationInfo()
            {
                var info = new RecruitCommunicationInfo(LogId, MethodId, null, Comment, null, null, null, true);
                return info;
            }
            /// <summary>
            ///  Indicates if the entry is writable
            /// </summary>
            public bool IsWritable { get; private set; }
        }
        /// <summary>
        /// Gets the user communication log model.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="logs">The logs.</param>
        /// <returns></returns>
        public IUserCommunicationLogModel GetUserCommunicationLogModel(int userId, List<Log> logs)
        {
            var model = new UserCommunicationLogModel();
            model.RecruitContactInformation = new RecruitPreferredContactInfo(userId, null, null, null, null, null, null);
            model.RecruitCommunicationLog = logs.ConvertAll(x => x.GetRecruitCommuncationInfo());
            return model;
        }
    }
}