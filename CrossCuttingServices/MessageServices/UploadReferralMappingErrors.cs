using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status values returned from SaveProfile
    /// </summary>
    public enum UploadReferralMappingErrors
    {
        /// <summary>
        /// Status values returned from SaveProfile
        /// </summary>
        ApplicationAssigned = 0,
        /// <summary>
        /// Status values returned from SaveProfile
        /// </summary>
        ValidLogNumber = 1,
        /// <summary>
        /// Status values returned from SaveProfile
        /// </summary>
        ValidPanelAbbreviation = 2,
        /// <summary>
        /// Status values returned from SaveProfile
        /// </summary>
        AnyErrors = 3
    }
}
