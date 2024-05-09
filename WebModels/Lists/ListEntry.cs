
namespace Sra.P2rmis.WebModels.Lists
{
    /// <summary>
    /// Webmodel drop down content
    /// </summary>
    public class ListEntry: IListEntry
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - force the caller to supply both.
        /// </summary>
        /// <param name="index">Index value</param>
        /// <param name="value">Display value</param>
        public ListEntry(int index, string value)
        {
            this.Index = index;
            this.Value = value;
        }
        #endregion
        /// <summary>
        /// Entity index of the list entry.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        public string Value { get; set; }
    }
}
