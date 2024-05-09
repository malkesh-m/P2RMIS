using Sra.P2rmis.Dal.Repository;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for OnLine Scoring objects.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the PanelManagement repository functions.
        /// </summary>
        IApplicationScoringRepository ApplicationScoringRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationReviewStatu repository functions.
        /// </summary>
        IGenericRepository<ApplicationReviewStatu> ApplicationReviewStatuGenericRepository { get; }
        /// <summary>
        /// Provides database access for the ClientScoringScaleLegendItem repository functions.
        /// </summary>
        IClientScoringScaleLegendItemRepository ClientScoringScaleLegendItemRepository { get; }
        /// <summary>
        /// Provides database access for the ClientScoringScaleLegend repository functions.
        /// </summary>
        IClientScoringScaleLegendRepository ClientScoringScaleLegendRepository { get; }
        /// <summary>
        /// Provides database access for the ClientScoringScaleAdjectival repository functions.
        /// </summary>
        IGenericRepository<ClientScoringScaleAdjectival> ClientScoringScaleAdjectivalRepository { get; }
        /// <summary>
        /// Provides database access for the ClientScoringScale repository functions.
        /// </summary>
        IGenericRepository<ClientScoringScale> ClientScoringScaleRepository { get; }
        /// <summary>
        /// Gets the application stage step repository.
        /// </summary>
        /// <value>
        /// The application stage step repository.
        /// </value>
        IGenericRepository<ApplicationStageStep> ApplicationStageStepRepository { get; }
        /// <summary>
        /// Gets the application stage repository.
        /// </summary>
        /// <value>
        /// The application stage repository.
        /// </value>
        IGenericRepository<ApplicationStage> ApplicationStageRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationStageStepDiscussion generic repository functions.
        /// </summary>
        IGenericRepository<ApplicationStageStepDiscussion> ApplicationStageStepDiscussionRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationStageStepDiscussionComment repository functions.
        /// </summary>
        IGenericRepository<ApplicationStageStepDiscussionComment> ApplicationStageStepDiscussionCommentRepository { get; }

    }
}
