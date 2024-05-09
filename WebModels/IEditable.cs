
namespace Sra.P2rmis.WebModels
{
    /// <summary>
    /// Interface defining common edit commands
    /// </summary>
    public interface IEditable
    {
        /// <summary>
        /// Delete the object contained in the model from an object (e.g. associated record from a database table)
        /// </summary>
        bool IsDeletable { get; set; }
        /// <summary>
        /// Delete the object contained in the model from an object (e.g. associated record from a database table) 
        /// </summary>
        /// <returns></returns>
        bool IsDeleted();
        /// <summary>
        /// Does the model have data?
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        bool HasData();

    }
}
