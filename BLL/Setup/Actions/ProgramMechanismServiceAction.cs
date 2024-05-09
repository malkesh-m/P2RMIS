using Sra.P2rmis.Dal;
using System.Collections.Generic;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ProgramMechanism.
    /// </summary>
    internal class ProgramMechanismServiceAction: ServiceAction<ProgramMechanism>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramMechanismServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed ProgramMechanism
        /// </summary>
        protected ProgramMechanism CRUDedProgramMechanism { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">ProgramMechanism entity being populated</param>
        protected override void Populate(ProgramMechanism entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// <summary>
        /// And we tell it how to determine if we have data
        /// </summary>
        protected override bool HasData { get { return this.Block.HasData(); } }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// What happens before the add is done
        /// </summary>
        protected override void PreAdd()
        {
            AwardSetupBlock block = this.Block as AwardSetupBlock;
            if (block.IsPreAppAdd())
            {
                ClientAwardType entity = UnitOfWork.ClientAwardTypeRepository.GetByID(block.ClientAwardTypeId);
                PreAwardTypeCreator creator = new PreAwardTypeCreator(UnitOfWork, entity, this.UserId);
                creator.Execute();
                block.AddPreAppClientAwardType(creator.PreAppClientAwardType);
            }
        }
        /// <summary>
        /// What happens after an add is done.
        /// </summary>
        protected override void PostAdd(ProgramMechanism entity)
        {
            //
            // And we remember the ProgramMechanism we just created.
            //
            this.CRUDedProgramMechanism = entity;
        }
        /// <summary>
        /// What happens after a ProgramMechanism record is modified.  If the 
        /// ProgramMechanism has a child then specific values are updated in the 
        /// child also.
        /// </summary>
        /// <param name="entity">ProgramMechanism entity modified</param>
        protected override void PostModify(ProgramMechanism entity)
        {
            //
            // one also needs to update any associated Pre-app's.  
            //
            if (entity.MayHaveChildern())
            {
                ProgramMechanism childEntity = UnitOfWork.ProgramMechanismRepository.Select().FirstOrDefault(x => x.ParentProgramMechanismId == entity.ProgramMechanismId);
                if (childEntity != null)
                {
                    AwardSetupBlock parentBlock = this.Block as AwardSetupBlock;
                    AwardSetupBlock childBlock = AwardSetupBlock.Create(childEntity);
                    childBlock.SetValues(parentBlock.FundingOpportunityId, parentBlock.PartneringPiAllowedFlag);
                    childBlock.Populate(this.UserId, childEntity);
                    UnitOfWork.ProgramMechanismRepository.Update(childEntity);
                }
            }
        }
        protected override bool IsDelete { get { return this.Block.IsDelete; } }
        /// <summary>
        /// Define the post processing here.  By definition (when the comment was written)
        /// the PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new ProgramMechanismEntityInfo(this.CRUDedProgramMechanism.ProgramMechanismId) };
            }
        }
        #endregion
    }
}
