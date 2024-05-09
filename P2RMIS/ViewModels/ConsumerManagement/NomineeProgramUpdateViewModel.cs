using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    public class NomineeProgramUpdateViewModel
    {
        public NomineeProgramUpdateViewModel() { }

        public NomineeProgramUpdateViewModel(int clientProgramId, string fiscalYear, string diseaseSite,
            int? affectedId, int? score, string comments)
        {
            ClientProgramId = clientProgramId;
            FiscalYear = fiscalYear;
            DiseaseSite = diseaseSite;
            AffectedId = affectedId;
            Score = score;
            Comments = comments;
        }
        /// <summary>
        /// Client program identifier
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Disease site
        /// </summary>
        public string DiseaseSite { get; set; }
        /// <summary>
        /// Affected entity
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
    }
}