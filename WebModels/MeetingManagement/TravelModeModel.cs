
namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public interface ITravelModeModel
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        int TravelModeId { get; set; }
        /// <summary>
        /// The abbreviation.
        /// </summary>
        string Abbrevication { get; set; }
        /// <summary>
        /// Whether the travel mode can contain travel flights.
        /// </summary>
        bool CanContainTravelFlights { get; set; }
    }
    public class TravelModeModel
    {
        public TravelModeModel(int travelModeId, string abbreviation, bool canContainTravelFlights)
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
