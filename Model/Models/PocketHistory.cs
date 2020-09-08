using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public partial class PocketHistory : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public long ToUSer { get; set; }
        public int TypeUser { get; set; }
        public int Transaction { get; set; }
        public double Value { get; set; } = 0;
        public string Text { get; set; } 
    }
}
