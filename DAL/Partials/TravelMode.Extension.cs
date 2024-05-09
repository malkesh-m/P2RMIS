
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's Travel Mode object. Contains lookup values within TravelMode lookup table.
    /// </summary>

    public partial class TravelMode
    {
        /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            public const int Air = 1;
            public const int Other = 2;
            public const int POV = 3;
            public const int RentalCar = 4;
            public const int Train = 5;
        }
        /// <summary>
        /// Whether the travel mode can contain travel flights
        /// </summary>
        /// <returns></returns>
        public bool CanContainTravelFlights()
        {
            return TravelModeId == Indexes.Air ||
                TravelModeId == Indexes.Train ||
                TravelModeId == Indexes.Other;
        }
    }
}
