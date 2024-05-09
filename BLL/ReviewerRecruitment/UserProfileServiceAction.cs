using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    internal class UserProfileServiceAction : ServiceAction<UserProfile>
    {
        /// <summary>
        /// Populates the specified profile type identifier.
        /// </summary>
        /// <param name="profileTypeId">The profile type identifier.</param>
        public void Populate(int profileTypeId)
        {
            this.ProfileTypeId = profileTypeId;
        }
        #region Properties
        /// <summary>
        /// Gets or sets the profile type identifier.
        /// </summary>
        /// <value>
        /// The profile type identifier.
        /// </value>
        internal int ProfileTypeId { get; set; }
        #endregion
        protected override void Populate(UserProfile entity)
        {
            entity.Populate(this.ProfileTypeId);
        }
        /// <summary>
        /// Indicates if the PanelUserAssignment has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string is considered as data.
                //
                return true;
            }
        }
    }
}
