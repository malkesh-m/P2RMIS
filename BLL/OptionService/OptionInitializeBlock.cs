using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.OptionService
{
    /// <summary>
    /// Base option block common to all functionality.
    /// </summary>
    internal interface IOptionInitializeBlock
    {
        /// <summary>
        /// Unit of work 
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Entity identifier of the user requesting the action
        /// </summary>
        int UserId { get; set; }
    }
    /// <summary>
    /// Base option block common to all functionality.
    /// </summary>
    internal class OptionInitializeBlock : IOptionInitializeBlock
    {
        /// <summary>
        /// Unit of work 
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Entity identifier of the user requesting the action
        /// </summary>
        public int UserId { get; set; }
    }
}
