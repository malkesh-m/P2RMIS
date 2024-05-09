using Sra.P2rmis.Dal.Repository.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal
{
    public partial interface IUnitOfWork
    {
        IPolicyRepository PolicyRepository { get; }
        IPolicyWeekDayRepository PolicyWeekDayRepository { get; }
        IPolicyNetworkRangeRepository PolicyNetworkRangeRepository { get; }
        IPolicyHistoryRepository PolicyHistoryRepository { get; }
    }
}
