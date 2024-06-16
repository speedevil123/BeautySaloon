using System.ComponentModel.DataAnnotations;

namespace HospitalManager.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Эл. почта обязательна!")]
        public string? EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
