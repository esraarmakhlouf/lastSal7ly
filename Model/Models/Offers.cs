using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Offers")]
    public class Offer : BaseEntity
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
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "PositiveNumbersOnly")]
        public double OfferValue { get; set; } = 0;

        //Main Item
        [Required(ErrorMessage = "Required")]
        public long MainCategoryId { get; set; }

        [ForeignKey("MainCategoryId")]
        public virtual Service Service { get; set; }

        public long? MainItemId { get; set; }
        [ForeignKey("MainItemId")]
        public virtual Item MainItem { get; set; }

        public long? FreeItemId { get; set; }
        [ForeignKey("FreeItemId")]
        public virtual Item FreeItem { get; set; }

        [RegularExpression(@"^\+?\d+$", ErrorMessage = "PositiveNumbersOnly")]
        public double MainQty { get; set; } = 0;
        [RegularExpression(@"^\+?\d+$", ErrorMessage = "PositiveNumbersOnly")]
        public double FreeQty { get; set; } = 0;

      
        public bool ApplyThroughCoupon { get; set; } = false;
        public string CouponCode { get; set; }
        //Offer Image
        public string ImagePath { get; set; }

    }
}
