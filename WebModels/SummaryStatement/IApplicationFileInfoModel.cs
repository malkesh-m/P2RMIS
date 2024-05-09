

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// ApplicationFileInfoModel interface display and linking properties for appication files
    /// </summary>
    public interface IApplicationFileInfoModel
    {
        /// <summary>
        /// FileLink - File's Link
        /// </summary>
        string FileLink { get; set; }
        /// <summary>
        /// FileDisplayName - File's Display Name
        /// </summary>
        string FileDisplayName { get; set; }
        /// <summary>
        /// FileSize - File's Size in bytes
        /// </summary>
        long FileSize { get; set; }
    }
}
