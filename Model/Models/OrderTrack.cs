using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
  public partial class OrderTrack 
    {
        [Key]
        public long Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long? CreatedBy { get; set; }
        [Required(ErrorMessage = "Required")]
       
        public long? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        //[Required(ErrorMessage = "Required")]
        public int? OrderTrackActionId { get; set; }
        [ForeignKey("OrderTrackActionId")]
        public virtual OrderTrackAction OrderTrackAction { get; set; }

    }
}
