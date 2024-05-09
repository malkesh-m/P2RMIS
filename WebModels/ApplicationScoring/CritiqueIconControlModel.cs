
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Results of the business rules controlling the viability of the icons on the Critique view
    /// </summary>
    public interface ICritiqueIconControlModel
    {
        /// <summary>
        /// Indicates if the user can Add or Edit comments
        /// </summary>
        bool CanAddEditComments { get; }
        /// <summary>
        /// Indicates if the user can View comments
        /// </summary>
        bool CanAViewComments { get;}
        /// <summary>
        /// Indicates if the Scorecard icon is enabled for this user
        /// </summary>
        bool IsScoreCardEnabled { get;}
        /// <summary>
        /// Indicates if the Rating summary icon is shown (always true)
        /// </summary>
        bool ShowRatingSummary { get; }
        /// <summary>
        /// Indicates if the Application icon is shown (always true)
        /// </summary>
        bool showApplication { get; }
        /// <summary>
        /// Indicates if the user is a reviewer 
        /// </summary>
        bool IsReviewer { get; }
        /// <summary>
        /// Indicates if the user is a client 
        /// </summary>
        bool IsClient { get; }
    }
    /// <summary>
    /// Results of the business rules controlling the viability of the icons on the Critique view
    /// </summary>
    public class CritiqueIconControlModel : ICritiqueIconControlModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="canAddEditComments">Can the user Add/Edit comments</param>
        /// <param name="canAViewComments">Can the user View comments</param>
        /// <param name="isScoreCardEnabled">Is the WScorecard icon enabled</param>
        /// <param name="isReviewer">Indicates if the user is a reviewer</param>
        public CritiqueIconControlModel(bool canAddEditComments, bool canAViewComments, bool isScoreCardEnabled, bool isReviewer, bool isClient)
        {
            this.CanAddEditComments = canAddEditComments;
            this.CanAViewComments = canAViewComments;
            this.IsScoreCardEnabled = isScoreCardEnabled;
            //
            //  the Rating Summary & Applications are always shown
            //
            this.ShowRatingSummary = true;
            this.showApplication = true;
            this.IsClient = isClient;
            this.IsReviewer = isReviewer;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Indicates if the user can Add or Edit comments
        /// </summary>
        public bool CanAddEditComments { get; private set; }
        /// <summary>
        /// Indicates if the user can View comments
        /// </summary>
        public bool CanAViewComments { get; private set; }
        /// <summary>
        /// Indicates if the Scorecard icon is enabled for this user
        /// </summary>
        public bool IsScoreCardEnabled { get; private set; }
        /// <summary>
        /// Indicates if the Rating summary icon is shown (always true)
        /// </summary>
        public bool ShowRatingSummary { get; private set; }
        /// <summary>
        /// Indicates if the Application icon is shown (always true)
        /// </summary>
        public bool showApplication { get; private set; }
        /// <summary>
        /// Indicates if the user is a reviewer 
        /// </summary>
        public bool IsReviewer { get; private set; }
        /// <summary>
        /// Indicates if the user is a client 
        /// </summary>
        public bool IsClient { get; private set; }
        #endregion
    }
}
