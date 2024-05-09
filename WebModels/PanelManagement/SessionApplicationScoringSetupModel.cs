namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Indicates if scoring has been set up for all applications on a panel.
    /// </summary>
    public interface ISessionApplicationScoringSetupModel
    {
        /// <summary>
        /// Indicates if scoring has been set up for all applications on a panel.
        /// </summary>
        bool IsScoringSetUp { get; set; }
    }
    /// <summary>
    /// Indicates if scoring has been set up for all applications on a panel.
    /// </summary>
    public class SessionApplicationScoringSetupModel: ISessionApplicationScoringSetupModel
    {
        /// <summary>
        /// Indicates if scoring has been set up for all applications on a panel.
        /// </summary>
        public bool IsScoringSetUp { get; set; }
    }
}
