using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
  public partial class OrderServices : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required(ErrorMessage = "Required")]
        public long ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public string ServiceImage { get; set; }
        public string ServiceComment { get; set; }

        [StringLength(500)]
        public string Review { get; set; }
        public int? Rate { get; set; }
        public bool IsPriority { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Compare("DeliverTimeTo",ErrorMessage = "DeliverTimeFrom less than DeliverTimeTo")]
        public DateTime DeliverTimeFrom { get; set; }
        public DateTime DeliverTimeTo { get; set; }
        public string PromoCode { get; set; }
        public double Price { get; set; }
        public double DiscounttPrice { get; set; }

    }
}
