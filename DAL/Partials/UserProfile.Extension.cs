using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class UserProfile: IStandardDateFields
    {
        /// <summary>
        /// Populate UserProfile entity object for Add or Modify operations.
        /// </summary>
        /// <param name="profileTypeId">ProfileType entity identifier</param>
        public void Populate(int profileTypeId)
        {
            this.ProfileTypeId = profileTypeId;
        }
    }
}
