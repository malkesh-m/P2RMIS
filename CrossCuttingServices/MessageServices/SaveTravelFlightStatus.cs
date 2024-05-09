using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Save travel flight status
    /// </summary>
    public enum SaveTravelFlightStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Flight was successfully saved
        /// </summary>
        Success = 1,
        /// <summary>
        /// The invalid panel user assignment identifier
        /// </summary>
        InvalidPanelUserAssignmentId = 2,
        /// <summary>
        /// The invalid departure date
        /// </summary>
        InvalidDepartureDate = 3,
        /// <summary>
        /// The invalid arrival date
        /// </summary>
        InvalidArrivalDate = 4
    }
}
