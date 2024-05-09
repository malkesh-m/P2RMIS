using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class SessionAssignmentModel
    {
        /// <summary>
        /// Gets or sets the assigned values.
        /// </summary>
        /// <value>
        /// The assigned values.
        /// </value>
        public int[] AssignedValues { get; set; }
        /// <summary>
        /// Gets or sets the unassigned values.
        /// </summary>
        /// <value>
        /// The unassigned values.
        /// </value>
        public int[] UnassignedValues { get; set; }
    }
}