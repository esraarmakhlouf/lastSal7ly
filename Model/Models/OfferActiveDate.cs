using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OfferActiveDates")]
    public class OfferActiveDate
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long OfferId { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime EndDate { get; set; }

        [NotMapped]
        public string StrStartDate { get; set; }
        [NotMapped]
        public string StrEndDate { get; set; }
    }
}
