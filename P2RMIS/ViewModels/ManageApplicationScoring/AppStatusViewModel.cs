using Sra.P2rmis.Bll;

namespace Sra.P2rmis.Web.UI.Models
{
    public class AppStatusViewModel
    {
        #region Constants
        public class AppStatusActions
        {
            public const string Activate = "Activate";
            public const string Deactivate = "Deactivate";
            public const string Triage = "Triage";
            public const string Disapprove = "Disapprove";
            public const string Champion = "Champion";
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AppStatusViewModel()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// the panel application id
        /// </summary>
        public int PanelAppId { get; set; }
        /// <summary>
        /// the current review status the panel application is in
        /// </summary>
        public int CurrentStatus { get; set; }
        /// <summary>
        /// the action the user wants to perform on the application
        /// </summary>
        public string ChangeStatusTo { get; set; }
        /// <summary>
        /// the applications log number
        /// </summary>
        public string ApplicationId { get; set; }

        #region App Statuses
        /// <summary>
        /// Disapproved review status
        /// </summary>
        public int Disapproved
        {
            get { return LookupService.LookupReviewStatusDisapproved; }
        }
        /// <summary>
        /// Triaged Review Status
        /// </summary>
        public int Triaged
        {
            get { return LookupService.LookupReviewStatusTriaged; }
        }
        /// <summary>
        /// Active Review Status
        /// </summary>
        public int Active
        {
            get { return LookupService.LookupReviewStatusActive; }
        }
        /// <summary>
        /// Ready to Score review status
        /// </summary>
        public int ReadyToScore
        {
            get { return LookupService.LookupReviewStatusFullReview; }
        }
        /// <summary>
        /// Scored review status
        /// </summary>
        public int Scored
        {
            get { return LookupService.LookupReviewStatusScored; }
        }
        /// <summary>
        /// Scoring review status
        /// </summary>
        public int Scoring
        {
            get { return LookupService.LookupReviewStatusScoring; }
        }
        #endregion        

        #endregion
        #region Helpers
        #endregion
    }
}