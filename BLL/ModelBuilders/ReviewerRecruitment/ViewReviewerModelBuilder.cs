using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Bll.ModelBuilders.ReviewerRecruitment
{
    /// <summary>
    /// Builds zero or more IPanelReviewerModel web models for the specified SessionPanel.
    /// </summary>
    internal class ViewReviewerModelBuilder: ContainerModelBuilderBase
    {
        #region Constructin & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public ViewReviewerModelBuilder(IUnitOfWork unitOfWork, int sessionPanelId)
            : base(unitOfWork)
        {
            this.SessionPanelId = sessionPanelId;
            this.Results = new Container<IPanelReviewerModel>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        protected int SessionPanelId { get; set; }
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IPanelReviewerModel> Results { get; private set; }
        #endregion
        #region Builder
        /// <summary>
        /// Build a container of WorkList models.
        /// </summary>
        /// <returns>Model container of WorkList models.</returns>
        public override void BuildContainer()
        {
            //
            // retrieve the SessionPanel
            //
            SessionPanel sessionPanelEntity = GetThisSessionPanel(this.SessionPanelId);
            int meetingTypeId = sessionPanelEntity.MeetingTypeId().Value;
            //
            // Retrieve the Program the SessionPanel is for.  Will need this to determine
            // IsPreviouslyParticipated for each reviewer.  Do it once since it will be the
            // same for each.  
            //
            var clientProgramEntityId = sessionPanelEntity.GetProgramYear().ClientProgramId;
            //
            // Retrieve the ClientProgram entity and itemize the ProgramYear entity identifiers.
            // We will use these to determine if a reviewer has had previous program experience
            //
            ClientProgram clientProgramEntity = GetThisClientProgram(clientProgramEntityId);
            List<int> programYearIdentifierList = clientProgramEntity.ItemizeProgramYearIds();

            List<IPanelReviewerModel> list = new List<IPanelReviewerModel>();
            Retrieve<PanelUserAssignment>(sessionPanelEntity.AssignedReviewers(), list, programYearIdentifierList, MeetingType.MeetingTypeToParticipationType(meetingTypeId), ParticipationMethod.Indexes.InPerson);
            //
            // Filter the potential reviewers 
            //
            var potentialReviewers = FilterPotential(sessionPanelEntity.PanelUserPotentialAssignments);
            Retrieve<PanelUserPotentialAssignment>(potentialReviewers, list, programYearIdentifierList, MeetingType.MeetingTypeToParticipationType(meetingTypeId), ParticipationMethod.Indexes.InPerson);
            //
            // Now set the list of results.
            //
            this.Results.ModelList = list;
        }
        /// <summary>
        /// Build the model list.  Entity objects are typed as IPanelReviewer objects via the entity extension classes.
        /// This permits the same method to retrieve the information from PanelUserAssignment & PanelUserPotentialAssignments 
        /// entities.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entityCollectioCollection">Collection of entity types</param>
        /// <param name="programYearIdentifierList">List of IPanelReviewerModel objects</param>
        /// <param name="participationTypeId">SessionPanel ParticipationType entity identifier</param>
        /// <param name="inPersonParticipationId">ParticipationType entity identifier</param>
        protected void Retrieve<T>(ICollection<T> entityCollectioCollection, List<IPanelReviewerModel> list, List<int> programYearIdentifierList, int participationTypeId, int inPersonParticipationId) where T: IPanelReviewer
        {
            //
            // We send back the type name.  This may be necessary on the return trip for one or more service operations
            //
            string typeName = typeof(T).Name;

            foreach (var item in entityCollectioCollection)
            {
                var model = new PanelReviewerModel(item.UserId, item.User.UserInfoEntity().UserInfoID, item.EntityId(), typeName, participationTypeId, inPersonParticipationId);
                UserInfo userInfoEntity = item.User.UserInfoEntity();
                //
                // Initial the model
                //
                model.SetReviewerInformation(userInfoEntity.FirstName, userInfoEntity.LastName, userInfoEntity.SuffixText, userInfoEntity?.Gender?.Gender1, userInfoEntity?.Ethnicity?.EthnicityLabel, userInfoEntity.GetPreferredWebsite()?.WebsiteAddress, userInfoEntity.Institution, userInfoEntity.Position, userInfoEntity.HasPreferredEmail());
                model.SetIs(userInfoEntity.IsBlocked(item.SessionPanel.ClientId()), item.IsPotentialChair(), userInfoEntity.User.IsUserPreviouslyAssigned(programYearIdentifierList));
                model.setAcademicInformation(userInfoEntity.GetUserResumeId(), AcademicRankAbbreviation(userInfoEntity.AcademicRankId), userInfoEntity.Expertise, userInfoEntity.User.Rating(), userInfoEntity.GetDegreesNames());
                model.SetMilitary(userInfoEntity.GetMilitaryBranch(), userInfoEntity.GetMilitaryRank(), userInfoEntity.GetMilitaryStatus());
                model.SetParticant(item.Level(), item?.ParticipationMethod?.ParticipationMethodLabel, item?.ClientRole?.RoleName, item?.ClientRole?.RoleAbbreviation, item?.ClientParticipantType?.ParticipantTypeAbbreviation, item?.ClientApprovalFlag);
                list.Add(model);
            }
        }
        /// <summary>
        /// Filters the collection of PanelUserPotentialAssignment to only non recruited reviewers.
        /// </summary>
        /// <param name="collection">Collection of all PanelUserPotentialAssignment entities</param>
        /// <returns>Collection of non recruited PanelUserPotentialAssignment entities</returns>
        protected ICollection<PanelUserPotentialAssignment> FilterPotential(ICollection<PanelUserPotentialAssignment> collection)
        {
            return collection.Where(x => x.RecruitedFlag == false).ToList();
        }
        /// <summary>
        /// Retrieve the academic rank description
        /// </summary>
        /// <param name="academicRankId">AcademicRank entity identifier</param>
        /// <returns>Academic rank description if one exists</returns>
        protected string AcademicRank(int? academicRankId)
        {
            return GetThisAcademicRank(academicRankId)?.Rank;
        }
        /// <summary>
        /// Retrieve the academic rank abbreviation description
        /// </summary>
        /// <param name="academicRankId">AcademicRank entity identifier</param>
        /// <returns>Academic rank abbreviation description if one exists</returns>
        protected string AcademicRankAbbreviation(int? academicRankId)
        {
            return GetThisAcademicRank(academicRankId)?.RankAbbreviation;
        }

        #endregion
    }
}
