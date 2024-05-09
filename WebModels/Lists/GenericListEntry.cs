namespace Sra.P2rmis.WebModels.Lists
{
    #region GenericList
    /// <summary>
    /// Represents a list entry in a more generic case.
    /// </summary>
    /// <typeparam name="TIndex">Type of Index property</typeparam>
    /// <typeparam name="TValue">type of Value property</typeparam>
    public interface IGenericListEntry<TIndex, TValue>
    {
        TIndex Index { get; set; }
        TValue Value { get; set; }
    }
    /// <summary>
    /// Represents a list entry in a more generic case.
    /// </summary>
    /// <typeparam name="TIndex">Type of Index property</typeparam>
    /// <typeparam name="TValue">type of Value property</typeparam>
    public class GenericListEntry<TIndex, TValue> : IGenericListEntry<TIndex, TValue>
    {
        /// <summary>
        /// Entity index of the list entry.
        /// </summary>
        public TIndex Index { get; set; }
        /// <summary>
        /// Display value
        /// </summary>
        public TValue Value { get; set; }
    }
    #endregion
    #region GenericActiveListEntry
    /// <summary>
    /// Represents a list entry with an active indicator
    /// </summary>
    /// <typeparam name="TIndex">Type of Index property</typeparam>
    /// <typeparam name="TValue">type of Value property</typeparam>
    public interface IGenericActiveProgramListEntry<TIndex, TValue> : IGenericListEntry<TIndex, TValue>
    {
        bool IsActive { get; set; }
        string ProgramAbbreviation { get; set; }
        int ProgramYearId { get; set; }
    }
    /// <summary>
    /// Represents a list entry with an active indicator
    /// </summary>
    /// <typeparam name="TIndex">Type of Index property</typeparam>
    /// <typeparam name="TValue">type of Value property</typeparam>
    public class ActiveProgramListEntry<TIndex, TValue> : GenericListEntry<TIndex, TValue>, IGenericActiveProgramListEntry<TIndex, TValue>
    {
        /// <summary>
        /// Indicates if the list entry is active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        public int ProgramYearId { get; set; }
    }
    #endregion
    #region GenericDescriptionList
    /// <summary>
    /// Represents a list entry with a description in a more generic case.
    /// </summary>
    /// <typeparam name="TIndex">Type of Index property</typeparam>
    /// <typeparam name="TValue">Type of Value property</typeparam>
    /// <typeparam name="TDescription">Type of Description property</typeparam>
    public interface IGenericDescriptionList<TIndex, TValue, TDescription> : IGenericListEntry<TIndex, TValue>
    {
        /// <summary>
        /// List entry's description
        /// </summary>
        TDescription Description { get; set; }
    }
    /// <summary>
    /// Represents a list entry with a description in a more generic case.
    /// </summary>
    /// <typeparam name="TIndex">Type of Index property</typeparam>
    /// <typeparam name="TValue">Type of Value property</typeparam>
    /// <typeparam name="TDescription">Type of Description property</typeparam>
    public class GenericDescriptionList<TIndex, TValue, TDescription> : GenericListEntry<TIndex, TValue>, IGenericDescriptionList<TIndex, TValue, TDescription>
    {
        /// <summary>
        /// List entry's description
        /// </summary>
        public TDescription Description { get; set; }
    }
    #endregion
}
