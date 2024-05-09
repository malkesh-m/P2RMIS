using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.EntityChanges;


namespace Sra.P2rmis.Dal
{
    public partial class UserDegree : IStandardDateFields, ILogEntityChanges
    {
        #region Static Attributes
        public static readonly Dictionary<string, PropertyChange> ChangeLogRequired = new Dictionary<string, PropertyChange>
        {
            // need both fields values to compute composite change for either degree or major changing
            { "DegreeId", new PropertyChange(typeof(int), UserInfoChangeType.Indexes.Degrees, true) },                            // User's Degree
            { "Major", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.Major, true) }                               // User's Major
        };
        #endregion
        #region Other Attributes
        /// <summary>
        /// List of errors after validation is performed.
        /// </summary>
        public IList<SaveProfileStatus> Errors { get; set; }
        #endregion
        #region Classes
        /// <summary>
        /// Field Sizes
        /// </summary>
        public class FieldLength
        {
            public static int Major = 40;
        }
        #endregion
        /// <summary>
        /// Populates a new UserDegree in preparation for addition to the repository.
        /// </summary>
        /// <param name="degreeId">The degree identifier</param>
        /// <param name="major">The major</param>
        /// <param name="userInfoId">The user's info identifier</param>
        public void Populate(int degreeId, string major, int userInfoId, int userId)
        {
            this.DegreeId = degreeId;
            this.Major = major;
            this.UserInfoId = userInfoId;

            Helper.UpdateModifiedFields(this, userId);
            Helper.UpdateCreatedFields(this, userId);
        }
        /// <summary>
        /// Determines if the User has valid data
        /// </summary>
        /// <returns><True if all properties are correct length & required are defined; false otherwise/returns>
        public bool IsValid()
        {
            Errors = new List<SaveProfileStatus>();
            return AreSizesCorrect() & IsRequiredSupplied() & IsWellFormed();
        }
        /// <summary>
        /// Verifies that all field sizes are the correct length or less.
        /// </summary>
        /// <returns><True if all properties are correct length; false otherwise/returns>
        public bool AreSizesCorrect()
        {
            bool result = (
                Helper.CheckLength(FieldLength.Major, Major, SaveProfileStatus.MajorTooLong, Errors)
               );

            return result;
        }
        /// <summary>
        /// Verifies that all required information is supplied.
        /// </summary>
        /// <returns><True if all Required properties have data; false otherwise/returns>
        public bool IsRequiredSupplied()
        {
            return ((Helper.CheckPositive(DegreeId, SaveProfileStatus.DegreeIdNotSupplied, Errors))
                    );
        }
        /// <summary>
        /// Verifies any specific formatting specifications
        /// </summary>
        /// <returns></returns>
        public bool IsWellFormed()
        {
            return true;
        }
        /// <summary>
        /// Returns the properties to log.
        /// </summary>
        /// <returns>Dictionary of properties to log for changes</returns>
        public Dictionary<string, PropertyChange> PropertiesToLog()
        {
            return ChangeLogRequired;
        }
        /// <summary>
        /// Returns the name of the entity's key property.
        /// </summary>
        public string KeyPropertyName
        {
            get { return nameof(UserDegreeId); }
        }
    }
}
