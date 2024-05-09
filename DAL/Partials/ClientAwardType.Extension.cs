using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ClientAwardType object.
    /// </summary>
    public partial class ClientAwardType: IStandardDateFields
    {
        /// <summary>
        /// Determines if the ClientAwardType is a PreAppAward
        /// </summary>
        /// <returns>True if entity is a PreApp type; false otherwise</returns>
        public bool IsPreAppType()
        {
            return MechanismRelationshipType.IsPreApp(this.MechanismRelationshipTypeId);
        }
    }
}
