namespace Prospects.Domain.Companies.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompanyClassification
    {
        [Key]
        public int CompanyClassificationId { get; set; }

        [Display(Name = "Descrição")]
        public string CompanyClassificationDescription { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string CompanyClassificationNotes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
