namespace Prospects.API.Models.Companies
{
    using Prospects.API.Models.Contacts;
    using System;
    using System.Collections.Generic;

    public class CompanyResponse
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanySector { get; set; }

        public string CompanyNIF { get; set; }

        public string CompanyLegalForm { get; set; } //LDA, UNIPESSOAL, etc

        public string Capital { get; set; } //Capital Social

        public bool Status { get; set; } //aberto/ fechado

        public string CompanyProspectlStatus { get; set; } //contactado, para contactar, etc

        public string CompanyNotes { get; set; }

        public string CompanyAddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public List<ContactResponse> Contacts { get; set; }

        public string Image { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}