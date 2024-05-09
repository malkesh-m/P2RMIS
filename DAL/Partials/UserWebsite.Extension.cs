using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class UserWebsite : IStandardDateFields
    {
        #region Other Attributes
        /// <summary>
        /// List of errors after validation is performed.
        /// </summary>
        public IList<SaveProfileStatus> Errors { get; set; }
        #endregion
        /// <summary>
        /// Populates a new UserWebsite in preparation for addition to the repository.
        /// </summary>
        /// <param name="WebsiteAddress">The website address</param>
        /// <param name="WebsiteTypeId">The website type</param>
        public void Populate(string WebsiteAddress, int WebsiteTypeId)
        {
            this.WebsiteAddress = WebsiteAddress;
            this.WebsiteTypeId = WebsiteTypeId;
        }
    }
}
