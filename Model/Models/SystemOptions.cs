using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("SystemOptions")]
    public class SystemOption : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string ArabicName { get; set; }

        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string CategoryArName { get; set; }

        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string CategoryEnName { get; set; }

        [StringLength(200, ErrorMessage = "Must be at max {1} characters")]
        public string ControlType { get; set; }

        public string DropdownOptions { get; set; }
        public string DefaultValue { get; set; }
        public string Value { get; set; }
        public string ArabicValue { get; set; }
        public string EnglishValue { get; set; }
        public string Title { get; set; }

        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public bool IsReadOnly { get; set; } = false;
        public bool IsHidden { get; set; } = false;

    }
}
