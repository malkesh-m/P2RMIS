using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Files;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.FileService
{
    /// <summary>
    /// FileService provides services related to storing and accessing file objects from the database or 
    /// disk.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Retrieves the contract as a word document.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>Contract word document</returns>
        byte[] RetrieveWordContract(int panelUserAssignmentId);
        /// <summary>
        /// Retrieve an ApplicationAbstract from the database or directory location.
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="PanelManagementService">Reference to the PanelManagementService</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown if abstract file is not on disk or description in database.</exception>
        AbstractFileModel RetrieveAbstractFile(int applicationId, IPanelManagementService PanelManagementService);
        /// <summary>
        /// Retrieve a list of application files associated with this application.
        /// <param name="appId">the application to obtain the list of available files for</param>
        /// <returns>Enumerable list of file information.</returns>
        Container<IFileInfoModel> GetFileInfo(int appId);
        /// <summary>
        /// Contructs an application file path and name
        /// </summary>
        /// <param name="logNumber">The aapplication log number</param>
        /// <param name="fileSuffix">The file suffix</param>
        /// <param name="folder">The file folder</param>
        /// <returns></returns>
        string GetApplicationFilePathAndName(string logNumber, string fileSuffix, string folder);
        /// <summary>
        /// Retrieves the list of email templates for the specified ProgramYear entity identifier.
        /// </summary>
        /// <param name="programYearEntityId">ProgramYear entity identifier</param>
        /// <returns>Container of ITemplateFileInfoModel models that list the available templates</returns>
        Container<ITemplateFileInfoModel> RetriveEmailTemplatesList(int? programYearEntityId, int panelApplicationId);
        /// <summary>
        /// Gets the application file URL.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns>Contents of the application file</returns>
        byte[] GetApplicationFile(string logNumber, string fileSuffix);
    }
}
