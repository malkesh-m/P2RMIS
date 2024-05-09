using Sra.P2rmis.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal
{
    public partial class SessionPayRate : IStandardDateFields, IFeeSchedule
    {
        /// <summary>
        /// Wrapper to return the entity identifier
        /// </summary>
        /// <returns>Entity's unique identifier</returns>
        public int Index()
        {
            return this.SessionPayRateId;
        }
    }
}
