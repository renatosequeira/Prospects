namespace Prospects.Domain.Companies.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LegalForm
    {
        [Key]
        public int LegalFormId { get; set; }

        [Display(Name = "Descrição")]
        public string LegalFormDescription { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string LegalFormNotes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
