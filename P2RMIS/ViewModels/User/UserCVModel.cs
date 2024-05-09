using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.UI.Models
{
    public class UserCVModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int FileID { get; set; }
        
        [Required(ErrorMessage = "Please select a file to upload")]
        public HttpPostedFileBase fileUpload { get; set; }
    }

}