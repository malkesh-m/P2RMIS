using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    internal partial class RepositoryHelpers
    {
        #region Constructors & Setup
        /// <summary>
        /// Default constructor.  Private default constructor controls construction & instantiation.
        /// </summary>
        private RepositoryHelpers() {}
        #endregion
        #region The Helpers
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelId">Panel identifier</param>
        /// <returns>Enumerable collection of application details objects</returns>
        internal static IEnumerable<uspViewPanelDetails_Result> GetApplicationDetailsByPanelId(P2RMISNETEntities context, int panelId, int userId)
        {

            var results = context.uspViewPanelDetails(panelId, userId).OrderBy(x => x.Triaged).ThenBy(x => x.Order).ThenBy(x => x.ApplicationId).ToList();
            results.ForEach(m =>
            {
                if (m.Adjectival == true) m.AverageOE = 0;
            });
            return results;
        }
        /// <summary>
        /// Retrieves a Meeting's Sessions by programId.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="programId">Program identifier; may not be null</param>
        /// <param name="userId"></param>
        /// <param name="elevatedPerms"></param>
        /// <returns>Enumerable collection containing program's Sessions objects</returns>
        internal static IEnumerable<MeetingSessionModel> GetMeetingSessionsByProgramId(P2RMISNETEntities context, int programId, int? userId, bool elevatedPerms)
        {
            return (from ms in context.MeetingSessions
             join meeting in context.ClientMeetings on ms.ClientMeetingId equals meeting.ClientMeetingId
             join panel in context.SessionPanels
                   on ms.MeetingSessionId equals panel.MeetingSessionId
             join programPanel in context.ProgramPanels on panel.SessionPanelId equals programPanel.SessionPanelId
             join panelParticipant in context.PanelUserAssignments on panel.SessionPanelId equals panelParticipant.SessionPanelId
             where
               (elevatedPerms == true || panelParticipant.UserId == userId) &&
               (meeting.MeetingTypeId == MeetingType.Onsite ||
               meeting.MeetingTypeId == MeetingType.Teleconference || meeting.MeetingTypeId == MeetingType.VideoConference) &&
               programPanel.ProgramYearId == programId
             select new MeetingSessionModel()
             {
                 MeetingSessionId = ms.MeetingSessionId,
                 ClientMeetingId = ms.ClientMeetingId,
                 StartDate = ms.StartDate,
                 EndDate = ms.EndDate,
                 SessionAbbreviation = ms.SessionAbbreviation,
                 SessionDescription = ms.SessionDescription,
                 ProgramYearId = programPanel.ProgramYearId
             }
             ).Distinct().OrderByDescending(d => d.StartDate);
        }

        /// <summary>
        /// Retrieves a program's Panels by SessionIds.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="programId"></param>
        /// <param name="sessionIds">Session identifiers</param>
        /// <param name="userId"></param>
        /// <param name="elevatedPerms"></param>
        /// <returns>Enumerable collection of Panels objects</returns>
        internal static IEnumerable<SessionPanel> GetPanelsBySessionIds(P2RMISNETEntities context, int programId, IEnumerable<int> sessionIds, int? userId, bool elevatedPerms)
        {
            return (from panelParticipant in context.PanelUserAssignments
             join panel in context.SessionPanels on panelParticipant.SessionPanelId equals panel.SessionPanelId
             join programPanel in context.ProgramPanels on panel.SessionPanelId equals programPanel.SessionPanelId
             join meetingSession in context.MeetingSessions on panel.MeetingSessionId equals meetingSession.MeetingSessionId
             where
               (elevatedPerms == true || panelParticipant.UserId == userId) &&
               sessionIds.Contains(meetingSession.MeetingSessionId) && programPanel.ProgramYearId == programId
             orderby
               panel.PanelAbbreviation
             select panel).Distinct().OrderBy(p => p.PanelAbbreviation);
        }
        /// <summary>
        /// Returns the open programs list.
        /// </summary>
        /// <param name="clientList">List of clients</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Enumerable list of ProgramListResultModels objects</returns>
        internal static IEnumerable<ProgramListResultModel> GetOpenProgramsList(List<int> clientList, P2RMISNETEntities context)
        {
            int[] clientIds = clientList.ToArray();
            var result = from programFY in context.ProgramYears 
                         join clientProgram in context.ClientPrograms on programFY.ClientProgramId equals clientProgram.ClientProgramId
                         join client in context.Clients on clientProgram.ClientId equals client.ClientID
                         orderby programFY.Year descending, clientProgram.ProgramDescription
                         where programFY.DateClosed == null  &&
                               clientIds.Contains(client.ClientID)
                         select new ProgramListResultModel { ProgramFY = programFY, ClientProgram = clientProgram };

            return result;
        }

        /// <summary>
        /// Returns the open programs list of only assigned programs
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns></returns>
        internal static IEnumerable<ProgramListResultModel> GetAssignedOpenProgramsList(int? userId, P2RMISNETEntities context)
        {
            return (from panelParticipant in context.PanelUserAssignments
                         join panel in context.SessionPanels on panelParticipant.SessionPanelId equals panel.SessionPanelId
                         join programPanel in context.ProgramPanels on panel.SessionPanelId equals programPanel.SessionPanelId
                         join programFY in context.ProgramYears on programPanel.ProgramYearId equals programFY.ProgramYearId
                         join clientProgram in context.ClientPrograms on programFY.ClientProgramId equals clientProgram.ClientProgramId
                         join session in context.MeetingSessions on panel.MeetingSessionId equals session.MeetingSessionId
                         join meeting in context.ClientMeetings on session.ClientMeetingId equals meeting.ClientMeetingId
                    where programFY.DateClosed == null && panelParticipant.UserId == userId && (meeting.MeetingTypeId == MeetingType.Onsite ||
                                        meeting.MeetingTypeId == MeetingType.Teleconference || meeting.MeetingTypeId == MeetingType.VideoConference)
                         select new ProgramListResultModel { ProgramFY = programFY, ClientProgram = clientProgram}).Distinct().OrderByDescending(d => d.ProgramFY.Year).ThenBy(d => d.ClientProgram.ProgramDescription);
        }
        /// <summary>
        /// Returns counts of applications by mechanism
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelIds">List of session panel identifiers</param>
        /// <returns>Enumerable list of ApplicationCount objects</returns>
        internal static IEnumerable<ApplicationCount> GetMechanismApplicationCount(P2RMISNETEntities context, IEnumerable<int> sessionPanelIds)
        {
            return (from pa in context.PanelApplications
                        join app in context.Applications on pa.ApplicationId equals app.ApplicationId
                        join pm in context.ProgramMechanism on app.ProgramMechanismId equals pm.ProgramMechanismId
                        join cat in context.ClientAwardTypes on pm.ClientAwardTypeId equals cat.ClientAwardTypeId
                        join ars in context.ApplicationReviewStatus on pa.PanelApplicationId equals ars.PanelApplicationId
                        where sessionPanelIds.Contains(pa.SessionPanelId) && ars.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review
                        group new { cat, ars } by new
                        {
                            AwardAbbreviation = cat.AwardAbbreviation
                        }
                        into countGrp
                        select new ApplicationCount
                        {
                            AwardAbbreviation = countGrp.Key.AwardAbbreviation,
                            TotalApplications = countGrp.Count(),
                            TotalScored = countGrp.Count(x => x.ars.ReviewStatusId == ReviewStatu.Scored),
                            TotalExpedited = countGrp.Count(x => x.ars.ReviewStatusId == ReviewStatu.Triaged)
                        });
        }
        /// <summary>
        /// Returns counts of applications by mechanism
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">The session panel identifiers</param>
        /// <returns>Enumerable list of ApplicationCount objects</returns>
        internal static IEnumerable<ApplicationCount> GetMechanismApplicationCount(P2RMISNETEntities context, int sessionPanelId)
        {
            List<int> panelId = new List<int>();
            panelId.Add(sessionPanelId);

            return GetMechanismApplicationCount(context, panelId);
        }
        /// <summary>
        /// Retrieves the descriptive information describing the abstract file
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <returns>Enumeration of ApplicationFileModels (should only be one)</returns>
        internal static IEnumerable<ApplicationFileModel> GetClientAbstractFileType(P2RMISNETEntities context, int applicationId)
        {
            var result = from app in context.Applications
                            join progAward in context.ProgramMechanism on app.ProgramMechanismId equals progAward.ProgramMechanismId
                            join clientAwardType in context.ClientAwardTypes on progAward.ClientAwardTypeId equals clientAwardType.ClientAwardTypeId
                            join clientFileConfiguration in context.ClientFileConfigurations on clientAwardType.ClientId equals clientFileConfiguration.ClientId
                            where app.ApplicationId == applicationId && clientFileConfiguration.AbstractFlag
                            orderby clientFileConfiguration.DisplayLabel
                         select new ApplicationFileModel
                             {
                                 DisplayLabel = clientFileConfiguration.DisplayLabel,
                                 LogNumber = app.LogNumber,
                                 FileSuffix = ApplicationSuffixSeperator + clientFileConfiguration.FileSuffix + ApplicationPdfExtension,
                                 Folder = AbstractFolder
                             };
            return result;

        }
        #endregion
    }
}
