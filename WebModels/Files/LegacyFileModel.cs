namespace Sra.P2rmis.WebModels.Files
{
    /// <summary>
    /// Model representing a legacy file from PRMIS.
    /// </summary>
    public interface ILegacyFileModel : IBuiltModel
    {
        #region Attributes
        /// <summary>
        /// The file content.
        /// </summary>
        byte[] FileContent { get; }
        /// <summary>
        /// Filename to use when downloading to client machine.
        /// </summary>
        string Filename { get; }
        #endregion
    }
    /// <summary>
    /// Model representing a legacy file from PRMIS.
    /// </summary>
    public class LegacyFileModel: ILegacyFileModel
    {
        #region Constructo & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public LegacyFileModel()
        {
            this.FileContent = new byte[4];
            this.Filename = string.Empty;
        }
        /// <summary>
        /// Populate the model
        /// </summary>
        /// <param name="fileContent">The file content</param>
        /// <param name="fileName">Filename</param>
        public void Populate(byte[] fileContent, string fileName)
        {
            this.FileContent = fileContent;
            this.Filename = fileName;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The file content.
        /// </summary>
        public byte[] FileContent { get; private set; }
        /// <summary>
        /// Filename to use when downloading to client machine.
        /// </summary>
        public string Filename { get; private set; }
        #endregion
    }
}
