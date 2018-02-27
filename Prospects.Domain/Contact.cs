namespace Prospects.Domain
{
    using Newtonsoft.Json;
    using Prospects.Domain.Communications;
    using Prospects.Domain.Companies;
    using System;
    using System.Collections.Generic;
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
        [DataType(DataType.PhoneNumber)]
        public string ContactMobile { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Category_ContactEmail_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string ContactEmail { get; set; }

        [Display(Name ="Website")]
        [DataType(DataType.Url)]
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

        [Display (Name = "Empresa")]
        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }




        //New additions
        //[DataType(DataType.Date)]
        //[Display(Name = "Data do contacto")]
        //public DateTime? ContactDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Resposta")]
        public bool ContactResult { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notas")]
        public string ContactNotes { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Perfil LinkedIn")]
        public string LinkedInProfile { get; set; }


        [JsonIgnore]
        public virtual ICollection<Communication> Communications { get; set; }
    }
}
