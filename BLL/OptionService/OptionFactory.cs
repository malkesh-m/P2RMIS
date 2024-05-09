using Sra.P2rmis.Bll.OptionService.ResetContract;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.OptionService
{
    /// <summary>
    /// Factory class & methods to instantiate an optional functionality object.
    /// </summary>
    internal static class OptionFactory
    {
        /// <summary>
        /// Factory method to create OptionAction class
        /// </summary>
        /// <param name="selection">Identifier for the object to construct.</param>
        public static IOptionAction Create(IUnitOfWork unitOfWork, int clientId, int selection)
        {
            IOptionAction result = new OptionAction();

            switch (selection)
            {
                //
                // Optional functionality is to reset the contract
                //
                case SystemConfiguration.Indexes.ResetContractOnUpdate:
                    result = new OptionContractToaster();
                    result = result.DetermineIfWanted(unitOfWork, clientId, selection) ? result : new OptionAction();
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
