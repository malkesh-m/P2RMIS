using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Contains information related to an application and associated critique data by step
    /// </summary>
    public interface IApplicationCritiqueModel
    {
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Last name of the principal investigator for the application
        /// </summary>
        string PiLastName { get; set; }
        /// <summary>
        /// Gets or sets the first name of the pi.
        /// </summary>
        /// <value>
        /// The first name of the pi.
        /// </value>
        string PiFirstName { get; set; }
        /// <summary>
        /// Last name of the principal investigator for the application
        /// </summary>
        string PiInstitution { get; set; }

        /// <summary>
        /// Award abbreviated name
        /// </summary>
        string AwardAbbreviation { get; set; }

        /// <summary>
        /// Collection of Reviewer Critique information
        /// </summary>
        IEnumerable<IReviewerCritiquePhase> ReviewerCritiques { get; set; }
    }
}