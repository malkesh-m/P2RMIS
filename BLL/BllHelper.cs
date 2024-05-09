using System.Configuration;
using System.IO;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Collection of helper methods
    /// </summary>
    internal class BllHelper
    {
        /// <summary>
        /// Test if an optional id value is a good value.
        /// </summary>
        /// <param name="value">null able value to test</param>
        /// <returns>True if valid; false if not</returns>
        public static bool IdOk(int? value)
        {
            return ((value == null) || ((value != null) && (value > 0)));
        }

        /// <summary>
        /// Build file path and name string for accessing files from legacy P2RMIS
        /// </summary>
        /// <param name="logNumber">the workflow id</param>
        /// <param name="fileSuffix">suffix following the log number to differentiate the type of file</param>
        /// <param name="fileFolder">folder in which the file resides</param>
        /// <returns>Full physical file path</returns>
        internal static string GetPhysicalFilePath(string logNumber, string fileSuffix, string fileFolder)
        {
            string fileRoot = ConfigurationManager.AppSettings["appFileRoot"];
            string filePath = ConfigurationManager.AppSettings["appFilePath"];

            string temp = Path.Combine(fileRoot, filePath, fileFolder);
            return Path.Combine(temp,  logNumber) + fileSuffix;
        }
    }
}
