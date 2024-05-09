
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model identifying an incomplete criteria name
    /// </summary>
    public interface IIncompleteCriteriaNameModel
    {
        /// <summary>
        /// Incomplete criteria name
        /// </summary>
        string CriteriaName { get; }
        /// <summary>
        /// ApplicaitonWorkflowStepElement entity identifier
        /// </summary>
        int ApplicationWorkflowStepElementId { get; }
    }
    /// <summary>
    /// Model identifying an incomplete criteria name
    /// </summary>
    public class IncompleteCriteriaNameModel : IIncompleteCriteriaNameModel
    {
        #region Construction & setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="critiqueName">Criteria name</param>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElement entity identifier</param>
        public IncompleteCriteriaNameModel(string criteriaName, int applicationWorkflowStepElementId)
        {
            this.CriteriaName = criteriaName;
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Incomplete criteria name
        /// </summary>
        public string CriteriaName { get; private set; }
        /// <summary>
        /// ApplicaitonWorkflowStepElement entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; private set; }
        #endregion
    }
}
