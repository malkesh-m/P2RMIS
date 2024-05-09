using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Model for Panel deliverable data
    /// </summary>
    public class PanelDeliverableModel
    {
        #region Properties        
        /// <summary>
        /// Gets or sets the panel identifier.
        /// </summary>
        /// <value>
        /// The panel identifier.
        /// </value>
        [XmlElement("panelid", typeof(string))]
        [DataTableIgnore]
        public string PanelId { get; set; }

        /// <summary>
        /// Gets or sets the panel name in CDATA format.
        /// </summary>
        /// <value>
        /// The panel name in CDATA format.
        /// </value>
        [XmlElement("panelname", typeof(string))]
        [DataTableColumnName(Name = "Panel Name")]
        public string PanelName { get; set; }

        /// <summary>
        /// Gets or sets the panel abbreviation in CDATA format.
        /// </summary>
        /// <value>
        /// The panel abbreviation in CDATA format.
        /// </value>
        [XmlElement("panelabbreviation", typeof(string))]
        [DataTableColumnName(Name = "Panel Abbreviation")]
        public string PanelAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the science area.
        /// </summary>
        /// <value>
        /// The science area.
        /// </value>
        [XmlElement("sciencearea", typeof(string))]
        [DataTableColumnName(Name = "Science Area")]
        public string ScienceArea { get; set; } = "Other";

        #endregion      
    }
    /// <summary>
    /// Container for panel deliverable data for XML serialization. Necessary to support legacy web service format
    /// </summary>
    [XmlRoot("program")]
    public class PanelDeliverableContainer
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public PanelDeliverableContainer()
        {
            Rows = new List<PanelDeliverableModel>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelDeliverableContainer"/> class.
        /// </summary>
        /// <param name="records">The row items to make up the deliverable.</param>
        public PanelDeliverableContainer(IEnumerable<PanelDeliverableModel> records) : this()
        {            
            foreach (var record in records)
            {
                Rows.Add(record);
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the rows of the deliverable.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [XmlElement(ElementName = "session")]
        public List<PanelDeliverableModel> Rows { get; set; }
        #endregion
    }
}
