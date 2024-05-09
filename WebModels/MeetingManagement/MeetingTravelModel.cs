using Sra.P2rmis.CrossCuttingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public class MeetingTravelModel
    {
        public interface IMeetingTravelModel
        {
            /// <summary>
            /// Gets or sets the first name.
            /// </summary>
            /// <value>
            /// The first name.
            /// </value>
            string FirstName { get; set; }
            /// <summary>
            /// Gets or sets the last name.
            /// </summary>
            /// <value>
            /// The last name.
            /// </value>
            string LastName { get; set; }
            /// <summary>
            /// Gets or sets the panel name.
            /// </summary>
            /// <value>
            /// The panel name.
            /// </value>
            string PanelName { get; set; }
            /// <summary>
            /// Gets or sets the reservation.
            /// </summary>
            /// <value>
            /// The reservation.
            /// </value>
            string Reservation { get; set; }
            /// <summary>
            /// Gets or sets the fare.
            /// </summary>
            /// <value>
            /// The fare.
            /// </value>
            Nullable<decimal> Fare { get; set; }
            /// <summary>
            /// Gets or sets the agent fee.
            /// </summary>
            /// <value>
            /// The agent fee.
            /// </value>
            Nullable<decimal> AgentFee { get; set; }
            /// <summary>
            /// Gets or sets agent fee #2.
            /// </summary>
            Nullable<decimal> AgentFee2 { get; set; }
            /// <summary>
            /// Gets or sets the change fee.
            /// </summary>
            /// <value>
            /// The change fee.
            /// </value>
            Nullable<decimal> ChangeFee { get; set; }
            /// <summary>
            /// Gets or sets the ground.
            /// </summary>
            /// <value>
            /// The ground.
            /// </value>
            bool Ground { get; set; }
            /// <summary>
            /// Gets or sets the nte amount.
            /// </summary>
            /// <value>
            /// The amount.
            /// </value>
            Nullable<decimal> NteAmount { get; set; }
            /// <summary>
            /// Gets or sets the gsa rate.
            /// </summary>
            /// <value>
            /// The gsa rate.
            /// </value>
            Nullable<decimal> GsaRate { get; set; }
            /// <summary>
            /// Gets or sets the no gsa.
            /// </summary>
            /// <value>
            /// The no gsa.
            /// </value>
            bool NoGsa { get; set; }
            /// <summary>
            /// Gets or sets the client amount.
            /// </summary>
            /// <value>
            /// The client amount.
            /// </value>
            Nullable<decimal> ClientAmount { get; set; }
            /// <summary>
            /// Gets or sets the cancelled date.
            /// </summary>
            /// <value>
            /// The cancelled date.
            /// </value>
            DateTime? CancelledDate { get; set; }
            /// <summary>
            /// Gets or sets the special request.
            /// </summary>
            /// <value>
            /// The special request.
            /// </value>
            string SpecialRequest { get; set; }

            /// <summary>
            /// Gets or sets the isDataComplete flag.
            /// </summary>
            /// <value>
            /// The isDataComplete flag.
            /// </value>
            bool IsDataComplete { get; set; }

            /// <summary>
            /// Gets or sets the modified date.
            /// </summary>
            /// <value>
            /// The modified date.
            /// </value>
            DateTime? ModifiedDate { get; set; }
            /// <summary>
            /// Gets or sets the modified date by.
            /// </summary>
            /// <value>
            /// The modified date by.
            /// </value>
            string ModifiedDateBy { get; set; }
            /// <summary>
            /// Gets or sets the meeting registration identifier.
            /// </summary>
            /// <value>
            /// The meeting registration identifier.
            /// </value>
            int? MeetingRegistrationId { get; set; }
            /// <summary>
            /// Gets or sets the panel user assignment identifier.
            /// </summary>
            /// <value>
            /// The panel user assignment identifier.
            /// </value>
            int? PanelUserAssignmentId {get;set;}
            /// <summary>
            /// Gets or sets the session user assignment identifier.
            /// </summary>
            /// <value>
            /// The session user assignment identifier.
            /// </value>
            int? SessionUserAssignmentId { get;set;}
        }
        public MeetingTravelModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingTravelModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">Name of the panel.</param>
        public MeetingTravelModel(string firstName, string lastName, string panelName, int? panelUserAssignmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            PanelName = panelName;
            PanelUserAssignmentId = panelUserAssignmentId;
        }

        /// <summary>
        /// Overload for non-reviewers that does not take a panel.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        public MeetingTravelModel(string firstName, string lastName, int? sessionUserAssignmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            SessionUserAssignmentId = sessionUserAssignmentId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingTravelModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">The panel name.</param>
        /// <param name="reservation">The reservation code.</param>
        /// <param name="travelMode">The travel mode type.</param>
        /// <param name="fare">The fare amount.</param>
        /// <param name="agentFee">The agent fee amount.</param>
        /// <param name="changeFee">The change fee amount.</param>
        /// <param name="ground">The ground transportation.</param>
        /// <param name="nteAmount">The NTE amount.</param>
        /// <param name="gsaRate">The GSA rate.</param>
        /// <param name="noGsa">The GSA.</param>
        /// <param name="clientAmount">The client approved amount.</param>
        /// <param name="cancelledDate">The cancellation date.</param>
        /// <param name="specialRequest">the special request.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedDateBy">The modifier.</param>
        public MeetingTravelModel(string firstName, string lastName, string panelName, string reservation, int? travelId, Nullable<decimal> fare,
            Nullable<decimal> agentFee, Nullable<decimal> agentFee2, Nullable<decimal> changeFee, bool ground, Nullable<decimal> nteAmount, Nullable<decimal> gsaRate, bool noGsa, 
            Nullable<decimal> clientAmount, DateTime? cancelledDate, string travelRequestComments, DateTime? modifiedDate, string modifiedDateBy, int? meetingRegistrationId, int? panelUserAssignmentId,
            string subTab1Link, string subTab2Link, string subTab3Link)
        {
            FirstName = firstName;
            LastName = lastName;
            PanelName = panelName;
            ReservationCode = reservation;
            TravelId = travelId;
            Fare = fare;
            AgentFee = agentFee;
            AgentFee2 = agentFee2;
            ChangeFee = changeFee;
            Ground = ground;
            NteAmount = nteAmount;
            GsaRate = gsaRate;
            NoGsa = noGsa;
            ClientAmount = clientAmount;
            CancelledDate = cancelledDate;
            SpecialRequest = travelRequestComments;
            ModifiedDate = modifiedDate;
            ModifiedDateBy = modifiedDateBy;
            MeetingRegistrationId = meetingRegistrationId;
            PanelUserAssignmentId = panelUserAssignmentId;
            SubTab1Link = subTab1Link + "?panelUserAssignmentId=" + panelUserAssignmentId;
            SubTab2Link = subTab2Link + "?panelUserAssignmentId=" + panelUserAssignmentId;
            SubTab3Link = subTab3Link + "?panelUserAssignmentId=" + panelUserAssignmentId;
        }

        /// <summary>
        /// This method is used to add travel data to the model.
        /// </summary>
        /// <param name="reservation">The reservation.</param>
        /// <param name="travelId">The travel identifier.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="agentFee2">The agent fee2.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientAmount">The client amount.</param>
        /// <param name="cancelledDate">The cancellation date.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <param name="MeetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public void PopulateTravel(string reservation, int? travelId, decimal? fare, decimal? agentFee, Nullable<decimal> agentFee2, decimal? changeFee,
                        bool ground, decimal? nteAmount, decimal? gsaRate, bool noGsa, decimal? clientAmount, DateTime? cancelledDate, string travelRequestComments, bool isDataComplete,
                        int? MeetingRegistrationId, DateTime? modifiedDate, string modifiedBy)
        {
            ReservationCode = reservation;
            TravelId = travelId;
            Fare = fare;
            AgentFee = agentFee;
            AgentFee2 = agentFee2;
            ChangeFee = changeFee;
            Ground = ground;
            NteAmount = nteAmount;
            GsaRate = gsaRate;
            NoGsa = noGsa;
            ClientAmount = clientAmount;
            CancelledDate = cancelledDate;
            SpecialRequest = travelRequestComments;
            IsDataComplete = isDataComplete;
            ModifiedDate = modifiedDate;
            ModifiedDateBy = modifiedBy;
        }

        /// <summary>
        /// This method is used to add hotel data to the model.
        /// </summary>
        /// <param name="cancelledDate">The cancelled date.</param>
        public void PopulateHotel(DateTime? cancelledDate)
        {
            CancelledDate = cancelledDate;
        }

        /// <summary>
        /// Populates the sublinks.
        /// </summary>
        /// <param name="subTab1Link">The sub tab1 link.</param>
        /// <param name="subTab2Link">The sub tab2 link.</param>
        /// <param name="subTab3Link">The sub tab3 link.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        public void PopulateSublinks(string subTab1Link, string subTab2Link, string subTab3Link, int? panelUserAssignmentId, int? sessionUserAssignmentId)
        {
            SubTab1Link = ViewHelpers.BuildHotelTravelSublink(subTab1Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab2Link = ViewHelpers.BuildHotelTravelSublink(subTab2Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab3Link = ViewHelpers.BuildHotelTravelSublink(subTab3Link, panelUserAssignmentId, sessionUserAssignmentId);
        }

        #region Properties
        /// <summary>
        /// Gets or sets first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets panel name
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or set reservation
        /// </summary>
        public string ReservationCode { get; set; }
        /// <summary>
        /// Gets or sets the travel identifier.
        /// </summary>
        /// <value>
        /// The travel identifier.
        /// </value>
        public int? TravelId { get; set; }
        /// <summary>
        /// Gets or sets fare
        /// </summary>
        public Nullable<decimal> Fare { get; set; }
        /// <summary>
        /// Gets or sets agent fee
        /// </summary>
        public Nullable<decimal> AgentFee { get; set; }
        /// <summary>
        /// Gets or sets agent fee #2.
        /// </summary>
        public Nullable<decimal> AgentFee2 { get; set; }
        /// <summary>
        /// Gets or sets change fee
        /// </summary>
        public Nullable<decimal> ChangeFee { get; set; }
        /// <summary>
        /// Gets or sets ground
        /// </summary>
        public bool Ground { get; set; }
        /// <summary>
        /// Gets or sets nte amount
        /// </summary>
        public Nullable<decimal> NteAmount { get; set; }
        /// <summary>
        /// Gets or sets GSA rate
        /// </summary>
        public Nullable<decimal> GsaRate { get; set; }
        /// <summary>
        /// Gets or sets No GSA
        /// </summary>
        public bool NoGsa { get; set; }
        /// <summary>
        /// Gets or sets client amount
        /// </summary>
        public Nullable<decimal> ClientAmount { get; set; }
        /// <summary>
        /// Gets or sets cancelled date
        /// </summary>
        public DateTime? CancelledDate { get; set; }
        /// <summary>
        /// Gets or sets special request
        /// </summary>
        public string SpecialRequest { get; set; }
        /// <summary>
        /// Gets or sets modfied date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets modified date by
        /// </summary>
        public string ModifiedDateBy { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        public int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId {get;set;}
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        public int? SessionUserAssignmentId { get;set;}
        /// <summary>
        /// Gets or sets the sub tab1 link.
        /// </summary>
        /// <value>
        /// The sub tab1 link.
        /// </value>
        public string SubTab1Link { get; set; }
        /// <summary>
        /// Gets or sets the sub tab2 link.
        /// </summary>
        /// <value>
        /// The sub tab2 link.
        /// </value>
        public string SubTab2Link { get; set; }
        /// <summary>
        /// Gets or sets the sub tab3 link.
        /// </summary>
        /// <value>
        /// The sub tab3 link.
        /// </value>
        public string SubTab3Link { get; set; }

        /// <summary>
        /// Gets or sets the isDataComplete flag.
        /// </summary>
        /// <value>
        /// The isDataComplete flag.
        /// </value>
        public bool IsDataComplete { get; set; }
        #endregion

    }
}