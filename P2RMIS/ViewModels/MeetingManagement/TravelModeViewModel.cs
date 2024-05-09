using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class TravelModeViewModel
    {
        public TravelModeViewModel(int travelModeId, string abbreviation, bool canContainTravelFlights)
        {
            TravelModeId = travelModeId;
            Abbrevication = abbreviation;
            CanContainTravelFlights = canContainTravelFlights;
        }
        /// <summary>
        /// The identifier.
        /// </summary>
        public int TravelModeId { get; set; }
        /// <summary>
        /// The abbreviation.
        /// </summary>
        public string Abbrevication { get; set; }
        /// <summary>
        /// Whether the travel mode can contain travel flights.
        /// </summary>
        public bool CanContainTravelFlights { get; set; }
    }
}