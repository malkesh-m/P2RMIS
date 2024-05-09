using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using System;
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ProgramMechanism object
    /// </summary>
    public partial class ProgramMechanism: IStandardDateFields
    {
        /// <summary>
        /// Returns panels associated with a given mechanism
        /// </summary>
        /// <returns>Enumerable collection of SessionPanel objects</returns>
        public IEnumerable<SessionPanel> GetPanelsForMechanism()
        {
            return this.Applications.SelectMany(x => x.PanelApplications).Select(x => x.SessionPanel).Distinct();
        }
        /// <summary>
        /// Wrapper to return the award abbreviation for the ClientAwardType
        /// </summary>
        /// <returns>Award abbreviation</returns>
        public string AwardAbbreviation()
        {
            return this.ClientAwardType.AwardAbbreviation;
        }
        /// <summary>
        /// Indicates if the entity could have children.  It can have children if there is no 
        /// ParentProgramMechanismId value set. 
        /// </summary>
        /// <returns>True if the entity may have children; false otherwise</returns>
        public bool MayHaveChildern()
        {
            //
            // this is based on the assumption (which was valid at the time) that only 
            // child records will have the MechanismRelationshipTypeId values
            //
            return !this.MechanismRelationshipTypeId.HasValue;
        }

        public void PopulateSummarySetupInfo(int lastUpdatedUserId, DateTime lastUpdateDate)
        {
            this.SummarySetupLastUpdatedBy = lastUpdatedUserId;
            this.SummarySetupLastUpdatedDate = lastUpdateDate;
        }
    }
}
