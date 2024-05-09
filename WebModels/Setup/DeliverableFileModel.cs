

namespace Sra.P2rmis.WebModels.Setup
{
    
    /// <summary>
    /// Model representing a deliverable file and contents
    /// </summary>
    public class DeliverableFileModel : IDeliverableFileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliverableFileModel"/> class.
        /// </summary>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="deliverableName">Name of the deliverable.</param>
        /// <param name="deliverableFileRaw">The deliverable file raw.</param>
        /// <param name="deliverableOutputFormat">The deliverable output format.</param>
        public DeliverableFileModel(string programAbbreviation, string fiscalYear, int? receiptCycle, string deliverableName, byte[] deliverableQcFile)
        {
            ProgramAbbreviation = programAbbreviation;
            FiscalYear = fiscalYear;
            ReceiptCycle = receiptCycle;
            DeliverableName = deliverableName;
            DeliverableQcFile = deliverableQcFile;
            DeliverableFileName = ReceiptCycle != null ?
                $"{fiscalYear}-{programAbbreviation}-{receiptCycle}-{deliverableName}.xlsx" :
                $"{fiscalYear}-{programAbbreviation}-{deliverableName}.xlsx";
        }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation { get; internal set; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; internal set; }

        /// <summary>
        /// Gets or sets the receipt cycle.
        /// </summary>
        /// <value>
        /// The receipt cycle.
        /// </value>
        public int? ReceiptCycle { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the deliverable.
        /// </summary>
        /// <value>
        /// The name of the deliverable.
        /// </value>
        public string DeliverableName { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the deliverable file.
        /// </summary>
        /// <value>
        /// The name of the deliverable file.
        /// </value>
        public string DeliverableFileName { get; internal set; }

        /// <summary>
        /// Gets or sets the deliverable file raw format from database.
        /// </summary>
        /// <value>
        /// The deliverable file raw.
        /// </value>
        public byte[] DeliverableQcFile { get; internal set; }
    }

    /// <summary>
    /// Interface representing a deliverable file and contents
    /// </summary>
    public interface IDeliverableFileModel
    {
        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        string ProgramAbbreviation { get; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; }

        /// <summary>
        /// Gets or sets the receipt cycle.
        /// </summary>
        /// <value>
        /// The receipt cycle.
        /// </value>
        int? ReceiptCycle { get; }

        /// <summary>
        /// Gets or sets the name of the deliverable.
        /// </summary>
        /// <value>
        /// The name of the deliverable.
        /// </value>
        string DeliverableName { get; }

        /// <summary>
        /// Gets or sets the name of the deliverable file.
        /// </summary>
        /// <value>
        /// The name of the deliverable file.
        /// </value>
        string DeliverableFileName { get; }

        /// <summary>
        /// Gets or sets the deliverable file raw format from database.
        /// </summary>
        /// <value>
        /// The deliverable file raw.
        /// </value>
        byte[] DeliverableQcFile { get; }
    }
}
