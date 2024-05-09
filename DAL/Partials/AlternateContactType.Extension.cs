
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods/properties for Entity Framework's AlternateContactType object.
    /// </summary>
    public partial class AlternateContactType
    {
        /// <summary>
        /// Lookup constant correlation values for AlternateContactTypeId and AlternateContactType
        /// </summary>
        public const int Assistant = 1;
        public const int Spouse = 2;
        public const int Other = 3;
        public const int Emergency = 4;
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Can be deleted.  No reference</remarks>
        public bool IsSpouse
        {
            get { return this.AlternateContactTypeId == Spouse; }
        }
    }
}
