using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Model describing an Abstract file
    /// </summary>
    public interface IAbstractFileModel
    {
        /// <summary>
        /// Abstract file type
        /// </summary>
        AbstractFileType Type { get; }
        /// <summary>
        /// Abstract file text/PDF
        /// </summary>
        Byte[] AbstractText { get; }
    }
    /// <summary>
    /// Model describing an Abstract file
    /// </summary>
    public class AbstractFileModel: IAbstractFileModel
    {
        /// <summary>
        /// Identification of the Abstract file type
        /// </summary>
        #region Construction & Set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Type of the abstract</param>
        /// <param name="abstractText">The abstract</param>
        public AbstractFileModel(AbstractFileType type, byte[] abstractText)
        {
            this.Type = type;
            this.AbstractText = abstractText;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Abstract file type
        /// </summary>
        public AbstractFileType Type { get; private set; }
        /// <summary>
        /// Abstract file text/PDF
        /// </summary>
        public Byte[] AbstractText { get; private set; }
        #endregion
    }
    /// <summary>
    /// Abstract file types
    /// </summary>
    public enum AbstractFileType
    {
        /// <summary>
        /// Default value
        /// </summary>
        Default = 0,
        /// <summary>
        /// Abstract file is text
        /// </summary>
        TextType = 1,
        /// <summary>
        /// Abstract file is PDF
        /// </summary>
        PdfType = 2
    }
}
