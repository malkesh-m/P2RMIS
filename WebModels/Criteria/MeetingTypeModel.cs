using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Criteria
{
    public interface IMeetingTypeModel
    {
        int MeetingTypeId { get; set; }
        string MeetingTypeName { get; set; }
    }
    public class MeetingTypeModel : IMeetingTypeModel
    {
        public MeetingTypeModel(int meetingTypeId, string meetingTypeName)
        {
            MeetingTypeId = meetingTypeId;
            MeetingTypeName = meetingTypeName;
        }
        public int MeetingTypeId { get; set; }
        public string MeetingTypeName { get; set; }
    }
}
