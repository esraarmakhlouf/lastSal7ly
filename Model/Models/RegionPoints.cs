using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
  public partial class RegionPoints : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public double lat { get; set; }

        public double lng { get; set; }

        [Required(ErrorMessage = "Required")]
        public long RegionId { get; set; }


    }
}
