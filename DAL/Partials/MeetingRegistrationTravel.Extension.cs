using Sra.P2rmis.Dal.Interfaces;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingRegistrationTravel object. 
    /// </summary>
    public partial class MeetingRegistrationTravel : IStandardDateFields
    {
        /// <summary>
        /// Populates the specified travel mode identifier.
        /// </summary>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="agentFee2">The 2nd agent fee.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientApprovedAmount">The client approved amount.</param>
        /// <param name="cancelledDate">The cancelled date.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        public void Populate(int? travelModeId, decimal? fare, decimal? agentFee, decimal? agentFee2, decimal? changeFee, bool ground, decimal? nteAmount, decimal? gsaRate,
            bool noGsa, decimal? clientApprovedAmount, DateTime? cancelledDate, string travelRequestComments)
        {
            this.TravelModeId = travelModeId;
            this.Fare = fare;
            this.AgentFee = agentFee;
            this.AgentFee2 = agentFee2;
            this.ChangeFee = changeFee;
            this.Ground = ground;
            this.NteAmount = nteAmount;
            this.GsaRate = gsaRate;
            this.NoGsa = noGsa;
            this.ClientApprovedAmount = clientApprovedAmount;
            this.CancellationDate = cancelledDate;
            this.TravelRequestComments = travelRequestComments;
        }

        /// <summary>
        /// Populates the specified travel mode identifier. This is an overload that adds reservation code
        /// </summary>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="fare">The fare.</param>
        /// <param name="agentFee">The agent fee.</param>
        /// <param name="agentFee2">The 2nd agent fee.</param>
        /// <param name="changeFee">The change fee.</param>
        /// <param name="ground">if set to <c>true</c> [ground].</param>
        /// <param name="nteAmount">The nte amount.</param>
        /// <param name="gsaRate">The gsa rate.</param>
        /// <param name="noGsa">if set to <c>true</c> [no gsa].</param>
        /// <param name="clientApprovedAmount">The client approved amount.</param>
        /// <param name="cancelledDate">The cancelled date.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        /// <param name="reservation">The reservation.</param>
        public void Populate(int? travelModeId, decimal? fare, decimal? agentFee, decimal? agentFee2, decimal? changeFee, bool ground, decimal? nteAmount, decimal? gsaRate,
    bool noGsa, decimal? clientApprovedAmount, DateTime? cancelledDate, string travelRequestComments, string reservation)
        {
            this.TravelModeId = travelModeId;
            this.Fare = fare;
            this.AgentFee = agentFee;
            this.AgentFee2 = agentFee2;
            this.ChangeFee = changeFee;
            this.Ground = ground;
            this.NteAmount = nteAmount;
            this.GsaRate = gsaRate;
            this.NoGsa = noGsa;
            this.ClientApprovedAmount = clientApprovedAmount;
            this.CancellationDate = cancelledDate;
            this.TravelRequestComments = travelRequestComments;
            this.ReservationCode = reservation;
        }
    }
}
