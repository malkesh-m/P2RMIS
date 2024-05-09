using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Dashboard page
    /// </summary>
    public class LoginVerificationMessageViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the LoginVerificationMessageViewModel class.
        /// </summary>
        public LoginVerificationMessageViewModel()
        {
            VerificationMessages = new List<string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of verification/registration messages to display on the dashboard
        /// </summary>
        public List<string> VerificationMessages { get; set; }
        #endregion
    }
}