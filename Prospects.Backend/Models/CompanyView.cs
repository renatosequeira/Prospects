namespace Prospects.Backend.Models
{
    using Prospects.Domain.Companies;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class CompanyView : Company
    {
        [Display(Name = "Logotipo empresa")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}