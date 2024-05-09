using Sra.P2rmis.Bll.ModelBuilders.Setup;
using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.Bll.Setup.Actions;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.Dal;
using System;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Provided services for System Setup.
    /// </summary>
    public partial interface ISetupService
    {
        Container<IMeetingSetupModel> RetrieveMeetingSetupGrid(int clientId, string year, int? programYearId);
        Container<IMeetingSetupModalModel> RetrieveMeetingSetupModal(int clientId, int clientMeetingId);
        Container<IGenericActiveProgramListEntry<int, string>> RetrieveAssignModalFiscalYearList(int clientId);
        Container<IUnassignProgramModalModel> UnassignProgramModal(int clientId, int clientMeetingId);
        /// <summary>
        /// Adds meeting
        /// </summary>
        /// <param name="clientId">Client identifier</param>
        /// <param name="meetingAbbreviation">Meeting abbreviation</param>
        /// <param name="meetingDescription">Meeting description</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="meetingLocation">Meeting location</param>
        /// <param name="meetingTypeId">Meeting type identifier</param>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        ServiceState AddMeeting(int clientId, string meetingAbbreviation, string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId, int userId);
        /// <summary>
        /// Modifies meeting
        /// </summary>
        /// <param name="clientMeetingId">Client meeting identifier</param>
        /// <param name="clientId">Client identifier</param>
        /// <param name="meetingAbbreviation">Meeting abbreviation</param>
        /// <param name="meetingDescription">Meeting description</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="meetingLocation">Meeting location</param>
        /// <param name="meetingTypeId">Meeting type identifier</param>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        ServiceState ModifyMeeting(int clientMeetingId, int clientId, string clientAbbreviation, string meetingAbbreviation, string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId, int userId);
        ServiceState DeleteMeeting(int clientMeetingId, int userId);    
        /// <summary>
        /// Assigns program meetings.
        /// </summary>
        /// <param name="programYearIds">Program year identifier</param>
        /// <param name="clientMeetingId">Client meeting identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        ServiceState AssignProgramMeetings(List<int> programYearIds, int clientMeetingId, int userId);
        ServiceState UnassignProgramMeetings(List<int> ProgramMeetingIds, int userId);
    }
    /// <summary>
    /// Provides services for the Meeting functions of System Setup.
    /// </summary>
    public partial class SetupService
    {
        /// <summary>
        /// Retrieves a container to populate the Meeting Setup grid
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IMeetingSetupModel model for the given Client id</returns>
        public virtual Container<IMeetingSetupModel> RetrieveMeetingSetupGrid(int clientId, string year, int? programYearId)
        {
            ValidateInt(clientId, FullName(nameof(SetupService), nameof(RetrieveMeetingSetupGrid)), nameof(clientId));

            MeetingSetupModelBuilder builder = new MeetingSetupModelBuilder(this.UnitOfWork, clientId, year, programYearId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate the Meeting Setup modal to edit
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientMeetingId">ClientMeeting entity identifier to edit</param>
        /// <returns>Container of IMeetingSetupModalModel model for the given Client id</returns>
        public virtual Container<IMeetingSetupModalModel> RetrieveMeetingSetupModal(int clientId, int clientMeetingId)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveMeetingSetupModal));
            ValidateInt(clientId, name, nameof(clientId));
            ValidateInt(clientMeetingId, name, nameof(clientMeetingId));

            MeetingSetupModalModelBuilder builder = new MeetingSetupModalModelBuilder(this.UnitOfWork, clientId, clientMeetingId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container of a client's Fiscal Year values.  Each value has an indicator to indicate
        /// if the fiscal year is active.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IMeetingSetupModalModel model for the given Client id</returns>
        public virtual Container<IGenericActiveProgramListEntry<int, string>> RetrieveAssignModalFiscalYearList(int clientId)
        {
            ValidateInt(clientId, FullName(nameof(SetupService), nameof(RetrieveAssignModalFiscalYearList)), nameof(clientId));

            AssignProgramFiscalYearModelBuilder builder = new AssignProgramFiscalYearModelBuilder(this.UnitOfWork, clientId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container of a meeting's assigned programs. 
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        /// <returns>Container of IMeetingSetupModalModel model for the given Client id</returns>
        public virtual Container<IUnassignProgramModalModel> UnassignProgramModal(int clientId, int clientMeetingId)
        {
            string name = FullName(nameof(SetupService), nameof(UnassignProgramModal));
            ValidateInt(clientId, name, nameof(clientId));
            ValidateInt(clientMeetingId, name, nameof(clientMeetingId));

            UnassignProgramModalModelBuilder builder = new UnassignProgramModalModelBuilder(this.UnitOfWork, clientId, clientMeetingId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Service method to add an new ClientMeeting.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="meetingAbbreviation">Meeting abbreviation</param>
        /// <param name="meetingDescription">Meeting description</param>
        /// <param name="startDate">Meeting start date & time</param>
        /// <param name="endDate">Meeting end date & time</param>
        /// <param name="meetingTypeId">Meeting type</param>
        /// <param name="meetingLocation">Meeting location</param>
        /// <param name="hotelId">Hotel where meeting will be held</param>
        /// <param name="userId">User entity identifier off user creating meeting</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public virtual ServiceState AddMeeting(int clientId, string meetingAbbreviation, string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddMeeting));
            ValidateInteger(clientId, name, nameof(clientId));
            ValidateString(meetingAbbreviation, name, nameof(meetingAbbreviation));
            ValidateString(meetingDescription, name, nameof(meetingDescription));
            ValidateString(meetingLocation, name, nameof(meetingLocation));
            ValidateInteger(meetingTypeId, name, nameof(meetingTypeId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Meeting block &  operation and add it.
            //
            MeetingBlock block = new MeetingBlock(clientId, meetingAbbreviation, meetingDescription, startDate, endDate, meetingLocation, meetingTypeId, hotelId);

            block.ConfigureAdd();

            return DoCrud(block, null, CrudAction.Add, 0, true, userId);
        }
        /// <summary>
        /// Modifies the meeting.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientAbbreviation">The client abbreviation.</param>
        /// <param name="meetingAbbreviation">The meeting abbreviation.</param>
        /// <param name="meetingDescription">The meeting description.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="meetingTypeId">The meeting type identifier.</param>
        /// <param name="meetingLocation">The meeting location.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual ServiceState ModifyMeeting(int clientMeetingId, int clientId, string clientAbbreviation,
            string meetingAbbreviation, string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddMeeting));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateInteger(clientId, name, nameof(clientId));
            ValidateString(clientAbbreviation, name, nameof(clientAbbreviation));
            ValidateString(meetingAbbreviation, name, nameof(meetingAbbreviation));
            ValidateString(meetingDescription, name, nameof(meetingDescription));
            ValidateString(meetingLocation, name, nameof(meetingLocation));
            ValidateInteger(meetingTypeId, name, nameof(meetingTypeId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Meeting block &  operation and add it.
            //
            MeetingBlock block = new MeetingBlock(clientMeetingId, clientId, clientAbbreviation, meetingAbbreviation, meetingDescription, startDate, endDate, meetingLocation, meetingTypeId, hotelId);

            block.ConfigureModify();

            return DoCrud(block, null, CrudAction.Modify, clientMeetingId, true, userId);
        }
        /// <summary>
        /// Deletes the meeting.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState DeleteMeeting(int clientMeetingId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteMeeting));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            MeetingBlock block = new MeetingBlock(clientMeetingId);
            block.ConfigureDelete();

            return DoCrud(block, null, CrudAction.Delete, clientMeetingId, true, userId);
        }
        /// <summary>
        /// Assigns program meeting.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private ServiceState AssignProgramMeeting(int programYearId, int clientMeetingId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AssignProgramMeeting));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Meeting block &  operation and add it.
            //
            ProgramMeetingBlock block = new ProgramMeetingBlock(programYearId, clientMeetingId);
            block.ConfigureAdd();
            return DoCrud(block, null, CrudAction.Add, 0, false, userId);
        }
        /// <summary>
        /// Assigns the program meeting.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState AssignProgramMeetings(List<int> programYearIds, int clientMeetingId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AssignProgramMeetings));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Meeting block &  operation and add it.
            //
            var states = new List<ServiceState>();
            foreach (var programYearId in programYearIds)
            {
                var state = AssignProgramMeeting(programYearId, clientMeetingId, userId);
                states.Add(state);
                if (!state.IsSuccessful)
                {
                    break;
                }
            }
            ServiceState theResult = ServiceState.Merge(states);
            if (theResult != null && theResult.IsSuccessful)
            {
                UnitOfWork.Save();
            }
            return theResult;
        }
        /// <summary>
        /// Unassigns the program meeting.
        /// </summary>
        /// <param name="programMeetingId">The program meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private ServiceState UnassignProgramMeeting(int programMeetingId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteProgram));
            ValidateInteger(programMeetingId, name, nameof(programMeetingId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            ProgramMeetingBlock block = new ProgramMeetingBlock(programMeetingId);
            block.ConfigureDelete();

            return DoCrud(block, null, CrudAction.Delete, programMeetingId, false, userId);
        }
        /// <summary>
        /// Unassigns the program meetings.
        /// </summary>
        /// <param name="programMeetingIds">The program meeting identifiers.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState UnassignProgramMeetings(List<int> programMeetingId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(UnassignProgramMeetings));
            ValidateInteger(userId, name, nameof(userId));

            List<ServiceState> results = new List<ServiceState>();
            foreach (int id in programMeetingId)
            {
                ServiceState result = UnassignProgramMeeting(id, userId);
                results.Add(result);
                //
                // If the delete was not successful then there is no need to make any further attempts
                //
                if (!result.IsSuccessful)
                {
                    break;
                }
            }
            //
            // Now combine the ServiceStates and save.  
            //
            ServiceState theResult = ServiceState.Merge(results);
            if (theResult != null && theResult.IsSuccessful)
            {
                UnitOfWork.Save();
            }
            return theResult;
        }

        /// <summary>
        /// Does all the heavy lifting to perform CRUD operations for ClientMeeting setup
        /// </summary>
        /// <param name="block">Parameter block containing values for operation</param>
        /// <param name="entity">ClientMeeting under edit</param>
        /// <param name="operation">CRUDAction to perform</param>
        /// <param name="entityId">ClientMeeting entity identifier of entity</param>
        /// <param name="doUpdate">Indicates if the operation should be saved when executed</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState DoCrud(MeetingBlock block, ClientMeeting entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            //
            // 1) Get the rules we need to apply
            //
            RuleEngine<ClientMeeting> rules = RuleEngineConstructors.CreateClientMeetingEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            ClientMeetingServiceAction action = new ClientMeetingServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ClientMeetingRepository, doUpdate, entityId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }

        internal virtual ServiceState DoCrud(ProgramMeetingBlock block, ProgramMeeting entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            //
            // 1) Get the rules we need to apply
            //
            RuleEngine<ProgramMeeting> rules = RuleEngineConstructors.CreateProgramMeetingEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            ProgramMeetingServiceAction action = new ProgramMeetingServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramMeetingRepository, doUpdate, entityId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }        
    }
}
