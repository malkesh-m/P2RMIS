

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User Position Information
    /// </summary>
    public class PositionInfoModel : Editable, IPositionInfoModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 1;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the PositionInfoModel</param>
        public static void InitializeModel(PositionInfoModel model) { }
        /// <summary>
        /// User position identifier
        /// </summary>
        public int UserPositionId { get; set; }
        /// <summary>
        /// User position title
        /// </summary>
        public string PositionTitle { get; set; }
        /// <summary>
        /// User department title { get;set; }
        /// </summary>
        public string DepartmentTitle { get; set; }
        /// <summary>
        /// Whether the position is primary
        /// </summary>
        public bool PrimaryFlag { get; set; }
        #region Helpers
        /// <summary>
        /// Indicates if the position was deleted
        /// </summary>
        /// <returns>True if the position was deleted; false otherwise</returns>
        /// <remarks>needs unit test</remarks>
        public override bool IsDeletable
        {
            get { return ((UserPositionId != 0) & (string.IsNullOrWhiteSpace(DepartmentTitle) & string.IsNullOrWhiteSpace(PositionTitle)));}
        }
        /// <summary>
        /// Indicates if the position is and add (ie has data)
        /// </summary>
        /// <returns>True if the position is to be added; false otherwise</returns>
        /// <remarks>needs unit test</remarks>
        public override bool HasData()
        {
            return (!string.IsNullOrWhiteSpace(DepartmentTitle) || !string.IsNullOrWhiteSpace(PositionTitle));
        }
        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        /// <remarks>needs unit test</remarks>
        private static PositionInfoModel _default;
        public static PositionInfoModel Default
        {
            get
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new PositionInfoModel();
                }
                return _default;
            }
        }
        #endregion
    }
}
