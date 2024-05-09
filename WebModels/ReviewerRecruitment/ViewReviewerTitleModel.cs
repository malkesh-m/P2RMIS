namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    #region Interface
    /// <summary>
    /// View model to host information for ViewReviewers title line.
    /// </summary>
    public interface IViewReviewerTitleModel
    {
        /// <summary>
        /// SRO First Name
        /// </summary>
        string SroFirstName { get; }
        /// <summary>
        /// SRO Last Name
        /// </summary>
        string SroLastName { get; }
        /// <summary>
        /// RTA First Name
        /// </summary>
        string RtaFirstName { get; }
        /// <summary>
        /// RTA Last Name
        /// </summary>
        string RtaLastName { get; }
    }
    #endregion
    #region Web Model
    /// <summary>
    /// View model to host information for ViewReviewers title line.
    /// </summary>
    public class ViewReviewerTitleModel : IViewReviewerTitleModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sroFirstName">SRO first name</param>
        /// <param name="sroLastName">SRO last name</param>
        /// <param name="rtaFirstName">RTA first name</param>
        /// <param name="rtaLastName">RTA last name</param>
        public ViewReviewerTitleModel(string sroFirstName, string sroLastName, string rtaFirstName, string rtaLastName)
        {
            this.SroFirstName = sroFirstName;
            this.SroLastName = sroLastName;
            this.RtaFirstName = rtaFirstName;
            this.RtaLastName = rtaLastName;
        }
        #endregion
        /// <summary>
        /// SRO First Name
        /// </summary>
        public string SroFirstName { get; private set; }
        /// <summary>
        /// SRO Last Name
        /// </summary>
        public string SroLastName { get; private set; }
        /// <summary>
        /// RTA First Name
        /// </summary>
        public string RtaFirstName { get; private set; }
        /// <summary>
        /// RTA Last Name
        /// </summary>
        public string RtaLastName { get; private set; }
    } 
    #endregion
}
