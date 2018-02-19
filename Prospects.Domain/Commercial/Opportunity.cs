using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Domain.Commercial
{
    public class Opportunity
    {
        public int OpportunityId { get; set; }
        public string ContactId { get; set; }
        public string OpportunityStatus { get; set; } //InitialMeeting; Commercial Proposition Requested; Comercial Proposition Sent; 
        public DateTime OpportunityDateOpened { get; set; }
        public DateTime OpportunityDateClosed { get; set; }
        public string OpportunityOwner { get; set; }
        public string OpportunityUser { get; set; }
    }
}
