using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("JobTitles")]
    public partial class JobTitle:BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; }

        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string EnglishName { get; set; }
       


        public bool IsMaster { get; set; } = false;
    }
}
