using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.SessionPanelDetails;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for Admin Note
    /// </summary>
    public class AdminNoteViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminNoteViewModel"/> class.
        /// </summary>
        public AdminNoteViewModel() {}
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminNoteViewModel"/> class.
        /// </summary>
        /// <param name="adminBudgetNoteModel">The admin budget note model.</param>
        /// <param name="applicationId">The application identifier</param>
        public AdminNoteViewModel(IAdminBudgetNoteModel adminBudgetNoteModel, int applicationId)
        {
            ApplicationId = applicationId;
            ApplicationBudgetId = adminBudgetNoteModel.ApplicationBudgetId;
            Note = adminBudgetNoteModel.Comment;
            ModifiedDate = ViewHelpers.FormatLastUpdateDateTime(adminBudgetNoteModel.ModifiedDate);
            ModifiedByName = ViewHelpers.ConstructNameWithSpace(adminBudgetNoteModel.ModifiedByFirstName, adminBudgetNoteModel.ModifiedByLastName);
        }
        /// <summary>
        /// Gets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; private set; }
        /// <summary>
        /// Gets the application budget identifier.
        /// </summary>
        /// <value>
        /// The application budget identifier.
        /// </value>
        public int ApplicationBudgetId { get; private set; }
        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public string Note { get; private set; }
        /// <summary>
        /// Gets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public string ModifiedDate { get; private set; }
        /// <summary>
        /// Gets the name of person who makes the last modification
        /// </summary>
        /// <value>
        /// The name of person who makes the last modification
        /// </summary>
        /// </value>
        public string ModifiedByName { get; private set; }
    }
}