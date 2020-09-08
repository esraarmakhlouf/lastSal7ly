using System.ComponentModel.DataAnnotations;

namespace Model
{
    public partial class Screens : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ScreenCode { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Icon { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ModuleId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Rank { get; set; } = 1;

        public string URL { get; set; }
    }
}
