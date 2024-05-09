using System.Collections.Generic;
using System.Xml.Serialization;
using Sra.P2rmis.CrossCuttingServices;
using System.Xml;
using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Scoring deliverable data interface
    /// </summary>
    public interface IScoringDeliverableModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IScoringDeliverableModel"/> is disapproved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disapproved; otherwise, <c>false</c>.
        /// </value>
        bool Disapproved { get; set; }
        /// <summary>
        /// Gets or sets the eval1.
        /// </summary>
        /// <value>
        /// The eval1.
        /// </value>
        float? Eval1 { get; set; }
        /// <summary>
        /// Gets or sets the eval10.
        /// </summary>
        /// <value>
        /// The eval10.
        /// </value>
        float? Eval10 { get; set; }
        /// <summary>
        /// Gets or sets the eval2.
        /// </summary>
        /// <value>
        /// The eval2.
        /// </value>
        float? Eval2 { get; set; }
        /// <summary>
        /// Gets or sets the eval3.
        /// </summary>
        /// <value>
        /// The eval3.
        /// </value>
        float? Eval3 { get; set; }
        /// <summary>
        /// Gets or sets the eval4.
        /// </summary>
        /// <value>
        /// The eval4.
        /// </value>
        float? Eval4 { get; set; }
        /// <summary>
        /// Gets or sets the eval5.
        /// </summary>
        /// <value>
        /// The eval5.
        /// </value>
        float? Eval5 { get; set; }
        /// <summary>
        /// Gets or sets the eval6.
        /// </summary>
        /// <value>
        /// The eval6.
        /// </value>
        float? Eval6 { get; set; }
        /// <summary>
        /// Gets or sets the eval7.
        /// </summary>
        /// <value>
        /// The eval7.
        /// </value>
        float? Eval7 { get; set; }
        /// <summary>
        /// Gets or sets the eval8.
        /// </summary>
        /// <value>
        /// The eval8.
        /// </value>
        float? Eval8 { get; set; }
        /// <summary>
        /// Gets or sets the eval9.
        /// </summary>
        /// <value>
        /// The eval9.
        /// </value>
        float? Eval9 { get; set; }
        /// <summary>
        /// Gets or sets the global score.
        /// </summary>
        /// <value>
        /// The global score.
        /// </value>
        float? GlobalScore { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the standard deviation.
        /// </summary>
        /// <value>
        /// The standard deviation.
        /// </value>
        float? StandardDeviation { get; set; }

        /// <summary>
        /// Whether the application has been triaged.
        /// </summary>
        /// <value>
        /// The triaged status.
        /// </value>
        int Triaged { get; set; }
    }
    /// <summary>
    /// Model representing scoring deliverable data
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.Setup.IScoringDeliverableModel" />
    public class ScoringDeliverableModel : DataDeliverableModel, IScoringDeliverableModel
    {
        [XmlAttribute(AttributeName = "id")]
        [DataTableIgnore]
        public int ID { get; set; }
        
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Application ID")]
        public string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets the disapproved c data.
        /// </summary>
        /// <value>
        /// The disapproved c data.
        /// </value>
        [XmlElement("disapproved", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement DisapprovedCData { get; set; }

        /// <summary>
        /// Gets or sets the eval1.
        /// </summary>
        /// <value>
        /// The eval1.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 1")]
        public float? Eval1 { get; set; }
        /// <summary>
        /// Gets or sets the eval1 c data.
        /// </summary>
        /// <value>
        /// The eval1 c data.
        /// </value>
        [XmlElement("eval1", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval1CData { get; set; }

        /// <summary>
        /// Gets or sets the eval10 c data.
        /// </summary>
        /// <value>
        /// The eval10 c data.
        /// </value>
        [XmlElement("eval10", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval10CData { get; set; }

        /// <summary>
        /// Gets or sets the eval2.
        /// </summary>
        /// <value>
        /// The eval2.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 2")]
        public float? Eval2 { get; set; }
        /// <summary>
        /// Gets or sets the eval2 c data.
        /// </summary>
        /// <value>
        /// The eval2 c data.
        /// </value>
        [XmlElement("eval2", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval2CData { get; set; }
        /// <summary>
        /// Gets or sets the eval3.
        /// </summary>
        /// <value>
        /// The eval3.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 3")]
        public float? Eval3 { get; set; }
        /// <summary>
        /// Gets or sets the eval3 c data.
        /// </summary>
        /// <value>
        /// The eval3 c data.
        /// </value>
        [XmlElement("eval3", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval3CData { get; set; }
        /// <summary>
        /// Gets or sets the eval4.
        /// </summary>
        /// <value>
        /// The eval4.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 4")]
        public float? Eval4 { get; set; }
        /// <summary>
        /// Gets or sets the eval4 c data.
        /// </summary>
        /// <value>
        /// The eval4 c data.
        /// </value>
        [XmlElement("eval4", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval4CData { get; set; }
        /// <summary>
        /// Gets or sets the eval5.
        /// </summary>
        /// <value>
        /// The eval5.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 5")]
        public float? Eval5 { get; set; }
        /// <summary>
        /// Gets or sets the eval5 c data.
        /// </summary>
        /// <value>
        /// The eval5 c data.
        /// </value>
        [XmlElement("eval5", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval5CData { get; set; }
        /// <summary>
        /// Gets or sets the eval6.
        /// </summary>
        /// <value>
        /// The eval6.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 6")]
        public float? Eval6 { get; set; }
        /// <summary>
        /// Gets or sets the eval6 c data.
        /// </summary>
        /// <value>
        /// The eval6 c data.
        /// </value>
        [XmlElement("eval6", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval6CData { get; set; }
        /// <summary>
        /// Gets or sets the eval7.
        /// </summary>
        /// <value>
        /// The eval7.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 7")]
        public float? Eval7 { get; set; }
        /// <summary>
        /// Gets or sets the eval7 c data.
        /// </summary>
        /// <value>
        /// The eval7 c data.
        /// </value>
        [XmlElement("eval7", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval7CData { get; set; }
        /// <summary>
        /// Gets or sets the eval8.
        /// </summary>
        /// <value>
        /// The eval8.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 8")]
        public float? Eval8 { get; set; }
        /// <summary>
        /// Gets or sets the eval8 c data.
        /// </summary>
        /// <value>
        /// The eval8 c data.
        /// </value>
        [XmlElement("eval8", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval8CData { get; set; }
        /// <summary>
        /// Gets or sets the eval9.
        /// </summary>
        /// <value>
        /// The eval9.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 9")]
        public float? Eval9 { get; set; }
        /// <summary>
        /// Gets or sets the eval9 c data.
        /// </summary>
        /// <value>
        /// The eval9 c data.
        /// </value>
        [XmlElement("eval9", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement Eval9CData { get; set; }
        /// <summary>
        /// Gets or sets the eval10.
        /// </summary>
        /// <value>
        /// The eval10.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Score 10")]
        public float? Eval10 { get; set; }

        /// <summary>
        /// Gets or sets the global score.
        /// </summary>
        /// <value>
        /// The global score.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Overall Score")]
        public float? GlobalScore { get; set; }
        /// <summary>
        /// Gets or sets the global score c data.
        /// </summary>
        /// <value>
        /// The global score c data.
        /// </value>
        [XmlElement("global_score", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement GlobalScoreCData { get; set; }

        /// <summary>
        /// Gets or sets the log number c data.
        /// </summary>
        /// <value>
        /// The log number c data.
        /// </value>
        [XmlElement("log_no", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement LogNumberCData { get; set; }

        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Panel Abbreviation")]
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation c data.
        /// </summary>
        /// <value>
        /// The panel abbreviation c data.
        /// </value>
        [XmlElement("panel_abrv", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement PanelAbbreviationCData { get; set; }

        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Panel Name")]
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the panel name c data.
        /// </summary>
        /// <value>
        /// The panel name c data.
        /// </value>
        [XmlElement("panel_name", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement PanelNameCData { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ScoringDeliverableModel"/> is disapproved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disapproved; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Disapproved")]
        public bool Disapproved { get; set; }

        /// <summary>
        /// Percentile.
        /// </summary>
        [XmlIgnore]
        [DataTableColumnName(Name = "Percentile")]
        public int? Percentile { get; set; }

        /// <summary>
        /// Percentile.
        /// </summary>
        [XmlElement("percentile", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement PercentileCData { get; set; }

        /// <summary>
        /// Gets or sets the standard deviation.
        /// </summary>
        /// <value>
        /// The standard deviation.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Standard Deviation")]
        public float? StandardDeviation { get; set; }
        /// <summary>
        /// Gets or sets the standard deviation c data.
        /// </summary>
        /// <value>
        /// The standard deviation c data.
        /// </value>
        [XmlElement("std_dev", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement StandardDeviationCData { get; set; }

        /// <summary>
        /// Whether the application has been triaged.
        /// </summary>
        /// <value>
        /// The triaged status.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Triaged")]
        public int Triaged { get; set; }
        /// <summary>
        /// Gets or sets the triaged c data.
        /// </summary>
        /// <value>
        /// The triaged c data.
        /// </value>
        [XmlElement("triaged", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement TriagedCData { get; set; }
        /// <summary>
        /// Sets the c data.
        /// </summary>
        public void SetCData()
        {
            XmlDocument doc = new XmlDocument();
            LogNumberCData = CreateXmlDataElement(LogNumber, "string");
            PanelAbbreviationCData = CreateXmlDataElement(PanelAbbreviation, "string");
            PanelNameCData = CreateXmlDataElement(PanelName, "string");
            GlobalScoreCData = CreateXmlDataElement(FormatScore(GlobalScore), "float");
            StandardDeviationCData = CreateXmlDataElement(FormatScore(StandardDeviation), "float");
            DisapprovedCData = CreateXmlDataElement(Disapproved.ToString().ToUpper(), "boolean");
            Eval1CData = CreateXmlDataElement(FormatScore(Eval1), "float");
            Eval2CData = CreateXmlDataElement(FormatScore(Eval2), "float");
            Eval3CData = CreateXmlDataElement(FormatScore(Eval3), "float");
            Eval4CData = CreateXmlDataElement(FormatScore(Eval4), "float");
            Eval5CData = CreateXmlDataElement(FormatScore(Eval5), "float");
            Eval6CData = CreateXmlDataElement(FormatScore(Eval6), "float");
            Eval7CData = CreateXmlDataElement(FormatScore(Eval7), "float");
            Eval8CData = CreateXmlDataElement(FormatScore(Eval8), "float");
            Eval9CData = CreateXmlDataElement(FormatScore(Eval9), "float");
            Eval10CData = CreateXmlDataElement(FormatScore(Eval10), "float");
            PercentileCData = CreateXmlDataElement(Percentile, "integer");
            TriagedCData = CreateXmlDataElement(Triaged, "integer");
        }
        /// <summary>
        /// Formats score to contain one decimal point to be used in service output
        /// </summary>
        /// <param name="score">The score in float format</param>
        /// <returns></returns>
        private string FormatScore(float? score)
        {
            return score != null ? ((float)score).ToString("n1") : null;
        }
    }

    /// <summary>
    /// Container for Score deliverable data for XML serialization. Necessary to support legacy web service format.
    /// </summary>
    [XmlRoot("score_data")]
    public class ScoringDeliverableContainer
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoringDeliverableContainer"/> class.
        /// </summary>
        /// <param name="records">The row items to make up the deliverable.</param>
        public ScoringDeliverableContainer(IEnumerable<ScoringDeliverableModel> records)
        {
            var rowCount = 0;
            Rows = new List<ScoringDeliverableModel>();
            foreach (var record in records)
            {
                rowCount++;
                record.ID = rowCount;
                record.SetCData();
                Rows.Add(record);
            }
            RowCount = rowCount;
            ColumnCount = 18;
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoringDeliverableContainer"/> class.
        /// </summary>
        /// <remarks>Needed for serialization</remarks>
        internal ScoringDeliverableContainer()
        {
        }
        #region Properties
        /// <summary>
        /// Gets the column count.
        /// </summary>
        /// <value>
        /// The column count.
        /// </value>
        [XmlAttribute(AttributeName = "columns")]
        public int ColumnCount { get; set; }
        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        [XmlAttribute(AttributeName = "rows")]
        public int RowCount { get; set; }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [XmlElement(ElementName = "row")]
        public List<ScoringDeliverableModel> Rows { get; set; }
        #endregion

    }
}
