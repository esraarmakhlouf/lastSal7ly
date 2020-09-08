using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.APIModels
{
    public class TechnicalProfileModel
    {
        [Required(ErrorMessage = "Required")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile Number Required")]
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "Numbers Only")]
        public string Mobile { get; set; }
        public string Location { get; set; }
    }
}
