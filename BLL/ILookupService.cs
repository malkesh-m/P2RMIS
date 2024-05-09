using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.MeetingManagement;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// The LookupService provides services to return collections of model
    /// data from entity objects intended for population of drop downs or 
    /// other fixed data sets.
    /// </summary>
    public interface ILookupService
    {
        /// <summary>
        /// Retrieves vales for 'Gender' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListGender();
        /// <summary>
        /// Retrieves vales for 'Ethnicity' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListEthnicity();

        /// <summary>
        /// Retrieves vales for Contract status drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListContractStatus();
        /// <summary>
        /// Retrieves vales for 'Prefix' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListPrefix();
        /// <summary>
        /// Retrieves vales for 'PhoneType' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListPhoneType();
        /// <summary>
        /// Retrieves vales for 'State' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListStateByName();
        /// <summary>
        /// Retrieves vales for 'Country' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListCountryByName();
        /// <summary>
        /// List US and Canada as countries
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListCountryUsCanada();
        /// <summary>
        /// Retrieves vales for 'MilitaryRank' drop downs
        /// </summary>
        /// <param name="parameterName">description of parameter </param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IListEntry> ListMilitaryService();
        /// <summary>
        /// Retrieves vales for 'Degree' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListDegree();
        /// <summary>
        /// Retrieves vales for 'ProfileType' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListProfileType();
        /// <summary>
        /// Retrieves vales for 'Military Rank' drop downs
        /// </summary>
        /// <param name="service">Service to list ranks</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListMilitaryRanks(string service);
        /// <summary>
        /// Retrieves vales for 'MilitaryStatusType' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListMilitaryStatusType();
        /// <summary>
        /// Retrieves vales for 'AddressType' drop downs for organizational or personal
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListOrganizationalPersonalAddressType();
        /// <summary>
        /// Retrieves vales for 'AlternateContactTypes' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListAlternateContactType();
        /// <summary>
        /// Retrieves values for the 'Recovery Question' drop downs
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListRecoveryQuestions();
        /// <summary>
        /// Retrieves(constructs) values for the 'De-activate Account' drop down on the
        /// manage account modal.
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListDeActivateAccount();
        /// <summary>
        /// Retrieves SystemRoles for the specified profile type.  
        /// </summary>
        /// <param name="targetProfileTypeId">Target ProfileType entity identifier</param>
        /// <param name="targetSystemPriorityOrder">Target user's role priority order.</param>
        /// <param name="userProfileTypeId">Current user ProfileType entity identifier</param>
        /// <param name="userRoleOrder">User role hierarchy order value</param>
        /// <returns>Container of IListEntry models; where model contains: WhatWasLookupRole identifier, WhatWasLookupRole value</returns>
        Container<IListEntry> ListProfileTypesRoles(int? targetProfileTypeId, int? targetSystemPriorityOrder, int userProfileTypeId, int? userRoleOrder);
        /// <summary>
        /// Retrieves values for the AcademicRank drop down list
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListAcademicRank();
        /// <summary>
        /// Retrieves values for the AcademicRankAbbreviation drop down list
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListAcademicRankAbbreviation();
        /// <summary>
        /// Retrieves values for the ProfessionalAffiliation drop down list
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListProfessionalAffiliation();
        /// <summary>
        /// Retrieves vales for "Participant Type" drop down on the Reviewer Recruitment page.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListParticipantType(int clientId);
        /// <summary>
        /// Retrieves vales for "Participant Role" drop down on the Reviewer Recruitment page.
        /// Only "Participant Types" that are marked as active (ActiveFlag == TRUE) are returned.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListParticipantRole(int clientId);
        /// <summary>
        /// Retrieves vales for "Participant Role" drop down on the Reviewer Recruitment page.
        /// Only "Participant Types" that are marked as active (ActiveFlag == TRUE) are returned.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListParticipantRoleAbbreviation(int clientId);
        /// <summary>
        /// Retrieves vales for "Program" drop down on the Reviewer Recruitment page.
        /// All programs (open or closed) are returned.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListDistinctPanelForProgramYear(int programYearId);
        /// <summary>
        /// Retrieves vales for "Program" drop down on the Reviewer Recruitment page.
        /// All programs (open or closed) are returned.
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListDistinctPanelForProgram(int clientProgramId);
        /// <summary>
        /// Retrieves vales for the CommunicationMethod drop down
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListCommunicationMethod();
        /// <summary>
        /// Retrieves vales for the ParticipationMethods
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListParticipationMethods();
        /// <summary>
        /// Retrieves vales for the ParticipationMethods
        /// </summary>
        /// <returns>Container of IListEntry models</returns>
        Container<ILogicalListEntry> ListParticipationLevels();
        /// <summary>
        /// Retrieves values for the Workflow Step dropdown
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow entity identifier</param>
        /// <returns>Container of IListEntry models</returns>
        Container<IListEntry> ListWorkflowSteps(int applicationWorkflowId);
        /// <summary>
        /// Lists the travel modes.
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListTravelModes();
        /// <summary>
        /// Lists the travel modes with details.
        /// </summary>
        /// <returns></returns>
        Container<TravelModeModel> ListTravelModesWithDetails();
        /// <summary>
        /// Generate a list of Award Types.  
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListDescription models to list AwardTypes for the specified client</returns>
        Container<IListDescription> ListAwardTypes(int clientId);
        /// <summary>
        /// Generate a list of child award types.  
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IListEntry models to list AwardTypes for the specified client</returns>
        Container<IListDescription> ListChildAwardTypes(int clientId);
        /// <summary>
        /// Lists the award types by client, filtering out existing awards with passed in programYearId and receiptCycle.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns>
        /// Container of IListDescription models to list AwardTypes for the specified client, minus existing awards in specified programyear/cycle
        /// </returns>
        Container<IListDescription> ListAwardTypesByClientWithCycleFilter(int programYearId, int cycle);
        /// <summary>
        /// List the ScoringTemplates for the specified client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListScoringTemplates(int clientId);
        /// <summary>
        /// List the Evaluation Criteria for the specified client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListClientEvaluationCriteria(int clientId);
        Container<IGenericDescriptionList<int, string, string>> ListClientEvaluationCriteria2(int clientId);
        /// <summary>
        /// List the Meeting Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListMeetingTypes();
        /// <summary>
        /// List the Hotels
        /// </summary>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListHotels();
        /// <summary>
        /// List the Client abbreviation
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListClientAbbreviation(int clientId);
        /// <summary>
        /// List the sessions for a specific meeting.
        /// </summary>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListMeetingSessions(int clientMeetingId);
        /// <summary>
        /// List the sessions for a specific meeting.
        /// </summary>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        /// <param name="programYearId">ClientMeeting entity identifier</param>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListMeetingSessions(int clientMeetingId, int programYearId);
        /// <summary>
        /// Lists the session panels.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <returns></returns>
        Container<IListEntry> ListSessionPanels(int meetingSessionId, int? programYearId);
        /// <summary>
        /// Lists the panels by meeting program.
        /// </summary>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        Container<IListEntry> ListPanelsByMeetingProgram(int? meetingId, int? programId);
        /// <summary>
        /// Lists the sessions by meeting program.
        /// </summary>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <param name="programId">The program identifier.</param>
        /// <returns></returns>
        Container<IListEntry> ListSessionsByMeetingProgram(int? meetingId, int? programId);
        /// <summary>
        /// Checks if the values entered match a preapp.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        bool CheckForAwardPreApps(int programYearId, int cycle);
        /// <summary>
        /// List nominee affected entities
        /// </summary>
        /// <returns></returns>
        Container<IListEntry> ListNomineeAffected();
        /// <summary>
        /// List recent years
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        Container<string> ListRecentYears(int number);

        /// <summary>
        /// List the Policy Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListPolicyTypes();

        /// <summary>
        /// List the Policy Restriction Types
        /// </summary>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListPolicyRestrictionTypes();

        /// <summary>
        /// List the Weekdays
        /// </summary>
        /// <returns>Container of IList objects</returns>
        Container<IListEntry> ListWeekDays();
    }
}
