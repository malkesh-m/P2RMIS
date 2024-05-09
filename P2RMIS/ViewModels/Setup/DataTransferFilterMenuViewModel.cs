using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Criteria;
using System.ComponentModel.DataAnnotations;

namespace Sra.P2rmis.Web.UI.Models
{
    public class DataTransferFilterMenuViewModel
    {
        #region Constructor
        //Initialize lists
        public DataTransferFilterMenuViewModel()
        {
            Programs = new List<IClientProgramModel>();
            FiscalYears = new List<IProgramYearModel>();
            Cycles = new List<int>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the transfer types for data transfer.
        /// </summary>
        /// <value>
        /// The transfer types.
        /// </value>
        /// <remarks>If there is ever more than one type this can be populated dynamically</remarks>
        public List<string> TransferTypes => new List<string>() { "Transfer Single Mechanism" };
        /// <summary>
        /// List of Programs
        /// </summary>
        public List<IClientProgramModel> Programs { get; set; }
        /// <summary>
        /// List of Fiscal Years
        /// </summary>
        public List<IProgramYearModel> FiscalYears { get; set; }
        /// <summary>
        /// List of cycles
        /// </summary>
        public List<int> Cycles { get; set; }
        /// <summary>
        /// The selected program (required)
        /// </summary>
        [Required(ErrorMessage = "Please select a program")]
        public int SelectedProgram { get; set; }
        /// <summary>
        /// Selected fiscal year (required)
        /// </summary>
        [Required(ErrorMessage = "Please select a fiscal year")]
        public int SelectedFy { get; set; }
        /// <summary>
        /// Selected Cycle
        /// </summary>
        [Required(ErrorMessage = "Please select a cycle")]
        public int? SelectedCycle { get; set; }
        #endregion
    }
}