using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// show each criterion in criteria
    /// </summary>
    public class CriteriaDeliverableModel
    {
        [XmlElement("criterion")]
        public List<CriterionDeliverableModel> CriterionList { get; set; }
    }
    /// <summary>
    /// Criterion deliverable model
    /// </summary>
    public class CriterionDeliverableModel
    {
        #region Properties   

        /// <summary>
        /// get fiscal year
        /// </summary>
        [XmlIgnore]
        [DataTableIgnore]
        public string FiscalYear { get; set; }

        /// <summary>
        /// get receipt cycle
        /// </summary>
        [XmlIgnore]
        [DataTableIgnore]
        public int ReceiptCycle { get; set; }

        /// <summary>
        /// get award type
        /// </summary>
        [XmlIgnore]
        [DataTableIgnore]
        public string AwardType { get; set; }

        /// <summary>
        /// get Atm id
        /// </summary>
        [XmlIgnore]
        [DataTableIgnore]
        public int AtmId { get; set; }

        /// <summary>
        /// get short description
        /// </summary>
        [XmlIgnore]
        [DataTableColumnName(Name = "Short Description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [XmlElement("ecmid", typeof(int?))]
        [DataTableIgnore]
        public int? EcmId { get; set; }

        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        [XmlElement("name", typeof(string))]
        [DataTableColumnName(Name = "Criterion Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the budget comments.
        /// </summary>
        /// <value>
        /// The budget comments.
        /// </value>
        [XmlElement("abbreviation", typeof(string))]
        [DataTableColumnName(Name = "Criterion Abbreviation")]
        public string ElementAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the duration of the recommended.
        /// </summary>
        /// <value>
        /// The duration of the recommended.
        /// </value>
        [XmlElement("sortorder", typeof(int))]
        [DataTableColumnName(Name = "SortOrder")]
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the duration of the requested.
        /// </summary>
        /// <value>
        /// The duration of the requested.
        /// </value>
        [XmlElement("summstatementsortorder", typeof(string))]
        [DataTableColumnName(Name = "SummaryStatementOrder")]
        public string SumStateSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the recommended total funding.
        /// </summary>
        /// <value>
        /// The recommended total funding.
        /// </value>
        [XmlElement("wordcount", typeof(string))]
        [DataTableColumnName(Name = "WordCount")]
        public string WordCount { get; set; }

        /// <summary>
        /// Gets or sets the requested indirect costs.
        /// </summary>
        /// <value>
        /// The requested indirect costs.
        /// </value>
        [XmlElement("isscoreenabled", typeof(int))]
        [DataTableIgnore]
        public int IsScoreEnabled { get; set; }
        /// <summary>
        /// Score flag text.
        /// </summary>
        [XmlIgnore]
        [DataTableColumnName(Name = "ScoreFlag")]
        public string IsScoreEnabledText { get; set; }
        /// <summary>
        /// Gets or sets the requested direct.
        /// </summary>
        /// <value>
        /// The requested direct.
        /// </value>
        [XmlElement("istextenabled", typeof(int))]
        [DataTableIgnore]
        public int IsTextEnabled { get; set; }
        /// <summary>
        /// Text flag text.
        /// </summary>
        [XmlIgnore]
        [DataTableColumnName(Name = "TextFlag")]
        public string IsTextEnabledText { get; set; }
        /// <summary>
        /// Gets or sets the requested direct.
        /// </summary>
        /// <value>
        /// The requested direct.
        /// </value>
        [XmlElement("isoverallenabled", typeof(int))]
        [DataTableIgnore]
        public int IsOverallEnabled { get; set; }
        /// <summary>
        /// Overall flag text.
        /// </summary>
        [XmlIgnore]
        [DataTableColumnName(Name = "OverallFlag")]
        public string IsOverallEnabledText { get; set; }
        /// <summary>
        /// Gets or sets the requested direct.
        /// </summary>
        /// <value>
        /// The requested direct.
        /// </value>
        [XmlElement("evalinstructions", typeof(string))]
        [DataTableIgnore]
        public string EvalInstructions { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the requested direct.
        /// </summary>
        /// <value>
        /// The requested direct.
        /// </value>
        [XmlElement("weight", typeof(string))]
        [DataTableIgnore]
        public string Weight { get; set; } = string.Empty;
        #endregion
    }
    /// <summary>
    /// get mechanism
    /// </summary>    
    public class MechanismDeliverableModel
    {
        /// <summary>
        /// mechanism award type
        /// </summary>
        [XmlElement("awardtype", typeof(string))]
        [DataTableIgnore]
        public string AwardType { get; set; }

        /// <summary>
        /// mechanism Atm id
        /// </summary
        [XmlElement("atmid", typeof(int))]
        [DataTableIgnore]
        public int AtmId { get; set; }

        /// <summary>
        /// mechanism short description
        /// </summary>
        [XmlElement("shortdescription", typeof(string))]
        [DataTableColumnName(Name = "Short Description")]
        public string ShortDescription { get; set; }
        /// <summary>
        /// Gets or sets the criterion list.
        /// </summary>
        /// <value>
        /// The criterion list.
        /// </value>
        [XmlIgnore]
        [DataTableIgnore]
        public List<CriterionDeliverableModel> CriterionList { get; set; }
        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        [XmlElement("criteria")]
        [DataTableIgnore]
        public CriteriaDeliverableModel CriteriaHolder { get; set; }
    }
    /// <summary>
    /// show each mechanism 
    /// </summary>
    public class MechanismsDeliverableModel
    {
        [XmlElement("mechanism")]
        public List<MechanismDeliverableModel> Mechanisms { get; set; }
    }
    /// <summary>
    /// Container for criteria deliverable data for XML serialization. Necessary to support legacy web service format
    /// </summary>
    [XmlRoot("program")]
    public class CriteriaDeliverableContainer
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fiscalYear">Fiscal year.</param>
        /// <param name="receiptCycle">Receipt cycle.</param>
        public CriteriaDeliverableContainer(string fiscalYear, int receiptCycle)
        {
            FiscalYear = fiscalYear;
            ReceiptCycle = receiptCycle;
            MechanismsHolder = new MechanismsDeliverableModel();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaDeliverableContainer"/> class.
        /// </summary>
        /// <param name="records">The row items to make up the deliverable.</param>
        public CriteriaDeliverableContainer(IEnumerable<MechanismDeliverableModel> records)
        {                           
            var mechanismList = new List<MechanismDeliverableModel>();

            foreach (var record in records)
            {
                var criteriaHolder = new CriteriaDeliverableModel();
                var criterionList = new List<CriterionDeliverableModel>();     
                foreach (var item in record.CriterionList)
                {
                    FiscalYear = item.FiscalYear;                    
                    ReceiptCycle = item.ReceiptCycle;
                    item.IsOverallEnabledText = ConvertToYesNo(item.IsOverallEnabled);
                    item.IsScoreEnabledText = ConvertToYesNo(item.IsScoreEnabled);
                    item.IsTextEnabledText = ConvertToYesNo(item.IsTextEnabled);
                    criterionList.Add(item);
                }
                criteriaHolder.CriterionList = criterionList;
                var mechanism = new MechanismDeliverableModel
                {
                    AwardType = record.AwardType,                    
                    AtmId = record.AtmId,
                    ShortDescription = record.ShortDescription,                    
                    CriteriaHolder = criteriaHolder
                };
                mechanismList.Add(mechanism);
            }
            MechanismsHolder = new MechanismsDeliverableModel
            {
                Mechanisms = mechanismList
            };
        }
        /// <summary>
        /// Converts to Yes/No value.
        /// </summary>
        /// <param name="integerValue">Integer value.</param>
        /// <returns></returns>
        private string ConvertToYesNo(int integerValue)
        {
            return integerValue == 1 ? "Yes" : "No";
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="CriterionDeliverableModel"/> class.
        /// </summary>
        /// <remarks>Parameterless constructor needed for serialization</remarks>
        internal CriteriaDeliverableContainer()
        {
        }
        #region Properties
        /// <summary>
        /// award type fiscal year
        /// </summary>
        [XmlElement("fiscalyear", typeof(string))]
        public string FiscalYear { get; set; }
        /// <summary>
        /// mechanism receipt cycle
        /// </summary>
        [XmlElement("cycle", typeof(int))]
        public int ReceiptCycle { get; set; }
        /// <summary>
        /// get or set mechanism deliverable  list
        /// </summary>
        [XmlElement("mechanisms")]
        public MechanismsDeliverableModel MechanismsHolder { get; set; }

        #endregion
    }
}
