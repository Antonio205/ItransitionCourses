using System.ComponentModel.DataAnnotations;

namespace Task_4.ViewModels
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


  


    }
}