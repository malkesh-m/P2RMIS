using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ProgramPayRate object. 
    /// </summary>
    public partial class ProgramPayRate: IFeeSchedule, IStandardDateFields
    {
        /// <summary>
        /// Wrapper to return the entity identifier
        /// </summary>
        /// <returns>Entity's unique identifier</returns>
        public int Index()
        {
            return this.ProgramPayRateId;
        }
    }
}
