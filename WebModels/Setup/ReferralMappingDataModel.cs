using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Setup
{
    public  partial class ReferralMappingDataModel
    {
        /// <summary>
        /// session panel id
        /// </summary>
        public int? SessionPanelId { get; set; }
        /// <summary>
        /// panel abbreviation
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int? PanelApplicationId { get; set; }
        /// <summary>
        /// application cycle
        /// </summary>
        public int? Cycle { get; set; }
        /// <summary>
        /// application log number
        /// </summary>
        public string AppLogNumber { get; set; }
        /// <summary>
        /// application id
        /// </summary>
        public int? ApplicationId { get; set; }
    }
}
