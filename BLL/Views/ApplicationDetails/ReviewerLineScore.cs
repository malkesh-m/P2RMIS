using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ReviewerLineScore : IReviewerLineScore
    {
       #region Constructor
        /// <summary>
        /// Constructor populates the ReviewerLineScore from a business layer Reviewer object.
        /// </summary>
        /// <param name="value"></param>
        public ReviewerLineScore(IReviewerFacts value)
            : this(value.Role)
        {
            this.Name = ViewHelpers.ConstructName(value.LastName, value.FirstName);
            this.ProgramPartId = value.PrgPartId;
            this.ApplicationId = value.ApplicationId;
            //
            // A note on this computation.  Because the participantTypeId is specif to each
            // client the value "COI" is returned out of the stored procedure.  The stored procedure
            // does however do the retrieval based on ProgramParticipantId
            //
            this.IsCoi = !string.IsNullOrWhiteSpace(value.COI);
            this.PartType = value.PartType;
            this.HasComment = value.HasComment;
            this.CritiqueType = value.CritiqueRequired;
            this.ReviewerId = value.PrgPartId;
            this.PanelId = value.PanelId;
            this.PanelApplicationId = value.PanelApplicationId;
            this.ReviewerUserId = value.ReviewerUserId;
            this.ApplicationIdentifier = value.ApplicationIdentifier;
            this.AdminNotesExist = value.AdminNotesExist;
        }

        

        /// <summary>
        /// Constructor for adding a calculated row
        /// </summary>
        /// <param name="role">Role identifier</param>
        public ReviewerLineScore(string role)
        {
            this.Name = string.Empty;
            this.Role = role;
            this.Scores = new Dictionary<int, decimal>();
            this.CritiqueType = CritiqueRequiredType.None;
            this.CriteriaScores = new Dictionary<int, string>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Reviewer name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Reviewer role
        /// </summary>
        public string Role { get; private set; }
        /// <summary>
        /// Reviewer role
        /// </summary>
        public string PartType { get; private set; }
        /// <summary>
        /// Reviewer name
        /// </summary>
        public int ProgramPartId { get; private set; }
        /// <summary>
        /// Evaluation scores.  Individual scores are index by evaluation 
        /// order.
        /// </summary>
        public Dictionary<int, decimal> Scores { get; private set; }
        /// <summary>
        /// Evaluation scores as a string.
        /// </summary>
        public Dictionary<int, string> CriteriaScores { get; private set; }
        /// 
        /// Indicates if the entry is  ---
        /// 
        public bool IsCoi { get; private set; }
        /// <summary>
        /// Indicates if this reviewer has a comment.
        /// </summary>
        public bool HasComment { get; set; }
        /// <summary>
        /// Indicates the critique type
        /// </summary>
        public CritiqueRequiredType CritiqueType { get; private set; }
        /// <summary>
        /// Reviewer identifier
        /// </summary>
        public int ReviewerId { get; set; }
        /// <summary>
        /// Unique Identifier for Application
        /// </summary>
        public string ApplicationId { get; private set; }
        /// <summary>
        /// Unique Identifier for Panel
        /// </summary>
        public int PanelId { get; private set; }
        /// <summary>
        /// Unique identifier for a panel application
        /// </summary>
        public int PanelApplicationId { get; private set; }
        /// <summary>
        /// The UserId of the reviewer (not PanelUserAssignmentId)
        /// </summary>
        public int ReviewerUserId { get; set; }
        /// <summary>
        /// Gets or sets the admin notes exist.
        /// </summary>
        /// <value>
        /// Whether the admin notes exist.
        /// </value>
        public int AdminNotesExist { get; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationIdentifier { get; }
        #endregion
    }
}
