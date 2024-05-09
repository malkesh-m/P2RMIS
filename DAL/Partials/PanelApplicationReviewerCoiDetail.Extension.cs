using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelApplicationReviewerCoiDetail object. 
    /// </summary>	
    public partial class PanelApplicationReviewerCoiDetail : IStandardDateFields
    {
        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static PanelApplicationReviewerCoiDetail _default;
        public static PanelApplicationReviewerCoiDetail Default
        {
            get
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new PanelApplicationReviewerCoiDetail();
                }
                return _default;
            }
        }
        /// <summary>
        /// Returns null if no ClientCoiType assigned.  Primarily used for Linq retrieval
        /// to indicate there was no selection.
        /// </summary>
        public int? NullableClientCoiTypeId
        {
            get 
            {
                int? result = null;

                if (this.ClientCoiTypeId > 0)
                {
                    result = this.ClientCoiTypeId;
                }
                return result;
            }
        }

        /// <summary>
        /// Modifies the current record with the supplied information.
        /// </summary>
        /// <param name="clientCoiTypeId">The client coi type identifier</param>
        /// <param name="userId">User identifier of the user entering the coi details</param>
        /// <returns>PanelApplicationReviewerCoiDetail models</returns>
        public PanelApplicationReviewerCoiDetail Modify(int clientCoiTypeId, int userId)
        {
            this.ClientCoiTypeId = clientCoiTypeId;

            Helper.UpdateModifiedFields(this, userId);
            return this;
        }
        /// <summary>
        /// Fills in the empty current record with the specified record with the supplied information.
        /// </summary>
        /// <param name="clientCoiTypeId">The client coi type identifier</param>
        /// <param name="panelApplicationReviewerExpertiseId">The panel appliction reviewer expertise identifier</param>
        /// <param name="userId">TUser identifier of the user entering the coi details</param>
        /// <returns>PanelApplicationReviewerCoiDetail models</returns>
        public PanelApplicationReviewerCoiDetail Populate(int clientCoiTypeId, int panelApplicationReviewerExpertiseId, int userId)
        {
            PanelApplicationReviewerCoiDetail details = new PanelApplicationReviewerCoiDetail();

            details.Modify(clientCoiTypeId, userId);
            details.PanelApplicationReviewerExpertiseId = panelApplicationReviewerExpertiseId;
            Helper.UpdateCreatedFields(details, userId);

            return details;
        }
    }
}
