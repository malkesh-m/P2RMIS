using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    public interface ISessionPanelModel
    {
        /// <summary>
        /// Gets or sets the panel identifier.
        /// </summary>
        /// <value>
        /// The panel identifier.
        /// </value>
        int PanelId { get; set; }
        /// <summary>
        /// Gets or sets the meeting session identifier.
        /// </summary>
        /// <value>
        /// The meeting session identifier.
        /// </value>
        int? MeetingSessionId { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        int ProgramYearId { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        string Year { get; set; }
        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        string ProgramName { get; set; }
        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        string ProgramAbbreviation { get; set; }
    }
}
