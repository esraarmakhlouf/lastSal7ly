using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Model.APIModels
{
    public class ApiCartListModel
    {
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public long ItemId { get; set; }
        [Required]
        public long Quantity { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
