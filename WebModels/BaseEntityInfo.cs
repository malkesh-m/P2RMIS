namespace Sra.P2rmis.WebModels
{
    #region Interface
    /// <summary>
    /// Marker interface indicating the object contains information about a CRUD operation
    /// </summary>
    public interface IEntityInfo
    {
        int EntityId { get;}
    }
    #endregion
    #region EntityInfo objects
    /// <summary>
    /// Base EntityInfo class
    /// </summary>
    public class BaseEntityInfo : IEntityInfo
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public BaseEntityInfo(int entityId)
        {
            this.EntityId = entityId;
        }
        #endregion
        #region Attributes
        public int EntityId { get; }
        #endregion
    }
    #endregion
}
