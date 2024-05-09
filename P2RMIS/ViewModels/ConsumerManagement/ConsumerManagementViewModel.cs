using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ConsumerManagementViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConsumerManagementViewModel() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nomineeTypes"></param>
        public ConsumerManagementViewModel(List<KeyValuePair<int, string>> nomineeTypes)
        {
            NomineeTypes = nomineeTypes; 
        }
        /// <summary>
        /// Nominee types
        /// </summary>
        public List<KeyValuePair<int, string>> NomineeTypes { get; set; }
        /// <summary>
        /// Nominee type identifier
        /// </summary>
        public int? NomineeTypeId { get; set; }
    }
}