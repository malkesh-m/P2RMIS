using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to MechanismTemplateElement entities.
    /// </summary>
    public interface IMechanismTemplateElementRepository : IGenericRepository<MechanismTemplateElement>
    {
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(int id, int userId);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(MechanismTemplateElement entity, int userId);
        /// <summary>
        /// Gets the by program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        IEnumerable<MechanismTemplateElement> GetByProgramMechanismId(int programMechanismId);
    }

    public class MechanismTemplateElementRepository : GenericRepository<MechanismTemplateElement>, IMechanismTemplateElementRepository
    {
        public MechanismTemplateElementRepository(P2RMISNETEntities context) : base(context) { }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        public void Delete(int id, int userId)
        {
            var entity = GetByID(id);
            Helper.UpdateDeletedFields(entity, userId);
            Delete(id);
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        public void Delete(MechanismTemplateElement entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        /// <summary>
        /// Gets the by program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        public IEnumerable<MechanismTemplateElement> GetByProgramMechanismId(int programMechanismId)
        {
            var templates = context.MechanismTemplates.Where(x => x.ProgramMechanismId == programMechanismId);
            var elements = templates.SelectMany(x => x.MechanismTemplateElements);
            return elements;
        }
    }
}
