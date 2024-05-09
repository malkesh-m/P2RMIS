

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public interface IUserDegreeModel : IEditable
    {
        /// <summary>
        /// The user degree id
        /// </summary>
        int UserDegreeId { get; set; }
        /// <summary>
        /// The degree identifier
        /// </summary>
        int? DegreeId { get; set; }
        /// <summary>
        /// The degree name
        /// </summary>
        string DegreeName { get; set; }
        /// <summary>
        /// The major the degree was obtained in
        /// </summary>
        string Major { get; set; }
    }
}
