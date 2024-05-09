using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AssignProgramsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignProgramsViewModel"/> class.
        /// </summary>
        public AssignProgramsViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignProgramsViewModel"/> class.
        /// </summary>
        /// <param name="awardMechanismModel">The award mechanism model.</param>
        public AssignProgramsViewModel(List<IGenericActiveProgramListEntry<int, string>> availablePrograms, 
            List<IUnassignProgramListModel> assignedPrograms)
        {
            AssignedPrograms = assignedPrograms.ConvertAll(
                x => new Tuple<int, string, bool>(x.ProgramMeetingId, String.Format("{0} - {1}", x.Year, x.ProgramAbbreviation), x.IsPanelAssigned));
            AvailablePrograms = availablePrograms.Where(x => !assignedPrograms.Exists(y => y.ProgramYearId == x.ProgramYearId)).ToList()
                .ConvertAll(x => new Tuple<int, string, bool>(x.ProgramYearId, String.Format("{0} - {1}", x.Value, x.ProgramAbbreviation), x.IsActive));
        }

        /// <summary>
        /// Gets the assigned programs.
        /// </summary>
        /// <value>
        /// The assigned programs.
        /// </value>
        [JsonProperty("assignedPrograms")]
        public List<Tuple<int, string, bool>> AssignedPrograms { get; private set; }

        /// <summary>
        /// Gets the available programs.
        /// </summary>
        /// <value>
        /// The available programs.
        /// </value>
        [JsonProperty("availablePrograms")]
        public List<Tuple<int, string, bool>> AvailablePrograms { get; private set; }

        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; private set; }  
        /// <summary>
        /// Program year identifiers.
        /// </summary>
        public List<int> ProgramYearIds { get; set; }
    }
}