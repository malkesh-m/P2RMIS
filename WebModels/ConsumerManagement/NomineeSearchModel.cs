using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.ConsumerManagement
{
    /// <summary>
    /// Nominee model
    /// </summary>
    public class NomineeSearchModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int NomineeId { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Nominating organization
        /// </summary>
        public string NominatingOrganization { get; set; }
        /// <summary>
        /// Program
        /// </summary>
        public string Program { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Score
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// User identifier
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// User info identifier
        /// </summary>
        public int? UserInfoId { get; set; }
    }
}
