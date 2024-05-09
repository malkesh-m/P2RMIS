
namespace Sra.P2rmis.Dal
{
    public partial class ApplicationTemplateElement
    {
        /// <summary>
        /// Returns the criteria description
        /// </summary>
        /// <returns>Element description</returns>
        public string GetCriteriaElementDescription()
        {
            return this.MechanismTemplateElement.GetCriteriaElementDescription();
        }

        /// <summary>
        /// Gets the criteria sort order.
        /// </summary>
        /// <returns></returns>
        public int GetCriteriaSortOrder()
        {
            return this.MechanismTemplateElement.SortOrder;
        }
    }
}
