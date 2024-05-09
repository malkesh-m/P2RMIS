namespace Sra.P2rmis.Dal
{
    public partial class MechanismRelationshipType
    {
        /// <summary>
        /// Specific index values.
        /// </summary> 
        public class Indexes
        {
            /// <summary>
            /// Pre-Application mechanism
            /// </summary>
            public static int PreApplication = 1;
        }
        /// <summary>
        /// Indicates if the index identifies as a Pre-App award.
        /// </summary>
        /// <param name="index">Value to test</param>
        /// <returns>True if the index identifies as a Pre-App award; false otherwise</returns>
        public static bool IsPreApp(int? index)
        {
            return (index.HasValue)? index == Indexes.PreApplication: false;
        }
    }
}
