using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Model.APIModels
{
    public class ApiUserLoginModel
    {
        [Required]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Numbers Only")]
        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class ApiLoginModel
    {
        [Required]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "NotValidEmailAddress")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
