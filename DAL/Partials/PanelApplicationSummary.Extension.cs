using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelApplicationSummary object. 
    /// </summary>	
    public partial class PanelApplicationSummary: IStandardDateFields
    {
        /// <summary>
        /// Populates the PanelApplicationSummary entity
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="summaryText">Summary text to add</param>
        public void Populate(int panelApplicationId, string summaryText)
        {
            this.PanelApplicationId = panelApplicationId;
            this.SummaryText = summaryText;
        }
    }
}
