using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("ItemImages")]
    public class ItemImage
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ItemId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
