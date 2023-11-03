using System.ComponentModel.DataAnnotations;

namespace KeyShop.ViewModels {
    public class LoginViewModel {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "error with email")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
