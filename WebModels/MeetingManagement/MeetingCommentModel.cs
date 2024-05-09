using Sra.P2rmis.CrossCuttingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public class MeetingCommentModel : IMeetingCommentModel
    {
        
        public MeetingCommentModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingCommentModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">Name of the panel.</param>
        public MeetingCommentModel(string firstName, string lastName, string panelName)
        {
            FirstName = firstName;
            LastName = lastName;
            PanelName = panelName;
        }

        /// <summary>
        /// Overload for non-reviewers.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public MeetingCommentModel(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingCommentModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="internalComments">The internal comments.</param>
        public MeetingCommentModel(string firstName, string lastName, string panelName, string internalComments, int? meetingRegistrationId, DateTime? modifiedDate, string modifiedDateBy)
        {
            FirstName = firstName;
            LastName = lastName;
            PanelName = panelName;
            InternalComments = internalComments;
            ModifiedDate = modifiedDate;
            ModifiedByName = modifiedDateBy;
        }

        /// <summary>
        /// Overload for non reviewers, without panel.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="internalComments">The internal comments.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedDateBy">The modified date by.</param>
        public MeetingCommentModel(string firstName, string lastName, string internalComments, int? meetingRegistrationId, DateTime? modifiedDate, string modifiedDateBy)
        {
            FirstName = firstName;
            LastName = lastName;
            InternalComments = internalComments;
            ModifiedDate = modifiedDate;
            ModifiedByName = modifiedDateBy;
        }

        #region Properties
        /// <summary>
        /// Gets or sets first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets panel name
        /// </summary>
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
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        /// <value>
        /// The last updated by.
        /// </value>
        public string ModifiedByName { get; set; }
        #endregion
    }
    public interface IMeetingCommentModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }
        /// <summary>
        /// Gets or sets the panel name.
        /// </summary>
        /// <value>
        /// The panel name.
        /// </value>
        string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the internal comments.
        /// </summary>
        /// <value>
        /// The internal comments.
        /// </value>
        string InternalComments { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        DateTime? ModifiedDate {get;set;}
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        string ModifiedByName {get;set;}
    }
}
