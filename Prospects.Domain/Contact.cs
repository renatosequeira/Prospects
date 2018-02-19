namespace Prospects.Domain
{
    using Newtonsoft.Json;
    using Prospects.Domain.Companies;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
        [Display(Name ="Nome")]
        public string ContactName { get; set; }

        [Display(Name ="Telemóvel")]
        public string ContactMobile { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Category_ContactEmail_Index", IsUnique = true)]
        public string ContactEmail { get; set; }

        [Display(Name ="Website")]
        public string ContactWebsite { get; set; }

        [Display(Name = "Empresa")]
        public string ContactCompany { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Contactado")]
        public bool Contacted { get; set; }

        public string ContactAddedBy { get; set; }

        [Display(Name = "Função na empresa")]
        public string ContactPositionInCompany { get; set; }


        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }
    }
}
