using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.ApplicationDetails;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// UI model for an Application's Details 
    /// </summary>
    public class ApplicationDetailsViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor.  
        /// </summary>
        public ApplicationDetailsViewModel() 
        { 
            this.ApplicationInformationModel = new ApplicationInformationModel();
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="container">-----</param>
        public ApplicationDetailsViewModel(IApplicationDetailsContainer container)
        {
            ///
            /// Populate the view model properties from the BL data view.
            ///
            this.ApplicationInformationModel = new ApplicationInformationModel();
            this.ApplicationDetails = new List<IReviewerLineScore>(container.Details).ConvertAll(new Converter<IReviewerLineScore, ApplicationDetails>(IReviewerLineScoreToApplicationDetails));
            this.Columns = new List<KeyValuePair<int, string>>(container.Columns);
            this.ColumnAltText = new Dictionary<int,string>(container.ColumnAltText);
            this.ReviewerComments = new List<IReviewerCommentFacts>(container.ReviewerComments).ConvertAll(new Converter<IReviewerCommentFacts, ReviewerComments>(IReviewerCommentFactsToReviewerComments));
            this.UserApplicationComments = new List<IUserApplicationCommentFacts>(container.UserApplicationComments).ConvertAll(new Converter<IUserApplicationCommentFacts, UserApplicationComments>(IUserApplicationCommentFactsToUserApplicationComments));
            this.CommentLookupTypes = new List<ICommentLookupTypes>(container.CommentLookupTypes).ConvertAll(new Converter<ICommentLookupTypes,CommentLookupTypes>(ICommentLookupTypesToCommentLookupTypes));
            if (container.Averages != null)
            {
                this.Averages = IReviewerLineScoreToApplicationDetails(container.Averages);
            }

            if (container.StandardDeviation != null)
            {
                this.StandardDeviation = IReviewerLineScoreToApplicationDetails(container.StandardDeviation);
            }
            
        }
        #endregion

        #region Properties
        /// <summary>
        /// User id for current user logged in
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Collection of Application Information for a single application (old)
        /// </summary>
        public ApplicationInfo ApplicationInformation { get; set; }
        /// <summary>
        /// Collection of Application Information for a single application
        /// </summary>
        public IApplicationInformationModel ApplicationInformationModel { get; set; }
        /// <summary>
        /// Collection of Application Details for a single application
        /// </summary>
        public IEnumerable<ApplicationDetails> ApplicationDetails { get; set; }
        /// <summary>
        /// Collection of Reviewer Comments for a single application
        /// </summary>
        public IEnumerable<UserApplicationComments> UserApplicationComments { get; set; }
        /// <summary>
        /// Collection of User Comments for a single application
        /// </summary>
        public IEnumerable<ReviewerComments> ReviewerComments { get; set; }
        /// <summary>
        /// Collection of Comment Types available to add
        /// </summary>
        public IEnumerable<CommentLookupTypes> CommentLookupTypes { get; set; }
        /// <summary>
        /// Ordered column titles
        /// </summary>
        public List<KeyValuePair<int, string>> Columns { get; set; }
        /// <summary>
        /// Contains the Alt text values for the column titles
        /// </summary>
        public Dictionary<int, string> ColumnAltText { get; set; }
        /// <summary>
        /// Application details averages
        /// </summary>
        public ApplicationDetails Averages { get; set; }
        /// <summary>
        /// Application details standard deviation
        /// </summary>
        public ApplicationDetails StandardDeviation { get; set; }
        /// <summary>
        /// Client scoring scale legend
        /// </summary>
        public ClientScoringScaleLegendModel ClientScoringScaleLegend { get; set; }
        /// <summary>
        /// The last page URL
        /// </summary>
        public string LastPageUrl { get; set; }
        /// <summary>
        /// Whether the current user has permission to edit a user's assignment type
        /// </summary>
        public bool CanEditAssignmentType { get; set; }
        /// <summary>
        /// Whether the current user has permission to edit a user's score card
        /// </summary>
        public bool CanEditScoreCard { get; set; }
        /// <summary>
        /// Indicates if the user can only view critiques.  Used to indicate
        /// clients which cannont start incomplete critiques.
        /// </summary>
        public bool CanOnlyViewCritique { get; set; }
        /// <summary>
        /// Number of reviewers that are supposed to score the application
        /// </summary>
        public int NumberOfReviewers
        {
            get
            {
                return ApplicationDetails.Count(x => !x.isCoi);
            }
        }
        /// <summary>
        /// Number of reviewers that scored the application
        /// </summary>
        public int NumberOfScored
        {
            get
            {
                return ApplicationDetails.Where(x => x.Scores.Count == Columns.Count).Count();
            }
        }
        /// <summary>
        /// Indicates if the Overall key line should be displayed
        /// </summary>
        public bool DisplayOverallKey
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ClientScoringScaleLegend.OverallScaleLabel);
            }
        }
        /// <summary>
        /// Indicates if the Criterion key line should be displayed
        /// </summary>
        public bool DisplayCriterionKey
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ClientScoringScaleLegend.CriterionScaleLabel);
            }
        }

        public bool IsFromMyWorkspace { get; internal set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Converts a business layer ReviewerLineScore to a presentation layer ApplicationDetails object.
        /// </summary>
        /// <param name="item">ReviewerLineScore object to convert</param>
        /// <returns>ApplicationDetails object</returns>
        private static ApplicationDetails IReviewerLineScoreToApplicationDetails(IReviewerLineScore item)
        {
            return new ApplicationDetails(item);
        }
        /// <summary>
        /// Converts a business layer IReviewerCommentFact object to a presentation layer ReviewerComments object.
        /// </summary>
        /// <param name="item">IReviewerCommentFact object to convert</param>
        /// <returns>ReviewerComments object</returns>
        private static ReviewerComments IReviewerCommentFactsToReviewerComments(IReviewerCommentFacts item)
        {
            return new ReviewerComments(item);
        }
        /// <summary>
        /// Converts a business layer IUserApplicationCommentFact object to a presentation layer UserComments object.
        /// </summary>
        /// <param name="item">IUserCommentFact object to convert</param>
        /// <returns>ReviewerComments object</returns>
        private static UserApplicationComments IUserApplicationCommentFactsToUserApplicationComments(IUserApplicationCommentFacts item)
        {
            return new UserApplicationComments(item);
        }
        /// <summary>
        /// Converts a business layer IUserApplicationCommentFact object to a presentation layer UserComments object.
        /// </summary>
        /// <param name="item">IUserCommentFact object to convert</param>
        /// <returns>ReviewerComments object</returns>
        private static CommentLookupTypes ICommentLookupTypesToCommentLookupTypes(ICommentLookupTypes item)
        {
            return new CommentLookupTypes(item);
        }
        #endregion
    }
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ApplicationInfo
    {
        #region Constructors

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ApplicationInfo() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">-----</param>
        internal ApplicationInfo(IApplicationDetailFact value)
        {
            ///
            /// Initialize the application's information
            /// 
            this.ApplicationId = value.ApplicationId;
            this.PI = value.PrincipalInvestigatorName;
            this.PiOrgName = value.PiOrgName;
            this.ApplicationTitle = value.ApplicationTitle;
            this.AwardAbbreviation = value.AwardDesccription;
            this.PanelId = value.PanelId;
            this.isSessionOpen = value.isSessionOpen;
            this.SessionClosedMessage = value.SessionClosedMessage;
            this.ProgramId = value.ProgramId;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Application identifier.
        /// </summary>
        public string ApplicationId { get; private set; }
        /// <summary>
        /// PI for the Application
        /// </summary>
        public string PI { get; private set; }
        /// <summary>
        /// The applications title.
        /// </summary>
        public string ApplicationTitle { get; private set; }
        /// <summary>
        /// Award Mechanism abbreviation for the application
        /// </summary>
        public string AwardAbbreviation { get; private set; }
        /// <summary>
        /// The name of the PIs Organization/Institution
        /// </summary>
        public string PiOrgName { get; private set; }
        /// <summary>
        /// Panel Unique Identifier
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// Boolean for whether panels session is open or not
        /// </summary>
        public string SessionClosedMessage { get; set; }
        /// <summary>
        /// String to display in place of add comment when session is closed
        /// </summary>
        public bool isSessionOpen { get; set; }
        /// <summary>
        /// Applications program Id
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// Constructs scoreboard link
        /// </summary>
        public IHtmlString ScoreboardLink { get { return new HtmlString("<a class='ajax focus' href='/Scoreboard/Scoreboard/?applicationId=" + ApplicationId + "&panelId=" + PanelId +"'>View Scoreboard</a>");} }
        /// <summary>
        /// Constructs add comment link if session is open
        /// </summary>
        public IHtmlString AddCommentLink { get { return (isSessionOpen == true) ? new HtmlString("<div class='addComment'><a href='#addComment' data-toggle='modal'>Add Note</a></div>") : new HtmlString(SessionClosedMessage); } }

        public IHtmlString BackToPanelDetailsLink { get { return new HtmlString("<a class='focus' href='/MyWorkspace/ManageApplicationScoring/?OpenPrograms=" + ProgramId + "&panelId=" + PanelId + "'>Return to Panel Details</a>"); } }
        
        #endregion
    }
    /// <summary>
    /// Application Details
    /// </summary>
    public class ApplicationDetails
    {
        #region Constants

        private class Constants 
        {
            public const string critiqueImg = "../../Content/themes/base/images/Critique.png";
            public const string revCommentImg = "../../Content/themes/base/images/Comment.png";
            public const string revCritiqueImg = "../../Content/themes/base/images/RevisedCrit.png";
            public const string coiImg = "../../Content/themes/base/images/COI.png";
            public const string critiqueMissingImg = "../../Content/themes/base/images/CritiqueMissing.png";
            public const string altCritiqueImg = "Critique Icon";
            public const string altCoiImg = "Conflict of Interest Icon";
            public const string altRevCritiqueImg = "Revised Critique Icon";
            public const string altRevCommentImg = "Reviewer Comment Icon";
            public const string altCritiqueMissingImg = "Critique Missing Icon";
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        internal ApplicationDetails(IReviewerLineScore value)
        {
            this.ReviewerName = value.Name;
            this.ReviewerRole = value.Role;
            //this.PartType = value.PartType;
            this.Scores = value.CriteriaScores;
            this.isCoi = value.IsCoi;
            this.hasComment = value.HasComment;
            this.critiqueType = value.CritiqueType;
            this.ReviewerId = value.ProgramPartId;
            this.ApplicationId = value.ApplicationId;
            this.PanelId = value.PanelId;
            this.PanelApplicationId = value.PanelApplicationId;
            this.ReviewerUserId = value.ReviewerUserId;
        }

        

        #endregion

        #region Properties
        /// <summary>
        /// Reviewers unique identifier
        /// </summary>
        public int ReviewerId { get; internal set; }
        /// <summary>
        /// Reviewers Name
        /// </summary>
        public string ReviewerName { get; internal set; }
        /// <summary>
        /// The reviewers role for that application
        /// </summary>
        public string ReviewerRole { get; internal set; }
        /// <summary>
        /// The participant type for that application
        /// </summary>
        public string PartType { get; internal set; }
        /// <summary>
        /// Unique identifier for Application
        /// </summary>
        public string ApplicationId { get; internal set; }
        /// <summary>
        /// Unique identifier for a panel application assignment
        /// </summary>
        public int PanelApplicationId { get; internal set; }
        /// <summary>
        /// Unique identifier for Panel
        /// </summary>
        public int PanelId { get; internal set; }
        /// <summary>
        /// Returns whether or not the reviewer is a COI for the application
        /// </summary>
        public bool isCoi { get; internal set; }
        /// <summary>
        /// Returns whether or not the reviewer has a comment for the application
        /// </summary>
        public bool hasComment { get; internal set; }
        /// <summary>
        /// Returns what to show for the critique type icon
        /// </summary>
        public CritiqueRequiredType critiqueType { get; internal set; }
        /// <summary>
        /// Dictionary of Scores
        /// </summary>
        public Dictionary<int, string> Scores { get; set; }
        /// <summary>
        /// The UserId of the reviewer (not PanelUserAssignmentId)
        /// </summary>
        public int ReviewerUserId { get; set; }
        ///<summary>
        /// Displays comment modal window link if reviewer has a comment
        ///</summary>
        public IHtmlString commentLink 
        {
            get { return (hasComment) ? new HtmlString("<a href='#" + ReviewerId + "' data-toggle='modal'><img src='" + Constants.revCommentImg + "' alt='" + Constants.altRevCommentImg + "' /></a>") : new HtmlString("&nbsp;");}
        }
        ///<summary>
        /// Displays COI flag if reviewer is a COI
        ///</summary>
        public IHtmlString coiImage
        {
            get { return (isCoi) ? new HtmlString("<img src='" + Constants.coiImg + "' alt='" + Constants.altCoiImg + "'/>") : new HtmlString("&nbsp;"); }
        }
        /// <summary>
        /// Whether or not a critique exists
        /// </summary>
        public bool IsCritique
        {
            get 
            {
                return critiqueType == CritiqueRequiredType.Critique;
            }
        }
        /// <summary>
        /// Whether or not a revised critique exists
        /// </summary>
        public bool IsCritiqueRevised
        {
            get
            {
                return critiqueType == CritiqueRequiredType.RevisedCritique;
            }
        }
        /// <summary>
        /// Is abstained
        /// </summary>
        public bool IsAbstain
        {
            get { return Scores.Count > 0 && Scores.Values.All(x => x == "A"); }
        }
        /// <summary>
        /// Whether or not a critique is missing
        /// </summary>
        public bool IsCritiqueMissing
        {
            get
            {
                return critiqueType == CritiqueRequiredType.CritiqueMissing;
            }
        }
        /// <summary>
        /// True if the reviewer has scored.
        /// </summary>
        public bool HasScored
        {
            get
            {
                return Scores.Any() && !isCoi;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Determines value for data-scoreReceived attribute
        /// </summary>
        /// <returns>"1" if the reviewer has scored; "0" if not.</returns>
        public string IsScoreReceived()
        {
            return (this.HasScored) ? "1" : "0";
        }
        #endregion
    }

    /// <summary>
    /// Reviewer comments for a specific application
    /// </summary>
    public class ReviewerComments
    {
        #region Constructors

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ReviewerComments() { }
        /// <summary>
        /// Construct & populate the view's comment object from a business layer IReviewerComment object
        /// </summary>
        internal ReviewerComments(IReviewerCommentFacts value) 
        { 
            this.ReviewerId = value.ProgramPartId;
            this.ApplicationId = value.ApplicationId;
            this.ReviewerComment = value.Comment;
            this.ReviewerName = value.ReviewerName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Reviewers unique identifier
        /// </summary>
        public int ReviewerId { get; private set; }
        /// <summary>
        /// The application id the comment was left for
        /// </summary>
        public string ApplicationId { get; private set; }
        /// <summary>
        /// Reviewers comment for a specific application
        /// </summary>
        public string ReviewerComment { get; private set; }
        ///<summary>
        /// Reviewers Name
        ///</summary>
        public string ReviewerName { get; private set; }

        #endregion
    }
    public class UserApplicationComments
    {
        #region Constants
        private class Constants 
        {
            public const string deleteImg = "../../Content/themes/base/images/RevisedCrit.png";
            public const string deleteImgAlt = "Delete Comment";
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private UserApplicationComments() { }
        /// <summary>
        /// Construct & populate the view's comment object from a business layer IUserApplicationComment object
        /// </summary>
        internal UserApplicationComments(IUserApplicationCommentFacts value)
        {
            this.CommentID = value.CommentID;
            this.UserID = value.UserID;
            this.ApplicationID = value.ApplicationID;
            this.UserFullName = value.UserFullName;
            this.Comments = value.Comments;
            this.ModifiedDate = value.ModifiedDate;
            this.CommentLkpId = value.CommentLkpId;
            this.CommentLkpDescription = value.CommentLkpDescription;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Comment identifier
        /// </summary>
        public int CommentID { get; private set; }
        /// <summary>
        /// User identifier
        /// </summary>
        public int UserID { get; private set; }
        /// <summary>
        /// Application identifier
        /// </summary>
        public string ApplicationID { get; private set; }
        /// <summary>
        /// Full name of comment author
        /// </summary>
        public string UserFullName { get; private set; }
        /// <summary>
        /// User comment for a particular application
        /// </summary>
        [Required(ErrorMessage = "Note is required")]
        public string Comments { get; private set; }
        /// <summary>
        /// Date the comment was last modified
        /// </summary>
        public string ModifiedDate { get; private set; }
        /// <summary>
        /// Comment lookup ID
        /// </summary>
        public int CommentLkpId { get; set; }
        /// <summary>
        /// Comment lookup description
        /// </summary>
        public string CommentLkpDescription { get; set; }
        /// <summary>
        /// Form names for each editable comment
        /// </summary>
        public string CommentFormName
        {
            get { return "CommentEdit" + CommentID; }
        }
        #endregion
    }

    public class CommentLookupTypes
    {
        #region Constructors

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private CommentLookupTypes() { }
        /// <summary>
        /// Construct & populate the view's comment object from a business layer IReviewerComment object
        /// </summary>
        internal CommentLookupTypes(ICommentLookupTypes value) 
        {
            this.CommentTypeId = value.CommentTypeId;
            this.CommentTypeDescription = value.CommentTypeDescription;
        }

        #endregion

        #region Properties

        public int CommentTypeId { get; set; }
        public string CommentTypeDescription { get; set; }

        #endregion
    }
}
