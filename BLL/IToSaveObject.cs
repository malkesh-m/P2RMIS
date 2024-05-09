
namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Interface for ToSaveObjects.  ToSaveObjects are related user specified selections that are
    /// to be manipulated together. (Think first name & last name).  
    /// </summary>
    public interface ToSaveObject
    {
        /// <summary>
        /// Implementation supplied method to validate individual objects.
        /// </summary>
        void Validate();
    }
}
