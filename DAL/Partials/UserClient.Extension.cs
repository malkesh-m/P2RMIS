using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserClient object. 
    /// </summary>		
    public partial class UserClient : IStandardDateFields
    {
        public void Populate(int userId, int clientId)
        {
            this.UserID = userId;
            this.ClientID = clientId;
        }
    }
}
