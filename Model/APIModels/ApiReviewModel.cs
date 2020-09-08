using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.APIModels
{
    public class ApiReviewItemModel
    {
        [Required(ErrorMessage = "Required")]
        public long OrderId { get; set; }
        public long ItemId { get; set; }

        public int Rate { get; set; }
        public string Review { get; set; }
    }

    public class ApiReviewServiceModel
    {
        [Required(ErrorMessage = "Required")]
        public long OrderId { get; set; }
        public long ServiceId { get; set; }

        public int Rate { get; set; }
        public string Review { get; set; }
    }
}
