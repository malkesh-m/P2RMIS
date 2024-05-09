using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.EntityChanges;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// interface encapsulating the rules and methods for user W9 address rules to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public interface IUserW9ChangeLogRules
    {
    }
    /// <summary>
    /// class encapsulating the rules and methods for user W9 address rules to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public class UserW9ChangeLogRules : IUserW9ChangeLogRules
    {
        /// <summary>
        /// Table names of interest for creating change record
        /// </summary>
        public static string User = "User";
        public static string W9Verified = "W9Verified";
    /// <summary>
    /// Removes the case where W9 is set to verified
    /// </summary>
    /// <param name="changes">The list of the current changes</param>
    /// <returns>The list of the current changes minus the 'True' case for verified</returns>
        public static List<EntityPropertyChange> Filter(List<EntityPropertyChange> changes)
        {
            List<EntityPropertyChange> list = new List<EntityPropertyChange>();

            if (changes.Count > 0)
            {
                list = changes.Where(x => !(x.EntityTableName == User && x.EntityFieldName == W9Verified && x.NewValue == "True")).ToList();
            }

            return list;
        }
    }
}
