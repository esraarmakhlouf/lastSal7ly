using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("ForgetPasswordURLs")]
    public class ForgetPasswordURL
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Must be at max {1} characters")]
        public Guid Token { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Required")]
        public DateTime Expiration { get; set; } = DateTime.Now.AddMinutes(30);

        [Required(ErrorMessage = "Required")]
        public long ToId { get; set; }
        public int ToType { get; set; }
    }
}
