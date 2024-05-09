using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Common services available across applications.
    /// </summary>
    public partial class CriteriaService : ServerBase,  ICriteriaService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public CriteriaService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion


        #region New services provided (these use the new data structure)
        /// <summary>
        /// Retrieves the open programs for a single client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of open programs</returns>
        public Container<IClientProgramModel> GetOpenClientPrograms(int clientId)
        {
            List<int> clientIds = new List<int>(1);
            clientIds.Add(clientId);
            return GetOpenClientPrograms(clientIds);
        }
        /// <summary>
        /// Gets open programs for a set of clientIds
        /// </summary>
        /// <param name="clientIds">List of client id's programs to retrieve</param>
        /// <returns>Container of open programs</returns>
        public Container<IClientProgramModel> GetOpenClientPrograms(List<int> clientIds)
        {
            clientIds.ForEach(
                x => ValidateInt(x, "CriteriaService.GetOpenClientPrograms", "clientIds"));
            var result = new Container<IClientProgramModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var clientPrograms = UnitOfWork.ClientProgramRepository.Get(x => clientIds.Contains(x.ClientId));
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = FilterOpenPrograms(clientPrograms);
            return result;
        }
        /// <summary>
        /// Gets the open program years for a specified program
        /// </summary>
        /// <param name="clientProgramId">Identifier for a program</param>
        /// <returns>Container of open program years</returns>
        public Container<IProgramYearModel> GetOpenProgramYears(int clientProgramId)
        {
            ValidateInt(clientProgramId, "CriteriaService.GetOpenProgramYears", "clientProgramId");
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.Get(x => x.ClientProgramId == clientProgramId);
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = FilterOpenProgramYears(programYears);
            return result;
        }

        /// <summary>
        /// Gets the open program years
        /// </summary>
        /// <returns>Container of open program years</returns>
        public Container<IProgramYearModel> GetAllProgramYears()
        {
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.Get();
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertProgramYears(programYears);
            return result;
        }

        /// <summary>
        /// Gets the open program years for a specified program
        /// </summary>
        /// <param name="clientProgramIds">Identifier for a program</param>
        /// <returns>Container of open program years</returns>
        public Container<IProgramYearModel> GetAllProgramYears(List<int> clientProgramIds)
        {
            clientProgramIds.ForEach(
                x => ValidateInt(x, "CriteriaService.GetAllProgramYears", "clientProgramIds"));
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.Get(x => clientProgramIds.Contains(x.ClientProgramId));
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertProgramYears(programYears);
            return result;
        }
        /// <summary>
        /// Gets the open program years for a specified program
        /// </summary>
        /// <param name="clientProgramId">Identifier for a program</param>
        /// <returns>Container of open program years</returns>
        public Container<IProgramYearModel> GetAllProgramYears(int clientProgramId)
        {
            ValidateInt(clientProgramId, "CriteriaService.GetAllProgramYears", "clientProgramId");
            List<int> clientProgramIds = new List<int> { clientProgramId };
            return GetAllProgramYears(clientProgramIds);
        }

        /// <summary>
        /// Gets the open program years where the user is an SRO
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        public Container<IProgramYearModel> GetAllProgramYearsByUserId(int userId)
        {
            string name = FullName(nameof(CriteriaService), nameof(GetAllProgramYears));
            
            ValidateInt(userId, name, nameof(userId));
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.Get();
            //
            // Now we filter it to only those years that the user is an SRO on any SessionPanel
            //
            List<ProgramYear> filteredListOfClientProgramEntityIds = FilterFiscalYearsBySROPanels(userId);
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertProgramYears(filteredListOfClientProgramEntityIds);

            return result;
        }

        /// <summary>
        /// Gets the open program years for a specified program where the user is an SRO
        /// </summary>
        /// <param name="clientProgramIds"></param>
        /// <param name="userId">User entity identifier</param>
        /// <returns></returns>
        public Container<IProgramYearModel> GetAllProgramYears(List<int> clientProgramIds, int userId)
        {
            string name = FullName(nameof(CriteriaService), nameof(GetAllProgramYears));
            ValidateCollection(clientProgramIds, name, nameof(clientProgramIds));
            ValidateInt(userId, name, nameof(userId));
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.Get(x => clientProgramIds.Contains(x.ClientProgramId));
            //
            // Now we filter it to only those years that the user is an SRO on any SessionPanel
            //
            List<ProgramYear> filteredListOfClientProgramEntityIds = FilterFiscalYearsBySROPanels(clientProgramIds, userId);
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertProgramYears(filteredListOfClientProgramEntityIds);

            return result;
        }

        /// <summary>
        /// get all fiscal years
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Container<IProgramYearModel> GetAllProgramYearsForPanelBadges(int userId)
        {
            string name = FullName(nameof(CriteriaService), nameof(GetAllProgramYears));
            ValidateInt(userId, name, nameof(userId));
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.GetAll();
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertProgramYears(programYears);

            return result;

        }
        /// <summary>
        /// get all fiscal year with retain params
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Container<IProgramYearModel> GetAllProgramYearsForPanelBadges()
        {
            string name = FullName(nameof(CriteriaService), nameof(GetAllProgramYears));
            var result = new Container<IProgramYearModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programYears = UnitOfWork.ProgramYearRepository.GetAll();
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertProgramYears(programYears);

            return result;

        }
        /// <summary>
        /// get all meeting type for selected fiscal year
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Container<IMeetingTypeModel> GetAllMeetingType(List<string> fiscalYear, int userId)
        {
            var result = new Container<IMeetingTypeModel>();
            List<int> meetingTypeIds = new List<int>();
            string name = FullName(nameof(CriteriaService), nameof(GetAllMeetingType));
            ValidateInt(userId, name, nameof(userId));
            var clientMeeting = UnitOfWork.ReportViewerRepository.GetMeetings(fiscalYear);
            foreach (var item in clientMeeting)
            {
                meetingTypeIds.Add(item.MeetingTypeId);
            }

        meetingTypeIds = meetingTypeIds.Distinct().ToList();
        var meetingType = UnitOfWork.MeetingTypeRepository.Select().Where(x => meetingTypeIds.Contains(x.MeetingTypeId));
        result.ModelList = ConvertMeetingType(meetingType);
            return result;
        }
    /// <summary>
    /// get all meeting type for retains
    /// </summary>
    /// <param name="fiscalYear"></param>
    /// <returns></returns>
    public Container<IMeetingTypeModel> GetAllMeetingType(List<string> fiscalYear)
    {
        var result = new Container<IMeetingTypeModel>();
        List<int> meetingTypeIds = new List<int>();
        string name = FullName(nameof(CriteriaService), nameof(GetAllMeetingType));
        var clientMeeting =  UnitOfWork.ReportViewerRepository.GetMeetings(fiscalYear);
        foreach (var item in clientMeeting)
        {
            meetingTypeIds.Add(item.MeetingTypeId);
        }
            meetingTypeIds = meetingTypeIds.Distinct().ToList();
        var meetingType = UnitOfWork.MeetingTypeRepository.Select().Where(x => meetingTypeIds.Contains(x.MeetingTypeId));
        result.ModelList = ConvertMeetingType(meetingType);
        return result;
    }

    /// <summary>
        /// Gets all client programs for a list of clients.
        /// </summary>
        /// <param name="clientIds">The client ids.</param>
        /// <returns>Container of client program web models</returns>
        public Container<IClientProgramModel> GetAllClientPrograms(List<int> clientIds)
        {
            clientIds.ForEach(
                x => ValidateInt(x, "CriteriaService.GetOpenClientPrograms", "clientIds"));
            var result = new Container<IClientProgramModel>();
            //
            // Retrieve list of client programs for the specified client
            //
            var clientPrograms = UnitOfWork.ClientProgramRepository.Get(x => clientIds.Contains(x.ClientId));
            //
            // Filters open programs and returns a model list
            //
            result.ModelList = ConvertPrograms(clientPrograms);
            return result;
        }
        public Container<IClientProgramModel> GetAllClientPrograms(List<int> clientProgramIds, int userId)
        {
            string name = FullName(nameof(CriteriaService), nameof(GetAllClientPrograms));
            ValidateCollection(clientProgramIds, name, nameof(clientProgramIds));
            ValidateInt(userId, name, nameof(userId));

            var result = new Container<IClientProgramModel>();
            List<int> filteredListOfClientProgramEntityIds = FilterClientsBySROPanels(clientProgramIds, userId);

            //
            // think this could be reversed to improve performance
            //
            var clientPrograms = UnitOfWork.ClientProgramRepository.Get(x => filteredListOfClientProgramEntityIds.Contains(x.ClientProgramId));
            //
            // And now we populate the model
            //
            result.ModelList = ConvertPrograms(clientPrograms);
            return result;
        }
        /// <summary>
        /// Retrieves the receipt cycles existing for a given program year
        /// </summary>
        /// <param name="programYearId">Identifier for a program year</param>
        /// <returns>Container of receipt cycles</returns>
        public Container<int> GetProgramYearCycles(int programYearId)
        {
            ValidateInt(programYearId, "CriteriaService.GetProgramYearCycles", "programYearId");
            var result = new Container<int>();
            //
            // Retrieve list of client programs for the specified client
            //
            var mechanisms = UnitOfWork.ProgramMechanismRepository.Get(x => x.ProgramYearId == programYearId);
            result.ModelList = mechanisms.Where(x => x.ReceiptCycle != null).Select(x => (int)x.ReceiptCycle).Distinct().OrderBy(x => x);
            return result;
        }
        /// <summary>
        /// Retrieves the receipt cycles existing for a list of program years
        /// </summary>
        /// <param name="clientProgramIds">List of Identifiers for a client program</param>
        /// <param name="years">Fiscal year list</param>
        /// <returns>Container of receipt cycles</returns>
        public Container<int> GetProgramYearCycles(List<int> clientProgramIds, List<string> years)
        {
            clientProgramIds.ForEach(
                x => ValidateInt(x, "CriteriaService.GetProgramYearCycles", "clientProgramIds"));
            var result = new Container<int>();
            //
            // Retrieve list of client programs for the specified client
            //
            var programMechanisms = UnitOfWork.ProgramMechanismRepository.Get(x => clientProgramIds.Contains(x.ProgramYear.ClientProgramId) && years.Contains(x.ProgramYear.Year));
            result.ModelList = programMechanisms.Select(x => x.ReceiptCycle ?? 0);
            return result;
        }
        /// <summary>
        /// Retrieves the session panels for a given program year and cycle
        /// </summary>
        /// <param name="programYearId">Identifier for a program year</param>
        /// <returns>Container of session panels</returns>
        public Container<ISessionPanelModel> GetSessionPanels(int programYearId)
        {
            ValidateInt(programYearId, "CriteriaService.GetSessionPanels", "programYearId");
            var result = new Container<ISessionPanelModel>();
            //
            // Retrieve a list of mechanisms for the specified program year and cycle
            //
            var sessionPanels = UnitOfWork.SessionPanelRepository.Get(x => x.ProgramPanels.Any(y => y.ProgramYearId == programYearId));
        if (sessionPanels != null)
            {
                result.ModelList = sessionPanels.Select(x => new SessionPanelModel(x.SessionPanelId, x.PanelAbbreviation, x.PanelName)).OrderBy(x => x.PanelAbbreviation);
            }
            return result;
        }
        /// <summary>
        /// Retrieves the session panels for a given list of program years
        /// </summary>
        /// <param name="clientProgramIds">Identifier for a client program</param>
        /// <param name="years">List of fiscal years</param>
        /// <returns>Container of session panels</returns>
    public Container<ISessionPanelModel> GetSessionPanels(List<int> clientProgramIds, List<string> years)
        {
            clientProgramIds.ForEach(
                x => ValidateInt(x, "CriteriaService.GetSessionPanels", "clientProgramIds"));
            var result = new Container<ISessionPanelModel>();
            //
            // Retrieve a list of mechanisms for the specified program year and cycle
            //
            // Creating a ProgramPanelRepository may provide better performance
            var programYears = UnitOfWork.ProgramYearRepository.Select().Where(x => clientProgramIds.Contains(x.ClientProgramId) && years.Contains(x.Year));
        if (programYears != null)
            {
            result.ModelList = programYears.SelectMany(x => x.ProgramPanels).Select(x => new SessionPanelModel()
            {
                    SessionPanelId = x.SessionPanelId,
                    PanelAbbreviation = x.SessionPanel.PanelAbbreviation,
                    PanelName = x.SessionPanel.PanelName,
                    ProgramAbbreviation = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                Year = x.ProgramYear.Year
            });
            }
            return result;
        }
        /// <summary>
        /// Gets the awards.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>Container of Award information</returns>
        public Container<IAwardModel> GetAwards(int programYearId, int? cycle, int? panelId)
        {
            //
            // Set up default return
            // 
            Container<IAwardModel> container = new Container<IAwardModel>();

            if (IsGetAwardsParametersValid(programYearId, cycle, panelId))
            {
                //
                // Call the DL and retrieve the list of awards for the specified selections
                //
                var results = UnitOfWork.ProgramMechanismRepository
                .Get(x => x.ProgramYearId == programYearId &&
                    (cycle == null || x.ReceiptCycle == cycle) &&
                        (panelId == null || x.Applications.Any(y => y.PanelApplications.Any(z => z.SessionPanelId == panelId))));
                //
                // Then populate the container & return
                //
                container.ModelList = results.Select(x => new AwardModel
                {
                    AwardTypeId = x.ClientAwardTypeId,
                    AwardAbbreviation = x.ClientAwardType.AwardAbbreviation,
                    AwardDescription = x.ClientAwardType.AwardDescription
            });
            }

            return container;
        }
        /// <summary>
        /// Retrieves the session panels for a given list of program years where the user is an SRO.
        /// </summary>
        /// <param name="clientProgramIds">Identifier for a client program</param>
        /// <param name="years">List of fiscal years</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of session panels</returns>
        public Container<ISessionPanelModel> GetSessionPanels(List<int> clientProgramIds, List<string> years, int userId)
        {
            string name = FullName(nameof(CriteriaService), nameof(GetSessionPanels));
            ValidateCollection(clientProgramIds, name, nameof(clientProgramIds));
            ValidateCollection(years, name, nameof(years));
            ValidateInt(userId, name, nameof(userId));

            var result = new Container<ISessionPanelModel>();
            //
            // Retrieve a list of mechanisms for the specified program year and cycle
            //
            var programYears = UnitOfWork.ProgramYearRepository.Select().Where(x => clientProgramIds.Contains(x.ClientProgramId) && years.Contains(x.Year));
            result.ModelList = programYears.SelectMany(x => x.ProgramPanels).
                //
                // Filter the ProgramPanels where the user actually has an SRO assignment
                //
            Where(x => (x.SessionPanel.PanelUserAssignments.Any(z => z.UserId == userId && z.ClientParticipantType.ParticipantTypeAbbreviation == ClientParticipantType.SRO))).
                //
                // Then we pull out the data bits we desire
                //
            Select(x => new SessionPanelModel()
            {
                    SessionPanelId = x.SessionPanelId,
                    PanelAbbreviation = x.SessionPanel.PanelAbbreviation,
                    PanelName = x.SessionPanel.PanelName,
                    ProgramAbbreviation = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                Year = x.ProgramYear.Year
            });
            //
            // and we return them
            //
            return result;
        }
        /// <summary>
        /// Filter program year and return a list of program years
        /// </summary>
        /// <param name="programYears">Collections of ProgramYears</param>
        /// <returns>List of ProgramYearModel web models</returns>
        internal List<IProgramYearModel> FilterOpenProgramYears(IEnumerable<ProgramYear> programYears)
        {
            return programYears.Where(x => x.ProgramYearId != null).OrderByDescending(x => x.Year).Select(programYear => new ProgramYearModel(programYear.ProgramYearId, programYear.ClientProgramId, programYear.Year)).Cast<IProgramYearModel>().Distinct().ToList();
        }

        /// <summary>
        /// Filter open programs and return a list of open programs
        /// </summary>
        /// <param name="clientPrograms">Collections of ClientPrograms</param>
        /// <returns>List of ClientProgramModel web models</returns>
        internal List<IClientProgramModel> FilterOpenPrograms(IEnumerable<ClientProgram> clientPrograms)
        {
            var list = new List<IClientProgramModel>();
            foreach (var openClientProgram in clientPrograms.Where(x => x.ProgramYears.Any(y => y.DateClosed == null)).OrderBy(x => x.ProgramAbbreviation))
            {
                list.Add(new ClientProgramModel(openClientProgram.ClientProgramId, openClientProgram.ProgramAbbreviation,
                    openClientProgram.ProgramDescription, openClientProgram.ProgramYears.ToList().ConvertAll(x => x.Year)));
            }
            return list;
        }

        /// <summary>
        /// Convert program year DL and return a list of program years as WebModel
        /// </summary>
        /// <param name="programYears">Collections of ProgramYears</param>
        /// <returns>List of ProgramYearModel web models</returns>
        internal List<IProgramYearModel> ConvertProgramYears(IEnumerable<ProgramYear> programYears)
        {
            return programYears.Select(programYear => new ProgramYearModel(programYear.ProgramYearId, programYear.ClientProgramId, programYear.Year)).Cast<IProgramYearModel>().ToList();
        }

        /// <summary>
        /// Convert client program DL and return a list of client programs as WebModel
        /// </summary>
        /// <param name="clientPrograms">Collections of ClientPrograms</param>
        /// <returns>List of ClientProgramModel web models</returns>
        internal List<IClientProgramModel> ConvertPrograms(IEnumerable<ClientProgram> clientPrograms)
        {
            var list = new List<IClientProgramModel>();
            foreach (var openClientProgram in clientPrograms)
            {
                list.Add(new ClientProgramModel(openClientProgram.ClientProgramId, openClientProgram.ProgramAbbreviation,
                    openClientProgram.ProgramDescription));
            }
            return list;
        }
    /// <summary>
    /// Convert meeting type DL and return a list of meeting type as webModel
    /// </summary>
    /// <param name="meetingType"></param>
    /// <returns></returns>

    internal List<IMeetingTypeModel> ConvertMeetingType(IEnumerable<MeetingType> meetingType)
    {
        var list = new List<IMeetingTypeModel>();
        foreach (var meeting in meetingType)
        {
            list.Add(new MeetingTypeModel(meeting.MeetingTypeId, meeting.MeetingTypeName));

        }

        return list;
    }
    /// <summary>
    /// convert client meeting type DL and return a list of client meeting as webModel
    /// </summary>
    /// <param name="clientMeetings"></param>
    /// <param name="fiscalYear"></param>
    /// <returns></returns>
    internal List<IClientMeetingModel> ConvertMeetings(IQueryable<ClientMeeting> clientMeetings, List<string> fiscalYear)
    {
            var list = new List<IClientMeetingModel>();
            var meetingsFiltered = clientMeetings.Where(x => x.ProgramMeetings.Any(y => fiscalYear.Contains(y.ProgramYear.Year)));
            foreach (var meeting in meetingsFiltered)
            {
                var programs = meeting.ProgramMeetings;
                foreach (var program in programs)
                {
                    list.Add(new ClientMeetingModel
                    {
                        ClientMeetingId = program.ClientMeetingId,
                        MeetingDescription = program.ClientMeeting.MeetingDescription,
                        MeetingTypeId = program.ClientMeeting.MeetingTypeId

                    });
                }
            }
            list = (from li in list select li).GroupBy(x => new { x.ClientMeetingId, x.MeetingDescription })
                .Select(y => y.FirstOrDefault())
                .ToList();
            return list;
        }

        #endregion

        #region Helper Methods
        /// <summary>
        /// Validates the parameters for GetAwards:
        /// - program is not null or empty
        /// - fiscal year is not null or empty
        /// - cycle is null or if not null greater than 0
        /// - panel Id is null or if not null greater than 0
        /// </summary>
        /// <param name="programYear">Program year identifier</param>
        /// <param name="cycle">the cycle</param>
        /// <param name="panelId">the panel id</param>
        /// <returns>True if parameters valid; false otherwise</returns>
        private bool IsGetAwardsParametersValid(int programYearId, int? cycle, int? panelId)
        {
        return
                ((programYearId > 0) &&
                BllHelper.IdOk(cycle) &&
                BllHelper.IdOk(panelId)
                );
        }
        /// <summary>
        /// Filters a list of ClientProgram to a contain only ClientPrograms where the user is a SRO
        /// </summary>
        /// <param name="clientIds">List of Client entity ids</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>List of ClientProgram entity ids for which the user is assigned</returns>
        protected virtual List<int> FilterClientsBySROPanels(List<int> clientIds, int userId)
        {

            IEnumerable<ClientProgram> clientProgramsWhereUserIsSro = UnitOfWork.ProgramYearRepository.Select()
                .Where(x => x.ProgramPanels.SelectMany(y => y.SessionPanel.PanelUserAssignments).Any(z => z.UserId == userId && z.ClientParticipantType.ParticipantTypeAbbreviation == ClientParticipantType.SRO)).Select(x => x.ClientProgram);
            IEnumerable<ClientProgram> usersSelections = UnitOfWork.ClientProgramRepository.Get(x => clientIds.Contains(x.ClientId));
            return clientProgramsWhereUserIsSro.Intersect(usersSelections).Select(x => x.ClientProgramId).Distinct().ToList();
        }

        /// <summary>
        /// Filters a list of ProgramYears to a contain only ProgramsYears where the user is an SRO
        /// </summary>
        /// <param name="clientProgramIds">List of clientProgramIds</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>List of ClientProgram entity ids for which the user is assigned</returns>
        protected virtual List<ProgramYear> FilterFiscalYearsBySROPanels(int userId)
        {

            IEnumerable<ProgramYear> programYearsWhereUserIsSro = UnitOfWork.ProgramYearRepository.Select()
                .Where(x => x.ProgramPanels.SelectMany(y => y.SessionPanel.PanelUserAssignments).Any(z => z.UserId == userId && z.ClientParticipantType.ParticipantTypeAbbreviation == ClientParticipantType.SRO));
            IEnumerable<ProgramYear> usersSelections = UnitOfWork.ProgramYearRepository.Get();

            return programYearsWhereUserIsSro.Intersect(usersSelections).Select(x => x).Distinct().ToList();
        }

        /// <summary>
        /// Filters a list of ProgramYears to a contain only ProgramsYears where the user is an SRO
        /// </summary>
        /// <param name="clientProgramIds">List of clientProgramIds</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>List of ClientProgram entity ids for which the user is assigned</returns>
        protected virtual List<ProgramYear> FilterFiscalYearsBySROPanels(List<int> clientProgramIds, int userId)
        {

            IEnumerable<ProgramYear> programYearsWhereUserIsSro = UnitOfWork.ProgramYearRepository.Select()
                .Where(x => x.ProgramPanels.SelectMany(y => y.SessionPanel.PanelUserAssignments).Any(z => z.UserId == userId && z.ClientParticipantType.ParticipantTypeAbbreviation == ClientParticipantType.SRO));
            IEnumerable<ProgramYear> usersSelections = UnitOfWork.ProgramYearRepository.Get(x => clientProgramIds.Contains(x.ClientProgramId));

            return programYearsWhereUserIsSro.Intersect(usersSelections).Select(x => x).Distinct().ToList();
        }
    /// <summary>
    /// get all meeting by meeting type
    /// </summary>
    /// <param name="meetingTypeIds"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Container<IClientMeetingModel> GetMeetingsByMeetingType(List<int> meetingTypeIds, List<string> fiscalYear, int userId)
    {
        string name = FullName(nameof(CriteriaService), nameof(GetMeetingsByMeetingType));
        ValidateInt(userId, name, nameof(userId));
        ValidateCollection(meetingTypeIds, name, nameof(meetingTypeIds));

        var result = new Container<IClientMeetingModel>();

        //
        // think this could be reversed to improve performance
        //
        var clientMeetings = UnitOfWork.ClientMeetingRepository.Select().Where(x => meetingTypeIds.Contains(x.MeetingTypeId));
        //
        // And now we populate the model
        //
        result.ModelList = ConvertMeetings(clientMeetings, fiscalYear);
        return result;
    }
        #endregion
}
}
