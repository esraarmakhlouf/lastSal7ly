using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("ServicesImages")]
    public class OrderServicesImages
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Required")]
        public long OrderId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
