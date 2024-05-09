using Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.Setup.Blocks;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.WebModels;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ProgramYear.
    /// </summary>
    public class ProgramYearServiceAction : ServiceAction<ProgramYear>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramYearServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// When a new ClientProgram is created this will have a value.
        /// </summary>
        protected ClientProgram NewClientProgram { get; set; }
        /// <summary>
        /// This is the CRUD'ed ProgramYear
        /// </summary>
        protected ProgramYear CRUDedProgramYear { get; set; }
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">ProgramYear entity being populated</param>
        protected override void Populate(ProgramYear entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// <summary>
        /// And we tell it how to determine if we have data
        /// </summary>
        protected override bool HasData { get { return this.Block.HasData(); } }
        #endregion
        #region Optional Overrides
        protected override bool IsDelete { get { return this.Block.IsDelete; } }
        /// <summary>
        /// Have to do it the old fashioned way.  ClientProgram does not have properties
        /// for "deleted".  Hence cannot use ServiceAction.
        /// </summary>
        protected override void PreAdd()
        {
            var block = this.Block as ProgramSetupBlock;

            if (block.ClientId.HasValue)
            {
                //
                // we create the CLientProgram the hard way
                //
                this.NewClientProgram = new ClientProgram();
                this.NewClientProgram.Populate(block.ClientId.Value, block.ProgramAbbreviation, block.ProgramDescription, this.UserId);
                UnitOfWork.ClientProgramRepository.Add(this.NewClientProgram);
            }
        }
        /// <summary>
        /// Add a ProgramYear to the new ClientProgram.
        /// </summary>
        protected override void PostAdd(ProgramYear entity)
        {
            //
            // if we have created a new ClientProgram then the ClientProgramId will
            // not have been set when the entity was populated.  In this case we
            // will need to add it to the ClientProgram directly.
            //
            if (this.NewClientProgram != null)
            {
                this.NewClientProgram.ProgramYears.Add(entity);
            }
            //
            // And we remember the ProgramYear we just created.
            //
            this.CRUDedProgramYear = entity;
        }
        /// <summary>
        /// Define the post processing here.  By definition (when the comment was written)
        /// the PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new ProgramEntityInfo(this.CRUDedProgramYear.ProgramYearId) };
            }
        }
        #endregion
    }
}
