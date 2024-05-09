using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to MechanismTemplateElementScoring entities.
    /// </summary>
    public interface IMechanismTemplateElementScoringRepository : IGenericRepository<MechanismTemplateElementScoring>
    {
        void Add(int mechanismTemplateElementId, int scoringId, int stepTypeId, int userId);
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
        void Delete(MechanismTemplateElementScoring entity, int userId);
        /// <summary>
        /// Deletes the by program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteByProgramMechanismId(int programMechanismId, int userId);
        /// <summary>
        /// Gets the by program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        IEnumerable<MechanismTemplateElementScoring> GetByProgramMechanismId(int programMechanismId);
    }

    public class MechanismTemplateElementScoringRepository : GenericRepository<MechanismTemplateElementScoring>, IMechanismTemplateElementScoringRepository
    {
        public MechanismTemplateElementScoringRepository(P2RMISNETEntities context) : base(context) {}

        public void Add(int mechanismTemplateElementId, int clientScoringId, int stepTypeId, int userId)
        {
            var entity = new MechanismTemplateElementScoring();
            entity.MechanismTemplateElementId = mechanismTemplateElementId;
            entity.ClientScoringId = clientScoringId;
            entity.StepTypeId = stepTypeId;
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
        public void Delete(MechanismTemplateElementScoring entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        /// <summary>
        /// Deletes the by program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteByProgramMechanismId(int programMechanismId, int userId)
        {
            var entities = GetByProgramMechanismId(programMechanismId);
            foreach(var entity in entities)
            {
                Delete(entity, userId);
            }
        }
        /// <summary>
        /// Gets the by program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        public IEnumerable<MechanismTemplateElementScoring> GetByProgramMechanismId(int programMechanismId)
        {
            var templates = context.MechanismTemplates.Where(x => x.ProgramMechanismId == programMechanismId);
            var elements = templates.SelectMany(x => x.MechanismTemplateElements);
            var entities = elements.SelectMany(x => x.MechanismTemplateElementScorings);
            return entities;
        }
    }
}
