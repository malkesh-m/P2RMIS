using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.Web.ViewModels;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model of the application for the pre-assignment stage
    /// </summary>
    public class PreAssignmentApplicationViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PreAssignmentApplicationViewModel()
        {
        }
        public PreAssignmentApplicationViewModel(IPreAssignmentModel assignmentModel)
        {
            LogNumber = assignmentModel.ApplicationLogNumber;
            PiNameAndOrganization = ViewHelpers.ConstructNameAndOrganization(assignmentModel.PIFirstName, assignmentModel.PILastName, assignmentModel.PIOrganization);
            Title = assignmentModel.Title;
            Mechanism = assignmentModel.Mechanism;
            ExpertiseLevel = assignmentModel.ExpertiseLevel;
            ActionText = string.IsNullOrEmpty(assignmentModel.ExpertiseLevel) ? "Add" : "Edit";
            PanelApplicationId = assignmentModel.PanelApplicationId;
            PanelUserAssignmentId = (int)assignmentModel.PanelUserAssignmentId;
        }

        /// <summary>
        /// Gets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        [JsonProperty("logNumber")]
        public string LogNumber { get; private set; }

        /// <summary>
        /// Gets the pi name and organization.
        /// </summary>
        /// <value>
        /// The pi name and organization.
        /// </value>
        [JsonProperty("piNameAndOrganization")]
        public string PiNameAndOrganization { get; private set; }
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [JsonProperty("title")]
        public string Title { get; private set; }
        /// <summary>
        /// Gets the mechanism.
        /// </summary>
        /// <value>
        /// The mechanism.
        /// </value>
        [JsonProperty("mechanism")]
        public string Mechanism { get; private set; }
        /// <summary>
        /// Gets the expertise level.
        /// </summary>
        /// <value>
        /// The expertise level.
        /// </value>
        [JsonProperty("expertiseLevel")]
        public string ExpertiseLevel { get; private set; }
        /// <summary>
        /// Gets the action text.
        /// </summary>
        /// <value>
        /// The action text.
        /// </value>
        [JsonProperty("actionText")]
        public string ActionText { get; private set; }
        /// <summary>
        /// Gets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        [JsonProperty("panelApplicationId")]
        public int PanelApplicationId { get; private set; }
        /// <summary>
        /// Gets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        [JsonProperty("panelUserAssignmentId")]
        public int PanelUserAssignmentId { get; private set; }
    }
}