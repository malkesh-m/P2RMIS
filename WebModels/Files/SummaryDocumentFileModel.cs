using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Files
{
    public interface ISummaryDocumentFileModel
    {
        /// <summary>
        /// Gets or sets the name of the log number.
        /// </summary>
        /// <value>
        /// The name of the log number.
        /// </value>
        string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; set; }

        /// <summary>
        /// Gets or sets the receipt cycle.
        /// </summary>
        /// <value>
        /// The receipt cycle.
        /// </value>
        int ReceiptCycle { get; set; }

        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        /// <value>
        /// The content of the file.
        /// </value>
        byte[] FileContent { get; set; }
    }

    /// <summary>
    /// Domain model to store summary document file information
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.Files.ISummaryDocumentFileModel" />
    public class SummaryDocumentFileModel : ISummaryDocumentFileModel
    {
        #region properties
        /// <summary>
        /// Gets or sets the name of the log number.
        /// </summary>
        /// <value>
        /// The name of the log number.
        /// </value>
        public string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }

        /// <summary>
        /// Gets or sets the receipt cycle.
        /// </summary>
        /// <value>
        /// The receipt cycle.
        /// </value>
        public int ReceiptCycle { get; set; }

        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        /// <value>
        /// The content of the file.
        /// </value>
        public byte[] FileContent { get; set; }
        #endregion
    }
}
