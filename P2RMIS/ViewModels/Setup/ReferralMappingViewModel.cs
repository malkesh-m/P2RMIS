using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    public class ReferralMappingViewModel
    {
        public const string RELEASED = "Released";
        public const string NOT_RELEASED = "Not Released";

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferralMappingViewModel"/> class.
        /// </summary>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="referredToPanel">The referred to panel.</param>
        /// <param name="nonCompliant">The non compliant.</param>
        /// <param name="withDrawn">The with drawn.</param>
        /// <param name="assignedToPanel">The assigned to panel.</param>
        public ReferralMappingViewModel(string panelName, int? sessionPanelId, int referredToPanel, int nonCompliant, int withDrawn,
                int assignedToPanel)
        {
            PanelName = panelName;
            SessionPanelId = sessionPanelId ?? 0;
            ReferredToPanel = referredToPanel;
            NonCompliant = nonCompliant;
            WithDrawn = withDrawn;
            AssignedToPanel = assignedToPanel;
            Status = assignedToPanel > 0 ? RELEASED : NOT_RELEASED;
        }

        /// <summary>
        /// panel name
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Session panel identifier.
        /// </summary>
        public int SessionPanelId { get; set; }
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
        /// number of application assigned to panel
        /// </summary>
        public int AssignedToPanel { get; set; }
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