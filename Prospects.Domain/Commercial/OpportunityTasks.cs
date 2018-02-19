using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Domain.Commercial
{
    public class OpportunityTasks
    {
        public int TaskId { get; set; }
        public int OpportunityId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDateOpened { get; set; }
        public DateTime TaskDateLimit { get; set; }
        public DateTime TaskDateClosed { get; set; }
        public string TaskPriority { get; set; }
        public string TaskNote { get; set; }
        public string TaskUser { get; set; }
        public bool TaskStatus { get; set; }

        //falta o anexo da tarefa
    }
}
