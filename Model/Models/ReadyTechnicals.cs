using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class ReadyTechnicals
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Required")]
        public long TechnicalId { get; set; }
        public virtual Users Technical { get; set; }
    }
}
