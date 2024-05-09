using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// interface for the edit citique phase model
    /// </summary>
    public interface IEditCritiquePhaseModel
    {
        /// <summary>
        /// Panel stage step identifer
        /// </summary>
        int PanelStageStepId { get; set; }
        /// <summary>
        /// The critique phase
        /// </summary>
        string CritiquePhase { get; set; }
        /// <summary>
        /// The critique due date
        /// </summary>
        DateTime? CritiqueDueDate { get; set; }
    }
    /// <summary>
    /// Model object containing the edit citique phase model
    /// </summary>
    public class EditCritiquePhaseModel : IEditCritiquePhaseModel
    {
        public EditCritiquePhaseModel(int id, string phase, DateTime? dueDate)
        {
            this.PanelStageStepId = id;
            this.CritiquePhase = phase;
            this.CritiqueDueDate = dueDate;
        }
        /// <summary>
        /// Panel stage step identifer
        /// </summary>
        public int PanelStageStepId { get; set; }
        /// <summary>
        /// The critique phase
        /// </summary>
        public string CritiquePhase { get; set; }
        /// <summary>
        /// The critique due date
        /// </summary>
        public DateTime? CritiqueDueDate { get; set; }
    }
}
