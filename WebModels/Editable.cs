
namespace Sra.P2rmis.WebModels
{
    /// <summary>
    /// Class defining common edit commands
    /// </summary>
    abstract public class Editable : IEditable
    {
        /// <summary>
        /// Delete the object contained in the model from an object (e.g. associated record from a database table)
        /// </summary>
        public virtual bool IsDeletable { get; set; }
        /// <summary>
        /// Delete the object contained in the model from an object (e.g. associated record from a database table) 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDeleted() 
        {
            return this.IsDeletable;
        }
        /// <summary>
        /// Does the model have data?
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public virtual bool HasData()
        { 
            return true;
        }
    }
}
