using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class MechanismTemplate: IStandardDateFields
    {
        /// <summary>
        /// Locate the matching MechanismTemplateElement to the MatchSet in this phase
        /// </summary>
        /// <param name="matchSet"><MatchSet describing the MechanismTemplateElement</param>
        /// <returns>MechanismTemplateElement that matches the specified MatchSet; null otherwise (which should not happen)</returns>
        public MechanismTemplateElement Match(MatchSet matchSet)
        {
            MechanismTemplateElement result = null;
            foreach(MechanismTemplateElement entity in this.MechanismTemplateElements)
            {
                if (entity.Match(matchSet) != null)
                {
                    result = entity;
                    break;
                }
            }
            return result;
        }
    }
}
