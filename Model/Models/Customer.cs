using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public partial class Customer : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; } = "";

        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string EnglishName { get; set; } = "";

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$", ErrorMessage = "PasswordRequirements")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "NotValidEmailAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Numbers Only")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        public long? CityId { get; set; }
        public virtual City City { get; set; }

        public bool active { get; set; } = true;

        public bool EmailActivated { get; set; } = false;

        public string ImageName { get; set; }
        public string mobToken { set; get; }
        public double Pocket { get; set; } = 0;
    }
}
