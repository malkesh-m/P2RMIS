using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PreviewCriteriaViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewCriteriaViewModel"/> class.
        /// </summary>
        public PreviewCriteriaViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewCriteriaViewModel"/> class.
        /// </summary>
        /// <param name="previewCriteria">The preview criteria.</param>
        public PreviewCriteriaViewModel(List<IPreviewCriteriaLayoutModel> previewCriteria, ClientScoringScaleLegendModel legend)
        {
            PreviewCriteria = previewCriteria.ConvertAll(x => new PreviewCriterionViewModel(x, legend));
        }

        /// <summary>
        /// Gets the preview criteria.
        /// </summary>
        /// <value>
        /// The preview criteria.
        /// </value>
        public List<PreviewCriterionViewModel> PreviewCriteria { get; private set; }        
    }
}