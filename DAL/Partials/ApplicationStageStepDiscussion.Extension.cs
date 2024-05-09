using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationStageStepDiscussion object. 
    /// </summary>
    public partial class ApplicationStageStepDiscussion : IStandardDateFields
    {
        /// <summary>
        /// Populates the entity
        /// </summary>
        /// <param name="applicationStageStepId">ApplicationStageStepId entity identifier</param>
        public void Populate(int applicationStageStepId)
        {
            this.ApplicationStageStepId = applicationStageStepId;
        }
    }
}
