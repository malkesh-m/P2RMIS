using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using DataLayer = Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views.SessionPanelDetails
{
    /// <summary>
    /// Business Layer representation of a Application details.
    /// </summary>
    public class ViewPanelDetails
    {
        #region Constants
        /// <summary>
        /// Mechanism details constants.
        /// </summary>
        public class Constants
        {
            /// <summary>
            /// Status values
            /// </summary>
            public class Status
            {
                public const string Active = "Active";
                public const string Incomplete = "Incomplete";
                public const string Pending = "Pending";
                public const string Expedite = "Expedited";
                public const string Complete = "Complete";
                public const string Unknown = "Unknown";
                public const string Disapprove = "Disapproved";
               
            }
            public const int DefaultOrder = 0;
            public const bool DefaultDisapproved = false;
            public const bool DefaultTriaged = false;
            public const int DefaultReviewTypeId = 0;
            public const int DefaultActualScores = 0;
            public const int DefaultPossibleScores = 0;
            public const decimal DefaultAverageOE = 0;
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor.  Populate from the DataLayer's ViewPanelDetails
        /// </summary>
        /// <param name="viewPanelDetail">Entity framework representation of the Mechanism details.</param>
        public ViewPanelDetails(DataLayer.uspViewPanelDetails_Result viewPanelDetail)
        {
            this.Order = Convert.ToInt32(viewPanelDetail.Order.GetValueOrDefault(Constants.DefaultOrder));
            this.ApplicationId = viewPanelDetail.ApplicationId;
            this.AwardMechanism = viewPanelDetail.AwardShortDescription;
            this.Average = viewPanelDetail.AverageOE;
            this.PanelId = viewPanelDetail.PanelId;
            this.ApplicationTitle = viewPanelDetail.ApplicationTitle;
            this.PIInstitution = viewPanelDetail.PIInstitution;
            this.Disapproved = viewPanelDetail.Disapproved.GetValueOrDefault(Constants.DefaultDisapproved);
            this.Triaged = viewPanelDetail.Triaged.GetValueOrDefault(Constants.DefaultTriaged);
            this.AwardShortDescription = viewPanelDetail.AwardShortDescription;
            this.ReviewTypeId = viewPanelDetail.ReviewTypeId.GetValueOrDefault(Constants.DefaultReviewTypeId);
            this.PossibleScores = viewPanelDetail.PossibleScores.GetValueOrDefault(Constants.DefaultPossibleScores);
            this.ActualScores = viewPanelDetail.ActualScores.GetValueOrDefault(Constants.DefaultActualScores);
            this.AverageOE = ViewHelpers.P2rmisRound(viewPanelDetail.AverageOE.GetValueOrDefault(Constants.DefaultAverageOE));
            this.ActiveApplication = viewPanelDetail.ActiveApplication;
            this.CommentsCount = viewPanelDetail.CommentsCount;
            this.COIs = viewPanelDetail.COIs;
            this.AssignmentNames = ViewHelpers.SplitDelimitedString(viewPanelDetail.AssignmentNames);
            this.AssignmentPartIds = ViewHelpers.SplitDelimitedString(viewPanelDetail.AssignmentPartIds);
            this.AssignmentSlots = ViewHelpers.SplitDelimitedString(viewPanelDetail.AssignmentSlots);
            this.AssignmentTypes = ViewHelpers.SplitDelimitedString(viewPanelDetail.AssignmentTypes);
            this.AssignmentCritiqueLinkAvailable = ViewHelpers.SplitDelimitedString(viewPanelDetail.AssignmentCritiqueAvailable);
            this.PanelApplicationId = viewPanelDetail.PanelApplicationId;
            this.ReviewStatusId = viewPanelDetail.ReviewStatusId;
            this.ReviewStatusName = (this.Triaged)? MessageService.TriagedLabel : viewPanelDetail.ReviewStatusName;
            this.ApplicationReviewStatusId = viewPanelDetail.ApplicationReviewStatusId;
            ///
            /// Construct the principal investigator name
            /// 
            this._principalInvestigator = ViewHelpers.ConstructName(viewPanelDetail.LastName, viewPanelDetail.FirstName);
            this.ApplicationIdentifier = viewPanelDetail.ApplicationIdentifier ?? 0;
            this.AdminNotesCount = viewPanelDetail.AdminNotesCount ?? 0;
            this.IsUserCoi = viewPanelDetail.UserCoi ?? false;
            this.IsAdjectival = viewPanelDetail.Adjectival ?? false;
        }
        

        #endregion
        #region Properties
        /// <summary>
        /// Application order;
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Application identifier/
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Active Application
        /// </summary>
        public bool? ActiveApplication { get; set; }
        /// <summary>
        /// Name of principal investigator
        /// </summary>
        private string _principalInvestigator;
        public string PrincipalInvestigator { get { return this._principalInvestigator; } }
        /// <summary>
        /// Award mechanism.
        /// </summary>
        public string AwardMechanism { get; set; }
        /// <summary>
        /// Average overall score
        /// </summary>
        public decimal? Average { get; set; }
        /// <summary>
        /// Owning Panel identifier.
        /// </summary>
        public int? PanelId { get; set; }
        /// <summary>
        /// Applications full title.
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// PI Institution
        /// </summary>
        public string PIInstitution { get; set; }
        /// <summary>
        /// Disapproved indicator.
        /// </summary>
        public bool Disapproved { get; set; }
        /// <summary>
        /// Triaged indicator.
        /// </summary>
        public bool Triaged { get; set; }
        /// <summary>
        /// Award short description
        /// </summary>
        public string AwardShortDescription { get; set; }
        /// <summary>
        /// Award's review type.
        /// </summary>
        public int ReviewTypeId { get; set; }
        /// <summary>
        /// Application's possible scores
        /// </summary>
        public long PossibleScores { get; set; }
        /// <summary>
        /// Application's actual scores.
        /// </summary>
        public long ActualScores { get; set; }
        /// <summary>
        /// Applications average -----
        /// </summary>
        public decimal AverageOE { get; set; }
        /// <summary>
        /// Count of comments by application id
        /// </summary>
        public int? CommentsCount { get; set; }
        /// <summary>
        /// Line break delimited list to be displayed as is in panel details table
        /// </summary>
        public string COIs { get; set; }
        /// <summary>
        /// Field containing assigned reviewers in HTML markup, to be displayed as is in panel details table
        /// </summary>
        public string[] AssignmentSlots { get; set; }
        /// <summary>
        /// Comma delimited list of assigned reviewer assignment types to application
        /// </summary>
        public string[] AssignmentTypes { get; set; }
        /// <summary>
        /// Comma delimited list of assigned reviewer's name to application
        /// </summary>
        public string[] AssignmentNames { get; set; }
        /// <summary>
        /// Comma delimited list of assigned reviewer's participant Ids to application
        /// </summary>
        public string[] AssignmentPartIds { get; set; }
        /// <summary>
        /// Array of whether the critique should be linked to. Defined in the data layer as true if it has been submitted and critique deadline has been passed, otherwise false.
        /// </summary>
        public string[] AssignmentCritiqueLinkAvailable { get; set; }
        /// <summary>
        /// The identifier for an application review status assignment
        /// </summary>
        public int? ApplicationReviewStatusId { get; set; }
        /// <summary>
        /// Display name for the review status
        /// </summary>
        public string ReviewStatusName { get; set; }
        /// <summary>
        /// Identifier for a review status
        /// </summary>
        public int? ReviewStatusId { get; set; }
        /// <summary>
        /// Identifier for a panel application assignment
        /// </summary>
        public int? PanelApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the admin notes count.
        /// </summary>
        /// <value>
        /// The admin notes count.
        /// </value>
        public int AdminNotesCount { get; set; }

        public bool IsUserCoi { get; set; }
        public bool IsAdjectival { get; set; }
        #endregion
        #region Helpers


        #endregion
    }
}
