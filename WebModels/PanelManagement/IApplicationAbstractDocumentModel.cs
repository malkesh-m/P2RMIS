namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for getting the text of the abstract from the database.
    /// </summary>
    public interface IApplicationAbstractDocumentModel
    {
        /// <summary>
        /// the text of the abstract
        /// </summary>
        string AbstractText { get; set; }
    }
}
