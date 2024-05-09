using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.EntityChanges;


namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// interface encapsulating the rules and methods for user degree changes to create a single EntityPropertyChange record
    /// from a collection of EntityPropertyChange records
    /// </summary>
    public interface IUserDegreeMajorChangeLogRules
    {
    }    /// <summary>
         /// class encapsulating the rules and methods for user degree changes to create a single EntityPropertyChange record
         /// from a collection of EntityPropertyChange records
         /// </summary>
    public class UserDegreeMajorChangeLogRules : IUserDegreeMajorChangeLogRules
    {
        #region static attibutes
        /// <summary>
        /// Field names of interest for creating change record
        /// </summary>
        public static string DegreeId = "DegreeId";
        public static string Major = "Major";
        public static string UserDegree = "UserDegree";
        #endregion
        public static List<EntityPropertyChange> ComputeMultipleUserInfoChangeRecord(IEnumerable<EntityPropertyChange> changes)
        {
            // create final list without the degree changes and working list with user changes
            List<EntityPropertyChange> finalList = new List<EntityPropertyChange>();
            List<EntityPropertyChange> workingList = changes.Where(x => x.EntityTableName == UserDegree).ToList();

            // create composite degree objects and add them into the final list
            while (workingList.Count > 0)
            {
                int id = workingList[0].EntityId;
                EntityPropertyChange degree = workingList.Where(x => x.EntityId == id && x.EntityFieldName == DegreeId).FirstOrDefault();
                EntityPropertyChange major = workingList.Where(x => x.EntityId == id && x.EntityFieldName == Major).FirstOrDefault();

                EntityPropertyChange final = BuildFinalChangeObject(degree, major);

                if (final != null)
                {
                    finalList.Add(final);
                }

                workingList = workingList.Where(x => x.EntityId != id).ToList();
            }

            return finalList;
        }
        /// <summary>
        /// Builds EntityPropertyChange record for degrees/major
        /// </summary>
        /// <param name="degree">The degree record object</param>
        /// <param name="major">The major record object</param>
        /// <returns>Combined EntityPropertyChange object containing degree and major</returns>
        public static EntityPropertyChange BuildFinalChangeObject(EntityPropertyChange degree, EntityPropertyChange major)
        {
            string oldValue = (degree.OldValue != null ? degree.OldValue + ", " : string.Empty) + (major.OldValue != null ? major.OldValue : string.Empty);
            string newValue = (degree.NewValue != null ? degree.NewValue + ", " : string.Empty) + (major.NewValue != null ? major.NewValue : string.Empty);

            return EntityPropertyChange.Create(degree.EntityFieldName, oldValue, newValue, degree.ChangeType, degree.EntityTableName, degree.EntityId, false);
        }
        /// <summary>
        /// Parses EntityPropertyChange object containing degreeId and major
        /// </summary>
        /// <param name="degree">The EntityProoertyChange object containing degreeId and major as delinerated string</param>
        /// <returns>KeyValuePair containing DegreeId as int and Major as string</returns>
        public static KeyValuePair<int, string> GetMajorAndDegress(EntityPropertyChange degree)
        {

            int idEnd = degree.NewValue.IndexOf(",");

            string id = degree.NewValue.Substring(0, idEnd).Trim();
            string major = degree.NewValue.Substring(idEnd + 1).Trim();

            int degreeId;
            if (!Int32.TryParse(id, out degreeId))
            {
                degreeId = 0;
            }

            return new KeyValuePair<int, string>(degreeId, major);
        }
    }
}
