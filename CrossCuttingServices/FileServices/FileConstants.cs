
namespace Sra.P2rmis.CrossCuttingServices.FileServices
{
    /// <summary>
    /// Collection of constants associated with interacting with files
    /// </summary>
    public static class FileConstants
    {
        /// <summary>
        /// Lookup reference for MimeType values for files returned form controller method
        /// </summary>
        public static class MimeTypes
        {
            /// <summary>
            /// The zip mime type
            /// </summary>
            public const string Zip = "application/zip";
            /// <summary>
            /// The doc mime type
            /// </summary>
            public const string Doc = "application/msword";
            /// <summary>
            /// The docx mime type
            /// </summary>
            public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// The PDF mime type
            /// </summary>
            public const string Pdf = "application/pdf";
            /// <summary>
            /// The excel XLSX mime type
            /// </summary>
            public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }

        /// <summary>
        /// Lookup reference for FileExtensions
        /// </summary>
        public static class FileExtensions
        {
            /// <summary>
            /// The docx file extension
            /// </summary>
            public const string Docx = "docx";
            /// <summary>
            /// The PDF file extension
            /// </summary>
            public const string Pdf = "pdf";
            /// <summary>
            /// The zip file extension
            /// </summary>
            public const string Zip = "zip";
            /// <summary>
            /// The excel XLSX extension
            /// </summary>
            public const string Xlsx = "xlsx";
            /// <summary>
            /// The html extension
            /// </summary>
            public const string Html = "html";
            /// <summary>
            /// The Rtf extension
            /// </summary>
            public const string Rtf = "rtf";
            /// <summary>
            /// The txt extension
            /// </summary>
            public const string Txt = "txt";
        }
    }
}
