using System.ComponentModel.DataAnnotations;

namespace Midterm_Assignment1_Login.Models.ViewModels
{
    public class RegisterVm : LoginVm
    {
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}