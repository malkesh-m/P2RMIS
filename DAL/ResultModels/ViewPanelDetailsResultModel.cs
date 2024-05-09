using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ViewPanelDetailsResultModel: SessionsResultModel
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewPanelDetailsResultModel() { }
        #endregion
        #region Properties
        /// <summary>
        /// TODO: Document me
        /// </summary>
        public IEnumerable<uspViewPanelDetails_Result> ViewPanelDetail { get; internal set; }
        
        #endregion
    }
}
