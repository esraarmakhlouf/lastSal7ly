using System.ComponentModel.DataAnnotations;

namespace Model
{
  public partial class OrderItems : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Required(ErrorMessage = "Required")]
        public long Quantity { get; set; }

        [Required(ErrorMessage = "Required")]
        //actual item price when purchase
        public double Price { get; set; }
        //actual purchase price after discount 
        public double DiscountPrice { get; set; }

        [StringLength(500)]
        public string Review { get; set; }
        public int? Rate { get; set; }
    }
}
