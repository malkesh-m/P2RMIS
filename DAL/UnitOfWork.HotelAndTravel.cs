using Sra.P2rmis.Dal.Repository.MeetingManagement;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for Hotel and Travel entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary>   
    public partial class UnitOfWork
    {
        #region MeetingRegistration Repository
        /// <summary>
        /// Provides database access for MeetingRegistrationRepository functions. 
        /// </summary>
        private IMeetingRegistrationRepository _meetingRegistrationRepository;
        public IMeetingRegistrationRepository MeetingRegistrationRepository { get { return LazyLoadMeetingRegistrationRepository(); } }
        /// <summary>
        /// Lazy load the MeetingRegistrationRepository
        /// </summary>
        /// <returns>MeetingRegistrationRepository</returns>
        private IMeetingRegistrationRepository LazyLoadMeetingRegistrationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._meetingRegistrationRepository == null)
            {
                this._meetingRegistrationRepository = new MeetingRegistrationRepository(_context);
            }
            return _meetingRegistrationRepository;
        }
        #endregion
        #region MeetingRegistrationTravel Repository
        /// <summary>
        /// Provides database access for MeetingRegistrationTravelRepository functions. 
        /// </summary>
        private IMeetingRegistrationTravelRepository _meetingRegistrationTravelRepository;
        public IMeetingRegistrationTravelRepository MeetingRegistrationTravelRepository { get { return LazyLoadMeetingRegistrationTravelRepository(); } }
        /// <summary>
        /// Lazy load the MeetingRegistrationTravelRepository
        /// </summary>
        /// <returns>MeetingRegistrationTravelRepository</returns>
        private IMeetingRegistrationTravelRepository LazyLoadMeetingRegistrationTravelRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._meetingRegistrationTravelRepository == null)
            {
                this._meetingRegistrationTravelRepository = new MeetingRegistrationTravelRepository(_context);
            }
            return _meetingRegistrationTravelRepository;
        }
        #endregion
        #region MeetingRegistrationHotel Repository
        /// <summary>
        /// Provides database access for MeetingRegistrationHotelRepository functions. 
        /// </summary>
        private IGenericRepository<MeetingRegistrationHotel> _meetingRegistrationHotelRepository;
        public IGenericRepository<MeetingRegistrationHotel> MeetingRegistrationHotelRepository { get { return LazyLoadMeetingRegistrationHotelRepository(); } }
        /// <summary>
        /// Lazy load the MeetingRegistrationHotelRepository
        /// </summary>
        /// <returns>MeetingRegistrationHotelRepository</returns>
        private IGenericRepository<MeetingRegistrationHotel> LazyLoadMeetingRegistrationHotelRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._meetingRegistrationHotelRepository == null)
            {
                this._meetingRegistrationHotelRepository = new GenericRepository<MeetingRegistrationHotel>(_context);
            }
            return _meetingRegistrationHotelRepository;
        }
        #endregion        
        #region MeetingRegistrationComment Repository
        /// <summary>
        /// Provides database access for MeetingRegistrationHotelRepository functions. 
        /// </summary>
        private IMeetingRegistrationCommentRepository _meetingRegistrationCommentRepository;
        public IMeetingRegistrationCommentRepository MeetingRegistrationCommentRepository { get { return LazyLoadMeetingRegistrationCommentRepository(); } }
        /// <summary>
        /// Lazy load the MeetingRegistrationHotelRepository
        /// </summary>
        /// <returns>MeetingRegistrationHotelRepository</returns>
        private IMeetingRegistrationCommentRepository LazyLoadMeetingRegistrationCommentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._meetingRegistrationCommentRepository == null)
            {
                this._meetingRegistrationCommentRepository = new MeetingRegistrationCommentRepository(_context);
            }
            return _meetingRegistrationCommentRepository;
        }
        #endregion
        #region MeetingRegistrationAttendance Repository
        /// <summary>
        /// Provides database access for MeetingRegistrationAttendanceRepository functions. 
        /// </summary>
        private IGenericRepository<MeetingRegistrationAttendance> _meetingRegistrationAttendanceRepository;
        public IGenericRepository<MeetingRegistrationAttendance> MeetingRegistrationAttendanceRepository { get { return LazyLoadMeetingRegistrationAttendanceRepository(); } }
        /// <summary>
        /// Lazy load the MeetingRegistrationAttendanceRepository
        /// </summary>
        /// <returns>MeetingRegistrationAttendanceRepository</returns>
        private IGenericRepository<MeetingRegistrationAttendance> LazyLoadMeetingRegistrationAttendanceRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._meetingRegistrationAttendanceRepository == null)
            {
                this._meetingRegistrationAttendanceRepository = new GenericRepository<MeetingRegistrationAttendance>(_context);
            }
            return _meetingRegistrationAttendanceRepository;
        }
        #endregion
        #region MeetingRegistrationTravelFlight Repository
        /// <summary>
        /// Provides database access for MeetingRegistrationTravelFlightRepository functions. 
        /// </summary>
        private IMeetingRegistrationTravelFlightRepository _meetingRegistrationTravelFlightRepository;
        public IMeetingRegistrationTravelFlightRepository MeetingRegistrationTravelFlightRepository { get { return LazyLoadMeetingRegistrationTravelFlightRepository(); } }
        /// <summary>
        /// Lazy load the MeetingRegistrationTravelFlightRepository
        /// </summary>
        /// <returns>MeetingRegistrationTravelFlightRepository</returns>
        private IMeetingRegistrationTravelFlightRepository LazyLoadMeetingRegistrationTravelFlightRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._meetingRegistrationTravelFlightRepository == null)
            {
                this._meetingRegistrationTravelFlightRepository = new MeetingRegistrationTravelFlightRepository(_context);
            }
            return _meetingRegistrationTravelFlightRepository;
        }
        #endregion
    }
}
