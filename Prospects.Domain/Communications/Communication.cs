namespace Prospects.Domain.Communications
{
    using System;

    public class Communication
    {
        public int CommunicationId { get; set; }
        public int ContactId { get; set; }
        public string CommunicationType { get; set; }
        public DateTime CommunicationDate { get; set; }
        public string CommunicationResponsible { get; set; }
        public bool CommunicationReply { get; set; } 
        public string CommunicationReplyStatus { get; set; } //YES: unknown email; to be qualified; meeting request; prospect; not interested - NO: to be contacted; not strategic
        public string CommunicationDescription { get; set; }
    }
}
