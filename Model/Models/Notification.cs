using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public partial class Notification : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public long ToUSer { get; set; }

        public int TypeOfUser { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string Text { get; set; }

        public string URl { get; set; }

        public bool Seen { get; set; }

        public bool IsAlert { get; set; }

    }
}
