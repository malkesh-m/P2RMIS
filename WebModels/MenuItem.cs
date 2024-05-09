using System.Collections.Generic;

namespace Sra.P2rmis.WebModels
{
    /// <summary>
    /// Definition of a generic menu item
    /// </summary>
    public class MenuItem : IMenuItem
    {
        #region Constants
        /// <summary>
        /// Indicates this item does not have a sort order
        /// </summary>
        public const int NO_SORT_ORDER = -1;
        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor.  Defined to ensure an empty list is constructed.
        /// </summary>
		public MenuItem ()
        {
            ///
            /// By default we construct an empty tree of menu items.
            /// 
            this._tree = new List<MenuItem>();
        }
	    #endregion
        #region Attributes
        /// <summary>
        /// group or report unique identifier 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// group or report name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// group or report description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// group or report sort order
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// List of menu type items.
        /// </summary>
        private List<MenuItem> _tree;
        public List<MenuItem> Tree { get { return this._tree; } }
        #endregion
    }
}
