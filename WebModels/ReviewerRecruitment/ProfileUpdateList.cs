
namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web model containing data for population of Profile Update modal list.
    /// </summary>
    public interface IProfileUpdateList
    {
        /// <summary>
        /// Initializes the profile update information
        /// </summary>
        /// <param name="label">The label for the user profile item</param>
        /// <param name="oldValue">The previous value of the user profile item</param>
        /// <param name="newValue">The current value of the user profile item</param>
        /// <param name="sortOrder">The sort order of the user profile item</param>
        /// <param name="userInfoChangeLogId">The user info change log identifier</param>
        /// <param name="userInfoId">The user info indentifier</param>
        void SetUserInformation(string label, string oldValue, string newValue, int sortOrder, int userInfoChangeLogId, int userInfoId);
        /// <summary>
        /// The display label of the profile field being updated
        /// </summary>
        string Label { get; }
        /// <summary>
        /// The previous value of the profile field
        /// </summary>
        string OldValue { get; }
        /// <summary>
        /// The current value of the profile field
        /// </summary>
        string NewValue { get; }
        /// <summary>
        /// The order the profile field changes are to be sorted by for display
        /// </summary>
        int SortOrder { get; }
        /// <summary>
        /// The user info change log identifier
        /// </summary>
        int UserInfoChangeLogId { get; }
        /// <summary>
        /// The user info indentifier
        /// </summary>
        int UserInfoId { get; }
    }
    /// <summary>
    /// Web model containing data for population of Profile Update modal list.
    /// </summary>
    public class ProfileUpdateList : IProfileUpdateList
    {

        #region Construction and set up
        /// <summary>
        /// Initializes the profile update information
        /// </summary>
        /// <param name="label">The label for the user profile item</param>
        /// <param name="oldValue">The previous value of the user profile item</param>
        /// <param name="newValue">The current value of the user profile item</param>
        /// <param name="sortOrder">The sort order of the user profile item</param>
        /// <param name="userInfoChangeLogId">The user info change log identifier</param>
        /// <param name="userInfoId">The user info indentifier</param>
        public void SetUserInformation(string label, string oldValue, string newValue, int sortOrder, int userInfoChangeLogId, int userInfoId)
        {
            this.Label = label;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.SortOrder = sortOrder;
            this.UserInfoChangeLogId = userInfoChangeLogId;
            this.UserInfoId = userInfoId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The display label of the profile field being updated
        /// </summary>
        public string Label { get; private set; }
        /// <summary>
        /// The previous value of the profile field
        /// </summary>
        public string OldValue { get; private set; }
        /// <summary>
        /// The current value of the profile field
        /// </summary>
        public string NewValue { get; private set; }
        /// <summary>
        /// The order the profile field changes are to be sorted by for display
        /// </summary>
        public int SortOrder { get; private set; }
        /// <summary>
        /// The user info change log identifier
        /// </summary>
        public int UserInfoChangeLogId { get; private set; }
        /// <summary>
        /// The user info indentifier
        /// </summary>
        public int UserInfoId { get; private set; }
        #endregion
    }
}
