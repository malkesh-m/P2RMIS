using System;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for Scoring Template Wizard
    /// </summary>
    public class ScoringTemplateWizardViewModel
    {
        public ScoringTemplateWizardViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoringTemplateWizardViewModel"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public ScoringTemplateWizardViewModel(IUploadScoringTemplateModalModel template)
        {
            TemplateTitle = template.TemplateTitle;
            PreMeetingPhases = template.TopGrid.ToList().ConvertAll(x => new ScoringTemplatePhaseViewModel(x));
            MeetingPhases = template.BottomGrid.ToList().ConvertAll(x => new ScoringTemplatePhaseViewModel(x));
        }

        /// <summary>
        /// Gets the template title.
        /// </summary>
        /// <value>
        /// The template title.
        /// </value>
        public string TemplateTitle { get; private set; }

        /// <summary>
        /// Gets the review phases.
        /// </summary>
        /// <value>
        /// The review phases.
        /// </value>
        public List<ScoringTemplatePhaseViewModel> PreMeetingPhases { get; private set; }

        /// <summary>
        /// Gets the final phases.
        /// </summary>
        /// <value>
        /// The final phases.
        /// </value>
        public List<ScoringTemplatePhaseViewModel> MeetingPhases { get; private set; }
    }
}