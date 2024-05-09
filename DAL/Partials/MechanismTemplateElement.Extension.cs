using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Describes properties necessary to match a MechanismTemplateElement in one phase
    /// to the MechanismTemplateElement in another phase.
    /// </summary>
    public struct MatchSet
    {
        /// <summary>
        /// ClientElement entity identifier
        /// </summary>
        public int ClientElementId { get; set; }
    }
    public partial class MechanismTemplateElement: IStandardDateFields
    {
        /// <summary>
        /// Returns the criteria description
        /// </summary>
        /// <returns>Element description</returns>
        public string GetCriteriaElementDescription()
        {
            return this.ClientElement.ElementDescription;
        }
        /// <summary>
        /// Return the properties needed to match this criteria in another phase.
        /// </summary>
        /// <returns>MatchSet containing property values needed to match a criteria in a phase to a another</returns>
        public MatchSet MatchProperty()
        {
            return new MatchSet { ClientElementId = this.ClientElementId};
        }
        /// <summary>
        /// Determines if this MechanismTemplateElement is a match to the supplied MatchSet
        /// </summary>
        /// <param name="matchSet">MechanismTemplateElement to match</param>
        /// <returns>this if matched; null otherwise</returns>
        public MechanismTemplateElement Match(MatchSet matchSet)
        {
            return (this.ClientElementId == matchSet.ClientElementId) ? this : null;
        }
    }
}
