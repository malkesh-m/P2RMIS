using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ReviewerFacts : IReviewerFacts
    {
        #region Constants
        /// <summary>
        /// Class constants
        /// </summary>
        private class Constants
        {
            public const string HasComment = "1";
            public const string Yes = "yes";
            public class CritiquePhase
            {
                public const string Initial = "initial";
                public const string Revised = "revised";
                public const string Meeting = "meeting";
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReviewerFacts() { }
        /// <summary>
        /// Construct & populate from a data layer Reviewer object.
        /// </summary>
        /// <param name="reviewer">Data layer Reviewer object</param>
        public ReviewerFacts(ReviewerInfo_Result reviewer)
        {
            this.Role = ViewHelpers.SetNonNull("" + reviewer.PartType + " / " + reviewer.RoleName);
            this.PrgPartId = reviewer.PanelUserAssignmentId;
            this.ApplicationId = ViewHelpers.SetNonNull(reviewer.ApplicationID);
            this.Prefix = ViewHelpers.SetNonNull(reviewer.Prefix);
            this.FirstName = ViewHelpers.SetNonNull(reviewer.FirstName);
            this.LastName = ViewHelpers.SetNonNull(reviewer.Lastname);
            this.COI  = ViewHelpers.SetNonNull(reviewer.COI);
            this.HasComment = (reviewer.Comment == Constants.HasComment);
            this.PanelId = reviewer.PanelID;
            //this.PartType = ViewHelpers.SetNonNull(reviewer.PartTypeDesc);
            this.CritiqueDeadline = reviewer.PhaseEnd.GetValueOrDefault();
            this.CritiqueRequired = DetermineIfCritiqueRequired(reviewer.Critique_Required, reviewer.CritiqueSubmitted.GetValueOrDefault(false), reviewer.CritiquePhase);
            this.PanelApplicationId = reviewer.PanelApplicationId;
            this.ReviewerUserId = reviewer.ReviewerUserId;
            this.ApplicationIdentifier = reviewer.ApplicationId1;
            this.AdminNotesExist = reviewer.AdminNotesExist;
        }



        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int PrgPartId { get; private set; }
        /// <summary>
        /// Application identifier
        /// </summary>
        public string ApplicationId { get; private set; }
        /// <summary>
        /// Unique identifier for panel
        /// </summary>
        public int PanelId { get; private set; }
        /// <summary>
        /// Reviewer's name prefix
        /// </summary>
        public string Prefix { get; private set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string COI { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public CritiqueRequiredType CritiqueRequired { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int AssignmentTypeId { get; private set; }
        /// <summary>
        /// Reviewers role
        /// </summary>
        public string Role { get; private set; }
        /// <summary>
        /// Indicates review has a comment
        /// </summary>
        public bool HasComment { get; private set; }
        /// <summary>
        /// Participant type abbreviation
        /// </summary>
        public string PartType { get; private set; }
        public DateTime CritiqueDeadline { get; private set; }
        /// <summary>
        /// Unique identifier for a panel application
        /// </summary>
        public int PanelApplicationId { get; private set; }
        /// <summary>
        /// The UserId of the reviewer (not PanelUserAssignmentId)
        /// </summary>
        public int ReviewerUserId { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the admin notes exist.
        /// </summary>
        /// <value>
        /// The admin notes exist.
        /// </value>
        public int AdminNotesExist { get; set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Determines if a critique is required from the data layer fields:
        ///     - Critique_Required
        ///     - Date Submitted
        ///     - CritiquePhase
        /// The rules to determine the critique required type are:
        ///     - Nothing will be displayed if the Critique_Required field is blank
        ///     - Critique Missing Icon will be displayed if the critique_required field says “yes” and the datesubmitted field is 0
        ///     - Critique Icon will be displayed if the critique_required field says “yes”, the datesubmitted field is 1, and the critiquephase is “initial” or “revised”
        ///     - Revised Critique Icon will be displayed if the critique_required field says “yes”, the datesubmitted field is 1, and the critiquephase is meeting
        /// </summary>
        /// <param name="value">-----</param>
        /// <param name="submitted">-----</param>
        /// <param name="critiquePhase">-----</param>
        /// <returns>CritiqueRequireType</returns>
        private CritiqueRequiredType DetermineIfCritiqueRequired(string value, bool submitted, string critiquePhase)
        {
            CritiqueRequiredType result = CritiqueRequiredType.None;

            if (value.Equals(Constants.Yes, System.StringComparison.CurrentCultureIgnoreCase))
            {
                
                    if (GlobalProperties.P2rmisDateTimeNow <= CritiqueDeadline)
                    {
                        result = CritiqueRequiredType.CritiqueUnavailable;
                    }
                    else if (submitted && ((critiquePhase.Equals(Constants.CritiquePhase.Initial, System.StringComparison.CurrentCultureIgnoreCase)) || (critiquePhase.Equals(Constants.CritiquePhase.Revised, System.StringComparison.CurrentCultureIgnoreCase))))
                    {
                        result = CritiqueRequiredType.Critique;
                    }
                    else if (critiquePhase.Equals(Constants.CritiquePhase.Meeting, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = CritiqueRequiredType.RevisedCritique;
                    }
                    else
                    {
                        result = CritiqueRequiredType.CritiqueMissing;
                    }
                }
            
            return result;
        }
        #endregion
    }
}
