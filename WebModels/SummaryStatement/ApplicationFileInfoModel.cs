
namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// ApplicationFileInfoModel interface display and linking properties for appication files
    /// </summary>
    public class ApplicationFileInfoModel : IApplicationFileInfoModel
    {
        /// <summary>
        /// FileLink - File's Link
        /// </summary>
        public string FileLink { get; set; }
        /// <summary>
        /// FileDisplayName - File's Display Name
        /// </summary>
        public string FileDisplayName { get; set; }
        /// <summary>
        /// FileSize - File's Size in bytes
        /// </summary>
        public long FileSize { get; set; }
    }
}
