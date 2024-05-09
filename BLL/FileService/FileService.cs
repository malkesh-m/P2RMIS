using Sra.P2rmis.Bll.ModelBuilders.FileServices;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.WebModels.Files;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Entity = Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.FileService
{
    /// <summary>
    /// FileService provides services related to storing and accessing file objects from the database or 
    /// disk.
    /// </summary>
    public class FileService: ServerBase, IFileService
    {
        #region Constants
        /// <summary>
        /// The valid extension for a "PDF" file.  (case is ignored)
        /// </summary>
        private static readonly string _pdfFileExtension = ".pdf";
        /// <summary>
        /// Seek location for start of file.
        /// </summary>
        private static readonly int _startOfFile = 0;
        /// <summary>
        /// The report server string representing .doc format
        /// </summary>
        private static readonly string _rsDocFormat = "WORD";
        /// <summary>
        /// The report server string representing .docx format
        /// </summary>
        private static readonly string _rsDocxFormat = "WORDOPENXML";
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public FileService()
        {
            UnitOfWork = new Entity.UnitOfWork();
        }
        #endregion
        #region Resume Services
        /// <summary>
        /// Construct a resume file name according to convention 
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="version">Version number</param>
        /// <returns>Name of file for a user's resume</returns>
        public static string ResumeName(string firstName, string lastName, int version, string extension)
        {
            string versionFormat = ConfigManager.UserResumeVersionFormatSpecification;

            return string.Format("{0}_{1}_{2}.{3}", firstName, lastName, string.Format(versionFormat, version), extension);
        }
        #endregion
        #region General File Services
        /// <summary>
        /// Determines if the stream is a Pdf file.  Validates if the file is
        /// an actual Pdf file by signature and the file has an extension of ".PDF".
        /// 
        /// The stream start is located (by seek to origin); the first four bytes 
        /// are read then the stream is rewound to the beginning.
        /// </summary>
        /// <param name="stream">file stream containing the document</param>
        /// <returns>True if the file has a signature for a "PDF" file; false otherwise</returns>
        public static bool IsPdfFile(Stream stream)
        {
            bool result = false;
            if (stream != null)
            {
                //
                // Seek to the start of the stream.
                //
                stream.Seek(_startOfFile, SeekOrigin.Begin);
                //
                // Now read the first four bytes (or whatever the size of the
                // signature.
                //
                byte[] buffer = new byte[PdfSignatureSize];
                BinaryReader reader = new BinaryReader(stream);
                reader.Read(buffer, _startOfFile, PdfSignatureSize);
                //
                // And rewind to the beginning and check the signature.
                //
                stream.Seek(_startOfFile, SeekOrigin.Begin);

                result = buffer.SequenceEqual(PdfSignature);
            }
            return result;
        }
        /// <summary>
        /// Compares the file extension to the approved extension for a pdf file. 
        /// (Case is ignored)
        /// </summary>
        /// <param name="fileName">File name.  Name is not available from stream</param>
        /// <returns>True if the file has an extension of ".PDF"; false otherwise</returns>
        public static bool IsPdfFile(string fileName)
        {
            return ((fileName != null) && (fileName.EndsWith(_pdfFileExtension, StringComparison.CurrentCultureIgnoreCase)));
        }
        /// <summary>
        /// Checks if the stream size is less than the maximum size.
        /// </summary>
        /// <param name="stream">file stream containing the document</param>
        /// <param name="size">Maximum file size</param>
        /// <returns>True if file size is less than or equal to the size parameter; false otherwise</returns>
        public static bool IsFileSizeCorrect(Stream stream, long size)
        {
            return ((stream != null) && (stream.Length <= size));
        }
        /// <summary>
        /// Checks if the stream size is less than the maximum size.
        /// </summary>
        /// <param name="stream">file stream containing the document</param>
        /// <param name="size">Maximum file size</param>
        /// <returns>True if file size is less than or equal to the size parameter; false otherwise</returns>
        public static bool IsFileSizeCorrect(Stream stream)
        {
            return IsFileSizeCorrect(stream, ConfigManager.UserResumeMaximuSize * 1000000);
        }
        /// <summary>
        /// Retrieve a list of application files associated with this application.
        /// <param name="appId">the application to obtain the list of available files for</param>
        /// <returns>container of file information.</returns>
        public Container<IFileInfoModel> GetFileInfo(int appId)
        {
            ValidateInteger(appId, "FileService.GetFileInfo", "appId");
            Container<IFileInfoModel> container = new Container<IFileInfoModel>();

            container.ModelList = GetClientFileInfoList(appId);

            return container;
        }
        /// <summary>
        /// Retrieve a list of application files associated with this application.
        /// <param name="appId">the application to obtain the list of available files for</param>
        /// <returns>Enumerable list of file.</returns>
        internal List<IFileInfoModel> GetClientFileInfoList(int appId)
        {
            List<IFileInfoModel> fileList = new List<IFileInfoModel>();

            var possibleTypes = UnitOfWork.SummaryManagementRepository.GetClientFileTypes(appId).ModelList.ToList();
            var logNo = possibleTypes.FirstOrDefault()?.LogNumber;
            if (logNo == null)
                throw new ArgumentException($"No possible file types configured for application {appId}");
            //append _ after the log number to avoid getting associated files that are no for this particular log number
            string logNoPrefix = $"{logNo}_";
            var existingTypes = S3Service.ListAssociatedFiles(logNoPrefix, ConfigManager.S3AppFolderName);

            foreach (var item in existingTypes)
            {
                var itemInfo = possibleTypes.FirstOrDefault(x => item.Item1.EndsWith($"{x.LogNumber}{x.FileSuffix}"));
                if (itemInfo != null && item.Item2 > 0)
                {
                    FileInfoModel info = new FileInfoModel()
                    {
                        FileSize = item.Item2,
                        ApplicationId = appId,
                        FileInfo = itemInfo

                    };
                    fileList.Add(info);
                }
            }
            return fileList;
        }
        /// <summary>
        /// Gets the application file.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns>Contents of the application file</returns>
        public byte[] GetApplicationFile(string logNumber, string fileSuffix)
        {
            ValidateString(logNumber, $"{nameof(FileService)}.{nameof(GetApplicationFile)}", nameof(logNumber));
            ValidateString(fileSuffix, $"{nameof(FileService)}.{nameof(GetApplicationFile)}", nameof(fileSuffix));

            var fileContents = S3Service.GetFileContents(ConstructAppKey(logNumber, fileSuffix), ConfigManager.S3AppFolderName);
            return fileContents;
        }
        #endregion
        #region Report File Services

        /// <summary>
        /// Retrieves the contract as a word document.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>Contract word document</returns>
        public byte[] RetrieveWordContract(int panelUserAssignmentId)
        {
            //Get report data
            var reportFile = GetContractReportName(panelUserAssignmentId);
            //Construct report URL
            var reportFullUrl = GetContractReportFullUrl(panelUserAssignmentId, reportFile);
            //Use web client to get file
            var returnFile = DownloadReportFile(reportFullUrl);
            //Return file to user
            return returnFile;
        }
        #endregion
        #region Abstract File Services
        /// <summary>
        /// Retrieve an ApplicationAbstract from the database or directory location.
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="PanelManagementService">Reference to the PanelManagementService</param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown if abstract file is not on disk or description in database.</exception>
        public AbstractFileModel RetrieveAbstractFile(int applicationId, IPanelManagementService PanelManagementService)
        {
            ValidateInt(applicationId, "FileService.RetrieveAbstractFile", "applicationId");

            AbstractFileModel resultM = null;
            byte[] result = new byte[2];
            bool fileExists = false;
            //
            // An application abstract can be either stored in the database as text
            // or stored as a PDF file in a configured location.  First figure out
            // which case we are dealing with.
            //
            if (PanelManagementService.IsAbstractInDatabase(applicationId))
            {
                //
                // This is the simple case.  All we have to do is retrieve the text from the database
                // and return it.
                //
                Entity.Application applicationEntity = UnitOfWork.ApplicationRepository.GetByID(applicationId);
                List<KeyValuePair<string, string>> appTexts = new List<KeyValuePair<string, string>>();
                applicationEntity.ApplicationTexts.Where(x => x.AbstractFlag == true).ToList().ForEach(y =>
                    appTexts.Add(new KeyValuePair<string, string>(y.ClientApplicationTextType?.TextType, y.BodyText)));
                var htmlContent = HtmlServices.GetContentsWithTitle(appTexts);
                result = Encoding.Default.GetBytes(htmlContent);
                resultM = new AbstractFileModel(AbstractFileType.TextType, result);
            }
            //
            // Well it was not in the database so it must reside on disk.  Or so we think.
            //
            else
            {
                //
                // We go to the database to retrieve information about how the file name is constructed
                //
                IEnumerable<IApplicationFileModel> collection = UnitOfWork.SummaryManagementRepository.GetClientAbstractFileType(applicationId);


                //
                // Loop through the configured abstract files and try to retreive.  Note this avoids using a list
                // operation to save on cost. This will only retreive one file for now until the UI support multiple
                //
                foreach (IApplicationFileModel model in collection)
                {
                    if (!fileExists)
                    {
                        try
                        {

                            result = S3Service.GetFileContents(ConstructAbstractAppKey(model.LogNumber, model.FileSuffix), ConfigManager.S3AppFolderName);
                            fileExists = true;
                        }
                        catch (Exception ex)
                        {
                            //avoid throwing error if file could not be retrieved and try other abstract combinations
                        }
                    }
                }
                //
                // Now that we have a path, check to make sure the file is there & then read it
                //
                CheckFileExists(fileExists);

                resultM = new AbstractFileModel(AbstractFileType.PdfType, result);
            }

            return resultM;
        }
        #endregion

        #region Application File Services
        /// <summary>
        /// Constructs an application file path and name
        /// </summary>
        /// <param name="logNumber">The application log number</param>
        /// <param name="fileSuffix">The file suffix</param>
        /// <param name="folder">The file folder</param>
        /// <returns></returns>
        public string GetApplicationFilePathAndName(string logNumber, string fileSuffix, string folder)
        {
            ValidateGetApplicationFilePathAndNameParameters(logNumber, fileSuffix, folder);

            string path = BllHelper.GetPhysicalFilePath(logNumber, fileSuffix, folder);
            return path;
        }
        #endregion
        #region Email Template Services
        /// <summary>
        /// Retrieves the list of email templates for the specified ProgramYear entity identifier.
        /// </summary>
        /// <param name="programYearEntityId">ProgramYear entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>Container of ITemplateFileInfoModel models that list the available templates</returns>
        public Container<ITemplateFileInfoModel> RetriveEmailTemplatesList(int? programYearEntityId, int sessionPanelId)
        {
            ValidateInt(sessionPanelId, FullName(nameof(FileService), nameof(RetriveEmailTemplatesList)), nameof(sessionPanelId));

            var builder = new AvailableEmailTemplateModelBuilder(UnitOfWork, programYearEntityId, sessionPanelId);
            builder.BuildContainer();
            return builder.Results;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Signature of a PDF file.
        /// </summary>
        private static byte[] _pdfSignature = new byte[] { 0x25, 0x50, 0x44, 0x46 };
        /// <summary>
        /// PDF file signature
        /// </summary>
        /// <remarks>
        //   Courtesy of http://www.filesignatures.net/index.php?page=search&search=25504446&mode=SIG
        /// </remarks>
        /// <returns>Byte array containing PDF file signature</returns>
        public static byte[] PdfSignature
        {
            get { return _pdfSignature; }
        }
        /// <summary>
        /// Length of the PDF signature
        /// </summary>
        public static int PdfSignatureSize
        {
            get { return _pdfSignature.Length; }
        }
        /// <summary>
        /// Downloads the report file.
        /// </summary>
        /// <param name="reportFullUrl">The report full URL.</param>
        /// <returns>Report file as byte data</returns>
        internal static byte[] DownloadReportFile(string reportFullUrl)
        {
            WebClient webClient = new WebClient
            {
                Credentials =
                    new NetworkCredential(ConfigManager.ReportUser, ConfigManager.ReportPassword,
                        ConfigManager.ReportDomain)
            };
            //Call report URL to get file
            var returnFile = webClient.DownloadData(reportFullUrl);
            return returnFile;
        }
        /// <summary>
        /// Gets the contract report full URL.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="reportFile">The report file.</param>
        /// <returns>The full URL for calling the file from reporting services</returns>
        /// <exception cref="System.ArgumentException">ReportName supplied was invalid;reportFile</exception>
        /// <remarks>Could be refactored to be a generic report url builder if report parameters are passed in</remarks>
        internal static string GetContractReportFullUrl(int panelUserAssignmentId, string reportFile)
        {
            var reportFullUrl = string.Empty;
            if (!string.IsNullOrEmpty(reportFile))
            {
                reportFullUrl = String.Format("{0}?{1}{2}&rs:Command=Render&rs:Format={3}&PanelUserAssignmentId={4}",
                    ConfigManager.ReportServerUrl, ConfigManager.ReportPath, reportFile, _rsDocxFormat,
                    panelUserAssignmentId);
            }
            else
            {
                throw new ArgumentException("ReportName supplied was invalid", "reportFile");
            }
            return reportFullUrl;
        }

        /// <summary>
        /// Gets the name of the appropriate contract report.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>Name of the report file to generate contract</returns>
        /// <exception cref="System.ArgumentException">Invalid panelUserAssignmentId for contract</exception>
        internal string GetContractReportName(int panelUserAssignmentId)
        {
            //Get report info
            var clientId = UnitOfWork.PanelUserAssignmentRepository.GetClientId(panelUserAssignmentId);
            var doc = UnitOfWork.ClientRegistrationDocumentRepository.GetContractDocument(clientId);
            var reportFile = string.Empty;
            if (doc != null)
            {
                reportFile = doc.ReportFileName;
            }
            else
            {
                throw new ArgumentException("Invalid panelUserAssignmentId for contract", "panelUserAssignmentId");
            }
            return reportFile;
        }
        /// <summary>
        /// Validation for the existence of a IApplicationFileModel
        /// </summary>
        /// <param name="model">IApplicationFileModel model</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <exception cref="Exception">Thrown with message if the model is null.</exception>
        private void CheckModelExists(IApplicationFileModel model, int applicationId)
        {
            if (model == null)
            {
                string message = string.Format("Expected information about application {0} was not located in database", applicationId);
                throw new Exception(message);
            }
        }
        /// <summary>
        /// Validation for the existence of a file
        /// </summary>
        /// <param name="fileExists">Whether a file was found</param>
        /// <exception cref="Exception">Thrown with message if the file is non-existent</exception>
        private void CheckFileExists(bool fileExists)
        {
            if (!fileExists)
            {
                string message = string.Format("Abstract file could not be retrieved.");
                throw new Exception(message);
            }
        }
        /// <summary>
        /// Constructs the abstract key for s3 retrieval.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="suffix">The suffix (no underscore or file extension).</param>
        /// <returns>application key value</returns>
        private string ConstructAbstractAppKey(string logNumber, string suffix)
        {
            return $"{logNumber}{suffix}";
        }

        /// <summary>
        /// Constructs the application key for s3 retrieval.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="suffix">The suffix (underscore and file extension).</param>
        /// <returns>application key value</returns>
        private string ConstructAppKey(string logNumber, string suffix)
        {
            return $"{logNumber}{suffix}";
        }
        /// <summary>
        /// Validates the parameters to GetApplicationFilePathAndName
        /// </summary>
        /// <param name="logNumber">The application log number</param>
        /// <param name="fileSuffix">The file suffix</param>
        /// <param name="folder">The file folder</param>
        internal void ValidateGetApplicationFilePathAndNameParameters(string logNumber, string fileSuffix, string folder)
        {
            ValidateString(logNumber, "FileService.GetApplicationFilePathAndName", "logNumber");
            ValidateString(fileSuffix, "FileService.GetApplicationFilePathAndName", "fileSuffix");
            ValidateString(folder, "FileService.GetApplicationFilePathAndName", "folder");
        }
        #endregion
    }

}
