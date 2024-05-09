using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of scoreboard ApplicationDetails methods.
    /// </summary>
	internal partial class RepositoryHelpers
	{


        /// <summary>
        /// Retrieves reviewer for an application by -----
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Enumerable collection of Reviewer objects</returns>
        internal static IEnumerable<ReviewerInfo_Result> GetApplicationReviewersByApplicationId(P2RMISNETEntities context, int panelApplicationId, int panelId)
        {
            var results = context.ReviewerInfo(panelApplicationId).OrderBy(x => x.Lastname);

            return results;
        }
        /// <summary>
        /// Retrieves reviewer for an application by -----
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Enumerable collection of Reviewer objects</returns>
        internal static IEnumerable<ReviewerInfo_Result> GetScoreboardReviewersByApplicationId(P2RMISNETEntities context, int panelApplicationId, int panelId)
        {
            var results = context.ReviewerInfo(panelApplicationId).OrderBy(x => x.SortOrder);
            return results;
        }

        /// <summary>
        /// Retrieves an Application Scores by Application and Program Part
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Enumerable collection of ReviewerScores objects</returns>
        internal static IEnumerable<ReviewerScores> GetApplicationScoresByApplicationId(P2RMISNETEntities context, int panelApplicationId)
        {
            var results = from pa in context.PanelApplications
                          join app in context.Applications on pa.ApplicationId equals app.ApplicationId
                          join progMech in context.ProgramMechanism on app.ProgramMechanismId equals progMech.ProgramMechanismId
                          join mechTemplate in context.MechanismTemplates on progMech.ProgramMechanismId equals mechTemplate.ProgramMechanismId
                          join mechTemplateElement in context.MechanismTemplateElements on mechTemplate.MechanismTemplateId equals mechTemplateElement.MechanismTemplateId
                          join clientElement in context.ClientElements on mechTemplateElement.ClientElementId equals clientElement.ClientElementId
                          join pua in context.PanelUserAssignments on pa.SessionPanelId equals pua.SessionPanelId
                          join cat in context.ClientParticipantTypes on pua.ClientParticipantTypeId equals cat.ClientParticipantTypeId
                          join appStage in context.ApplicationStages on pa.PanelApplicationId equals appStage.PanelApplicationId
                          join appTemplate in context.ApplicationTemplates on new { mechTemplate.MechanismTemplateId, appStage.ApplicationStageId } equals new { appTemplate.MechanismTemplateId, appTemplate.ApplicationStageId } into appTemplateGroup
                                from appTemplate in appTemplateGroup.DefaultIfEmpty()
                          join appTemplateElement in context.ApplicationTemplateElements on new { mechTemplateElement.MechanismTemplateElementId, appTemplate.ApplicationTemplateId } equals new { appTemplateElement.MechanismTemplateElementId, appTemplateElement.ApplicationTemplateId } into appTemplateElementGroup
                                from appTemplateElement in appTemplateElementGroup.DefaultIfEmpty()
                          join appWorkflow in context.ApplicationWorkflows on new { appStage.ApplicationStageId, pua.PanelUserAssignmentId } equals new { appWorkflow.ApplicationStageId, PanelUserAssignmentId = appWorkflow.PanelUserAssignmentId ?? 0 } into appWorkflowG
                          from appWorkflow in appWorkflowG.DefaultIfEmpty()
                          from appWorkflowStep in context.udfApplicationWorkflowLastStep(appWorkflow.ApplicationWorkflowId).DefaultIfEmpty()
                          join appWorkflowStepElement in context.ApplicationWorkflowStepElements on new { appWorkflowStep.ApplicationWorkflowStepId, appTemplateElement.ApplicationTemplateElementId } equals new { appWorkflowStepElement.ApplicationWorkflowStepId, appWorkflowStepElement.ApplicationTemplateElementId } into appWorkflowStepElementG
                          from appWorkflowStepElement in appWorkflowStepElementG.DefaultIfEmpty()
                          join appWorkflowStepElementContent in context.ApplicationWorkflowStepElementContents on appWorkflowStepElement.ApplicationWorkflowStepElementId equals appWorkflowStepElementContent.ApplicationWorkflowStepElementId into appWorkflowStepElementContentG
                          from appWorkflowStepElementContent in appWorkflowStepElementContentG.DefaultIfEmpty()
                          where
                                pa.PanelApplicationId == panelApplicationId &&
                                appStage.ReviewStageId == ReviewStage.Synchronous &&
                                cat.ReviewerFlag && mechTemplateElement.ScoreFlag &&
                                mechTemplate.ReviewStageId == ReviewStage.Synchronous
                          select new ReviewerScores
                          {
                              ApplicationId = app.LogNumber,
                              ShortDescription = clientElement.ElementIdentifier,
                              EvaluationCriteriaDescription = clientElement.ElementAbbreviation,
                              PrgPartId = pua.PanelUserAssignmentId,
                              Score = appWorkflowStepElementContent.Score,
                              CriteriaSortOrder = mechTemplateElement.SortOrder,
                              OverallEval = mechTemplateElement.OverallFlag,
                              AbstainFlag = appWorkflowStepElementContent.Abstain == null ? false : appWorkflowStepElementContent.Abstain,
                              UserId = pua.UserId,
                              ClientScoringId = appWorkflowStepElement.ClientScoringId,
                              AdjLabel = ((appWorkflowStepElement.ClientScoringScale != null) &
                                    (appWorkflowStepElement.ClientScoringScale.ScoreType.StartsWith("a") || appWorkflowStepElement.ClientScoringScale.ScoreType.StartsWith("A")
                                    )) ? appWorkflowStepElement.ClientScoringScale.ScoreType : null,
                              IntegerFlag = appWorkflowStepElement.ClientScoringScale != null &
                                    (appWorkflowStepElement.ClientScoringScale.ScoreType.ToLower() == ClientScoringScale.ScoringType.Integer.ToLower())
                          };
               
            return results;
        }
        /// <summary>
        /// Gets the peer review data.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        internal static IEnumerable<PeerReviewResultModel> GetPeerReviewData(P2RMISNETEntities context, int clientId)
        {
            var results = (from pr in context.uspGetPeerReviewData(clientId)
                           select new PeerReviewResultModel
                           {
                               ApplicationId = pr.ApplicationId,
                               LogNumber = pr.LogNumber,
                               PanelApplicationId = pr.PanelApplicationId,
                               PanelName = pr.PanelName,
                               PanelAbbreviation = pr.PanelAbbreviation,
                               StartDate = pr.StartDate,
                               EndDate = pr.EndDate,
                               MeetingTypeName = pr.MeetingTypeName,
                               AssignmentReleaseDate = pr.AssignmentReleaseDate,
                               ReviewStatusId = pr.ReviewStatusId,
                               ReviewStatusLabel = pr.ReviewStatusLabel,
                               AvgScore = pr.AvgScore,
                               StDev = pr.StDev,
                               FirstName = pr.FirstName,
                               LastName = pr.LastName,
                               AssignmentLabel = pr.AssignmentLabel,
                               ClientAssignmentTypeId = pr.ClientAssignmentTypeId,
                               SortOrder = pr.SortOrder,
                               CoiSignedDate = pr.CoiSignedDate,
                               ResolutionDate = pr.ResolutionDate,
                               ScreeningTcDate = pr.ScreeningTcDate,
                               ScreeningTcCritiqueDate = pr.ScreeningTcCritiqueDate,
                               Year = pr.Year,
                               ReceiptCycle = pr.ReceiptCycle ?? 0
                           });
            return results;
        }
        /// <summary>
        /// Retrieves a reviewer's comments by application identifier.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Enumerable collection of ReviewerComments objects</returns>
        internal static IEnumerable<ReviewerComments> GetReviewerCommentsByApplicationId(P2RMISNETEntities context, int panelApplicationId)
        {
            var results = from UserApplicationComment in context.UserApplicationComments
                          join PanelApplication in context.PanelApplications on UserApplicationComment.PanelApplicationId equals PanelApplication.PanelApplicationId
                          join PanelUserAssignment in context.PanelUserAssignments on new { PanelApplication.SessionPanelId, UserId = UserApplicationComment.UserID } equals new { PanelUserAssignment.SessionPanelId, PanelUserAssignment.UserId }
                          join Person in context.UserInfoes on UserApplicationComment.UserID equals Person.UserID
                          join Prefix in context.Prefixes on Person.PrefixId equals Prefix.PrefixId into prefixG
                          from Prefix in prefixG.DefaultIfEmpty()
                          where
                            UserApplicationComment.PanelApplicationId == panelApplicationId && UserApplicationComment.CommentTypeID == CommentType.Indexes.ReviewerNote
                          select new ReviewerComments
                          {
                              PanelId = PanelApplication.SessionPanelId,
                              PrgPartId = PanelUserAssignment.PanelUserAssignmentId,
                              ReviewerId = PanelUserAssignment.UserId,
                              Prefix = Prefix.PrefixName,
                              FirstName = Person.FirstName,
                              LastName = Person.LastName,
                              Comment = UserApplicationComment.Comments
                          };

            return results;
        }
        /// <summary>
        /// Retrieves all user comments for an application identifier.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Enumerable collection of UserApplicationComment objects</returns>
        internal static IEnumerable<UserApplicationComments> GetUserCommentsByApplicationId(P2RMISNETEntities context, int panelApplicationId, List<int> commentTypes)
        {
            var results = from UserApplicationComment in context.UserApplicationComments
                          join LookupCommentType in context.CommentTypes on UserApplicationComment.CommentTypeID equals LookupCommentType.CommentTypeID
                          join User in context.Users on UserApplicationComment.UserID equals User.UserID
                          join UserInfo in context.UserInfoes on User.UserID equals UserInfo.UserID
                          join Prefix in context.Prefixes on UserInfo.PrefixId equals Prefix.PrefixId into prefixG
                          from Prefix in prefixG.DefaultIfEmpty()
                          where UserApplicationComment.PanelApplicationId == panelApplicationId && commentTypes.Any(c => LookupCommentType.CommentTypeID.Equals(c))
                          select new UserApplicationComments
                          {
                              CommentID = UserApplicationComment.UserApplicationCommentID,
                              UserID = UserApplicationComment.UserID,
                              UserPrefix = Prefix.PrefixName,
                              UserFirstName = UserInfo.FirstName,
                              UserLastName = UserInfo.LastName,
                              Comments = UserApplicationComment.Comments,
                              ModifiedBy = UserApplicationComment.ModifiedBy,
                              ModifiedDate = UserApplicationComment.ModifiedDate,
                              CreatedBy = UserApplicationComment.CreatedBy,
                              CreatedDate = UserApplicationComment.CreatedDate,
                              CommentLkpId = LookupCommentType.CommentTypeID,
                              CommentLkpDescription = LookupCommentType.CommentTypeName
                          };
            return results;
        }


        /// <summary>
        /// Gets comment lookup values
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static IEnumerable<CommentTypes> GetCommentLookupValues(P2RMISNETEntities context, List<int> commentTypes)
        {
            var results = from LCT in context.CommentTypes
                          where commentTypes.Any(c => LCT.CommentTypeID.Equals(c))
                          select new CommentTypes
                          {
                              CommentTypeId = LCT.CommentTypeID,
                              CommentTypeDescription = LCT.CommentTypeName
                          };

            return results;
        }
        /// <summary>
        /// Gets comment lookup values
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static IEnumerable<CommentTypes> GetCommentExceptAdminLookupValues(P2RMISNETEntities context, List<int> commentTypes)
        {
            var results = from LCT in context.CommentTypes
                          where commentTypes.Any(c => LCT.CommentTypeID.Equals(c) && LCT.CommentTypeID != CommentType.Indexes.AdminNote)
                          select new CommentTypes
                          {
                              CommentTypeId = LCT.CommentTypeID,
                              CommentTypeDescription = LCT.CommentTypeName
                          };

            return results;
        }

        /// <summary>
        /// Gets the default participant type of the mapped participant.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="assignedUserId">The assigned user identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>ClientParticipantType object that the user is related</returns>
        internal static ClientParticipantType GetMappedParticipantType(P2RMISNETEntities context, int assignedUserId, int sessionPanelId)
        {
            var result = context.RoleParticipantTypes.Where(z =>
                z.ClientId == context.ProgramPanels.Where(x => x.SessionPanelId == sessionPanelId).Select(x => x.ProgramYear.ClientProgram.ClientId).FirstOrDefault()
                && z.SystemRoleId == context.UserSystemRoles.Where(x => x.UserID == assignedUserId).Select(x => x.SystemRoleId).FirstOrDefault()).Select(x => x.ClientParticipantType).FirstOrDefault();
            return result;
        }

        internal void InsertUserApplicationComment(P2RMISNETEntities context, UserApplicationComment userComment)
        {
            context.UserApplicationComments.Add(userComment);
        }

        internal void DeleteUserApplicationComment(P2RMISNETEntities context, int commentId)
        {
            UserApplicationComment userComment = context.UserApplicationComments.Find(commentId);
            context.UserApplicationComments.Remove(userComment);
        }

        internal void UpdateUserApplicationComment(P2RMISNETEntities context, UserApplicationComment userComment)
        {
            context.Entry(userComment).State = System.Data.Entity.EntityState.Modified;
        }
        /// <summary>
        /// get all applications by params
        /// </summary>
        /// <param name="context"></param>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="programYearId"></param>
        /// <param name="panelId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="awardId"></param>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        internal static IEnumerable<IApplicationsManagementModel> GetApplications(P2RMISNETEntities context, int? clientId, string fiscalYear, int? programYearId, int? panelId, int? receiptCycle, int? awardId, string logNumber, List<int> userClientIds)
        {
            var result = from Applications in context.Applications
                         join PanelApplication in context.PanelApplications on Applications.ApplicationId equals PanelApplication.ApplicationId into paa
                         from PanelApplication2 in paa.DefaultIfEmpty()
                         join SessionPanel in context.SessionPanels on PanelApplication2.SessionPanelId equals SessionPanel.SessionPanelId into sp
                         from SessionPanel2 in sp.DefaultIfEmpty()
                         join ProgramMechanism in context.ProgramMechanism on Applications.ProgramMechanismId equals ProgramMechanism.ProgramMechanismId
                         join ApplicationPersonnel in context.ApplicationPersonnels on Applications.ApplicationId equals ApplicationPersonnel.ApplicationId into aap
                         from ApplicationPersonnel2 in aap.DefaultIfEmpty()
                         join ClientAwardType in context.ClientAwardTypes on ProgramMechanism.ClientAwardTypeId equals ClientAwardType.ClientAwardTypeId
                         join ProgramYear in context.ProgramYears on ProgramMechanism.ProgramYearId equals ProgramYear.ProgramYearId
                         where ((ClientAwardType.ClientId == clientId || clientId == null)
                         && (ProgramYear.Year == fiscalYear || String.IsNullOrEmpty(fiscalYear))
                         && (ProgramMechanism.ProgramYearId == programYearId || programYearId == null)
                         && ApplicationPersonnel2.PrimaryFlag == true)
                         select new ApplicationsManagementModel
                         {
                             ApplicationId = Applications.ApplicationId,
                             LogNumber = Applications.LogNumber,
                             PiName = ApplicationPersonnel2.LastName + ", " + ApplicationPersonnel2.FirstName,
                             Title = Applications.ApplicationTitle,
                             PiOrganization = ApplicationPersonnel2.OrganizationName,
                             Panel = SessionPanel2 == null ? String.Empty : SessionPanel2.PanelAbbreviation,
                             Withdrawn = Applications.WithdrawnFlag,
                             Award = ClientAwardType.AwardAbbreviation,
                             SessionPanelId = SessionPanel2 == null ? 0 : SessionPanel2.SessionPanelId,
                             ReceiptCycle = ProgramMechanism.ReceiptCycle,
                             AwardId = ClientAwardType.ClientAwardTypeId,
                             WithdrawnBy = Applications.WithdrawnBy,
                             WithdrawnDate = Applications.WithdrawnDate,
                             ClientId = ClientAwardType.ClientId

                         };


            if (panelId != null)
            {
                result = result.Where(x => x.SessionPanelId == panelId);
            }
            if (receiptCycle != null)
            {
                result = result.Where(x => x.ReceiptCycle == receiptCycle);
            }
            if (awardId != null)
            {
                result = result.Where(x => x.AwardId == awardId);
            }

            if (!string.IsNullOrEmpty(logNumber))
            {
                result = result.Where(x => x.LogNumber.StartsWith(logNumber));
            }

            if (result != null && userClientIds.Contains(result.FirstOrDefault().ClientId))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}

    class foo
    {
        public int EcmID { get; set; }
        public string ApplicationID { get; set; }
        int _prgPartId;
        public int PrgPartID 
        {
            set
            {
                this._prgPartId = value;
            }
        }
        public int? PrgPartID2
        {
            set
            {
                this._prgPartId = value.GetValueOrDefault(-1);
            }
        }

        public int PrgPartIdValue { get { return this._prgPartId; } }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(foo p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (this.EcmID == p.EcmID) && (this.ApplicationID == p.ApplicationID) && (this.PrgPartIdValue == p.PrgPartIdValue);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

