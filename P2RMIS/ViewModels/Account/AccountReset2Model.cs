using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.UI.Models
{
  public class ResetModel2
    {
      [HiddenInput(DisplayValue = false)]
      public int UserID { get; set; }
      
      [HiddenInput(DisplayValue = false)]
      public string FullUserName { get; set; }

      [HiddenInput(DisplayValue = false)]
      public int QuestionNumber { get; set; }
      [HiddenInput(DisplayValue = false)]
      public string SecurityQuestion { get; set; }

      public string UserLogin { get; set; }
           
      [Required(ErrorMessage = "An answer to the security question is required")]
      [MaxLength(100, ErrorMessage = "Answer cannot be longer than 100 characters.")]
      public string Answer { get; set; }       
    }   
}
