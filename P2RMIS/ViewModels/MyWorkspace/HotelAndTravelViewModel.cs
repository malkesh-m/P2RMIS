using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.ApplicationManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.HotelAndTravel;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// My Workspace view model
    /// </summary>
    public class HotelAndTravelViewModel : UserProfileManagementViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HotelAndTravelViewModel() : base()
        {
        }

        public HotelAndTravelViewModel(IEnumerable<IMeetingListModel> meetingList, IEnumerable<IFactSheetModel> factSheet)
        {
            MeetingList = meetingList.ToList().ConvertAll(x => new MeetingListViewModel(x));
            FactSheet = factSheet.ToList().ConvertAll(x => new FactSheetViewModel(x));
        }
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the meeting list.
        /// </summary>
        /// <value>
        /// The meeting list.
        /// </value>
        public List<MeetingListViewModel> MeetingList { get; set; }
        /// <summary>
        /// Gets or sets the fact sheet.
        /// </summary>
        /// <value>
        /// The fact sheet.
        /// </value>
        public List<FactSheetViewModel> FactSheet { get; set; }
        #endregion

        public string LastPageUrl { get; set; }
    }
}