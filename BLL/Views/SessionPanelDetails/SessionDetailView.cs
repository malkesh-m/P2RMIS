using System;
using System.Collections.Generic;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class SessionDetailView
    {
        #region MyRegion
        /// <summary>
        /// Class constants
        /// </summary>
        private class Constants
        {
            public const int DefaultScoredApplicationCount = 0;
        }

        #endregion
        #region Constructors
        /// <summary>
        /// TODO:: document me
        /// </summary>
        protected SessionDetailView() { }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="theResults">SessionResultModel object</param>
        internal SessionDetailView(SessionsResultModel theResults)
        {
            this.FiscalYear = string.Empty; // -----
            ///
            /// 
            ///
            this.Sessions = new List<MeetingSessionModel>(theResults.Sessions).ConvertAll(new Converter<MeetingSessionModel, SessionView>(ProgramSessionToSessionView));
            this.Panels = new List<SessionPanel>(theResults.Panels).ConvertAll(new Converter<SessionPanel, PanelView>(PanelToPanelView));

            this.Mechanisms = theResults.ApplicationCounts;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="sessions">Enumerations of SessionView objects</param>
        /// <param name="panels">Enumerations of PanelView objeccts</param>
        /// <param name="theResults">The SessionsResultModdel object</param>
        internal SessionDetailView(IEnumerable<SessionView> sessions, IEnumerable<PanelView> panels, SessionsResultModel theResults): this(theResults)
        {
            ///
            /// Overwrite the Sessions & Panels instantiated by the base constructor.  They
            /// should be empty and set to the values provided.  They should be from the cache.
            ///
            this.Sessions = sessions;
            this.Panels = panels;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="sessions">-----</param>
        /// <param name="panels">-----</param>
        /// <param name="theResults">-----</param>
        internal SessionDetailView(IEnumerable<SessionView> sessions, SessionsResultModel theResults)
            : this(theResults)
        {
            ///
            /// Overwrite the Sessions instantiated by the base constructor.  They should be 
            /// empty and set to the values provided.  It should have been retrieve from the cache.
            ///
            this.Sessions = sessions;
        }
        #endregion
        /// <summary>
        /// Fiscal year for the application
        /// </summary>
        public string FiscalYear { get; internal set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public IEnumerable<SessionView> Sessions { get; internal set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public IEnumerable<PanelView> Panels { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public IEnumerable<ApplicationCount> Mechanisms { get; internal set; }

        #region Helper Methods
        /// <summary>
        /// Converts a data layer ProgramSession object into
        /// a business layer SessionView object.
        /// </summary>
        /// <param name="programSessionModel">Data layer ProgramSession object</param>
        /// <returns>SessionView object created from ProgramSession object</returns>
        private static SessionView ProgramSessionToSessionView(MeetingSessionModel programSessionModel)
        {
            return new SessionView(programSessionModel);
        }
        /// <summary>
        /// Converts a data layer Panel object into
        /// a business layer PanelView object.
        /// </summary>
        /// <param name="panelModel">Data layer Panel object</param>
        /// <returns>PanelView object created from Panel object</returns>
        private static PanelView PanelToPanelView(SessionPanel panelModel)
        {
            return new PanelView(panelModel);
        }

        #endregion
    }
}
