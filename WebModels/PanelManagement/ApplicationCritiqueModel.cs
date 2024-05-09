
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Contains information related to an application and associated critique data by step
    /// </summary>
    public class ApplicationCritiqueModel : IApplicationCritiqueModel
    {
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Last name of the principal investigator for the application
        /// </summary>
        public string PiLastName { get; set; }
        /// <summary>
        /// Gets or sets the first name of the pi.
        /// </summary>
        /// <value>
        /// The first name of the pi.
        /// </value>
        public string PiFirstName { get; set; }
        /// <summary>
        /// Last name of the principal investigator for the application
        /// </summary>
        public string PiInstitution { get; set; }
        /// <summary>
        /// Award abbreviated name
        /// </summary>
        public string AwardAbbreviation { get; set; }
        /// <summary>
        /// Award abbreviated name
        /// </summary>
        public IEnumerable<IReviewerCritiquePhase> ReviewerCritiques { get; set; }
    }
}
