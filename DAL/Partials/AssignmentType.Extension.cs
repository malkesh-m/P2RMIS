using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ReviewStatus object. Contains lookup values within AssignmentType table.
    /// </summary>
    public partial class AssignmentType
    {
        public const int Writer = 1;
        public const int Editor = 2;
        public const int SRM = 3;
        public const int Client = 4;
        public const int SR = 5;
        public const int CR = 6;
        public const int Reader = 7;
        public const int COI = 8;
        public const int SPR = 9;

        /// <summary>
        /// List of assignment types that are expected to submit critiques
        /// </summary>
        public static readonly IList<int> CritiqueAssignments = new ReadOnlyCollection<int>(new List<int> { 5, 6, 9 });

        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static AssignmentType _default;
        public static AssignmentType Default
        {
            get
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new AssignmentType();
                }
                return _default;
            }
        }
        /// <summary>
        /// Indicates if this assignment type is a COI type
        /// </summary>
        public bool IsCoi
        {
            get { return this.AssignmentTypeId == COI; }
        }
        /// <summary>
        /// Indicates if this assignment type is a Reader type
        /// </summary>
        public bool IsReader
        {
            get { return this.AssignmentTypeId == Reader; }
        }
    }
}
