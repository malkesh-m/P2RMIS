using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IReviewerLineScore
    {
        CritiqueRequiredType CritiqueType { get; }
        bool HasComment { get; }
        bool IsCoi { get; }
        string Name { get; }
        int ProgramPartId { get; }
        int ReviewerId { get; }
        string Role { get; }
        Dictionary<int, decimal> Scores { get; }
        /// <summary>
        /// Evaluation scores as a string.
        /// </summary>
        Dictionary<int, string> CriteriaScores { get; }
        string ApplicationId { get; }
        int PanelId { get; }
        int PanelApplicationId { get; }
        int ReviewerUserId { get; set; }
        /// <summary>
        /// Gets or sets the admin notes exist.
        /// </summary>
        /// <value>
        /// Whether the admin notes exist.
        /// </value>
        int AdminNotesExist { get; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        int ApplicationIdentifier { get; }
    }
}
