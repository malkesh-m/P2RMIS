using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    public class SessionPanelModel : ISessionPanelModel
    {
        public SessionPanelModel() { }

        public SessionPanelModel(int panelId, string panelAbbreviation, int programYearId, string year, string programAbbreviation, int? meetingSessionId)
        {
            PanelId = panelId;
            PanelAbbreviation = panelAbbreviation;
            ProgramYearId = programYearId;
            Year = year;
            ProgramAbbreviation = programAbbreviation;
            MeetingSessionId = meetingSessionId;
        }

        public SessionPanelModel(int panelId, string panelName, string panelAbbreviation, int programYearId, string year, string programName, string programAbbreviation, int? meetingSessionId) 
            : this(panelId, panelAbbreviation, programYearId, year, programAbbreviation, meetingSessionId)
        {
            PanelName = panelName;
            ProgramName = programName;
        }
        /// <summary>
        /// Gets or sets the panel identifier.
        /// </summary>
        /// <value>
        /// The panel identifier.
        /// </value>
        public int PanelId { get; set; }
        /// <summary>
        /// Gets or sets the meeting session identifier.
        /// </summary>
        /// <value>
        /// The meeting session identifier.
        /// </value>
        public int? MeetingSessionId { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string Year { get; set; }
        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }
        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation { get; set; }
    }
}
