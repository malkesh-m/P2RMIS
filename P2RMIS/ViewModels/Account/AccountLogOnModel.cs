using System.ComponentModel.DataAnnotations;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for Update User
    /// </summary>
    public class LogOnModel
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }



    }

}