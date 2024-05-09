using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using System;


namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// CrudBlock definition when adding a FiscalYear to a Program.  
    /// </summary>
    public class ProgramSetupBlock : CrudBlock<ProgramYear>, ICrudBlock
    {
        #region Construction & Setup
        /// Constructor
        /// 
        /// </summary>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <param name="clientProgramId">ClientProgram entity identifier to add the ProgramYear to</param>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="programYear">ProgramYear</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        public ProgramSetupBlock(int userId, int clientProgramId, Nullable<int> programYearId, string programYear, bool activate) :
            this(userId, clientProgramId, programYear, activate)
        {
            this.ProgramYearId = programYearId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <param name="clientProgramId">ClientProgram entity identifier to add the ProgramYear to</param>
        /// <param name="programYear">ProgramYear</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        public ProgramSetupBlock(int userId, int clientProgramId, string programYear, bool activate)
        {
            this.UserId = userId;
            this.ClientProgramId = clientProgramId;
            this.ProgramYear = programYear;
            this.Activate = activate;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <param name="clientProgramId">ClientProgram entity identifier to add the ProgramYear to</param>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        public ProgramSetupBlock(int userId, int clientProgramId, int programYearId)
        {
            this.UserId = userId;
            this.ClientProgramId = clientProgramId;
            this.ProgramYearId = programYearId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <param name="clientId"></param>
        /// <param name="programDescription"></param>
        /// <param name="programAbbreviation"></param>
        /// <param name="programYear">ProgramYear</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        public ProgramSetupBlock(int userId, int clientId, string programDescription, string programAbbreviation, string programYear, bool activate)
        {
            this.UserId = userId;
            this.ClientId = clientId;
            this.ProgramDescription = programDescription;
            this.ProgramAbbreviation = programAbbreviation;
            this.ProgramYear = programYear;
            this.Activate = activate;
        }
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
        #endregion
        #region Attributes
        /// <summary>
        /// ClientMeeting entity identifier.  
        /// </summary>
        internal Nullable<int> ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier.  
        /// </summary>
        internal Nullable<int> ProgramYearId { get; set; }
        /// <summary>
        /// ProgramYaer
        /// </summary>
        internal string ProgramYear { get; set; }
        /// <summary>
        /// Activate or deactivate the ProgramYear
        /// </summary>
        internal bool Activate { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        internal string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Program description
        /// </summary>
        internal string ProgramDescription { get; set; }
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public Nullable<int> ClientId { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">ProgramYear to populate</param>
        internal override void Populate(int userId, ProgramYear entity)
        {
            if (!IsDelete)
            {
                //
                // There are two cases we need to consider:
                //      1) both a ClientProgram & ProgramYear entity are created
                //      2) only a ProgramYear is created.
                // we can detect this by the ClientId.  It is only supplied when both are created.
                //
                entity.ClientProgramId = (this.ClientId.HasValue)? entity.ClientProgramId: this.ClientProgramId.Value;
                //
                // the ProgramYear can only be changed when added.  Otherwise
                // it is null.  If nothing is passed in then just resets it with
                // the existing.
                //
                entity.Year = this.ProgramYear ?? entity.Year;
                //
                // if the ProgramYear is to be deactivated and is not already
                // deactivated then deactivate (close) it.
                //
                if (!this.Activate & !entity.DateClosed.HasValue)
                {
                    entity.DateClosed = GlobalProperties.P2rmisDateTimeNow;
                }
                //
                // Otherwise it is to be activated.
                //
                else
                {
                    entity.DateClosed = null;
                }
            }
        }
        /// <summary>
        /// Indicates if the block has data.
        /// </summary>
        /// <returns>True if the block contains data; false otherwise</returns>
        internal override bool HasData() { return (IsAdd || IsModify); }
        #endregion
    }
}
