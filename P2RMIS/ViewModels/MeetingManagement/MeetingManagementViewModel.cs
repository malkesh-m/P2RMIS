using System.Collections.Generic;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System.Linq;
using Sra.P2rmis.WebModels.Setup;
using System;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class MeetingManagementViewModel : MMTabsViewModel
    {
        #region Constructors
        /// <summary>
        /// available applications view model
        /// </summary>
        public MeetingManagementViewModel() : base()
        {
            Clients = new List<KeyValuePair<int, string>>();
            ProgramYearId = ProgramYearId;
        }
        public MeetingManagementViewModel(List<UserProfileClientModel> clients) : this()
        {
            Clients = clients.ConvertAll(x => new KeyValuePair<int, string>(x.ClientId, x.ClientAbrv)).OrderBy(y => y.Value).ToList();
        }
        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public List<KeyValuePair<int, string>> Clients { get; private set; }
        /// <summary>
        /// Gets or sets the active years.
        /// </summary>
        /// <value>
        /// The active years.
        /// </value>
        public List<int>ActiveYears { get; set; }
        /// <summary>
        /// Gets or sets the client list.
        /// </summary>
        /// <value>
        /// The client list.
        /// </value>
        public List<int>ClientList {get;set;}
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingManagementViewModel"/> class.
        /// </summary>
        /// <param name="program">The program.</param>
        public MeetingManagementViewModel(IFilterableProgramModel program)
        {
            ProgramYearId = program.ProgramYearId;
            ProgramName = program.ProgramDescription;
            ProgramAbbr = program.ProgramAbbreviation;
            IsActive = program.IsActive;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; set; }

        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }

        /// <summary>
        /// Gets the program abbr.
        /// </summary>
        /// <value>
        /// The program abbr.
        /// </value>
        public string ProgramAbbr { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; private set; }
        #endregion
    }
}