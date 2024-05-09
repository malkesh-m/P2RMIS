using System.ComponentModel.DataAnnotations;

namespace Sra.P2rmis.Web.UI.Models
{
  public class ResetModel
    {
        [Required(ErrorMessage = "Email Address Required")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        public string UserLogin { get; set; }

        public int UserID { get; set; }
        
    }   
}