
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.CrossCuttingServices.FileServices
{
    /// <summary>
    /// Class provides useful methods for adding files to a user specific zip
    /// </summary>
    public class ZipService
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipService"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public ZipService(int userId)
        {
            UserId = userId;
        }

        #endregion

        #region Properties

        internal int UserId { get; }

        /// <summary>
        /// Gets the full path of the zip file.
        /// </summary>
        /// <value>
        /// The full path of the zip file.
        /// </value>
        public string ZipFullPath { get; private set; }

        /// <summary>
        /// Gets the root directory.
        /// </summary>
        /// <value>
        /// The root directory.
        /// </value>
        internal string RootDirectory => $"{ConfigManager.ReportStorageRoot}{UserId}";

        /// <summary>
        /// Gets the zip directory.
        /// </summary>
        /// <value>
        /// The zip directory.
        /// </value>
        internal string ZipSourceDirectory => $"{RootDirectory}\\source";

    #endregion
        #region Services
        /// <summary>
        /// Remove all the stored files.
        /// </summary>
        private void RemoveStoredFiles()
        {
            DirectoryInfo di = new DirectoryInfo(RootDirectory);
            if (di.Exists)
                di.Delete(true);
        }
        /// <summary>
        /// Removes the zip source directory.
        /// </summary>
        private void RemoveZipSourceDirectory()
        {
            DirectoryInfo di = new DirectoryInfo(ZipSourceDirectory);
            if (di.Exists)
                di.Delete(true);
        }

        /// <summary>
        /// Wrapper for adding a word (2007+) file.
        /// </summary>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="fy">The fy.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        public void AddWordFile(string programAbbreviation, string fy, int receiptCycle, string fileName, byte[] fileContent)
        {
            AddFile(programAbbreviation, fy, receiptCycle, fileName, FileConstants.FileExtensions.Docx, fileContent);
        }
        /// <summary>
        /// Adds a file to the directory.
        /// </summary>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="fy">The fy.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="fileContent">Content of the file.</param>
        public void AddFile(string programAbbreviation, string fy, int receiptCycle, string fileName, string fileExtension, byte[] fileContent)
        {
            string filePath =
                $"{ZipSourceDirectory}\\{programAbbreviation}\\{fy}\\Cycle {receiptCycle}\\{fileExtension}\\{fileName}.{fileExtension}";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllBytes(filePath, fileContent ?? Array.Empty<byte>());
        }
        /// <summary>
        /// Zips the contents of the directory.
        /// </summary>
        public void PerformZip()
        {
            ZipFullPath = string.Format("{0}\\{1:yyyy-MM-dd_hh-mm-ss-tt}.{2}", RootDirectory, GlobalProperties.P2rmisDateTimeNow, FileConstants.FileExtensions.Zip);
            ZipFile.CreateFromDirectory(ZipSourceDirectory, ZipFullPath, CompressionLevel.Optimal, false);
            RemoveZipSourceDirectory();
        }
        #endregion
    }
}
