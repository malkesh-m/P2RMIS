using System;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for Scoring Template Wizard
    /// </summary>
    public class ScoringTemplatePhaseViewModel
    {
        public const string Adjectival = "Adjectival";

        public ScoringTemplatePhaseViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoringTemplatePhaseViewModel"/> class.
        /// </summary>
        /// <param name="phase">The phase.</param>
        public ScoringTemplatePhaseViewModel(IUploadScoringTemplateGridModalModel phase)
        {
            PhaseName = phase.StepTypeName;
            OverallType = phase.OverallType;
            if (phase.OverallType == Adjectival)
            {
                OverallRange = String.Join(" ", phase.OverallAdjectivalScale.ToList());
            }
            else
            {
                OverallRange = String.Format("{0}-Highest {1}-Lowest", phase.OverallHighValue, phase.OverallLowValue);
            }
            EvaluationType = phase.CriteriaType;
            if (phase.CriteriaType == Adjectival)
            {
                EvaluationRange = String.Join(" ", phase.CriteriaAdjectivalScale.ToList());
            }
            else
            {
                EvaluationRange = String.Format("{0}-Highest {1}-Lowest", phase.CriteriaHighValue, phase.CriteriaLowValue);
            }
        }

        /// <summary>
        /// Gets the name of the phase.
        /// </summary>
        /// <value>
        /// The name of the phase.
        /// </value>
        public string PhaseName { get; private set; }

        /// <summary>
        /// Gets the type of the overall.
        /// </summary>
        /// <value>
        /// The type of the overall.
        /// </value>
        public string OverallType { get; private set; }

        /// <summary>
        /// Gets the overall range.
        /// </summary>
        /// <value>
        /// The overall range.
        /// </value>
        public string OverallRange { get; private set; }

        /// <summary>
        /// Gets the type of the evaluation.
        /// </summary>
        /// <value>
        /// The type of the evaluation.
        /// </value>
        public string EvaluationType { get; private set; }

        /// <summary>
        /// Gets the evaluation range.
        /// </summary>
        /// <value>
        /// The evaluation range.
        /// </value>
        public string EvaluationRange { get; private set; }
    }
}