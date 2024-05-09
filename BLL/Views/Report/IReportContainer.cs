using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views
{
    public interface IReportContainer
    {
        #region Properties

        IEnumerable<IReportFacts> Reports { get; }

        #endregion
    }
}
