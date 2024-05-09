
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ClientDataDeliverable
    {
        /// <summary>
        /// Class containing available API Methods for use in deliverables
        /// </summary>
        public static class ApiMethods
        {
            /// <summary>
            /// The budget deliverable
            /// </summary>
            public const string BudgetDeliverable = "GetBudgetDeliverable";

            /// <summary>
            /// The scoring deliverable
            /// </summary>
            public const string ScoringDeliverable = "GetScoringDeliverable";

            /// <summary>
            /// The panel deliverable
            /// </summary>
            public const string PanelDeliverable = "GetPanelDeliverable";

            /// <summary>
            /// The criteria deliverable
            /// </summary>
            public const string CriteriaDeliverable = "GetCriteriaDeliverable";
        }
    }
}
