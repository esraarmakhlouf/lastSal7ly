using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class BaseEntity
    {
        [StringLength(50, ErrorMessage = "Must be at max {1} characters")]
        public string Code { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
        public long? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public long? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModificationDate { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
