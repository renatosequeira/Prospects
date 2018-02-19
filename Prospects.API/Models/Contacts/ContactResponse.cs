namespace Prospects.API.Models.Contacts
{
    using System;

    public class ContactResponse
    {
        public int ContactId { get; set; }

        public string ContactName { get; set; }

        public string ContactMobile { get; set; }

        public string ContactEmail { get; set; }

        public string ContactWebsite { get; set; }

        public string ContactCompany { get; set; }

        public DateTime AddedDate { get; set; }

        public bool Contacted { get; set; }

        public string ContactAddedBy { get; set; }

        public string ContactPositionInCompany { get; set; }

    }
}