
namespace Sra.P2rmis.WebModels.Lists
{
    /// <summary>
    /// Webmodel drop down content
    /// </summary>
    public interface IListEntry
    {
        /// <summary>
        /// Entity index of the list entry.
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        string Value { get; set; }
    }
}
