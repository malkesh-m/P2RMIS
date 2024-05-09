
namespace Sra.P2rmis.WebModels.Lists
{
    /// <summary>
    /// Webmodel drop down content
    /// </summary>
    public interface ILogicalListEntry
    {
        /// <summary>
        /// Entity index of the list entry.
        /// </summary>
        bool Index { get; }
        /// <summary>
        /// Display value
        /// </summary>
        string Value { get; }
    }
    /// <summary>
    /// Webmodel drop down content
    /// </summary>
    public class LogicalListEntry: ILogicalListEntry
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - force the caller to supply both.
        /// </summary>
        /// <param name="index">Index value</param>
        /// <param name="value">Display value</param>
        public LogicalListEntry(bool index, string value)
        {
            this.Index = index;
            this.Value = value;
        }
        #endregion
        /// <summary>
        /// Entity index of the list entry.
        /// </summary>
        public bool Index { get; private set; }
        /// <summary>
        /// Display value
        /// </summary>
        public string Value { get; private set; }
    }
}
