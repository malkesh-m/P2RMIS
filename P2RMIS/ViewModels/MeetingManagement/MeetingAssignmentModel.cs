using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public class MeetingAssignmentModel
    {
        public List<KeyValuePair<int, string>> Sessions { get; set; }
        public List<int> Assignments { get; set; }
    }
}
