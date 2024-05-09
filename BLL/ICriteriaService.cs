using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Common services available across applications.
    /// </summary>
    public interface ICriteriaService
    {
        /// <summary>
        /// Retrieves the open programs for a single client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of open programs</returns>
        Container<IClientProgramModel> GetOpenClientPrograms(int clientId);

        /// <summary>
        /// Gets open programs for a set of clientIds
        /// </summary>
        /// <param name="clientIds">List of client id's programs to retrieve</param>
        /// <returns>Container of open programs</returns>
        Container<IClientProgramModel> GetOpenClientPrograms(List<int> clientIds);

        /// <summary>
        /// Gets the open program years for a specified program
        /// </summary>
        /// <param name="clientProgramId">Identifier for a program</param>
        /// <returns>Container of open program years</returns>
        Container<IProgramYearModel> GetOpenProgramYears(int clientProgramId);

        /// <summary>
        /// Retrieves the receipt cycles existing for a given program year
        /// </summary>
        /// <param name="programYearId">Identifier for a program year</param>
        /// <returns>Container of receipt cycles</returns>
        Container<int> GetProgramYearCycles(int programYearId);
        /// <summary>
        /// Gets all client programs.
        /// </summary>
        /// <param name="clientIds">The client ids.</param>
        /// <returns>Container of IClientProgramModel models</returns>
        Container<IClientProgramModel> GetAllClientPrograms(List<int> clientIds);
        /// <summary>
        /// Gets all client programs for a list of clients.
        /// </summary>
        /// <param name="clientProgramIds">The client ids.</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of client program web models</returns>
        Container<IClientProgramModel> GetAllClientPrograms(List<int> clientProgramIds, int userId);

        /// <summary>
        /// Gets the open program years
        /// </summary>
        /// <returns>Container of open program years</returns>
        Container<IProgramYearModel> GetAllProgramYears();

        /// <summary>
        /// Gets the open program years where the user is an SRO
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        Container<IProgramYearModel> GetAllProgramYearsByUserId(int userId);

        /// <summary>
        /// Gets all program years.
        /// </summary>
        /// <param name="clientProgramIds">The client program ids.</param>
        /// <returns></returns>
        Container<IProgramYearModel> GetAllProgramYears(List<int> clientProgramIds);
        /// <summary>
        /// Gets all program fiscal year for panel badges
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Container<IProgramYearModel> GetAllProgramYearsForPanelBadges(int userId);
        /// <summary>
        /// Gets the open program years for a specified programs
        /// </summary>
        /// <param name="clientProgramIds">ClientProgramIds</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of open program years</returns>
        Container<IProgramYearModel> GetAllProgramYears(List<int> clientProgramIds, int userId);
        /// <summary>
        /// Retrieves the receipt cycles existing for a list of program years
        /// </summary>
        /// <param name="clientProgramIds">List of Identifiers for a client program</param>
        /// <param name="years">Fiscal year list</param>
        /// <returns>Container of receipt cycles</returns>
        Container<int> GetProgramYearCycles(List<int> clientProgramIds, List<string> years );
        /// <summary>
        /// Retrieves the session panels for a given list of program years
        /// </summary>
        /// <param name="clientProgramIds">Identifier for a client program</param>
        /// <param name="years">List of fiscal years</param>
        /// <returns>Container of session panels</returns>
        Container<ISessionPanelModel> GetSessionPanels(List<int> clientProgramIds, List<string> years );
        /// <summary>
        /// Gets the session panels.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        Container<ISessionPanelModel> GetSessionPanels(int programYearId);
        /// <summary>
        /// Gets the open program years for a specified program
        /// </summary>
        /// <param name="clientProgramId">Identifier for a program</param>
        /// <returns>Container of open program years</returns>
        Container<IProgramYearModel> GetAllProgramYears(int clientProgramId);

        /// <summary>
        /// Gets the awards.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>Container of award information</returns>
        Container<IAwardModel> GetAwards(int programYearId, int? cycle, int? panelId);
        /// <summary>
        /// Retrieves the session panels for a given list of program years
        /// </summary>
        /// <param name="clientProgramIds">Identifier for a client program</param>
        /// <param name="years">List of fiscal years</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of session panels</returns>
        Container<ISessionPanelModel> GetSessionPanels(List<int> clientProgramIds, List<string> years, int userId);
        /// <summary>
        /// Retrieve all meeting type
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Container<IMeetingTypeModel> GetAllMeetingType(List<string> fiscalYear, int userId);
        /// <summary>
        /// Retrive all meeting Type
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Container<IMeetingTypeModel> GetAllMeetingType(List<string> list);
        /// <summary>
        /// get all fiscal for panel badge report with retains params
        /// </summary>
        /// <returns></returns>
        Container<IProgramYearModel> GetAllProgramYearsForPanelBadges();

        /// <summary>
        /// Wrapper method which determines which version of GetAllClientPrograms is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="list">List of client user entity identifiers</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IClientProgramModel models</returns>
        Container<IClientProgramModel> GetAllClientPrograms(List<int> list, bool isFiltered, int userId);
        /// <summary>
        /// Wrapper method which determines which version of GetAllProgramYears is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IProgramYearModel models</returns>
        Container<IProgramYearModel> GetAllProgramYears(int clientProgramId, bool isFiltered, int userId);
        /// <summary>
        /// Wrapper method which determines which version of GetSessionPanels is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of ISessionPanelModel models</returns>
        Container<ISessionPanelModel> GetSessionPanels(int programYearId, bool isFiltered, int userId);
        /// <summary>
        /// Wrapper method which determines which version of GetProgramYearCycles is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of ISessionPanelModel models</returns>
        Container<int> GetProgramYearCycles(int programYearId, bool isFiltered, int userId);

        /// <summary>
        /// Wrapper method which determines which version of GetProgramYearCycles is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="cycle">Receipt Cycle value (if any)</param>
        /// <param name="panelId">SessionPanel entity identifier (if any)</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IAwardModel representing SessionPanel entity identifiers</returns>
        Container<IAwardModel> GetAwards(int programYearId, int? cycle, int? panelId, bool isFiltered, int userId);
        /// <summary>
        /// get all meeting by meeting type
        /// </summary>
        /// <param name="meetingTypeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Container<IClientMeetingModel> GetMeetingsByMeetingType(List<int> meetingTypeId, List<string> fiscalYear, int userId);
    }
}
