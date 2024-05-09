using System.Linq;

namespace Sra.P2rmis.Dal
{ 
    /// <summary>
    /// Custom methods for Entity Framework's Client object. 
    /// </summary>
    public partial class Client
    {
        
        public enum ClientName
        {
            CPRIT = 9
        }

        /// <summary>
        /// Indicates if the client is "active".  By definition an active client is defined
        /// as having one ProgramYear that is not closed.
        /// </summary>
        /// <returns>True if the client is active: false otherwise</returns>
        public bool IsActive()
        {
            return this.ClientPrograms1.SelectMany(x => x.ProgramYears).Any(x => x.DateClosed == null);
        }
    }
}
