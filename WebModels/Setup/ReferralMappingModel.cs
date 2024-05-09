using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Setup
{
    public class ReferralMappingModel
    {
        /// <summary>
        /// Gets or sets the referral mapping identifier.
        /// </summary>
        /// <value>
        /// The referral mapping identifier.
        /// </value>
        public int? ReferralMappingId { get; set; }
        /// <summary>
        /// panel name
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Session panel identifier.
        /// </summary>
        public int? SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }
        /// <summary>
        /// number of application in panel
        /// </summary>
        public int ReferredToPanel { get; set; }
        /// <summary>
        /// application withdraw status
        /// </summary>
        public int WithDrawn { get; set; }
        /// <summary>
        /// application compliance status id
        /// </summary>
        public int? NonCompliant { get; set; }
        /// <summary>
        /// Gets or sets the partnered.
        /// </summary>
        /// <value>
        /// The partnered.
        /// </value>
        public bool? Partnered { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int? PanelApplicationId { get; set; }
        /// <summary>
        /// number of application assigned to panel
        /// </summary>
        public int? AssignedToPanelId { get; set; }
        /// <summary>
        /// referral mapping status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// application withdrawal status
        /// </summary>
        public bool WithdrawalStatus { get; set; }
        /// <summary>
        /// withdraw application toal
        /// </summary>
        public int withdrawnTotal { get; set; }
        /// <summary>
        /// application non campliance toal
        /// </summary>
        public int nonCompliantTotal { get; set; }
        /// <summary>
        /// application partnered toal
        /// </summary>
        public int partneredTotal { get; set; }
        /// <summary>
        /// application assigned total
        /// </summary>
        public int assignTopanelTotal { get; set; }
        /// <summary>
        /// referred aplication total
        /// </summary>
        public int referredToPanelTotal { get; set; }

    }
}
