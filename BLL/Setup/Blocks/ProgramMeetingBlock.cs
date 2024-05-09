using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for ClientMeeting (Meeting) setup.
    /// </summary>
    internal class ProgramMeetingBlock : CrudBlock<ProgramMeeting>, ICrudBlock
    {
        #region Construction & Setup
        
        public ProgramMeetingBlock(int programYearId, int clientMeetingId)
        {
            this.ProgramYearId = programYearId;
            this.ClientMeetingId = clientMeetingId;
        }

        public ProgramMeetingBlock(int programMeetingId)
        {
            this.ProgramMeetingId = programMeetingId;
        }
        #endregion
        #region Attributes

        public Nullable<int> ProgramMeetingId { get; private set; }
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; private set; }
        /// <summary>
        /// Gets the client meeting identifier.
        /// </summary>
        /// <value>
        /// The client meeting identifier.
        /// </value>
        public int ClientMeetingId { get; private set; }
        #endregion
        #region Methods
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">ProgramMeeting entity to populate</param>
        internal override void Populate(int userId, ProgramMeeting entity)
        {
            entity.ProgramYearId = this.ProgramYearId;
            entity.ClientMeetingId = this.ClientMeetingId;
        }
        #endregion
    }
}
