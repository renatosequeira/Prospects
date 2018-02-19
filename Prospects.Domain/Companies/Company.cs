﻿namespace Prospects.Domain.Companies
{
    using Newtonsoft.Json;
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
        public string CompanyPhone { get; set; }

        [Display(Name ="Email")]
        public string CompanyEmail { get; set; }

        [Display(Name = "Website")]
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
    }
}