using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table ("Ordertrackaction")]
  public partial class OrderTrackAction : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; } = "";
        public string EnglishName { get; set; } = "";

    }
}
