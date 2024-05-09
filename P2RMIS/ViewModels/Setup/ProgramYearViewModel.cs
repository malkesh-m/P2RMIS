using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ProgramYearViewModel
    {
        public ProgramYearViewModel() { }

        public ProgramYearViewModel(IFilterableProgramModel program) {
            ProgramYearId = program.ProgramYearId;
            ProgramName = program.ProgramDescription;
            ProgramAbbr = program.ProgramAbbreviation;
            IsActive = program.IsActive;
        }

        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; private set; }

        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; private set; }

        /// <summary>
        /// Gets the program abbr.
        /// </summary>
        /// <value>
        /// The program abbr.
        /// </value>
        public string ProgramAbbr { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; private set; }
    }
}