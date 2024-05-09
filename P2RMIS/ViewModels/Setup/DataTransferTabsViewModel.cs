using Sra.P2rmis.Web.ViewModels.Shared;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.Web.UI.Models
{
    public class DataTransferTabsViewModel : TabMenuViewModel
    {
        #region Constructor
        public DataTransferTabsViewModel() : base()
        {
            TabNames = new string[]
            {
                "Import Data", "Generate Data"
            };
            TabLinks = new string[]
            {
                "/Setup/ImportData", "/Setup/GenerateData"
            };
            TabRequiredPermissions = new string[]
            {
                Permissions.Setup.ImportData, Permissions.Setup.GenerateDeliverable
            };
            SetTabs();
        }
        #endregion
        #region Properties

        #endregion
    }
}