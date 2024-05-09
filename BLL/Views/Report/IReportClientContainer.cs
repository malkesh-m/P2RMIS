using System;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Views.Report
{
    public interface IReportClientContainer
    {
        /// <summary>
        /// List of client objects to populate dropdown
        /// </summary>
        IList<Tuple<int, string, string>> ClientList { get; }
    }
}
