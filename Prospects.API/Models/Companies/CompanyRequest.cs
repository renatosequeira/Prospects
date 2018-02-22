namespace Prospects.API.Models.Companies
{
    using Prospects.Domain.Companies;

    public class CompanyRequest : Company
    {
        public byte[] ImageArray { get; set; }
    }
}