
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's EmailAddressType object. Contains lookup values within EmailAddressType table.
    /// </summary>
    public partial class EmailAddressType
    {
        public const int Business = 1;
        public const int Personal = 2;
        public const int Alternate = 3;
    }
}
