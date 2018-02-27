namespace Prospects.Domain.Communications.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CommunicationType
    {
        [Key]
        public int CommunicationTypeId { get; set; }

        //[Display(Name = "Descrição")]
        //[Required(ErrorMessage = "The field {0} is required.")]
        //[MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        //[Index("CommunicationType_CommunicationTypeName_Index", IsUnique = true)]
        public string CommunicationTypeName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Communication> Communications { get; set; }

        [Display(Name ="Notas")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}
