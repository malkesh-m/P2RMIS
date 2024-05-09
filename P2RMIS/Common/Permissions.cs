using System;
using System.Configuration;
using System.Linq;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;

namespace Sra.P2rmis.Web.Common
{
    public class Permissions
    {

        private string getPanelRolesAuth(string panelRole)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["NetSqlAzMan"].ToString();

            IAzManStorage storage = new SqlAzManStorage(connectionString);
            storage.OpenConnection();
            IAzManStore store = storage.GetStore("PRSM");
            IAzManApplication application = store.GetApplication("IES.Net");

            //get a list of operations for a particular role
            IAzManItem myParticularRole = application.GetItem("Chair");

            IAzManItem[] operations = (from m in myParticularRole.GetMembers()
                                       where m.ItemType == ItemType.Operation
                                       select m).ToArray();

            string[] authorizedList = new String[operations.Length];
            int i = 0;
            //convert to comma delimited list
            foreach (IAzManItem a in operations)
            {
                authorizedList[i] = a.Name;
                i++;
            }

            storage.CloseConnection();

            //concatenate the authorized actions into a comma delimited string
            string authorizedActionList = String.Join(",", authorizedList);
            return authorizedActionList;
        }


        public bool ckPanelPermission(string panelRole, string permission)
        {
            string permissionList = getPanelRolesAuth(panelRole);
            Array permList = permissionList.Split(',').ToArray();

            foreach (String a in permList)
            {
                if (String.Compare(a, permission, true) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }

}