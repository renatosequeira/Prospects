namespace Prospects.Models.Contacts
{
    using System;

    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public object ContactWebsite { get; set; }
        public object ContactCompany { get; set; }
        public DateTime AddedDate { get; set; }
        public bool Contacted { get; set; }
        public object ContactAddedBy { get; set; }
        public object ContactPositionInCompany { get; set; }
    }
}
