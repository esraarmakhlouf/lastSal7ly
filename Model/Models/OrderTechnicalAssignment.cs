using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class OrderTechnicalAssignment
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? ModificationDate { get; set; }

        //[Required(ErrorMessage = "Required")]
        public long? TechnicalUserId { get; set; }
        [ForeignKey("TechnicalUserId")]
        public virtual Users TechnicalUser { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int status { get; set; }
    }
}
