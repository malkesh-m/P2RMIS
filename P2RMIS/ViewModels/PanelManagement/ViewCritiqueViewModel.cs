using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ViewCritiqueViewModel : PanelManagementViewModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public ViewCritiqueViewModel()
        {
            
        }
        #endregion

        #region Properties
        /// <summary>
        /// Application critique details
        /// </summary>
        public IApplicationCritiqueDetailsModel CritiqueDetails { get; set; }
        #endregion

        #region Helpers
        /// <summary>
        /// Standard panel management critique comments
        /// </summary>
        /// <param name="critiqueDetails">Critique details object</param>
        /// <param name="thisCritiquesSection">Critique section object</param>
        /// <returns>Critique comments in string format</returns>
        public string FormatCritiqueComments(IApplicationCritiqueDetailsModel critiqueDetails, ICritiqueSection thisCritiquesSection)
        {
            return (thisCritiquesSection.TextFlag ? 
                thisCritiquesSection.Text : string.Empty);
        }
        /// <summary>
        /// Criteria Score formatter for critique view
        /// </summary>
        /// <param name="critiqueDetails">Critique details object</param>
        /// <param name="thisCritiquesSection">Critique section object</param>
        /// <returns>Score in string format</returns>
        public string FormatCriteriaScore(IApplicationCritiqueDetailsModel critiqueDetails, ICritiqueSection thisCritiquesSection)
        {
            return (thisCritiquesSection.Score.GetValueOrDefault() != 0 ?
                thisCritiquesSection.Score.ToString() : Invariables.Labels.PanelManagement.Critiques.NotSubmitted);
        }
        #endregion
    }
}