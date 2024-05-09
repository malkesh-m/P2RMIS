using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelUserRegistrationDocument object. 
    /// </summary>
    public partial class PanelUserRegistrationDocumentContract : IStandardDateFields
    {
        /// <summary>
        /// Populate the entity.
        /// </summary>
        /// <param name="consultantFeeAmount">The amount of money promised to be paid to the reviewer</param>
        public void Populate(decimal? consultantFeeAmount, int contractStatusId)
        {
            FeeAmount = consultantFeeAmount;
            ContractStatusId = contractStatusId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultantFeeAmount"></param>
        /// <param name="contractStatusId"></param>
        /// <param name="bypassReason"></param>
        /// <param name="documentPath"></param>
        public void Populate(decimal? consultantFeeAmount, int contractStatusId, string bypassReason, string documentPath)
        {

            FeeAmount = consultantFeeAmount;
            ContractStatusId = contractStatusId;
            BypassReason = bypassReason;
            ContractFileLocation = documentPath;
         }

    }
}