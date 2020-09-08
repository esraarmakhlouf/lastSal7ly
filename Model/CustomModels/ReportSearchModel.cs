using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ReportSearchModel
    {
        public string DateRangeId { get; set; }
        public string Code { get; set; }
        public int PreviousDaysNum { get; set; }
        public string ChassisNum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long OrderActionId { get; set; }
        public long CustomerId { get; set; }

        public long UserId { get; set; }

        public long CityId { get; set; }
        public long ServiceId { get; set; }
    }

}
