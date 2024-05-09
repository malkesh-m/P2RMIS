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
    public class FiscalYearViewModel
    {
        public FiscalYearViewModel() { }

        public FiscalYearViewModel(IFilterableProgramYearModel programYear) {
            YearText = programYear.Year;
            YearValue = programYear.Year;
            IsActive = programYear.IsActive;
        }

        /// <summary>
        /// Gets the year text.
        /// </summary>
        /// <value>
        /// The year text.
        /// </value>
        public string YearText { get; private set; }

        /// <summary>
        /// Gets the year value.
        /// </summary>
        /// <value>
        /// The year value.
        /// </value>
        public string YearValue { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; private set; }
    }
}