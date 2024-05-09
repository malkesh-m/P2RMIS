using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{

	/// <summary>
    /// Custom methods for Entity Framework's CommunicationLogRecipient object. 
    /// </summary>
    public partial class CommunicationLogRecipient: IStandardDateFields
    {
        /// <summary>
        /// Populates a new CommunicationLogRecipient entity object
        /// </summary>
        /// <param name="communicationLogRecipientTypeId">Communication recipient identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="userId">User identifier of the user sending the attachment</param>
        /// <returns></returns>
        public CommunicationLogRecipient Populate(int communicationLogRecipientTypeId, int panelUserAssignmentId, int userId)
        {
            this.CommunicationLogRecipientTypeId = communicationLogRecipientTypeId;
            this.PanelUserAssignmentId = panelUserAssignmentId;
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
    }
}
