using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Library;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Library
{
    /// <summary>
    /// Build a TrainingDocumentModelBuilder which lists the training documents a
    /// user can see based on their permissions; meeting type & assignment type.
    /// </summary>
    internal class TrainingDocumentModelBuilder: ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public TrainingDocumentModelBuilder(IUnitOfWork unitOfWork, int programYearId, int userId, bool hasElevatedPermission)
            : base(unitOfWork, userId)
        {
            this.ProgramYearId = programYearId;
            this.HasElevatedPermission = hasElevatedPermission;
            this.Results = new Container<ITrainingDocumentModel>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        protected int ProgramYearId { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has elevated permission.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has elevated permission; otherwise, <c>false</c>.
        /// </value>
        protected bool HasElevatedPermission { get; private set; }

        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<ITrainingDocumentModel> Results { get; private set; }


        #endregion
        #region UserRegistrtionStatusModelBuilder services
        /// <summary>
        /// Build a container of TrainingDocuments.
        /// </summary>
        /// <returns>Model container of TrainingDocumentModels.</returns>
        public override void BuildContainer()
        {
            //
            //  First we retrieve the Program Year that was selected.
            //
            ProgramYear year = UnitOfWork.ProgramYearRepository.GetByID(ProgramYearId);
            // Pull in all training documents for the year (notice null handling as wildcard)
            IQueryable<PeerReviewDocument> trainingDocs = UnitOfWork.PeerReviewDocumentRepository.Select().Where(x => x.ArchivedFlag == false && x.PeerReviewDocumentTypeId == PeerReviewDocumentType.Lookups.Training
                && (x.ClientId == year.ClientProgram.ClientId)
                && (x.ClientProgramId == null || x.ClientProgramId == year.ClientProgramId) 
                && (x.FiscalYear == null || x.FiscalYear == year.Year));
            //
            // Now we determine the user's Participation types
            //
            IQueryable<PanelUserAssignment> userParticipations = UnitOfWork.PanelUserAssignmentRepository.Select().Where(x => x.UserId == UserId && x.SessionPanel.ProgramPanels.Any(y => y.ProgramYearId == ProgramYearId));
            //
            // Finally after all that we build an enumeration of TrainingDocuments that they can see
            //
            IQueryable<PeerReviewDocument> allowedTrainingDocuments = HasElevatedPermission ? trainingDocs : DetermineAllowedTrainingDocuments(trainingDocs, userParticipations);
            //
            // An construct a Container to return & set it up for return
            //
            Results.ModelList = CoustructModel(allowedTrainingDocuments);          
        }
        /// <summary>
        /// Filters the training documents by ProgramYear & UserParticipationType
        /// </summary>
        /// <param name="programYearEntity">ProgramYear entity</param>
        /// <param name="userParticipationTypes">List of userParticipationTypes</param>
        /// <param name="meetingTypes">List of the user's MeetingType entity identifiers</param>
        /// <returns>Enumeration of TraingingDocuments</returns>
        private IQueryable<PeerReviewDocument> DetermineAllowedTrainingDocuments(IQueryable<PeerReviewDocument> possibleDocs, IQueryable<PanelUserAssignment> userParticipations)
        {
            ///
            // Get the documents the user does not have access to
            // NULL serve as wildcard and indicate there are no restrictions
            // 
            var participationList = userParticipations.Select(x => new
            {
                ClientParticipantTypeId = x.ClientParticipantTypeId,
                MeetingTypeId = x.SessionPanel.MeetingSession.ClientMeeting.MeetingTypeId,
                ParticipationMethodId = x.ParticipationMethodId,
                RestrictedAssignedFlag = x.RestrictedAssignedFlag

            }).ToList();

            var docList = possibleDocs.ToList().SelectMany(x => x.PeerReviewDocumentAccesses).Select(x => new
            {
                AllowedParticipantTypes = x.ClientParticipantTypeIds != null ? x.ClientParticipantTypeIds.Split(',').Select(int.Parse).ToList() : null,
                AllowedMeetings = x.MeetingTypeIds != null ? x.MeetingTypeIds.Split(',').Select(int.Parse).ToList() : null,
                AllowedParticipationMethods = x.ParticipationMethodIds != null ? x.ParticipationMethodIds.Split(',').Select(int.Parse).ToList() : null,
                RestrictedAssignedFlag = x.RestrictedAssignedFlag,
                PeerReviewDocumentId = x.PeerReviewDocumentId
            }).ToList();
            
            List<int> deniedDocuments = docList.Where(x => (x.AllowedParticipantTypes != null && !participationList.Any(y => x.AllowedParticipantTypes.Contains(y.ClientParticipantTypeId)))
            || (x.AllowedMeetings != null && !participationList.Any(y => x.AllowedMeetings.Contains(y.MeetingTypeId)))
            || (x.AllowedParticipationMethods != null && !participationList.Any(y => x.AllowedParticipationMethods.Contains(y.ParticipationMethodId)))
            || (x.RestrictedAssignedFlag != null && !participationList.Any(y => y.RestrictedAssignedFlag == x.RestrictedAssignedFlag))).Select(x => x.PeerReviewDocumentId).ToList();

            var allowedDocs = deniedDocuments != null ? possibleDocs.Where(x => !deniedDocuments.Contains(x.PeerReviewDocumentId)) : possibleDocs;
            return allowedDocs;
        }
        /// <summary>
        /// Construct a List of TrainingDocumentModels from an IEnumerable collection of TrainingDocuments.
        /// </summary>
        /// <param name="trainingDocuments">List of TrainingDocumentModels</param>
        private List<TrainingDocumentModel> CoustructModel(IQueryable<PeerReviewDocument> trainingDocuments)
        {
            //
            // Create a list & populate it with models constructed from the TrainingDocuments
            //
            List<TrainingDocumentModel> list = new List<TrainingDocumentModel>();

            list = trainingDocuments.Select(t => new TrainingDocumentModel
            {
                TrainingCategoryId = t.TrainingCategoryId ?? 0,
                TrainingDocumentId = t.PeerReviewDocumentId,
                TrainingCategoryName = t.TrainingCategory.CategoryName,
                Name = t.Heading,
                ContentType = t.PeerReviewContentType.ContentType,
                Description = t.Description,
                FileType = t.FileType,
                ReviewedDate = t.UserPeerReviewDocuments.FirstOrDefault(y => y.UserId == UserId).ReviewDate,
                ContentUrl = t.ContentUrl,
                IsVideo = t.PeerReviewContentTypeId == PeerReviewContentType.Video,
                IsLink = t.PeerReviewContentTypeId == PeerReviewContentType.Link
            }
            ).OrderBy(x => x.TrainingCategoryName).ThenBy(x => x.Name).ToList();
            return list;
        }
        #endregion
    }
}
