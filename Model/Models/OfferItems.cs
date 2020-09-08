using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OfferItems")]
    public class OfferItem
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long OfferId { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ItemId { get; set; }
    }
}
