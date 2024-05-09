

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's Phone object. Contains lookup values within Phonetype lookup table.
    /// </summary>
    public partial class PhoneType
    {
        public const int CellText = 9;
        public const int CellNoText = 4;
        public const int Desk = 3;
        public const int Home = 6;
        public const int WorkFax = 2;
        public const int HomeFax = 5;
    }
}
