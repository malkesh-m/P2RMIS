using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.UI.Models
{
    public class UserListModel
    {
        //grid
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Institute { get; set; }
        public string Email { get; set; }

        
        public AddressModel Address { get; set; }
               
        //Filter
        //Dropdowns
        public int UserSystemRoleID { get; set; }
        public int UserStageID { get; set; }
        
        //automcompletes
        [Display(Prompt = "Enter Name, E-mail or Institution")]
        public string userFilter {get; set; }
        public int userFilterId { get; set; }
        [Display(Prompt = "Enter Fiscal year, Session or Panel Name")]
        public int panelFilter { get; set; }
        public string vendorFilter { get; set; }
     }

    public class AddressModel
    {
        public string Institution { get; set; }
    }
}
