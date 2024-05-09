using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Setup
{
    public class WithdrawApplicationModel
    {
        public int ApplicationId { get; set; }
        public int? WithdrawnBy { get; set; }
        public  bool WithdrawnFlag { get; set; }
        public DateTime? WithdrawnDate { get; set; }
    }
}
