namespace Prospects.Domain.Communications
{
    using Newtonsoft.Json;
    using Prospects.Domain.Communications.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Communication
    {
        public int CommunicationId { get; set; }

        [Display(Name = "Tipo")]
        public string CommunicationType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime CommunicationDate { get; set; }
        
        public string CommunicationResponsible { get; set; }

        [Display(Name = "Resposta")]
        public bool CommunicationReply { get; set; } 

        [Display(Name = "Classificação da resposta")]
        public string CommunicationReplyStatus { get; set; } //YES: unknown email; to be qualified; meeting request; prospect; not interested - NO: to be contacted; not strategic

        [Display(Name = "Descrição")]
        public string CommunicationDescription { get; set; }
       

        [Display(Name = "Identificação do contacto")]
        public int ContactId { get; set; }
        [JsonIgnore]
        public virtual Contact Contact { get; set; }


        [JsonIgnore]
        public int CommunicationTypeId { get; set; }
        [JsonIgnore]
        public virtual CommunicationType CommunicationTypeL { get; set; }
    }
}
