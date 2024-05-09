using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.UserProfileManagement
{
    public class UserClientBlockLogViewModel
    {
        public UserClientBlockLogViewModel(IUserClientBlockLog model)
        {
            Comments = model.Comments;
            Clients = model.ClientBlockFlags.ConvertAll(x => x.Key).ToList().Aggregate((i, j) => i + "," + j);
            EnteredBy = model.EnteredBy;
            EnteredDate = ViewHelpers.FormatDate(model.CreatedDate);
        }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public string Clients { get; set; }
        /// <summary>
        /// Gets or sets the entered by.
        /// </summary>
        /// <value>
        /// The entered by.
        /// </value>
        public string EnteredBy { get; set; }
        /// <summary>
        /// Gets or sets the entered date.
        /// </summary>
        /// <value>
        /// The entered date.
        /// </value>
        public string EnteredDate { get; set; }
    }
}