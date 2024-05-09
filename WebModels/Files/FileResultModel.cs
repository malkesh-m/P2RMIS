using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Files
{
    public interface IFileResultModel
    {
        /// <summary>
        /// The file content.
        /// </summary>
        byte[] FileContent { get; }
        /// <summary>
        /// Gets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        string LogNumber { get; }
        /// <summary>
        /// Gets the type of the MIME.
        /// </summary>
        /// <value>
        /// The type of the MIME.
        /// </value>
        string MimeType { get; }
    }

    /// <summary>
    /// Result model for returning file content via byte content
    /// </summary>
    public class FileResultModel : IFileResultModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileResultModel" /> class.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="mimeType">MIME type of file (ContentType).</param>
        /// <param name="fileContent">Content of the file.</param>
        public FileResultModel(string logNumber, string mimeType, byte[] fileContent)
        {
            LogNumber = logNumber;
            MimeType = mimeType;
            FileContent = fileContent;
        }
        #endregion
        #region Properties
        /// <summary>
        /// The file content.
        /// </summary>
        public byte[] FileContent { get; }
        /// <summary>
        /// Log number.
        /// </summary>
        public string LogNumber { get; }
        /// <summary>
        /// Gets the type of the MIME.
        /// </summary>
        /// <value>
        /// The type of the MIME.
        /// </value>
        public string MimeType { get; }

        
        #endregion
    }
}
