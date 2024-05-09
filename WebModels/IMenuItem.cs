using System.Collections.Generic;

namespace Sra.P2rmis.WebModels
{
    /// <summary>
    /// Definition of a generic menu item
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// group or report unique identifier 
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// group or report name 
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// group or report description
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// group or report sort order
        /// </summary>
        int SortOrder { get; set; }
        /// <summary>
        /// List of menu type items.
        /// </summary>
        List<MenuItem> Tree { get; }
    }
}
