using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for viewing reviewers information
    /// </summary>
    public class WorklistViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the work list view model
        /// </summary>
        public WorklistViewModel()
        {
            this.ClientList = new List<ClientViewModel>();
            this.WorkList = new List<WorklistDataViewModel>();
        }

        /// <summary>
        /// Populates the client list in the view model.
        /// </summary>
        /// <param name="clientListModel">The client list web model.</param>
        public void PopulateClientList(List<UserProfileClientModel> clientListModel)
        {
            foreach (var client in clientListModel)
            {
                ClientList.Add(new ClientViewModel(client));
            }
        }
        #endregion

        /// <summary>
        /// Populates the work list.
        /// </summary>
        /// <param name="workListModel">The work list model.</param>
        public void PopulateWorkList(List<IWorkList> workListModel)
        {
            if (workListModel.Count > 0)
            {
                foreach (var workitem in workListModel.Where(x => x.Display))
                {
                    WorkList.Add(new WorklistDataViewModel(workitem));
                }
            } else
            {
                NoWorkListMessage = MessageService.NoWorkListMessage;
            }
        }
        #region Properties
        /// <summary>
        /// Gets or sets the selected client identifier.
        /// </summary>
        /// <value>
        /// The selected client identifier.
        /// </value>
        public int SelectedClientId { get; set; }

        /// <summary>
        /// Gets the client list.
        /// </summary>
        /// <value>
        /// The client list.
        /// </value>
        public List<ClientViewModel> ClientList { get; private set; }

        /// <summary>
        /// Gets or sets the work list data.
        /// </summary>
        /// <value>
        /// The work list.
        /// </value>
        public List<WorklistDataViewModel> WorkList { get; private set; }

        /// <summary>
        /// Gets the message for "No Work List"
        /// </summary>
        /// <value>
        /// Message for "No Work List"
        /// </value>
        public string NoWorkListMessage { get; private set; }
        #endregion
        #region Nested types
        public class ClientViewModel
        {
            #region Constructor

            public ClientViewModel(IUserProfileClientModel clientModel)
            {
                this.ClientId = clientModel.ClientId;
                this.ClientName = clientModel.ClientName;
            }
            #endregion
            #region properties
            /// <summary>
            /// Gets the name of the client.
            /// </summary>
            /// <value>
            /// The name of the client.
            /// </value>
            public string ClientName { get; private set; }

            /// <summary>
            /// Gets the client identifier.
            /// </summary>
            /// <value>
            /// The client identifier.
            /// </value>
            public int ClientId { get; private set; }

            #endregion
        }

        public class WorklistDataViewModel
        {
            #region Constructor

            public WorklistDataViewModel(IWorkList workListModel)
            {
                this.Name = ViewHelpers.ConcatenateStringWithComma(workListModel.LastName, workListModel.FirstName);
                this.LastModifiedByName = ViewHelpers.ConstructShortName(workListModel.ModifedByFirstName, workListModel.ModifedByLastName);
                this.LastModifiedDate = ViewHelpers.FormatDate(workListModel.ModifiedOn);
                this.UserInfoId = workListModel.UserInfoId;
                this.ProfileName = workListModel.RoleName;
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the user who's profile was changed name.
            /// </summary>
            /// <value>
            /// The user whose profile was changed name.
            /// </value>
            public string Name { get; private set; }

            
            /// <summary>
            /// Gets or sets the last name of the user who modified the profile.
            /// </summary>
            /// <value>
            /// The last name of the user who last modified the profile.
            /// </value>
            public string LastModifiedByName { get; private set; }
            /// <summary>
            /// Gets or sets the date the profile was last modified with a flagged change.
            /// </summary>
            /// <value>
            /// The date the profile was last modified with a flagged change.
            /// </value>
            public string LastModifiedDate { get; private set; }

            /// <summary>
            /// Gets or sets the user information identifier.
            /// </summary>
            /// <value>
            /// The user information identifier for the user who's information was changed.
            /// </value>
            public int? UserInfoId { get; private set; }

            /// <summary>
            /// Gets the name of the profile.
            /// </summary>
            /// <value>
            /// The name of the profile.
            /// </value>
            public string ProfileName { get; private set; }
            #endregion
        }
        #endregion
    }
}