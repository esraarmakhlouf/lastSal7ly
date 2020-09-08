using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Decor.Model
{
    public class ErrorViewModel1
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}