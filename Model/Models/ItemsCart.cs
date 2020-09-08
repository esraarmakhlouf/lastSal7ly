using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
  public partial class ItemsCart 
    {
        [Key]
        public long Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Required")]
        public long CustomerId { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Required(ErrorMessage = "Required")]
        public long Quantity { get; set; }
    }
}
