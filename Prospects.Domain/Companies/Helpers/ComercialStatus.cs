namespace Prospects.Domain.Companies.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ComercialStatus
    {
        [Key]
        public int ComercialStatusId { get; set; }

        [Display(Name = "Descrição")]
        public string CommercialStatusDescription { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string CommercialStatusNotes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
