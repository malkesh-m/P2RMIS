using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.MeetingManagement
{
    public interface IMeetingRegistrationCommentRepository 
    {
        MeetingRegistrationComment Get(int meetingRegistrationId);

        MeetingRegistrationComment Populate(int meetingRegistrationId, string internalComment);
    }

    public class MeetingRegistrationCommentRepository : GenericRepository<MeetingRegistrationComment>, IMeetingRegistrationCommentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public MeetingRegistrationCommentRepository(P2RMISNETEntities context) : base(context) { }

        public MeetingRegistrationComment Get(int meetingRegistrationId)
        {
            var o = Get(x => x.MeetingRegistrationId == meetingRegistrationId).FirstOrDefault();
            return o;
        }

        public MeetingRegistrationComment Populate(int meetingRegistrationId, string internalComment)
        {
            var o = Get(meetingRegistrationId);
            if (o != null)
            {
                o.MeetingRegistrationId = meetingRegistrationId;
                o.InternalComments = internalComment;
            }
            else
            {
                o = new MeetingRegistrationComment();
                o.MeetingRegistrationId = meetingRegistrationId;
                o.InternalComments = internalComment;
                Add(o);
            }
            return o;
        }
        #endregion
    }
}
