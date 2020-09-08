using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
  public partial class Region :BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; }

        [StringLength(500, ErrorMessage = "Must be at max {1} characters")]
        public string EnglishName { get; set; }

        public bool IsCycle { get; set; }

        public double Radius { get; set; }
        public double CenterLat { get; set; }
        public double CenterLong { get; set; }
        


    }
}
