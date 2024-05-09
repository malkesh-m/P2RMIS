using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's SessionPanel object.
    /// </summary>
    public partial class SessionPanel: IStandardDateFields
    {
        /// <summary>
        /// A buffer in days for start date and end date for reviewer attendance and hotel checkin and checkout
        /// </summary>
        public const int MeetingAttendanceBufferDays = 2;

        /// <summary>
        /// Application's status.  
        /// </summary>
        public class ApplicationState
        {
            /// <summary>
            /// Application has not bee released.
            /// </summary>
            public static int PreAssignment { get { return 1; } }
            /// <summary>
            /// Application has been released
            /// </summary>
            public static int PostAssignment { get { return 2; } }
            /// <summary>
            /// Application is final
            /// </summary>
            public static int Final { get { return 3; } }
        }

        /// <summary>
        /// Finds the PanelApplication with the application log number associated with this SessionPanel.
        /// </summary>
        /// <param name="logNumber">Application log number</param>
        /// <returns>PanelApplication with the application whose log number was specified or null if not located</returns>
        public PanelApplication GetPanelApplicationByApplicationLogNumber(string logNumber)
        {
            return this.PanelApplications.FirstOrDefault(x => x.Application.LogNumber == logNumber);
        }
        /// <summary>
        /// Returns the release date.
        /// </summary>
        /// <returns></returns>
        public DateTime? ReleaseDate()
        {
            return this.PanelApplications.DefaultIfEmpty(new PanelApplication()).First().ReleaseDate();
        }
        /// <summary>
        /// Retrieves the ProgramYear entity
        /// </summary>
        /// <returns>ProgramYear entity object</returns>
        public ProgramYear GetProgramYear()
        {
            return this.ProgramPanels.DefaultIfEmpty(new ProgramPanel()).OrderByDescending(x => x.CreatedDate).First().ProgramYear;
        }

        /// <summary>
        /// Determines whether parent program is open (not part of a closed program).
        /// </summary>
        /// <returns>True if open; false if closed</returns>
        public bool IsPanelOpen()
        {
            return this.GetProgramYear() != null && GetProgramYear().DateClosed == null;
        }
        /// <summary>
        ///Obtain originator's first name
        /// </summary>
        /// <returns>returns originator's first name</returns>
        public string FirstName(int userId)
        {
            return this.PanelUserAssignments.Where(x => x.UserId == userId).FirstOrDefault().FirstName();
        }
        /// <summary>
        /// Obtain sender's last name
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>returns sender's last name</returns>
        public string LastName(int userId)
        {
            return this.PanelUserAssignments.Where(x => x.UserId == userId).FirstOrDefault().LastName();
        }
        /// <summary>
        /// Obtain sender's  panel assignment identifier
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Panel user assignment identifier</returns>
        public int PanelUserAssignmentId(int userId)
        {
            var pua = this.PanelUserAssignments.Where(x => x.UserId == userId).FirstOrDefault();
            return (pua != null) ? pua.PanelUserAssignmentId : 0;
        }
        /// <summary>
        /// Obtain sender's primary email address
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>User Email Address</returns>
        public string UserEmailAddress(int userId)
        {
            return this.PanelUserAssignments.Where(x => x.UserId == userId).FirstOrDefault().PrimaryUserEmailAddress();
        }
        /// <summary>
        /// Obtain senders's participant type abbreviation
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Participant Type Abbreviation</returns>
        public string ParticipantTypeAbbreviation(int userId)
        {
            var pta = this.PanelUserAssignments.Where(x => x.UserId == userId).FirstOrDefault();
                
            return (pta != null)? pta.ClientParticipantType.ParticipantTypeAbbreviation: string.Empty;
        }
        /// <summary>
        /// Determines if the panel is open.  A panel is open if
        /// - StartDate has a value
        /// - EndDate has a value
        /// - GlobalProperties.P2rmisDateTimeNow is greater than or equal to the StartDate
        /// - GlobalProperties.P2rmisDateTimeNow is less than or equal to the EndDate
        /// </summary>
        /// <returns>True if panel is open; false otherwise</returns>
        public bool IsActive()
        {
            return ((this.StartDate.HasValue) & (this.EndDate.HasValue))? (this.StartDate <= GlobalProperties.P2rmisDateTimeNow) & (GlobalProperties.P2rmisDateTimeNow <= this.EndDate): false;
        }
        /// <summary>
        /// Determines if the panel is within the dates to display hotel reservations
        /// - todays date is no more than 30 days after the session end date
        /// </summary>
        public bool IsIncludeForHotelReservation()
        {
            return (this.EndDate.HasValue ? (GlobalProperties.P2rmisDateTimeNow <= (DateTime)(this.EndDate.Value).AddDays(30)) : false);
        }
        /// <summary>
        /// Returns the Client entity identifier 
        /// </summary>
        /// <returns>Client entity identifier</returns>
        public int ClientId()
        {
            return this.MeetingSession.ClientId();
        }
        /// <summary>
        /// Determine the ApplicationState of this SessionPanel.
        /// </summary>
        /// <returns>ApplicationState of this PanelApplication</returns>
        public int PanelApplicationState(bool isReleased)
        {
            int result;
            //
            // If the session panel has already started and the panel is not an on-line panel then it is in the final state
            // If the session panel has been released it is PostAssigned
            // otherwise the session panel is PreAssigned
            //
            if (
                (this.StartDate.HasValue) && (this.StartDate.Value < GlobalProperties.P2rmisDateTimeNow) &&
                (this.MeetingTypeId() != Dal.MeetingType.Indexes.OnLineReview)
                )
            {
                result = ApplicationState.Final;
            }
            else if (isReleased)
            {
                result = ApplicationState.PostAssignment;
            }
            else
            {
                result = ApplicationState.PreAssignment;
            }
            return result;
        }

        /// <summary>
        /// Gets the current panel stage for the panel.
        /// </summary>
        /// <remarks>
        /// If the panel start date has passed and the synchronous stage has passed, we assume synchronous otherwise async
        /// </remarks>
        /// <returns>The Current PanelStage entity</returns>
        public PanelStage CurrentPanelStage()
        {
            var currentStageId = this.StartDate <= GlobalProperties.P2rmisDateTimeNow &&
                                 this.PanelStages.Any(x => x.ReviewStageId == ReviewStage.Synchronous)
                ? ReviewStage.Synchronous
                : ReviewStage.Asynchronous;
            return
                this.PanelStages.FirstOrDefault(
                    x =>
                        x.ReviewStage.ReviewStageId ==
                        (currentStageId));
        }
        /// <summary>
        /// Gets the current panel stage for the panel.  This version is called from the MyWorkspace grid and 
        /// will force OnLine meetings to stay in the post-assignment context
        /// </summary>
        /// <remarks>
        /// If the panel start date has passed and the synchronous stage has passed, we assume synchronous otherwise async
        /// </remarks>
        /// <returns>The Current PanelStage entity</returns>
        public PanelStage CurrentPanelStageOnLineMeeting()
        {
            var currentStageId = (!this.IsOnLineMeeting())? this.StartDate <= GlobalProperties.P2rmisDateTimeNow && this.PanelStages.Any(x => x.ReviewStageId == ReviewStage.Synchronous)
                                 ? ReviewStage.Synchronous : ReviewStage.Asynchronous : ReviewStage.Asynchronous;
            return
                this.PanelStages.FirstOrDefault(
                    x =>
                        x.ReviewStage.ReviewStageId ==
                        (currentStageId));
        }
        /// <summary>
        /// Determines the current phase of this PanelStage.
        /// </summary>
        /// <returns>Current Phase indicator</returns>
        /// <remarks>
        ///      This assumes that the calling sequence started with PanelApplication.CurrentPhase();
        ///      This assumes that the panel application has been released.
        ///      Unit tests not written
        /// </remarks>
        public PanelStageStep CurrentPhase()
        {
            PanelStage p = this.CurrentPanelStage();
            return p.CurrentPhase();
        }
        /// <summary>
        /// Determines the current phase of this PanelStage.  This version is called for the MyWorkspace grid 
        /// and will force OnLinePanel to display as if they stay in the post-assignment context.
        /// </summary>
        /// <returns>Current Phase indicator</returns>
        /// <remarks>
        ///      This assumes that the calling sequence started with PanelApplication.CurrentPhase();
        ///      This assumes that the panel application has been released.
        ///      Unit tests not written
        /// </remarks>        
        public PanelStageStep CurrentPhaseOnLineMeeting()
        {
            PanelStage p = this.CurrentPanelStageOnLineMeeting();
            return p.CurrentPhase();
        }
        /// <summary>
        /// Determines the end date & time for the current phase
        /// </summary>
        /// <returns>End date for the current phase</returns>
        public DateTime? CurrentPhaseDate()
        {
            DateTime? result = null;
            PanelStageStep panelStageStepEntity = this.CurrentPhase();
            if (panelStageStepEntity != null)
            {
                result = panelStageStepEntity.EndDate;
            }
            return result;
        }
        /// <summary>
        /// Determines the end date & time for the current phase
        /// </summary>
        /// <returns>Name of the current phase</returns>
        public string CurrentPhaseName()
        {
            string result = null;
            PanelStageStep panelStageStepEntity = this.CurrentPhaseOnLineMeeting();
            if (panelStageStepEntity != null)
            {
                result = panelStageStepEntity.StypeTypeName();
            }
            return result;
        }
        /// <summary>
        /// Obtain users PaneUserAssignment 
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Users PaneUserAssignment entity on this panel</returns>
        public PanelUserAssignment PanelUserAssignment(int userId)
        {
            return this.PanelUserAssignments.FirstOrDefault(x => x.UserId == userId);
        }
        /// <summary>
        /// Returns the user's participation type on the panel (if any)
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>User's participation type</returns>
        public string GetUsersParticipantType(int userId)
        {
            PanelUserAssignment panelUserAssignmentEntity = PanelUserAssignment(userId);
            return (panelUserAssignmentEntity != null) ? panelUserAssignmentEntity.ClientParticipantType.ParticipantTypeName : string.Empty;
        }
        /// <summary>
        /// Returns the user's participation role name on the panel (if any)
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>User's participation type</returns>
        public string GetUsersRoleName(int userId)
        {
            PanelUserAssignment panelUserAssignmentEntity = PanelUserAssignment(userId);
            return (panelUserAssignmentEntity != null && panelUserAssignmentEntity.ClientRole != null) ? panelUserAssignmentEntity.ClientRole.RoleName: string.Empty;
        }
        /// <summary>
        /// Indicates if the session is ended.  (The session end date is prior to now)
        /// </summary>
        /// <returns><True if the session is ended; false otherwise/returns>
        public bool IsSessionEnded()
        {
            return (this.EndDate < GlobalProperties.P2rmisDateTimeNow);
        }
        /// <summary>
        /// Determines if the PanelStageStep is in the Final phase/
        /// </summary>
        /// <returns>True if the PanelPhase is Final; false otherwise</returns>
        public bool IsFinalPhase()
        {
            return this.CurrentPhase().IsFinalPhase();
        }
        /// <summary>
        /// Gets the synchronous stage.
        /// </summary>
        /// <returns>PanelStage entity for the synchronous stage</returns>
        public PanelStage AsyncStage()
        {
            return (this.PanelStages)?.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Asynchronous);
        }
        /// <summary>
        /// Synchronizes the stage.
        /// </summary>
        /// <returns></returns>
        public PanelStage SyncStage()
        {
            return (this.PanelStages)?.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Synchronous);
        }
        /// <summary>
        /// Returns the SessionPanel's PanelStageStep for the specified step type;
        /// <param name="stepTypeid">StepType entity identifier</param>
        /// <returns>PanelStageStep for the specified StepType</returns>
        public PanelStageStep GetSpecificPanelStageStep(int stepTypeid)
        {
            return 
                //
                // First we collect all of the PanelStageSteps for the SessionPanel
                //
                this.PanelStages.SelectMany(x => x.PanelStageSteps).
                //
                // Then we locate the first one that matches the StepType identifier.
                // There should only be one.
                //
                First(x => x.StepTypeId == stepTypeid);
        }
        /// <summary>
        /// Returns the SessionPanel's PanelStage for the specified reviewStageId;
        /// <param name="reviewStageId">ReviewStage entity identifier</param>
        /// <returns>PanelStage for the specified ReviewStage identifier</returns>
        public PanelStage GetSpecificPanelStage(int reviewStageId)
        {
            return
                //
                // Find the PanelStage with the matching ReviewStage identifier.  
                //
                this.PanelStages.FirstOrDefault(x => x.ReviewStageId == reviewStageId);

        }
        /// <summary>
        /// Retrieves the fiscal year of the sessions program.
        /// </summary>
        /// <returns>Fiscal year</returns>
        public string GetFiscalYear()
        {
            return GetProgramYear()?.Year;
        }
        /// <summary>
        /// Returns the ProgramYearId
        /// </summary>
        /// <returns>ProgramYear entity identifier</returns>
        public int GetProgramYearId()
        {
            return GetProgramYear().ProgramYearId;
        }
        /// <summary>
        /// Returns the fiscal year year as a numerical value.  If for any
        /// reason the fiscal year cannot be converted DateTime.MinValue.Year
        /// is returned.
        /// </summary>
        /// <returns>Fiscal year year as a numerical value</returns>
        public int GetNumericFiscalYear()
        {
            int result = DateTime.MinValue.Year;
            try
            {
                result = Convert.ToInt32(GetFiscalYear());
            }
            catch { }
            return result;
        }
        /// <summary>
        /// Retrieves the Program Abbreviation of the sessions program.
        /// </summary>
        /// <returns></returns>
        public string GetProgramAbbreviation()
        {
            return GetProgramYear()?.ClientProgram?.ProgramAbbreviation;
        }
        /// <summary>
        /// The Sessions's meeting type.
        /// </summary>
        /// <returns>Session meeting type</returns>
        public string MeetingType()
        {
            return MeetingSession?.ClientMeeting.MeetingType.MeetingTypeName;
        }
        /// <summary>
        /// Returns the MeetingTypeId
        /// </summary>
        /// <returns>MeetingTypeId if exist; null otherwise</returns>
        public int? MeetingTypeId()
        {
            return MeetingSession?.ClientMeeting.MeetingType.MeetingTypeId;
        }
        /// <summary>
        /// Indicates if the SessionPanel is an Online meeting
        /// </summary>
        /// <returns>
        /// True if the SessionPanel is an Online meeting; False if the MeetingType is not defined or 
        /// false if the MeetingType is not OnLine meeting
        /// </returns>
        public bool IsOnLineMeeting()
        {
            int? meetingTypeId = this.MeetingTypeId();

            return (meetingTypeId.HasValue) ? (meetingTypeId == Dal.MeetingType.Indexes.OnLineReview) : false;
        }
        /// <summary>
        /// The session's client abbreviation.
        /// </summary>
        /// <returns>Session's client abbreviation</returns>
        public string ClientAbbreviation()
        {
            return this?.MeetingSession?.ClientMeeting?.Client?.ClientAbrv;
        }
        /// <summary>
        /// Return an enumeration of assigned reviewers.
        /// </summary>
        /// <returns>Enumeration of assigned reviewers</returns>
        public ICollection<PanelUserAssignment> AssignedReviewers()
        {
            return this.PanelUserAssignments.Where(x => x.ClientParticipantType.ReviewerFlag).ToList();
        }
        /// <summary>
        /// Determines whether [is reviewer assigned] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is reviewer assigned] [the specified user identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsReviewerAssigned(int userId)
        {
            return this.PanelUserAssignments.Count(x => x.ClientParticipantType.ReviewerFlag && x.UserId == userId) > 0;
        }
        /// <summary>
        /// Hotels the identifier.
        /// </summary>
        /// <returns></returns>
        public int? HotelId()
        {
            int? hotelId = this.MeetingSession.ClientMeeting?.HotelId;
            return hotelId;
        }
        /// <summary>
        /// Retrieves the hotel name where the session panel will be held.
        /// </summary>
        /// <returns>Name of hotel where the session panel will be held.</returns>
        public string HotelName()
        {
            string hotelName = this.MeetingSession.ClientMeeting?.Hotel?.HotelName;
            return hotelName ?? string.Empty;
        }
        /// <summary>
        /// Tests if the target date is within the SessionPanel date range
        /// </summary>
        /// <param name="target">Target date to test</param>
        /// <returns>
        /// True if the target date has a value and target date is greater than or equal to the 
        /// range lower end and target date is less than or equal to the upper end; false otherwise
        /// </returns>
        public bool WithinSessionPanelDates(DateTime? target)
        {
            //
            // make sure everything has a value
            // then check that it is within the session's dates.
            //
            return (target.HasValue) && (this.StartDate.HasValue) && (this.EndDate.HasValue) &&
                    (this.StartDate <= target) && (target <= this.EndDate);
        }
        /// <summary>
        /// Withins the session panel allowed dates.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public bool WithinSessionPanelAllowedDates(DateTime? target)
        {
            return (target.HasValue) && (this.StartDate.HasValue) && (this.EndDate.HasValue) &&
                    (this.StartDate <= ((DateTime)target).AddDays(MeetingAttendanceBufferDays)) && (target <= ((DateTime)this.EndDate).AddDays(MeetingAttendanceBufferDays));
        }
        /// <summary>
        /// Determines if scoring has been set up for the panel.
        /// </summary>
        /// <returns></returns>
        public bool IsScoringSetup()
        {
            bool result = false;
            //
            // There are two conditions that determine if scoring is set up. 
            //  - First if an Application's ProgramMechanism does not have any templates.  It is negated since we 
            //    are testing for an indication that will indicate that it is not set up.
            //
            result = !(this.PanelApplications.Any(x => x.Application.ProgramMechanism.MechanismTemplates.Count() == 0));
            //
            // So if they all have templates then we check to see if the MechanismTemplate has scoring set up.
            //
            if (result)
            {
                result = !this.PanelApplications.
                    //
                    //  For each of the panel applications we get their ProgramMechanism's MechanismTemplates With a Asynchronous ReviewStage
                    //
                    SelectMany(x => x.Application.ProgramMechanism.MechanismTemplates.Where(y => y.ReviewStageId == ReviewStage.Asynchronous)).
                    //
                    // And we finally check if any of these do not have template elements.
                    //
                    Any(x => x.MechanismTemplateElements.Count() == 0);
                //
                // Now we test if any of the templates have scorings
                //
                result = TestForScorings(result);
            }
            return result;
        }
        /// <summary>
        /// Third step in setting up scoring
        /// </summary>
        /// <param name="result">Previous result in test</param>
        /// <returns></returns>
        internal bool TestForScorings(bool result)
        {
            if (result)
            {
                IEnumerable<MechanismTemplateElement> enumeration = this.PanelApplications.
                   //
                   //  For each of the panel applications we get their ProgramMechanism's MechanismTemplates With a Asynchronous ReviewStage
                   //
                   SelectMany(x => x.Application.ProgramMechanism.MechanismTemplates.Where(y => y.ReviewStageId == ReviewStage.Asynchronous)).
                   //
                   // And then we determine if there are any MechanismTemplateElements that have the ScoreFlag set but do not have any MechanismTemplateElementScorings.
                   //
                   SelectMany(x => x.MechanismTemplateElements.Where(y => y.ScoreFlag & (y.MechanismTemplateElementScorings.Count() == 0)));
                //
                // And now we just test that there are not any
                //
                result = enumeration.Count() == 0;
            }
            return result;
        }
        /// <summary>
        /// Indicates if the SessionPanel is an Online meeting.
        /// </summary>
        /// <returns></returns>
        public bool IsOnLineReview()
        {
            return Dal.MeetingType.IsOnline(this.MeetingTypeId());
        }

        public int ParticipationMethodId()
        {
            return Dal.MeetingType.MeetingTypeToParticipationType(this.MeetingTypeId() ?? 1);
        }
    }
}
                
