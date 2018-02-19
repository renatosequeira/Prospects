namespace Prospects.Models.Companies
{
    using System;

    public class Company
    {

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public object CompanyAddress { get; set; }
        public object CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebsite { get; set; }
        public object CompanySector { get; set; }
        public string CompanyNIF { get; set; }
        public object CompanyLegalForm { get; set; }
        public object Capital { get; set; }
        public bool Status { get; set; }
        public object CompanyProspectlStatus { get; set; }
        public object CompanyNotes { get; set; }
        public object CompanyAddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string Image { get; set; } 

    }
}
