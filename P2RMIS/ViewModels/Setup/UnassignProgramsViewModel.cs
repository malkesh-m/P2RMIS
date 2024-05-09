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
    public class UnassignProgramsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnassignProgramsViewModel"/> class.
        /// </summary>
        public UnassignProgramsViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnassignProgramsViewModel"/> class.
        /// </summary>
        /// <param name="awardMechanismModel">The award mechanism model.</param>
        public UnassignProgramsViewModel(IUnassignProgramModalModel assignedProgramsModel)
        {
            AssignedPrograms = assignedProgramsModel.AssignPrograms.ConvertAll(
                x => new Tuple<int, string, bool>(x.ProgramMeetingId, String.Format("{0} - {1}", x.Year, x.ProgramAbbreviation), x.IsPanelAssigned));
            ClientAbbr = assignedProgramsModel.ClientAbrv;
            MeetingAbbr = assignedProgramsModel.MeetingAbbr;
            MeetingName = assignedProgramsModel.MeetingName;
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
        /// Gets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        [JsonProperty("clientId")]
        public int ProgramId { get; private set; }
        
        /// <summary>
        /// Gets the client abbr.
        /// </summary>
        /// <value>
        /// The client abbr.
        /// </value>
        [JsonProperty("clientAbbr")]
        public string ClientAbbr { get; private set; }

        /// <summary>
        /// Gets the meeting abbr.
        /// </summary>
        /// <value>
        /// The meeting abbr.
        /// </value>
        [JsonProperty("meetingAbbr")]
        public string MeetingAbbr { get; private set; }

        /// <summary>
        /// Gets the meeting abbr.
        /// </summary>
        /// <value>
        /// The meeting abbr.
        /// </value>
        [JsonProperty("meetingName")]
        public string MeetingName { get; private set; }
        /// <summary>
        /// Gets the assigned programs.
        /// </summary>
        /// <value>
        /// The assigned programs.
        /// </value>
        [JsonProperty("programMeetingId")]
        public string ProgramMeetingId { get; private set; }
    }
}