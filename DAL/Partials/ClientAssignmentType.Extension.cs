namespace Sra.P2rmis.Dal
{
    public partial class ClientAssignmentType
    {
        /// <summary>
        /// Returns null if no AssignmentTypeId assigned.  Primarily used for Linq retrieval
        /// to indicate there was no selection.
        /// </summary>
        public int? NullableAssignmentTypeId
        {
            get
            {
                int? result = null;

                if (this.AssignmentTypeId > 0)
                {
                    result = this.AssignmentTypeId;
                }
                return result;
            }
        }
        /// <summary>
        /// Indicates if this assignment type is a COI type
        /// </summary>
        public bool IsCoi
        {
            get { return this.AssignmentType.IsCoi; }
        }
        /// <summary>
        /// Indicates if this assignment type is a Reader type
        /// </summary>
        public bool IsReader
        {
            get { return this.AssignmentType.IsReader; }
        }
    }
}
