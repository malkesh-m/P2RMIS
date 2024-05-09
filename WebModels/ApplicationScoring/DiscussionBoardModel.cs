using System.Collections.Generic;


namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    #region Interface
    /// <summary>
    /// Model representing data related to a discussion board
    /// </summary>
    public interface IDiscussionBoardModel
    {
        /// <summary>
        /// Gets or sets the application stage step identifier.
        /// </summary>
        /// <value>
        /// The application stage step identifier.
        /// </value>
        int ApplicationStageStepId { get; set; }

        /// <summary>
        /// Gets or sets the application stage step discussion identifier.
        /// </summary>
        /// <value>
        /// The application stage step discussion identifier.
        /// </value>
        int? ApplicationStageStepDiscussionId { get; set; }

        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number that identifies the application.
        /// </value>
        string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [discussion exists].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [discussion exists]; otherwise, <c>false</c>.
        /// </value>
        bool DiscussionExists { get; set; }

        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        int PanelApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        int SessionPanelId { get; set; }

        /// <summary>
        /// Gets or sets the discussion comments.
        /// </summary>
        /// <value>
        /// The discussion comments.
        /// </value>
        IEnumerable<IDiscussionCommentModel> DiscussionComments { get; set; }

        /// <summary>
        /// Gets or sets the participants of the discussion.
        /// </summary>
        /// <value>
        /// The participants available to take part in the discussion.
        /// </value>
        IEnumerable<IDiscussionParticipantModel> Participants { get; set; }
        /// <summary>
        /// Indicates if the MOD has completed
        /// </summary>
        bool IsModDone { get; set; }
    }
    #endregion
    /// <summary>
    /// Model representing data related to a discussion board
    /// </summary>
    public class DiscussionBoardModel : IDiscussionBoardModel
    {
        #region Constructors
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the application stage step identifier.
        /// </summary>
        /// <value>
        /// The application stage step identifier.
        /// </value>
        public int ApplicationStageStepId { get; set; }

        /// <summary>
        /// Gets or sets the application stage step discussion identifier.
        /// </summary>
        /// <value>
        /// The application stage step discussion identifier.
        /// </value>
        public int? ApplicationStageStepDiscussionId { get; set; }

        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number that identifies the application.
        /// </value>
        public string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [discussion exists].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [discussion exists]; otherwise, <c>false</c>.
        /// </value>
        public bool DiscussionExists { get; set; }

        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; set; }

        /// <summary>
        /// Gets or sets the discussion comments.
        /// </summary>
        /// <value>
        /// The discussion comments.
        /// </value>
        public IEnumerable<IDiscussionCommentModel> DiscussionComments { get; set; }

        /// <summary>
        /// Gets or sets the participants of the discussion.
        /// </summary>
        /// <value>
        /// The participants available to take part in the discussion.
        /// </value>
        public IEnumerable<IDiscussionParticipantModel> Participants { get; set; }
        /// <summary>
        /// Indicates if the MOD has completed
        /// </summary>
        public bool IsModDone { get; set; }  
        #endregion

        

    }
}
