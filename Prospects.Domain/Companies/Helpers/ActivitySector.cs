namespace Prospects.Domain.Companies.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActivitySector
    {
        [Key]
        public int ActivitySectorId { get; set; }

        //[Display(Name = "Descrição")]
        //[Required(ErrorMessage = "The field {0} is required.")]
        //[MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        //[Index("ActivitySector_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
