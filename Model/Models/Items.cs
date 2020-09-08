using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Items")]
    public class Item : BaseEntity
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
        public string DescriptionArabic { get; set; }
        [Required(ErrorMessage = "Required")] 
        public string DescriptionEnglish { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ServiceId { get; set; }
        public virtual Service Service { get; set; }        

        [RegularExpression(@"^\+?\d+$", ErrorMessage = "PositiveNumbersOnly")]
        public double Price { get; set; } = 0;

        public virtual ICollection<ItemImage> ItemImages { get; set; }
    }
}
