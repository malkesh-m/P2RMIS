using System.Collections.Generic;
using System;

namespace Sra.P2rmis.Dal.EntityChanges
{
    /// <summary>
    /// Marks the DAL entity as supporting logging of property modifications
    /// </summary>
    public interface ILogEntityChanges
    {
        /// <summary>
        /// Returns the properties to log.
        /// </summary>
        /// <returns>Dictionary of properties to log for changes</returns>
        Dictionary<string, PropertyChange> PropertiesToLog();
        /// <summary>
        /// Returns the name of the entity's key property.
        /// </summary>
        string KeyPropertyName { get;}
    }
}
