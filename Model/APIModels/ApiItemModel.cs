using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.APIModels
{
    public class ApiItemModel
    {
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string Service { get; set; }
        public string Image { get; set; }
        public bool? IsFavourite { get; set; } = false;
    }
    public class ApiItemDetailsModel : ApiItemModel
    {
        [Required]
        public string Description { get; set; }
        public object Images { get; set; }
        public double Rate { get; set; }
    }
}
