

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// interface encapsulating the rules and methods for user address changes to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public interface IUSerAddressChangeLogRules
    {
    }
    /// <summary>
    /// Class encapsulating the rules and methods for user address changes to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public class UserAddressChangeLogRules : IUSerAddressChangeLogRules
    {
        /// <summary>
        /// Table names of interest for creating change record
        /// </summary>
        public static string UserAddress = "UserAddress";
    }
}
