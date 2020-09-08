using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.APIModels
{
    public class ApiServiceOrder
    {
        [Required(ErrorMessage = "Required")]
        public long CustomerId { get; set; }
        [Required(ErrorMessage = "Required")]
        public long ServiceId { get; set; }

        public string ServiceComment { get; set; }

        public bool IsPriority { get; set; }

        [Required(ErrorMessage = "Required")]
        //[Column(TypeName ="time")]
        [DataType(DataType.Time, ErrorMessage = "Enter Correct DeliverTimeFrom ")]
        //[JsonProperty("deliverTimeFrom")]
        //[JsonConverter(typeof(DateFormatConverter))]


        //[RegularExpression(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$", ErrorMessage = "Enter Correct DeliverTimeFrom 'HH:mm:ss'")]
        //public string DeliverTimeFrom { get; set; } = DateTime.Now.ToShortTimeString();
        public DateTime DeliverTimeFrom { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Time, ErrorMessage = "Enter Correct DeliverTimeTo ")]
        public DateTime DeliverTimeTo { get; set; }
        public string PromoCode { get; set; }
    }
    //internal class DateFormatConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    //{
    //    public DateFormatConverter()
    //    {
    //        DateTimeFormat = "HH:mm:ss";
    //    }
    //}
}
