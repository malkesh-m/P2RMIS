using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.WebModels.Files
{
    /// <summary>
    /// Interface for the object containing information about the file
    /// </summary>
    public interface IFileInfoModel
    {
        /// <summary>
        /// The Application File Model containing logical information about the file
        /// </summary>
        IApplicationFileModel FileInfo { get; set; }
        /// <summary>
        /// The physical size of the file
        /// </summary>
        long FileSize { get; set; }
        /// <summary>
        /// The identifier of the application associated with this file
        /// </summary>
        int ApplicationId { get; set; }
    }
    /// <summary>
    /// Object containing information about the file
    /// </summary>
    public class FileInfoModel : IFileInfoModel
    {
        /// <summary>
        /// The Application File Model containing logical information about the file
        /// </summary>
        public IApplicationFileModel FileInfo { get; set; }
        /// <summary>
        /// The physical size of the file
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// The identifier of the application associated with this file
        /// </summary>
        public int ApplicationId { get; set; }
    }
}
