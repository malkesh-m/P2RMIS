using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.ConsumerManagement
{
    public class NomineeProgramUpdateModel
    {
        public NomineeProgramUpdateModel() {
            ProgramFiscalYears = new List<KeyValuePair<int, string>>();
        }
        /// <summary>
        /// Nominee program identifier
        /// </summary>
        public int? NomineeProgramId { get; set; }
        /// <summary>
        /// Client program identifier
        /// </summary>
        public int? ClientProgramId { get; set; }
        /// <summary>
        /// The program year identifier
        /// </summary>
        public int? ProgramYearId { get; set; }
        /// <summary>
        /// Disease site
        /// </summary>
        public string DiseaseSite { get; set; }
        /// <summary>
        /// Affected identifier
        /// </summary>
        public int? AffectedId { get; set; }
        /// <summary>
        /// Score
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Nominee type identifier
        /// </summary>
        public int? NomineeTypeId { get; set; }
        /// <summary>
        /// Program fiscal years
        /// </summary>
        public List<KeyValuePair<int, string>> ProgramFiscalYears { get; set; }
    }
}
