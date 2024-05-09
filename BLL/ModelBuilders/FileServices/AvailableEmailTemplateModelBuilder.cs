using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Files;
using System.Linq;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.ModelBuilders.FileServices
{

    internal class AvailableEmailTemplateModelBuilder : ContainerModelBuilderBase
    {

        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="programYearEntityId">ProgramYear entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public AvailableEmailTemplateModelBuilder(IUnitOfWork unitOfWork, int? programYearEntityId, int sessionPanelId)
            : base(unitOfWork)
        {
            this.ProgramYearEntityId = programYearEntityId;
            this.SessionPanelId = sessionPanelId;
            this.Results = new Container<ITemplateFileInfoModel>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        private int? ProgramYearEntityId { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        private int SessionPanelId { get; set; }
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<ITemplateFileInfoModel> Results { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Build the model
        /// </summary>
        public override void BuildContainer()
        {
            //
            // There may not be a ProgramYear entity identifier.  But we have a SessionPanel entity identifier.
            // So get the ProgramYear entity identifier from the SessionPanel entity identifier if we need to.
            //
            EnsureProgramYearEntityIdExists();

            ProgramYear programYearEntity = this.GetThisProgramYearEntity(ProgramYearEntityId.Value);
            int meetingTypeId = this.UnitOfWork.SessionPanelRepository.GetByID(SessionPanelId).MeetingSession.ClientMeeting.MeetingTypeId;
            List<ITemplateFileInfoModel> list = new List<ITemplateFileInfoModel>();
            //
            // Now we just loop through the list of docs to create model
            //
            this.UnitOfWork.PeerReviewDocumentRepository.Select()
                .Where(x => x.ClientId == programYearEntity.ClientProgram.ClientId 
                    && (x.ClientProgramId == null || x.ClientProgramId == programYearEntity.ClientProgramId)
                    && (x.FiscalYear == null || x.FiscalYear == programYearEntity.Year)
                    && x.PeerReviewDocumentTypeId == PeerReviewDocumentType.Lookups.EmailTemplate
                    && !x.ArchivedFlag)
                .ToList()?.ForEach(x =>
                list.Add(new TemplateFileInfoModel(x.Heading, x.PeerReviewDocumentId, x.PeerReviewContentTypeId == PeerReviewContentType.Link, x.PeerReviewContentTypeId == PeerReviewContentType.Video, x.ContentUrl, x.PeerReviewDocumentAccesses.FirstOrDefault()?.MeetingTypeIds)));
            //
            // Finally set the list of templates to return, filter those that don't apply to the current panel meeting type, and sort
            //
            Results.ModelList = list.Where(x => x.MeetingTypes == null || x.MeetingTypes.Split(',').Select(int.Parse).Contains(meetingTypeId))
                                .OrderBy(x => x.DisplayLabel);
        }
        /// <summary>
        /// Retrieves the ProgramYear entity identifier (from the SessionPanel entity identifier)
        /// if the ProgramYear entity identifier does not exist.
        /// </summary>
        protected void EnsureProgramYearEntityIdExists()
        {
            if (!this.ProgramYearEntityId.HasValue)
            {
                SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(this.SessionPanelId);
                this.ProgramYearEntityId = sessionPanelEntity.GetProgramYearId();
            }
        }
        #endregion
    }
}
