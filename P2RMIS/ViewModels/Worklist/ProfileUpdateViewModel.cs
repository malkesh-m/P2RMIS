using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for profile update modal
    /// </summary>
    public class ProfileUpdateViewModel
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the ProfileUpdateViewModel class.
        /// </summary>
        public ProfileUpdateViewModel()
        {
            ProfileUpdateItems = new List<ProfileUpdateItemViewModel>();
        }
        /// <summary>
        /// Initializes a new instance of the ProfileUpdateViewModel class.
        /// </summary>
        /// <param name="profileUpdateList">The profile update list.</param>
        public ProfileUpdateViewModel(List<IProfileUpdateList> profileUpdateList) : this()
        {
            foreach (IProfileUpdateList profileUpdate in profileUpdateList)
            {
                var item = new ProfileUpdateItemViewModel(profileUpdate.Label, profileUpdate.OldValue, profileUpdate.NewValue,
                        profileUpdate.UserInfoChangeLogId);
                if (profileUpdateList.Count > 0)
                    UserInfoId = profileUpdateList[0].UserInfoId;
                ProfileUpdateItems.Add(item);
            }
        }
        #endregion        

        public int UserInfoId { get; private set; }
        /// <summary>
        /// Gets the profile update items.
        /// </summary>
        /// <value>
        /// The profile update items.
        /// </value>
        public List<ProfileUpdateItemViewModel> ProfileUpdateItems { get; private set; }
        /// <summary>
        /// View model for profile update item
        /// </summary>
        public class ProfileUpdateItemViewModel
        {
            /// <summary>
            /// Initializes a new instance of the ProfileUpdateItemViewModel class.
            /// </summary>
            /// <param name="fieldName">Name of the field.</param>
            /// <param name="from">From.</param>
            /// <param name="to">To.</param>
            /// <param name="changeLogId">Change log id.</param>
            public ProfileUpdateItemViewModel(string fieldName, string from, string to, int changeLogId)
            {
                FieldName = fieldName;
                From = from;
                To = to;
                ChangeLogId = changeLogId;
            }
            /// <summary>
            /// Gets the name of the field.
            /// </summary>
            /// <value>
            /// The name of the field.
            /// </value>
            public string FieldName { get; private set; }
            /// <summary>
            /// Gets from.
            /// </summary>
            /// <value>
            /// From.
            /// </value>
            public string From { get; private set; }
            /// <summary>
            /// Gets to.
            /// </summary>
            /// <value>
            /// To.
            /// </value>
            public string To { get; private set; }
            /// <summary>
            /// Gets the change log identifier.
            /// </summary>
            /// <value>
            /// The change log identifier.
            /// </value>
            public int ChangeLogId { get; private set; }
        }
    }
}