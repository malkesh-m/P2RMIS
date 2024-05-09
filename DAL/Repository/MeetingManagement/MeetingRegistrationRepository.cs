using Sra.P2rmis.WebModels.MeetingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.MeetingManagement
{
    public interface IMeetingRegistrationRepository : IGenericRepository<MeetingRegistration>
    {
        /// <summary>
        /// Adds the default registration.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        MeetingRegistration AddDefaultRegistration(int? panelUserAssignmentId, int? sessionUserAssignmentId, int userId);
        /// <summary>
        /// Gets the by panel user assignment identifier.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        MeetingRegistration GetByPanelUserAssignmentId(int panelUserAssignmentId);

        /// <summary>
        /// Gets the meeting registration list.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        IEnumerable<MeetingRegistration> GetMeetingRegistrationList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId);
        /// <summary>
        /// Gets the panel user assignments.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="onSiteOnly">The on site only.</param>
        /// <returns></returns>
        IEnumerable<MeetingAttendanceModel> GetReviewerAssignmentList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId, bool? onSiteOnly);

        /// <summary>
        /// Gets the panel user assignments.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="onSiteOnly">The on site only.</param>
        /// <returns></returns>
        IEnumerable<MeetingAttendanceModel> GetNonReviewerAssignmentList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId, bool? onSiteOnly);

    }

    public class MeetingRegistrationRepository : GenericRepository<MeetingRegistration>, IMeetingRegistrationRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public MeetingRegistrationRepository(P2RMISNETEntities context) : base(context) {}
        #endregion

        /// <summary>
        /// Adds the default registration.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionUserAssignmentId"></param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MeetingRegistration AddDefaultRegistration(int? panelUserAssignmentId, int? sessionUserAssignmentId, int userId)
        {
            var o = new MeetingRegistration();
            o.PanelUserAssignmentId = panelUserAssignmentId;
            o.SessionUserAssignmentId = sessionUserAssignmentId;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            return o;
        }

        /// <summary>
        /// Gets the by panel user assignment identifier.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        public MeetingRegistration GetByPanelUserAssignmentId(int panelUserAssignmentId)
        {
            return context.MeetingRegistrations.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId).FirstOrDefault();
        }

        /// <summary>
        /// Gets the meeting registration list.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public IEnumerable<MeetingRegistration> GetMeetingRegistrationList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId)
        {
            var os = context.MeetingRegistrations.Where(x => clientIdList.Contains(x.PanelUserAssignment.SessionPanel.MeetingSession.ClientMeeting.ClientId));
            os = ApplyNameFilters(os, firstName, lastName);
            os = ApplyPanelFilters(os, fiscalYear, programYearId, clientMeetingId, meetingSessionId, sessionPanelId);
            return os;
        }
        /// <summary>
        /// Gets user assignments for reviewers
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="onSiteOnly"></param>
        /// <returns></returns>
        public IEnumerable<MeetingAttendanceModel> GetReviewerAssignmentList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId, bool? onSiteOnly)
        {
            // build reviewer list
            var panelUserAssignment = context.PanelUserAssignments.Where(x => clientIdList.Contains(x.SessionPanel.MeetingSession.ClientMeeting.ClientId));

            if (onSiteOnly.HasValue && onSiteOnly == true)
            {
                panelUserAssignment = panelUserAssignment.Where(x => x.SessionPanel.MeetingSession.ClientMeeting.MeetingTypeId == MeetingType.Onsite);
            }

            panelUserAssignment = ApplyNameFilters(panelUserAssignment, firstName, lastName);
            panelUserAssignment = ApplyPanelFilters(panelUserAssignment, fiscalYear, programYearId, clientMeetingId, meetingSessionId, sessionPanelId);
            var result = panelUserAssignment.Select(x => new MeetingAttendanceModel
            {
                ReviewerUserId = x.UserId,
                FirstName = x.User.UserInfoes.FirstOrDefault().FirstName,
                LastName = x.User.UserInfoes.FirstOrDefault().LastName,
                RestrictedAssignedFlag = x.RestrictedAssignedFlag,
                ParticipationMethod = x.ParticipationMethod.ParticipationMethodLabel,
                Programs = x.SessionPanel.ProgramPanels.Select(y => y.ProgramYear.ClientProgram.ProgramAbbreviation).Distinct(),
                FiscalYear = x.SessionPanel.MeetingSession.ClientMeeting.ProgramMeetings.FirstOrDefault().ProgramYear.Year,
                PanelAbbreviation = x.SessionPanel.PanelAbbreviation,
                MeetingAbbreviation = x.SessionPanel.MeetingSession.ClientMeeting.MeetingAbbreviation,
                SessionName = x.SessionPanel.MeetingSession.SessionAbbreviation,
                HotelModifiedByUserId = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationHotels.FirstOrDefault().ModifiedBy,
                TravelModifiedByUserId = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationTravels.FirstOrDefault().ModifiedBy,
                InternalComments = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationComments.FirstOrDefault().InternalComments,
                MeetingRegistrationId = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationId,
                PanelUserAssignmentId = x.PanelUserAssignmentId,
                SessionUserAssignmentId = null,
                Email = x.User.UserInfoes.FirstOrDefault().UserEmails.Where(y => y.PrimaryFlag == true).FirstOrDefault().Email,
                Institution = x.User.UserInfoes.FirstOrDefault().Institution,
                Role = x.User.UserSystemRoles.FirstOrDefault().SystemRole.SystemRoleName,
                ParticipantType = x.ClientParticipantType.ParticipantTypeName
            }).OrderBy(x => x.LastName);
            return result;
        }

        /// <summary>
        /// Gets user assignments for non-reviewers.
        /// </summary>
        /// <param name="clientIdList">The client identifier list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="onSiteOnly"></param>
        /// <returns></returns>
        public IEnumerable<MeetingAttendanceModel> GetNonReviewerAssignmentList(List<int> clientIdList, string firstName, string lastName, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId, bool? onSiteOnly)
        {
            IQueryable<MeetingAttendanceModel> result = null;

            // check if any meeting parameters, i.e. dropdowns, are set 
            bool hasParams = HasMeetingParameters(fiscalYear, programYearId, clientMeetingId, meetingSessionId, sessionPanelId);

            // if only name was entered for search, and it's nonReviewerOnly, search without regard for assignments
            if (!hasParams && (onSiteOnly.HasValue && onSiteOnly == false)) 
            {
                // only users with clientId match to current user, and not a reviewer systemRole
                var nonReviewer = context.Users.Where(x => x.UserClients.Any(y => clientIdList.Contains(y.ClientID)) && x.UserSystemRoles.Any(y => y.SystemRoleId != SystemRole.Indexes.Reviewer));

                nonReviewer = ApplyNameFilters(nonReviewer, firstName, lastName);
                result = nonReviewer.Select(x => new MeetingAttendanceModel
                {
                    ReviewerUserId = x.UserID,
                    FirstName = x.UserInfoes.FirstOrDefault().FirstName,
                    LastName = x.UserInfoes.FirstOrDefault().LastName,
                    RestrictedAssignedFlag = false,
                    Email = x.UserInfoes.FirstOrDefault().UserEmails.Where(y => y.PrimaryFlag == true).FirstOrDefault().Email,
                    Institution = x.UserInfoes.FirstOrDefault().Institution,
                    Role = x.UserSystemRoles.FirstOrDefault().SystemRole.SystemRoleName
                }).OrderBy(x => x.LastName);
            }
            else // if meeting parameters are set and/or not nonReviewerOnly, include full search with assignments
            {
                // build non-reviewer list
                var sessionUserAssignment = context.SessionUserAssignments.Where(x => clientIdList.Contains(x.MeetingSession.ClientMeeting.ClientId));

                if (onSiteOnly.HasValue && onSiteOnly == true)
                {
                    sessionUserAssignment = sessionUserAssignment.Where(x => x.MeetingSession.ClientMeeting.MeetingTypeId == MeetingType.Onsite);
                }

                sessionUserAssignment = ApplyNameFilters(sessionUserAssignment, firstName, lastName);
                sessionUserAssignment = ApplySessionFilters(sessionUserAssignment, fiscalYear, programYearId, clientMeetingId, meetingSessionId);
                result = sessionUserAssignment.Select(x => new MeetingAttendanceModel
                {
                    ReviewerUserId = x.UserId,
                    FirstName = x.User.UserInfoes.FirstOrDefault().FirstName,
                    LastName = x.User.UserInfoes.FirstOrDefault().LastName,
                    RestrictedAssignedFlag = false,
                    ParticipationMethod = null,
                    Programs = x.MeetingSession.SessionPanels.SelectMany(y => y.ProgramPanels)
                        .Select(y2 => y2.ProgramYear.ClientProgram.ProgramAbbreviation).Distinct(),
                    FiscalYear = x.MeetingSession.ClientMeeting.ProgramMeetings.FirstOrDefault().ProgramYear.Year,
                    PanelAbbreviation = null,
                    MeetingAbbreviation = x.MeetingSession.ClientMeeting.MeetingAbbreviation,
                    SessionName = x.MeetingSession.SessionAbbreviation,
                    HotelModifiedByUserId = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationHotels.FirstOrDefault().ModifiedBy,
                    TravelModifiedByUserId = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationTravels.FirstOrDefault().ModifiedBy,
                    InternalComments = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationComments.FirstOrDefault().InternalComments,
                    MeetingRegistrationId = x.MeetingRegistrations.FirstOrDefault().MeetingRegistrationId,
                    PanelUserAssignmentId = null,
                    SessionUserAssignmentId = x.SessionUserAssignmentId,
                    Email = x.User.UserInfoes.FirstOrDefault().UserEmails.Where(y => y.PrimaryFlag == true).FirstOrDefault().Email,
                    Institution = x.User.UserInfoes.FirstOrDefault().Institution,
                    Role = x.User.UserSystemRoles.FirstOrDefault().SystemRole.SystemRoleName
                }).OrderBy(x => x.LastName);
            } 
            return result;
        }

        /// <summary>
        /// Determines whether any of the parameters passed in has a value.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>
        ///   <c>true</c> if [has meeting parameters] [the specified fiscal year]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasMeetingParameters(string fiscalYear, int? programYearId, int? clientMeetingId, int? meetingSessionId, int? sessionPanelId)
        {
            return (!string.IsNullOrEmpty(fiscalYear) || programYearId.HasValue || clientMeetingId.HasValue || meetingSessionId.HasValue || sessionPanelId.HasValue);
        }

        /// <summary>
        /// Applies the name filters.
        /// </summary>
        /// <param name="meetingRegistrationList">The meeting registration list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        private IQueryable<MeetingRegistration> ApplyNameFilters(IQueryable<MeetingRegistration> meetingRegistrationList, string firstName, string lastName)
        {
            if (!String.IsNullOrEmpty(firstName))
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.User.UserInfoes.FirstOrDefault().FirstName.StartsWith(firstName));
            if (!String.IsNullOrEmpty(lastName))
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.User.UserInfoes.FirstOrDefault().LastName.StartsWith(lastName));
            return meetingRegistrationList;
        }

        /// <summary>
        /// Overload for user entity, where search is for non-reviewers regardless of assignments.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        private IQueryable<User> ApplyNameFilters(IQueryable<User> users, string firstName, string lastName)
        {
            if (!String.IsNullOrEmpty(firstName))
                users = users.Where(x => x.UserInfoes.FirstOrDefault().FirstName.StartsWith(firstName));
            if (!String.IsNullOrEmpty(lastName))
                users = users.Where(x => x.UserInfoes.FirstOrDefault().LastName.StartsWith(lastName));
            return users;
        }
        /// <summary>
        /// Applies the panel filters.
        /// </summary>
        /// <param name="meetingRegistrationList">The meeting registration list.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        private IQueryable<MeetingRegistration> ApplyPanelFilters(IQueryable<MeetingRegistration> meetingRegistrationList, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId)
        {
            if (sessionPanelId != null && sessionPanelId > 0)
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.SessionPanelId == (int)sessionPanelId);
            if (meetingSessionId != null & meetingSessionId > 0)
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.SessionPanel.MeetingSessionId == (int)meetingSessionId);
            if (clientMeetingId != null && clientMeetingId > 0)
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.SessionPanel.MeetingSession.ClientMeetingId == (int)clientMeetingId);
            if (programYearId != null && programYearId > 0)
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.SessionPanel.ProgramPanels.OrderByDescending(y => y.CreatedDate).FirstOrDefault().ProgramYearId == (int)programYearId);
            if (!String.IsNullOrEmpty(fiscalYear))
                meetingRegistrationList = meetingRegistrationList.Where(x => x.PanelUserAssignment.SessionPanel.ProgramPanels.OrderByDescending(y => y.CreatedDate).FirstOrDefault().ProgramYear.Year == fiscalYear);
            return meetingRegistrationList;
        }

        /// <summary>
        /// Applies the name filters.
        /// </summary>
        /// <param name="panelUserAssignmentList">The panel user assignment list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        private IQueryable<PanelUserAssignment> ApplyNameFilters(IQueryable<PanelUserAssignment> panelUserAssignmentList, string firstName, string lastName)
        {
            if (!String.IsNullOrEmpty(firstName))
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.User.UserInfoes.FirstOrDefault().FirstName.StartsWith(firstName));
            if (!String.IsNullOrEmpty(lastName))
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.User.UserInfoes.FirstOrDefault().LastName.StartsWith(lastName));
            return panelUserAssignmentList;
        }
        /// <summary>
        /// Overload that takes SessionUserAssignment.
        /// </summary>
        /// <param name="sessionUserAssignmentList">The session user assignment list.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        private IQueryable<SessionUserAssignment> ApplyNameFilters(IQueryable<SessionUserAssignment> sessionUserAssignmentList, string firstName, string lastName)
        {
            if (!String.IsNullOrEmpty(firstName))
                sessionUserAssignmentList = sessionUserAssignmentList.Where(x => x.User.UserInfoes.FirstOrDefault().FirstName.StartsWith(firstName));
            if (!String.IsNullOrEmpty(lastName))
                sessionUserAssignmentList = sessionUserAssignmentList.Where(x => x.User.UserInfoes.FirstOrDefault().LastName.StartsWith(lastName));
            return sessionUserAssignmentList;
        }
        /// <summary>
        /// Applies the panel filters.
        /// </summary>
        /// <param name="panelUserAssignmentList">The panel user assignment list.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        private IQueryable<PanelUserAssignment> ApplyPanelFilters(IQueryable<PanelUserAssignment> panelUserAssignmentList, string fiscalYear, int? programYearId,
            int? clientMeetingId, int? meetingSessionId, int? sessionPanelId)
        {
            if (sessionPanelId != null && sessionPanelId > 0)
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.SessionPanelId == (int)sessionPanelId);
            if (meetingSessionId != null & meetingSessionId > 0)
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.SessionPanel.MeetingSessionId == (int)meetingSessionId);
            if (clientMeetingId != null && clientMeetingId > 0)
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.SessionPanel.MeetingSession.ClientMeetingId == (int)clientMeetingId);
            if (programYearId != null && programYearId > 0)
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.SessionPanel.ProgramPanels.OrderByDescending(y => y.CreatedDate).FirstOrDefault().ProgramYearId == (int)programYearId);
            if (!String.IsNullOrEmpty(fiscalYear))
                panelUserAssignmentList = panelUserAssignmentList.Where(x => x.SessionPanel.ProgramPanels.OrderByDescending(y => y.CreatedDate).FirstOrDefault().ProgramYear.Year == fiscalYear);
            return panelUserAssignmentList;
        }
        /// <summary>
        /// Applies the session filters.
        /// </summary>
        /// <param name="sessionUserAssignmentList">The session user assignment list.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        private IQueryable<SessionUserAssignment> ApplySessionFilters(IQueryable<SessionUserAssignment> sessionUserAssignmentList, string fiscalYear, int? programYearId,
    int? clientMeetingId, int? meetingSessionId)
        {
            if (meetingSessionId != null & meetingSessionId > 0)
                sessionUserAssignmentList = sessionUserAssignmentList.Where(x => x.MeetingSessionId == (int)meetingSessionId);
            if (clientMeetingId != null && clientMeetingId > 0)
                sessionUserAssignmentList = sessionUserAssignmentList.Where(x => x.MeetingSession.ClientMeetingId == (int)clientMeetingId);
            if (programYearId != null && programYearId > 0)
                sessionUserAssignmentList = sessionUserAssignmentList.Where(x => x.MeetingSession.SessionPanels.SelectMany(x2 => x2.ProgramPanels).Any(y => y.ProgramYearId == (int)programYearId));
            if (!String.IsNullOrEmpty(fiscalYear))
                sessionUserAssignmentList = sessionUserAssignmentList.Where(x => x.MeetingSession.SessionPanels.SelectMany(x2 => x2.ProgramPanels).Any(y => y.ProgramYear.Year == fiscalYear));
            return sessionUserAssignmentList;
        }
    }
}