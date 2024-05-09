using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's CommunicationLogAttachment
    /// </summary>		
    public partial class CommunicationLogAttachment: IStandardDateFields
    {
        /// <summary>
        /// Populates a new CommunicationLogAttachment entity object
        /// </summary>
        /// <param name="fileName">Attachment file name</param>
        /// <param name="userId">User identifier of the user sending the attachment</param>
        /// <returns><Entity object/returns>
        public CommunicationLogAttachment Populate(string fileName, int userId)
        {
            this.AttachmentFileName = fileName;
            //
            // Location is always set to the empty string because
            // the attachment is not written to disk but created from the 
            // HttpPostedFileBase object describing the attachment.
            //
            this.AttachmentLocation = string.Empty;
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
    }
}
