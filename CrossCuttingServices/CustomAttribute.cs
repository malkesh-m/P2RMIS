using System;

namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// Attribute which tells the data table converter to ignore processing
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class DataTableIgnoreAttribute : Attribute
    {

    }

    /// <summary>
    /// Attribute which specifies a column name for the data table converter
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class DataTableColumnNameAttribute : Attribute
    {
        /// <summary>
        /// The name of the column 
        /// </summary>
        public string Name { get; set; }
    }
}
