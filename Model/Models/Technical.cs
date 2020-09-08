using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public partial class Technical
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual Users User { get; set; }
        public double Pocket { get; set; } = 0;
        [Required(ErrorMessage = "Technical Service Required")]
        public long? ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [Required(ErrorMessage = "Technical Commission Required")]
        public double Commission { get; set; }
        public bool IsParentage { get; set; } = false;
        [DataType(DataType.DateTime)]
        public DateTime LastAssignTime { get; set; } = DateTime.Now;

    }
}
