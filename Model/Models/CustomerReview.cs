
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public partial class CustomerReview 
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ImagePath { get; set; }
    }
}
