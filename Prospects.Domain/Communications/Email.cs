using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Domain.Communications
{
    public class Email
    {
        public int EmailId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
