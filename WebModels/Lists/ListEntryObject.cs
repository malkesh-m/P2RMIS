namespace Sra.P2rmis.WebModels.Lists
{
    /// <summary>
    /// Drop down entry with alternative display value
    /// </summary>
    public interface IListDescription
    {
        /// <summary>
        /// Entity index of the list entry.
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        bool BooleanValue { get; set; }
    }
    /// <summary>
    /// Drop down entry with alternative display value
    /// </summary>
    public class ListDescription: IListDescription
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - force the caller to supply both.
        /// </summary>
        /// <param name="index">Index value</param>
        /// <param name="value">Display value</param>
        /// <param name="description">Alternative value</param>
        public ListDescription(int index, string value, string description)
        {
            this.Index = index;
            this.Value = value;
            this.Description = description;
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
        /// <summary>
        /// Display value
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        public bool BooleanValue { get; set; }
    }
}
