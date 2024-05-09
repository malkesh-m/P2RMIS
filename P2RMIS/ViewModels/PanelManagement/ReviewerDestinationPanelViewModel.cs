using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    public class ReviewerDestinationPanelViewModel
    {
        public const string OnsiteMeeting = "Onsite Meeting";
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewerDestinationPanelViewModel"/> class.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="meetingTypeName">Name of the meeting type.</param>
        public ReviewerDestinationPanelViewModel(int panelId, string panelName, string meetingTypeName)
        {
            PanelId = panelId;
            PanelName = panelName;
            MeetingTypeName = meetingTypeName;
            CanBeInPerson = meetingTypeName == OnsiteMeeting;
        }
        /// <summary>
        /// Gets the panel identifier.
        /// </summary>
        /// <value>
        /// The panel identifier.
        /// </value>
        public int PanelId { get; private set; }
        /// <summary>
        /// Gets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; private set; }
        /// <summary>
        /// Gets the name of the meeting type.
        /// </summary>
        /// <value>
        /// The name of the meeting type.
        /// </value>
        public string MeetingTypeName { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance can be in person.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can be in person; otherwise, <c>false</c>.
        /// </value>
        public bool CanBeInPerson { get; private set; }
    }
}