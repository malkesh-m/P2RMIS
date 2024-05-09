using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.LibraryService;
using Sra.P2rmis.Bll.Setup;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.MeetingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sra.P2rmis.Bll.HotelAndTravel;

namespace Sra.P2rmis.Web.Controllers
{       
    public class MeetingManagementBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Setup services.
        /// </summary>
        protected ISetupService theSetupService { get; set; }

        protected ICriteriaService theCriteriaService { get; set; }

        protected IUserProfileManagementService theUserProfileManagementService { get; set; }

        protected ILookupService theLookupService { get; set; }

        protected ILibraryService theLibraryService { get; set; }
        protected IHotelAndTravelService theHotelAndTravelService { get; set; }
        /// <summary>
        /// Gets or sets the meeting management service.
        /// </summary>
        /// <value>
        /// The meeting management service.
        /// </value>
        protected IMeetingManagementService theMeetingManagementService { get; set; }
        #endregion

        /// <summary>
        /// Contains names for the views created by this controller.
        /// </summary>
        public class ViewNames
        {
            public const string EditHotel = "EditHotel";
            public const string EditTravel = "EditTravel";
            public const string TravelLeg = "_TravelLeg";
            public const string UpdateAssignment = "_UpdateAssignment";
        }
    }        
}