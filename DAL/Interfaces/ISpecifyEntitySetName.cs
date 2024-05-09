namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Identifies the entity as an exception to the TypeName = EntitySetName
    /// convention.  Normally the Entity Framework names the EntitySet with the
    /// type name.  However there are instances where the Entity Framework 
    /// pluralization rules do not generate the TypeName for the EntitySet name.
    /// In these cases the EntitySet name is provided.
    /// </summary>
    interface ISpecifyEntitySetName
    {
        /// <summary>
        /// Returns the EntitySet name for the EntitySet
        /// </summary>
        string EntitySetName { get; }
    }
}
