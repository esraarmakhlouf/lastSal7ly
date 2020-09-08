using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
