using System;
using System.Web;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.ViewModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for the ManageCritique view
    /// </summary>
    public class ManageCritiquesViewModel : PanelManagementViewModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public ManageCritiquesViewModel()
        {
            Critiques = new PanelCritiqueSummariesViewModel();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Description of the session panel's critiques
        /// </summary>
        public PanelCritiqueSummariesViewModel Critiques { get; set; }
        /// <summary>
        /// Does the user have permission to modify the phase dates.
        /// </summary>
        public bool HasModifyDatePermission { get; set; }

        /// <summary>
        /// Whether the user has permission to submit, reset, and edit critique content on behalf of other users
        /// </summary>
        public bool HasManageCritiquePermission { get; set; }
        #endregion
        #region View Helpers
        /// <summary>
        /// Generates an attribute value from a base string and a numeric value
        /// </summary>
        /// <param name="discriminator">Attribute discriminator</param>
        /// <returns>HTML attribute value</returns>
        public static string HtmlAttributeValue(int discriminator)
        {
            return string.Format("{0}{1}", "tcol", discriminator);
        }
        /// <summary>
        /// Generates an column name value from a base string and a numeric value
        /// </summary>
        /// <param name="discriminator">Column name discriminator</param>
        /// <returns>HTML column name value</returns>
        public static string HtmlColumnName(int discriminator)
        {
            return string.Format("{0}{1}", "col", discriminator);
        }
        /// <summary>
        /// Wrapper for ViewHelper method.  Necessary because format includes
        /// html text (hard space) for required formatting.
        /// </summary>
        /// <param name="datetime">Date time value</param>
        /// <returns>Formatted date/time as an HTML string</returns>
        public HtmlString FormatCritiqueDate(Nullable<DateTime> datetime)
        {
            return new HtmlString(ViewHelpers.FormatCritiqueDateTime(datetime));
        }
        /// <summary>
        /// Returns the phase name for the specified sort order.
        /// </summary>
        /// <param name="sortOrder">Sort order</param>
        /// <returns>Phase name for the sort order; empty string if not specified</returns>
        public string PhaseName(int sortOrder)
        {
            return this.Critiques.PhaseHeaders.Where(x => x.Key == sortOrder).FirstOrDefault().Value?.StepName;
        }
        /// <summary>
        /// Indicates if the web model has any critiques.
        /// </summary>
        public bool HasCritiques
        {
            get
            {
                return this.Critiques.PanelCritiques?.Count > 0;
            }
        }
        /// <summary>
        /// Determines if the Submit button should be disabled
        /// </summary>
        /// <param name="submitted">Count of submitted critiques</param>
        /// <param name="assigned">Count of assigned critiques</param>
        /// <returns>Attribute value to disable button if submitted == assigned; empty string otherwise</returns>
        public string DisableButton(int submitted, int assigned)
        {
            return (submitted == assigned) ? "disabled" : string.Empty;
        }
        /// <summary>
        /// Determines if the  button should be disabled.
        /// </summary>
        /// <param name="submitted">Flag indicating if button should be disabled</param>
        /// <returns>Attribute value to disable button if submitted == assigned; empty string otherwise</returns>
        public string HideButton(bool hasPermission)
        {
            return (!hasPermission) ? "display: none;" : string.Empty;
        }
        private static HtmlString CheckboxSeparatorValue = new HtmlString(" &nbsp; | &nbsp;");
        private static HtmlString NoCheckboxSeparatorValue = new HtmlString(string.Empty);
        /// <summary>
        /// Determines the separator used between checkboxes on the ManageCritiques view.
        /// </summary>
        /// <param name="i">Critique phase count</param>
        /// <returns>Separator to use between the toggle checkboxes</returns>
        public HtmlString CheckboxSeparator(int i)
        {
            //
            // If the Critique phase count is the last phase header then we
            // do not want the separator.  Otherwise we do.
            //
            return (this.Critiques.PhaseHeaders.Count != i) ? CheckboxSeparatorValue : NoCheckboxSeparatorValue;
        }
        /// <summary>
        /// Returns the meeting session Id
        /// </summary>
        public int MeetionSessionId
        {
            get
            {
                //
                // It may be a hack but return -1 as the meeting session id.  Which i know will not be excepted on 
                // the server side when the update is serviced.
                //
                return (this.Critiques.MeetingSessionId.HasValue) ? this.Critiques.MeetingSessionId.Value : -1;
            }
        }
        /// <summary>
        /// What to display when things go south.
        /// </summary>
        /// <remarks>Thought needs to be give to where to put this so all view have access to the same message.</remarks>
        public string ErrorMessage
        {
            get
            {
                return "Sorry, there was a problem processing your request.";
            }
        }
        /// <summary>
        /// Gets or sets the active log number.
        /// </summary>
        /// <value>
        /// The active log number.
        /// </value>
        public string ActiveLogNumber { get; set; }
        #endregion

    }
}