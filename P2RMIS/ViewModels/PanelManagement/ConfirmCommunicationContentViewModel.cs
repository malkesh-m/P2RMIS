using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ConfirmCommunicationContentViewModel
    {
        #region Constructor
        public ConfirmCommunicationContentViewModel()
        {
            List<string> To = new List<string>();
            List<string> Cc = new List<string>();
            List<string> FileNames = new List<string>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The 'To' list of email address
        /// </summary>
        public List<string> To { get; set; }
        /// <summary>
        /// The 'From' email address
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// The 'Cc' list of email addresses
        /// </summary>
        public List<string> Cc { get; set; }
        /// <summary>
        /// The semi colin delinerated 'Bcc' email addresses
        /// </summary>
        public string Bcc { get; set; }
        /// <summary>
        /// The subject of the email
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The body of the email
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Attached file names
        /// </summary>
        public List<string> FileNames { get; set; }
        /// <summary>
        /// The total size of the attached files
        /// </summary>
        public int TotalSize { get; set; }
        /// <summary>
        /// The attached files
        /// </summary>
        List<HttpPostedFileBase> files { get; set; }
        /// <summary>
        /// The associated session panel identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        #endregion

        #region Helpers
        /// <summary>
        /// Formats a list of string into a semi colin delinerated string
        /// </summary>
        /// <param name="list">The list of strings</param>
        /// <returns>A semi colin delinerated string</returns>
        public string FormatCommunicationsList(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            list.ForEach(x => sb.AppendFormat("{0};", x));

            // remove trailing ';'
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }
        //public string FormatTotalAttachmentSize()
        //{
        //    int size = 0;

            
        //}
        #endregion
    }
}