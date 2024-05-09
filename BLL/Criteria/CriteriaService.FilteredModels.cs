using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll
{
    public partial class CriteriaService
    {
        /// <summary>
        /// Wrapper method which determines which version of GetAllClientPrograms is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="list">List of client user entity identifiers</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IClientProgramModel models</returns>
        public Container<IClientProgramModel> GetAllClientPrograms(List<int> list, bool isFiltered, int userId)
        {
            return (isFiltered) ?
                GetAllClientPrograms(list, userId) :
                GetAllClientPrograms(list);
        }
        /// <summary>
        /// Wrapper method which determines which version of GetAllProgramYears is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IProgramYearModel models</returns>
        public Container<IProgramYearModel> GetAllProgramYears(int clientProgramId, bool isFiltered, int userId)
        {
            List<int> list = new List<int> { clientProgramId };

            return (isFiltered) ?
                GetAllProgramYears(list, userId) :
                GetAllProgramYears(list);
        }
        /// <summary>
        /// Wrapper method which determines which version of GetSessionPanels is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of ISessionPanelModel models</returns>
        public Container<ISessionPanelModel> GetSessionPanels(int programYearId, bool isFiltered, int userId)
        {
            Container<ISessionPanelModel> result = new Container<ISessionPanelModel>();

            if (isFiltered)
            {
                //
                // Need to massage the parameters into different types to reuse 
                // the existing method
                //
                ProgramYear programYearEntity = UnitOfWork.ProgramYearRepository.GetByID(programYearId);
                List<string> proogramYearList = new List<string> { programYearEntity.Year };
                List<int> clientProgramList = new List<int> { programYearEntity.ClientProgramId };

                result = GetSessionPanels(clientProgramList, proogramYearList, userId);
            }
            else
            {
                result = GetSessionPanels(programYearId);
            }

            return result;
        }
        /// <summary>
        /// Wrapper method which determines which version of GetProgramYearCycles is called.  Determination is made
        /// based on a permission.  If the permission exists, the model list is filtered.  Otherwise the entire list
        /// is shown
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of ISessionPanelModel models</returns>
        public Container<int> GetProgramYearCycles(int programYearId, bool isFiltered, int userId)
        {
            Container<int> result = new Container<int>();

            if (isFiltered)
            {
                //
                // Seems a bit redundant but we first need to determine what SessionPanels the user is on
                //
                //List<int> list = ListSessionPanelIds(programYearId, isFiltered, userId);
                //
                // Then from there we can determine the available program cycles
                //
                result = GetProgramYearCycles(programYearId, userId);
            }
            else
            {
                result = GetProgramYearCycles(programYearId);
            }
            return result;
        }
        /// <summary>
        /// Retrieves the Receipt Cycles for one or more SessionPanels.
        /// </summary>
        /// <param name="list">List of SessionPanel entity identifiers</param>
        /// <returns>Container of ISessionPanelModel models</returns>
        internal virtual Container<int> GetProgramYearCycles(int programYearId, int userId)
        {
            Container<int> result = new Container<int>();

            //
            // Now that we have that we retrieve the distinct ReceiptCycles for
            // those session panels
            //
            var panels = UnitOfWork.SessionPanelRepository.Select().Where(x => x.PanelUserAssignments.Any(y => y.UserId == userId));
            var receiptCycles = panels.SelectMany(x => x.PanelApplications).
            Select(x => x.Application).
            Select(x => x.ProgramMechanism.ReceiptCycle.Value).Distinct();

            result.ModelList = receiptCycles;
            return result;
        }
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
        public Container<IAwardModel> GetAwards(int programYearId, int? cycle, int? panelId, bool isFiltered, int userId)
        {
            //
            // Set up default return
            // 
            Container<IAwardModel> result = new Container<IAwardModel>();

            if (isFiltered & (!panelId.HasValue))
            {
                var sessionPanels = UnitOfWork.SessionPanelRepository.Select().Where(x => x.PanelUserAssignments.Any(y => y.UserId == userId));
                //
                // Now we just retrieve the awards on those sessions
                //
                result = GetAwards(cycle, sessionPanels);
            }
            else
            {
                result = GetAwards(programYearId, cycle, panelId);
            }
            return result;
        }

        /// <summary>
        /// Retrieves the AwardMechanism of an SRO's SessionPanels 
        /// </summary>
        /// <param name="cycle">Receipt Cycle value (if any)</param>
        /// <param name="sessionPanelIds">Query retreiving session panel data</param>
        /// <returns>Container of IAwardModel representing SessionPanel entity identifiers</returns>
        internal virtual Container<IAwardModel> GetAwards(int? cycle, IQueryable<SessionPanel> sessionPanels)
        {
            //
            // Set up default return
            // 
            Container<IAwardModel> container = new Container<IAwardModel>();
            //
            // Now that we have those then we pull the ProgrmaMechanism on only those panels.  
            //
            var result = sessionPanels.SelectMany(x => x.PanelApplications).Select(x => x.Application.ProgramMechanism).Distinct();
            //
            // If the ReceiptCycle has a value then we filter it by the receipt cycle also.
            //
            if (cycle.HasValue)
            {
                result = result.Where(x => x.ReceiptCycle == cycle.Value);
            }
            //
            // Then populate the container & return
            //
            container.ModelList = result.Select(x => new AwardModel
            {
                AwardTypeId = x.ClientAwardTypeId,
                AwardAbbreviation = x.ClientAwardType.AwardAbbreviation,
                AwardDescription = x.ClientAwardType.AwardDescription
            });

            return container;
        }
    }
}
