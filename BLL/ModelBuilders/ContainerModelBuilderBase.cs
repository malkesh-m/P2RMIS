using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;

namespace Sra.P2rmis.Bll.ModelBuilders
{
    /// <summary>
    /// 
    /// </summary>
    internal class ContainerModelBuilderBase: ModelBuilderBase
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="userId">User entity identifier</param>
        public ContainerModelBuilderBase(IUnitOfWork unitOfWork, int userId): base(unitOfWork, userId)
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        public ContainerModelBuilderBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #endregion
        #region ServicesProvided
        /// <summary>
        /// Build the model
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Build is not supported for ContainerModelBuilders</exception>
        public override IBuiltModel Build()
        {
            throw new NotSupportedException("Build() is not supported for ContainerModelBuilders.  Call BuildContainer().");
        }
        /// <summary>
        /// Build the container.
        /// </summary>
        /// <returns>Model container.</returns>
        public virtual void BuildContainer()
        {
            throw new NotSupportedException("Default BuildContainer() implementation called.");
        }
        #endregion
    }
}
