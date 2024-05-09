
using System.ComponentModel.DataAnnotations;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public class UserDegreeModel : Editable, IUserDegreeModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 1;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the UserDegreeModel</param>
        public static void InitializeModel(UserDegreeModel model) { }
        /// <summary>
        /// The user degree id
        /// </summary>
        public int UserDegreeId { get; set; }
        /// <summary>
        /// The degree identifier
        /// </summary>
        public int? DegreeId { get; set; }
        /// <summary>
        /// The degree name
        /// </summary>
        public string DegreeName { get; set; }
        /// <summary>
        /// The major the degree was obtained in
        /// </summary>
        [StringLength(40)]
        public string Major { get; set; }
        /// <summary>
        /// Indicates if the position is deleted
        /// </summary>
        /// <returns>Returns true if the item is to be deleted, false otherwise</returns>
        public override bool IsDeleted()
        {
            return (UserDegreeId > 0 && (IsDeletable || !HasData()));
        }
        /// <summary>
        /// Indicates if the model has data.  The model has data if:
        /// - the major field is not null or whitespace
        /// - the degree id is non zero
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public override bool HasData()
        {
            return ((!string.IsNullOrWhiteSpace(Major)) || (DegreeId.GetValueOrDefault() > 0));
        }
    }
}
