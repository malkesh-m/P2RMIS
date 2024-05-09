using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Review phase view model for Session Wizard
    /// </summary>
    public class ReviewPhaseViewModel
    {
        public ReviewPhaseViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewPhaseViewModel"/> class.
        /// </summary>
        /// <param name="phase">The phase.</param>
        public ReviewPhaseViewModel(IGenericListEntry<Nullable<int>, IPhaseModel> phase)
        {
            PhaseId = phase.Index;
            PhaseName = phase.Value.ReviewPhase;
            StepTypeId = phase.Value.StepTypeId;
            StartDate = phase.Value.StartDate;
            EndDate = phase.Value.EndDate;
            ReopenDate = phase.Value.ReopenDate;
            CloseDate = phase.Value.CloseDate;
        }
        /// <summary>
        /// Gets the phase identifier.
        /// </summary>
        /// <value>
        /// The phase identifier.
        /// </value>
        public int? PhaseId { get; private set; }
        /// <summary>
        /// Step type identifier.
        /// </summary>
        public int StepTypeId { get; private set; }
        /// <summary>
        /// Gets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        public string PhaseName { get; private set; }
        /// <summary>
        /// Gets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; private set; }
        /// <summary>
        /// Gets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; private set; }
        /// <summary>
        /// Re-open date.
        /// </summary>
        public DateTime? ReopenDate { get; private set; }
        /// <summary>
        /// Close date.
        /// </summary>
        public DateTime? CloseDate { get; private set; }
    }
}