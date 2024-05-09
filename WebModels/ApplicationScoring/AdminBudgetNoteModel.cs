using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Definition of data fields required for the admin budget note model.
    /// </summary>  
    public interface IAdminBudgetNoteModel
    {
        /// <summary>
        /// The application budget identifier
        /// </summary>
        int ApplicationBudgetId { get; set; }
        /// <summary>
        /// The application identifier
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// Buget comment
        /// </summary>
        string Comment { get; set; }
        /// <summary>
        /// Comment modified by user identifier
        /// </summary>
        int? ModifiedBy { get; set; }
        /// <summary>
        /// Comment modification date
        /// </summary>
        DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the first name of the modified by.
        /// </summary>
        /// <value>
        /// The first name of the modified by.
        /// </value>
        string ModifiedByFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the modified by.
        /// </summary>
        /// <value>
        /// The last name of the modified by.
        /// </value>
        string ModifiedByLastName { get; set; }
        /// <summary>
        /// Populate the model
        /// </summary>
        /// <param name="applicationBudgetId">The application budget identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <param name="comment">The comment</param>
        /// <param name="modifiedBy">The comments modified by user's identifier</param>
        /// <param name="modifiedDate">The comments modified date</param>
        /// <param name="modifiedLastName">Last name of the modified.</param>
        /// <param name="modifiedFirstName">First name of the modified.</param>
        void Populate(int applicationBudgetId, int applicationId, string comment, int? modifiedBy, DateTime? modifiedDate, string modifiedLastName, string modifiedFirstName);
    }
    /// <summary>
    /// Definition of data fields required for the admin budget note model.
    /// </summary>  
    public class AdminBudgetNoteModel : IAdminBudgetNoteModel
    {
        /// <summary>
        /// The application budget identifier
        /// </summary>
        public int ApplicationBudgetId { get; set; }
        /// <summary>
        /// The application identifier
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Buget comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Comment modified by user identifier
        /// </summary>
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Comment modification date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the first name of the modified by.
        /// </summary>
        /// <value>
        /// The first name of the modified by first name.
        /// </value>
        public string ModifiedByFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the modiifed by.
        /// </summary>
        /// <value>
        /// The last name of the modified by.
        /// </value>
        public string ModifiedByLastName { get; set; }
        /// <summary>
        /// Populate the model
        /// </summary>
        /// <param name="applicationBudgetId">The application budget identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <param name="comment">The comment</param>
        /// <param name="modifiedBy">The comments modified by user's identifier</param>
        /// <param name="modifiedDate">The comments modified date</param>
        /// <param name="modifiedLastName">Last name of the user who last modified.</param>
        /// <param name="modifiedFirstName">First name of the modified.</param>
        public void Populate(int applicationBudgetId, int applicationId, string comment, int? modifiedBy, DateTime? modifiedDate, string modifiedLastName, string modifiedFirstName)
        {
            this.ApplicationBudgetId = applicationBudgetId;
            this.ApplicationId = applicationId;
            this.Comment = comment;
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate;
            this.ModifiedByFirstName = modifiedFirstName;
            this.ModifiedByLastName = modifiedLastName;
        }

    }
}
