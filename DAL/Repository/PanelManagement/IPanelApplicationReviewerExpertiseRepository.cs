
namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for PanelApplicationReviewerExpertiseRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary> 
    public interface IPanelApplicationReviewerExpertiseRepository : IGenericRepository<PanelApplicationReviewerExpertise>
    {
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        void Delete(int id, int userId);
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        void Delete(PanelApplicationReviewerExpertise entity, int userId);
 
    }
}
