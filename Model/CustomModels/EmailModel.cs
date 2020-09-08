using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string FromName { get; set; }
    }
}
