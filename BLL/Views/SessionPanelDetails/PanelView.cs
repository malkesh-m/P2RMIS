using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Business layer representation of a Panel
    /// </summary>
    public class PanelView
    {
        #region Constructor
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        public PanelView() {}
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="thePanel">-----</param>
        public PanelView(SessionPanel thePanel)
        {
            //
            // Initialize the variables just in case
            //
            PanelId = thePanel.SessionPanelId;
            SessionId = thePanel.MeetingSessionId;
            PanelName = thePanel.PanelName;
            PanelAbbreviation = thePanel.PanelAbbreviation;
        }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int? SessionId { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PanelName { get; private set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string PanelAbbreviation { get; set; }
        #endregion
    }
}
