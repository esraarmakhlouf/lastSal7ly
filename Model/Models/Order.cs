using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public partial class Order : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public double TechCommission { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
        //[Required(ErrorMessage = "Required")]
        public long? ResponsibleUserId { get; set; }
        [ForeignKey("ResponsibleUserId")]
        public virtual Users ResponsibleUser { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm:ss}")]
        public DateTime? DeliverDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? OrderTrackActionId { get; set; }
        [ForeignKey("OrderTrackActionId")]
        public virtual OrderTrackAction OrderTrackAction { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual OrderServices OrderService { get; set; }
        public virtual ICollection<OrderTrack> OrderTracks { get; set; }
        public virtual ICollection<OrderTechnicalAssignment> OrderTechAssign { get; set; }
        public virtual ICollection<OrderServicesImages> ServicesImages { get; set; }

        public int ResponsibleRate { get; set; }
        public string ResponsibleReview { get; set; }

    }
}
