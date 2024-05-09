using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Model for budget deliverable data
    /// </summary>
    public class BudgetDeliverableModel : DataDeliverableModel
    {
        #region Properties        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataTableIgnore]
        [XmlAttribute(AttributeName = "id")]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the budget comments c data.
        /// </summary>
        /// <value>
        /// The budget comments c data.
        /// </value>
        [XmlElement("budget_comments", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement BudgetCommentsCData { get; set; }
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
        /// Gets or sets the log number cdata.
        /// </summary>
        /// <value>
        /// The log number cdata.
        /// </value>
        [XmlElement("log_no", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement LogNumberCData { get; set; }
        /// <summary>
        /// Gets or sets the budget comments.
        /// </summary>
        /// <value>
        /// The budget comments.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Budget Comments")]
        public string BudgetComments { get; set; }
        /// <summary>
        /// Sets the duration of the recommended.
        /// </summary>
        /// <value>
        /// The duration of the recommended.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Recommended Duration")]
        public float? RecommendedDuration { get; set; }
        /// <summary>
        /// Gets or sets the duration of the recommended.
        /// </summary>
        /// <value>
        /// The duration of the recommended.
        /// </value>
        [XmlElement("rec_duration", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement RecommendedDurationCData { get; set; }
        /// <summary>
        /// Sets the duration of the requested.
        /// </summary>
        /// <value>
        /// The duration of the requested.
        /// </value>     
        [XmlIgnore]
        [DataTableColumnName(Name = "Requested Duration")]
        public float? RequestedDuration { get; set; }
        /// <summary>
        /// Gets or sets the recommended total funding.
        /// </summary>
        /// <value>
        /// The recommended total funding.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Recommended Total Funding")]
        public float? TotalFunding { get; set; }
        /// <summary>
        /// Gets or sets the total funding c data.
        /// </summary>
        /// <value>
        /// The total funding c data.
        /// </value>
        [XmlElement("rec_total_funding", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement TotalFundingCData { get; set; }
               
        /// <summary>
        /// Gets or sets the requested direct.
        /// </summary>
        /// <value>
        /// The requested direct.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Requested Direct")]
        public float? RequestedDirect { get; set; }

        [XmlElement("requested_direct", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement RequestedDirectCData { get; set; }
        /// <summary>
        /// Gets or sets the requested indirect costs.
        /// </summary>
        /// <value>
        /// The requested indirect costs.
        /// </value>
        [XmlIgnore]
        [DataTableColumnName(Name = "Requested Indirect")]
        public float? RequestedIndirect { get; set; }
        /// <summary>
        /// Gets or sets the requested indirect c data.
        /// </summary>
        /// <value>
        /// The requested indirect c data.
        /// </value>
        [XmlElement("requested_indirect", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement RequestedIndirectCData { get; set; }

        /// <summary>
        /// Gets or sets the duration of the requested.
        /// </summary>
        /// <value>
        /// The duration of the requested.
        /// </value>
        [XmlElement("req_duration", typeof(XmlDataElement))]
        [DataTableIgnore]
        public XmlDataElement RequestedDurationCData { get; set; }
        /// <summary>
        /// Gets or sets the project start date.
        /// </summary>
        /// <value>
        /// The project start date.
        /// </value>
        [XmlIgnore]
        [DataTableIgnore]
        public DateTime? ProjectStartDate { private get; set; }

        /// <summary>
        /// Gets or sets the project end date.
        /// </summary>
        /// <value>
        /// The project end date.
        /// </value>
        [XmlIgnore]
        [DataTableIgnore]
        public DateTime? ProjectEndDate { private get; set; }

        /// <summary>
        /// Gets or sets the triaged.
        /// </summary>
        /// <value>
        /// The triaged.
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
        /// The default budget comment if no comment exists
        /// </summary>
        [XmlIgnore]
        [DataTableIgnore]
        public const string DefaultBudgetComment = "No Changes Recommended";
        #endregion
        #region Helpers
        /// <summary>
        /// Populates the durations for the deliverable.
        /// </summary>
        public float? GetDuration()
        {
            float? duration = null;
            if (ProjectStartDate != null && ProjectEndDate != null )
            {
                duration = (float)Math.Round((((DateTime)ProjectEndDate - (DateTime)ProjectStartDate).Days / 365.0), 2);
            }
            return duration;
        }
        /// <summary>
        /// Sets the durations.
        /// </summary>
        public void SetDurations()
        {
            RequestedDuration = GetDuration();
        }
        /// <summary>
        /// Sets the c data.
        /// </summary>
        public void SetCData()
        {
            XmlDocument doc = new XmlDocument();
            LogNumberCData = CreateXmlDataElement(LogNumber, "string");
            BudgetCommentsCData = CreateXmlDataElement(BudgetComments, "string");
            RecommendedDurationCData = CreateXmlDataElement(RecommendedDuration != null ? ((float)RecommendedDuration).ToString("n2") : null, "float");
            RequestedDurationCData = CreateXmlDataElement(RequestedDuration != null ? ((float)RequestedDuration).ToString("n2") : null, "float");
            TotalFundingCData = CreateXmlDataElement(TotalFunding != null ? Convert.ToInt32(TotalFunding): (int?)null, "integer");
            RequestedIndirectCData = CreateXmlDataElement(RequestedIndirect != null ? Convert.ToInt32(RequestedIndirect) : (int?)null, "integer");
            RequestedDirectCData = CreateXmlDataElement(RequestedDirect != null ? Convert.ToInt32(RequestedDirect) : (int?)null, "integer");
            TriagedCData = CreateXmlDataElement(Triaged, "integer");
        }
        #endregion
    }
    /// <summary>
    /// Container for budget deliverable data for XML serialization. Necessary to support legacy web service format
    /// </summary>
    [XmlRoot("budget_data")]
    public class BudgetDeliverableContainer
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetDeliverableContainer"/> class.
        /// </summary>
        /// <param name="records">The row items to make up the deliverable.</param>
        public BudgetDeliverableContainer(IEnumerable<BudgetDeliverableModel> records)
        {
            var rowCount = 0;
            Rows = new List<BudgetDeliverableModel>();
            foreach (var record in records)
            {
                rowCount++;
                record.ID = rowCount;
                record.SetDurations();
                record.SetCData();
                Rows.Add(record);
            }
            RowCount = rowCount;
            ColumnCount = 8;
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetDeliverableModel"/> class.
        /// </summary>
        /// <remarks>Parameterless constructor needed for serialization</remarks>
        internal BudgetDeliverableContainer()
        {

        }
        #region Properties
        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <value>
        /// The row count.
        /// </value>
        [XmlAttribute(AttributeName = "rows")]
        public int RowCount { get; set; }
        /// <summary>
        /// Gets the column count.
        /// </summary>
        /// <value>
        /// The column count.
        /// </value>
        [XmlAttribute(AttributeName = "columns")]
        public int ColumnCount { get; set; }
        /// <summary>
        /// Gets or sets the rows of the deliverable.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [XmlElement(ElementName = "row")]
        public List<BudgetDeliverableModel> Rows { get; set; }
        #endregion
    }
}