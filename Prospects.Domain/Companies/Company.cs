namespace Prospects.Domain.Companies
{
    using Newtonsoft.Json;
    using Prospects.Domain.Companies.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name ="Nome Empresa")]
        public string CompanyName { get; set; }

        [Display(Name ="Morada Empresa")]
        public string CompanyAddress { get; set; }

        [Display(Name ="Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string CompanyPhone { get; set; }

        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string CompanyEmail { get; set; }

        [Display(Name = "Website")]
        [DataType(DataType.Url)]
        public string CompanyWebsite { get; set; }

        [Display(Name = "Setor Atividade")]
        public string CompanySector { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        [Index("Company_CompanyNIF_Index", IsUnique = true)]
        public string CompanyNIF { get; set; }

        [Display(Name = "Forma Legal")]
        public string CompanyLegalForm { get; set; } //LDA, UNIPESSOAL, etc

        [Display(Name = "Capital Social")]
        [DataType(DataType.Currency)]
        public string Capital { get; set; } //Capital Social

        [Display(Name = "Estado")]
        public bool Status { get; set; } //aberto/ fechado

        [Display(Name = "Estado Comercial")]
        public string CompanyProspectlStatus { get; set; } //contactado, para contactar, etc

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notas")]
        public string CompanyNotes { get; set; }

        public string CompanyAddedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }

        public string Image { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Display(Name = "CAE principal")]
        public string CAEPrincipal { get; set; }

        [Display(Name = "Cliente Estratégico")]
        public bool StrategicClient { get; set; }



        #region Virtual Properties
        [Display(Name = "Sector Actividade")]
        public int? ActivitySectorId { get; set; }
        [JsonIgnore]
        public virtual ActivitySector ActivitySector { get; set; }

        [Display(Name = "Forma legal")]
        public int? LegalFormId { get; set; }
        [JsonIgnore]
        public virtual LegalForm LegalForm { get; set; }

        [Display(Name = "Estado Comercial")]
        public int? ComercialStatusId { get; set; }
        [JsonIgnore]
        public virtual ComercialStatus ComercialStatus { get; set; }

        [Display(Name = "Classificação Empresa")]
        public int? CompanyClassificationId { get; set; }
        [JsonIgnore]
        public virtual CompanyClassification CompanyClassification { get; set; }
        #endregion

    }
}
