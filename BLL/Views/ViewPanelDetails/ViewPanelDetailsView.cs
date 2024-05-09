using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views.SessionPanelDetails;
using DataLayer=Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public class ViewPanelDetailsView: SessionDetailView
    {
        #region Constructors
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="sessions">-----</param>
        /// <param name="panels">-----</param>
        /// <param name="theResults">-----</param>
        internal ViewPanelDetailsView(IEnumerable<SessionView> sessions, IEnumerable<PanelView> panels, DataLayer.ResultModels.ViewPanelDetailsResultModel theResults)
            : base(sessions, panels, theResults)
        {
            this.ViewPanelDetails = new List<DataLayer.uspViewPanelDetails_Result>(theResults.ViewPanelDetail).ConvertAll(new Converter<DataLayer.uspViewPanelDetails_Result, ViewPanelDetails>(DataLayerViewPanelDetailsToViewPanelDetails));
        }
         /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="theResults">-----</param>
        internal ViewPanelDetailsView(DataLayer.ResultModels.ViewPanelDetailsResultModel theResults)
            : base(theResults)
        {
            this.ViewPanelDetails = new List<DataLayer.uspViewPanelDetails_Result>(theResults.ViewPanelDetail).ConvertAll(new Converter<DataLayer.uspViewPanelDetails_Result, ViewPanelDetails>(DataLayerViewPanelDetailsToViewPanelDetails));
        }
        #endregion
        #region Properties
        public IEnumerable<ViewPanelDetails> ViewPanelDetails;
        #endregion
        #region Helpers
        /// <summary>
        /// Converts a data layer ViewPanelDetails object into a business layer ViewPanelDetails object.
        /// </summary>
        /// <param name="viewPanelDetail">-----</param>
        /// <returns>-----</returns>
        private static ViewPanelDetails DataLayerViewPanelDetailsToViewPanelDetails(DataLayer.uspViewPanelDetails_Result viewPanelDetail)
        {
            return new ViewPanelDetails(viewPanelDetail);
        }
        #endregion
    }
}
