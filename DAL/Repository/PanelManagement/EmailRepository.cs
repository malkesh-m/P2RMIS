using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for Email objects.  This repository provides only
    /// retrieval services.  No CRUD methods are supported
    public class EmailRepository : IEmailRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// The database context.
        /// </summary>
        internal P2RMISNETEntities context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public EmailRepository(P2RMISNETEntities context)
        {
            this.context = context;
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Provides a list of the email recipients in the collection that matches the supplied communication type
        /// </summary>
        /// <param name="recipients">Communication log recipients</param>
        /// <param name="commType">The communications type</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <returns>ResultModel<IEmailAddress></returns>
        public ResultModel<IEmailAddress> FillEmailAddressRecipientList(ICollection<CommunicationLogRecipient> recipients, int commType, int sessionPanelId)
        {
            ResultModel<IEmailAddress> result = new ResultModel<IEmailAddress>();

            var address = recipients.Where(x => x.CommunicationLogRecipientTypeId == commType).Select(
                z => new EmailAddress
                {
                    UserEmailAddress = z.PanelUserAssignment?.User.UserInfoes.FirstOrDefault().UserEmails.FirstOrDefault().Email,
                    FirstName = z.PanelUserAssignment?.User.UserInfoes.FirstOrDefault().FirstName,
                    LastName = z.PanelUserAssignment?.User.UserInfoes.FirstOrDefault().LastName,
                    PanelUserAssignmentId = z.PanelUserAssignmentId,
                    SessionPanelId = sessionPanelId,
                    ParticipantTypeAbbreviation = z.PanelUserAssignment?.ClientParticipantType.ParticipantTypeAbbreviation
                }
                );

            result.ModelList = address;
            return result;
        }
        #endregion
    }
}
