using System.Linq;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to MechanismScoringTemplate entities.
    /// </summary>
    public interface IMechanismScoringTemplateRepository : IGenericRepository<MechanismScoringTemplate>
    {
        /// <summary>
        /// Adds the specified program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="scoringTemplateId">The scoring template identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Add(int programMechanismId, int scoringTemplateId, int userId);
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
        void Delete(MechanismScoringTemplate entity, int userId);
    }

    public class MechanismScoringTemplateRepository : GenericRepository<MechanismScoringTemplate>, IMechanismScoringTemplateRepository
    {
        public MechanismScoringTemplateRepository(P2RMISNETEntities context) : base(context) { }
        /// <summary>
        /// Int32s this instance.
        /// </summary>
        /// <returns></returns>
        public void Add(int programMechanismId, int scoringTemplateId, int userId)
        {
            var entity = new MechanismScoringTemplate();
            entity.ProgramMechanismId = programMechanismId;
            entity.ScoringTemplateId = scoringTemplateId;
            Helper.UpdateCreatedFields(entity, userId);
            Helper.UpdateModifiedFields(entity, userId);
            Add(entity);
        }
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
        public void Delete(MechanismScoringTemplate entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
    }
}
