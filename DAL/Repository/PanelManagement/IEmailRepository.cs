using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for Email objects.  This repository provides only
    /// retrieval services.  No CRUD methods are supported
    /// </summary> 
    public interface IEmailRepository
    {

        #region Services Provided
        /// <summary>
        /// Provides a list of the email recipients in the collection that matches the supplied communication type
        /// </summary>
        /// <param name="recipients">Communication log recipients</param>
        /// <param name="commType">The communications type</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <returns></returns>
        ResultModel<IEmailAddress> FillEmailAddressRecipientList(ICollection<CommunicationLogRecipient> recipients, int commType, int sessionPanelId);
        #endregion
    }
}
