

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User Position Information
    /// </summary>
    public interface IPositionInfoModel : IEditable
    {
        /// <summary>
        /// User position identifier
        /// 
        int UserPositionId { get; set; }
        /// <summary>
        /// User position title
        /// </summary>
        string PositionTitle { get; set; }
        /// <summary>
        /// User department title { get;set; }
        /// </summary>
        string DepartmentTitle { get; set; }
        /// <summary>
        /// Whether the position is primary
        /// </summary>
        bool PrimaryFlag { get; set; }
    }
}
