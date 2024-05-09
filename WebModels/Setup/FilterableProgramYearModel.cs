using Sra.P2rmis.WebModels.Criteria;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// ProgramYearModel that include property indicating if ProgramYear is active
    /// </summary>
    public interface IFilterableProgramYearModel: IProgramYearModel
    {
        /// <summary>
        /// Indicates if the ProgramYear is active.  (Has any
        /// non closed program)
        /// </summary>
        bool IsActive { get; set; }
    }
    /// <summary>
    /// ProgramYearModel that include property indicating if ProgramYear is active
    /// </summary>
    public class FilterableProgramYearModel: ProgramYearModel, IFilterableProgramYearModel
    {
        /// <summary>
        /// Indicates if the ProgramYear is active.  (Has any
        /// non closed program)
        /// </summary>
        public bool IsActive { get; set; }
    }
}
