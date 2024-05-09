using System;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.MeetingManagement;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.UI.Models;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class MeetingTravelViewModel : MMTabsViewModel
    {
        public MeetingTravelViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingTravelViewModel" /> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="reservation">The reservation.</param>
        /// <param name="travelMode">The travel mode.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientAmount">The client amount.</param>
        /// <param name="cancelledDate">The cancelled date.</param>
        /// <param name="specialRequest">The special request.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedDateBy">The modified date by.</param>
        public MeetingTravelViewModel(string firstName, string lastName, string panelName, string reservation, int? travelModeId, Nullable<decimal> fare,
            Nullable<decimal> agentFee, Nullable<decimal> agentFee2, Nullable<decimal> changeFee, bool ground, Nullable<decimal> nteAmount, Nullable<decimal> gsaRate, bool noGsa, Nullable<decimal> clientAmount, DateTime? cancelledDate,
            string specialRequest, bool isDataComplete, DateTime? modifiedDate, string modifiedDateBy, int? meetingRegistrationId, int? panelUserAssignmentId, int? sessionUserAssignmentId, string subTab1Link, string subTab2Link, string subTab3Link)
        {
            ReviewerName = ViewHelpers.ConstructNameWithSpace(firstName, lastName);
            PanelName = panelName;
            Reservation = reservation;
            TravelModeId = travelModeId;
            Fare = fare;
            AgentFee = agentFee;
            AgentFee2 = agentFee2;
            ChangeFee = changeFee;
            Ground = ground;
            NteAmount = nteAmount;
            GsaRate = gsaRate;
            NoGsa = noGsa;
            ClientApprovedAmount = clientAmount;
            CancelledDate = cancelledDate;
            SpecialRequest = specialRequest;
            IsDataComplete = isDataComplete;
            LastUpdated = ViewHelpers.FormatDate(modifiedDate);
            LastUpdatedBy = modifiedDateBy;
            MeetingRegistrationId = meetingRegistrationId;
            PanelUserAssignmentId = panelUserAssignmentId;
            SessionUserAssignmentId = sessionUserAssignmentId;
            SubTab1Link = ViewHelpers.BuildHotelTravelSublink(subTab1Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab2Link = ViewHelpers.BuildHotelTravelSublink(subTab2Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab3Link = ViewHelpers.BuildHotelTravelSublink(subTab3Link, panelUserAssignmentId, sessionUserAssignmentId);
        }

        #region Properties
        /// <summary>
        /// Gets or sets the name of the reviewer.
        /// </summary>
        /// <value>
        /// The name of the reviewer.
        /// </value>
        public string ReviewerName { get; set; }
        /// <summary>
        /// Gets or sets the panel name.
        /// </summary>
        /// <value>
        /// The panel name.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the reservation.
        /// </summary>
        /// <value>
        /// The reservation.
        /// </value>
        public string Reservation { get; set; }
        /// <summary>
        /// Gets or sets the travel mode.
        /// </summary>
        /// <value>
        /// The travel mode.
        /// </value>
        public int? TravelModeId { get; set; }
        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        /// <value>
        /// The fare.
        /// </value>
        public Nullable<decimal> Fare { get; set; }
        /// <summary>
        /// Gets or sets the agent fee.
        /// </summary>
        /// <value>
        /// The agent fee.
        /// </value>
        public Nullable<decimal> AgentFee { get; set; }
        /// <summary>
        /// Gets or sets the agent fee #2.
        /// </summary>
        public Nullable<decimal> AgentFee2 { get; set; }
        /// <summary>
        /// Gets or sets the change fee.
        /// </summary>
        /// <value>
        /// The change fee.
        /// </value>
        public Nullable<decimal> ChangeFee { get; set; }
        /// <summary>
        /// Gets or sets the ground.
        /// </summary>
        /// <value>
        /// The ground.
        /// </value>
        public bool Ground { get; set; }
        /// <summary>
        /// Gets or sets the nte amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public Nullable<decimal> NteAmount { get; set; }
        /// <summary>
        /// Gets or sets the gsa rate.
        /// </summary>
        /// <value>
        /// The gsa rate.
        /// </value>
        public Nullable<decimal> GsaRate { get; set; }
        /// <summary>
        /// Gets or sets the no gsa.
        /// </summary>
        /// <value>
        /// The no gsa.
        /// </value>
        public bool NoGsa { get; set; }
        /// <summary>
        /// Gets or sets the client amount.
        /// </summary>
        /// <value>
        /// The client amount.
        /// </value>
        public Nullable<decimal> ClientApprovedAmount { get; set; }
        /// <summary>
        /// Gets or sets the cancelled date.
        /// </summary>
        /// <value>
        /// The cancelled date.
        /// </value>
        public DateTime? CancelledDate { get; set; }
        /// <summary>
        /// Gets or sets the special request.
        /// </summary>
        /// <value>
        /// The special request.
        /// </value>
        public string SpecialRequest { get; set; }

        /// <summary>
        /// Gets or sets the isDataComplete flag.
        /// </summary>
        /// <value>
        /// The isDataComplete flag.
        /// </value>
        public bool IsDataComplete { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the modified date by.
        /// </summary>
        /// <value>
        /// The modified date by.
        /// </value>
        public string ModifiedDateBy { get; set; }
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public string LastUpdated { get; set; }
        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        /// <value>
        /// The last updated by.
        /// </value>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        public int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the travel mode dropdown.
        /// </summary>
        /// <value>
        /// The travel mode dropdown.
        /// </value>
        public List<TravelModeViewModel> TravelModeDropdown { get; set; } = new List<TravelModeViewModel>();
        /// <summary>
        /// Travel flights.
        /// </summary>
        public List<TravelFlightViewModel> Flights { get; set; } = new List<TravelFlightViewModel>();
        /// <summary>
        /// Gets or sets the sub tab1 link.
        /// </summary>
        /// <value>
        /// The sub tab1 link.
        /// </value>
        public string SubTab1Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab2 link.
        /// </summary>
        /// <value>
        /// The sub tab2 link.
        /// </value>
        public string SubTab2Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab3 link.
        /// </summary>
        /// <value>
        /// The sub tab3 link.
        /// </value>
        public string SubTab3Link {get;set;}
        /// <summary>
        /// Gets or sets the meeting details header model.
        /// </summary>
        /// <value>
        /// The meeting details header model.
        /// </value>
        public MeetingDetailsHeaderModel DetailsHeader { get; set; }
        /// <summary>
        /// Whether the user can manage travel flights.
        /// </summary>
        public bool CanManageTravelFlights { get; set; }
        #endregion
    }
}