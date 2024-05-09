using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Setup
{
    public class ApplicationWithdrawnModal
    {
        public int ApplicationId { get; set; }
        public string LogNumber { get; set; }
        public int? WithDrawnBy { get; set; }
        public DateTime? WithdrawnDate { get; set; }
        
    }
}
