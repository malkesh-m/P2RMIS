using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.MeetingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Sra.P2rmis.WebModels.MeetingManagement.MeetingCommentModel;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class MeetingCommentViewModel : MMTabsViewModel
    {
        public MeetingCommentViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingCommentViewModel" /> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedDateBy">The modified date by.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment id.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="subTab1Link">The sub tab1 link.</param>
        /// <param name="subTab2Link">The sub tab2 link.</param>
        /// <param name="subTab3Link">The sub tab3 link.</param>
        public MeetingCommentViewModel(string firstName, string lastName, string panelName, string internalComments, int? meetingRegistrationId, DateTime? modifiedDate, string modifiedDateBy, int? panelUserAssignmentId, int? sessionUserAssignmentId, string subTab1Link, string subTab2Link, string subTab3Link)
        {
            ReviewerName = ViewHelpers.ConstructNameWithSpace(firstName, lastName);
            PanelName = panelName;
            InternalComments = internalComments;
            MeetingRegistrationId = meetingRegistrationId;
            PanelUserAssignmentId = panelUserAssignmentId;
            SessionUserAssignmentId = sessionUserAssignmentId;
            LastUpdated = ViewHelpers.FormatDate(modifiedDate);
            LastUpdatedBy = modifiedDateBy;
            SubTab1Link = ViewHelpers.BuildHotelTravelSublink(subTab1Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab2Link = ViewHelpers.BuildHotelTravelSublink(subTab2Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab3Link = ViewHelpers.BuildHotelTravelSublink(subTab3Link, panelUserAssignmentId, sessionUserAssignmentId);
        }
        /// <summary>
        /// Gets or sets the assignment type threshold.
        /// </summary>
        /// <value>
        /// The assignment type threshold.
        /// </value>
        public List<IMeetingCommentModel> SaveCommentsDetails { get; set; }
        
        #region Properties
        /// <summary>
        /// Gets or sets the name of the reviewer.
        /// </summary>
        /// <value>
        /// The name of the reviewer.
        /// </value>
        public string ReviewerName { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the panel name.
        /// </summary>
        /// <value>
        /// The panel name.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the internal comments.
        /// </summary>
        /// <value>
        /// The internal comments.
        /// </value>
        public string InternalComments { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        public int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public string LastUpdated { get; set; }
        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        /// <value>
        /// The last updated by.
        /// </value>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId {get;set;}
        /// <summary>
        /// Gets or sets the sub tab1 link.
        /// </summary>
        /// <value>
        /// The sub tab1 link.
        /// </value>
        public string SubTab1Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab2 link.
        /// </summary>
        /// <value>
        /// The sub tab2 link.
        /// </value>
        public string SubTab2Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab3 link.
        /// </summary>
        /// <value>
        /// The sub tab3 link.
        /// </value>
        public string SubTab3Link {get;set;}
        /// <summary>
        /// Gets or sets the meeting details header model.
        /// </summary>
        /// <value>
        /// The meeting details header model.
        /// </value>
        public MeetingDetailsHeaderModel DetailsHeader { get; set; }
        #endregion
    }
}