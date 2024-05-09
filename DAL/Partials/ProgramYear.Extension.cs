using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ProgramYear object
    /// </summary>
    public partial class ProgramYear: IStandardDateFields
    {
        /// <summary>
        /// Returns a distinct list of valid receipt cycles for the program year
        /// </summary>
        /// <returns>Integer collection of receipt cycles</returns>
        public IEnumerable<int> GetReceiptCycles()
        {
            return this.ProgramMechanism.Where(x => x.ReceiptCycle != null).Select(x => (int)x.ReceiptCycle).Distinct().OrderBy(o => o);
        }
        /// <summary>
        /// Determines if Awards have been assigned.
        /// </summary>
        /// <returns></returns>
        public bool IsAwardsAssigned()
        {
            return (this.ProgramMechanism.Count > 0);
        }
    }
}
