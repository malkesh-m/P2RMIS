using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model indicating if a SessionPanel's Applications have been released and when.
    /// </summary>
    public interface IReleasePanelModel
    {
        /// <summary>
        /// Indicates if the Applications on the panel have been released
        /// </summary>
        bool IsReleased { get;}
        /// <summary>
        /// Date the Applications have been released.
        /// </summary>
        DateTime? ReleaseDate { get;}
    }
    public class ReleasePanelModel: IReleasePanelModel
    {
        #region Constructor & set up
        public ReleasePanelModel(bool isReleased, DateTime? releasedDate)
        {
            this.IsReleased = isReleased;
            this.ReleaseDate = releasedDate;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Indicates if the Applications on the panel have been released
        /// </summary>
        public bool IsReleased { get; private set; }
        /// <summary>
        /// Date the Applications have been released.
        /// </summary>
        public DateTime? ReleaseDate { get; private set; } 
        #endregion
    }
}
