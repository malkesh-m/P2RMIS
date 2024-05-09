using System;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public interface IReviewerFacts
    {
        int PrgPartId { get; }
        string ApplicationId { get; }
        string Prefix { get; }
        string FirstName { get; }
        string LastName { get; }
        string COI { get; }
        CritiqueRequiredType CritiqueRequired { get; }
        int AssignmentTypeId { get; }
        string Role { get; }
        bool HasComment { get; }
        int PanelId { get; }
        string PartType { get; }
        DateTime CritiqueDeadline { get; }
        int PanelApplicationId { get; }
        int ReviewerUserId { get; }
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        int ApplicationIdentifier { get; }

        /// <summary>
        /// Gets or sets the admin notes exist.
        /// </summary>
        /// <value>
        /// The admin notes exist.
        /// </value>
        int AdminNotesExist { get; }
    }
}
