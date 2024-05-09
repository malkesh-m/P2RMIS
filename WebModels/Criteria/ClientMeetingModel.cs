using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Criteria
{
    public interface IClientMeetingModel
    {
        int ClientMeetingId { get; set; }
        string MeetingDescription { get; set; }
        string Year { get; set; }
        int MeetingTypeId { get; set; }
    }
    public class ClientMeetingModel : IClientMeetingModel
    {
        //public ClientMeetingModel()
        //{

        //}
        //public ClientMeetingModel(int clientMeetingId, string meetingDescription)
        //{
        //    ClientMeetingId = clientMeetingId;
        //    MeetingDescription = meetingDescription;
        //}

        public int ClientMeetingId { get; set; }
        public string MeetingDescription { get; set; }
        public string Year { get; set; }
        public int MeetingTypeId { get; set; }
    }
}
