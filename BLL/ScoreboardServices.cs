using System;
using Sra.P2rmis.Bll.Views.Scoreboard;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Provides services for the Scoreboard view.  Services provided are:
    ///      - 
    /// </summary>
    public class ScoreboardServices : IScoreboardServices
    {
        #region Properties
        /// <summary>
        /// Indicates if the object has been disposed but not garbage collected.
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// TODO:: document me
        /// </summary>
        private UnitOfWork UnitOfWork { get; set; }
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScoreboardServices()
        {
            UnitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Dispose of the service.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose of the service
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            ///
            /// if the object has not been disposed & we should dispose the object
            /// 
            if ((!this._disposed) && (disposing))
            {
                if (UnitOfWork != null)
                {
                    UnitOfWork.Dispose();
                    this._disposed = true;
                }
            }
        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieve the scoreboard for the application.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <returns>-----</returns>
        public ScoreboardContainer GetScoreboardByApplicationIdPanelId(int panelApplicationId, int panelId)
        {
            ///
            /// Retrieve the data we are supposed to retrieve & shove it into our view
            ///  
            var results = UnitOfWork.ScoreboardRepository.GetScoreboardByApplicationIdPanelId(panelApplicationId, panelId);
            var view = new ScoreboardContainer(results);
            ///
            /// return the view to the controller
            /// 
            return view;
        }
        #endregion
   }
}
