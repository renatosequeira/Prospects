namespace Prospects.API.Controllers.Companies
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using API.Models.Companies;
    using Domain;
    using Domain.Companies;
    using Prospects.API.Helpers;
    using Prospects.API.Models.Contacts;

    [Authorize]
    public class CompaniesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Companies
        public async Task<IHttpActionResult> GetCompanies()
        {
            var companies = await db.Companies.ToListAsync();

            var companiesResponse = new List<CompanyResponse>();

            foreach (var company in companies)
            {
                var contactsResponse = new List<ContactResponse>();

                foreach (var contact in company.Contacts)
                {
                    contactsResponse.Add(new ContactResponse
                    {
                        AddedDate = contact.AddedDate,
                        ContactAddedBy = contact.ContactAddedBy,
                        ContactCompany = contact.ContactCompany,
                        Contacted = contact.Contacted,
                        ContactEmail = contact.ContactEmail,
                        ContactId = contact.ContactId,
                        ContactMobile = contact.ContactMobile,
                        ContactName = contact.ContactName,
                        ContactPositionInCompany = contact.ContactPositionInCompany,
                        ContactWebsite = contact.ContactWebsite,
                        
                    });
                }

                companiesResponse.Add(new CompanyResponse
                {
                    AddedDate = company.AddedDate,
                    Capital = company.Capital,
                    CompanyAddedBy = company.CompanyAddedBy,
                    CompanyAddress = company.CompanyAddress,
                    CompanyEmail = company.CompanyEmail,
                    CompanyId = company.CompanyId,
                    CompanyLegalForm = company.CompanyLegalForm,
                    CompanyName = company.CompanyName,
                    CompanyNIF = company.CompanyNIF,
                    CompanyNotes = company.CompanyNotes,
                    CompanyPhone = company.CompanyPhone,
                    CompanyProspectlStatus = company.CompanyProspectlStatus,
                    CompanySector = company.CompanySector,
                    CompanyWebsite = company.CompanyWebsite,
                    Image = company.Image,
                    Status = company.Status,
                    Contacts = contactsResponse,
                    Latitude = company.Latitude,
                    Longitude = company.Longitude
                });
            }

            return Ok(companiesResponse);
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> GetCompany(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCompany(int id, CompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.CompanyId)
            {
                return BadRequest();
            }

            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                //var folder = "~/Content/Images";
                var folder = "~/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    request.Image = fullPath;
                }
            }

            var company = ToCompany(request);

            db.Entry(company).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("Este NIF já se existe na base de dados!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> PostCompany(CompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                //var folder = "~/Content/Images";
                var folder = "~/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    request.Image = fullPath;
                }
            }

            var company = ToCompany(request);

            db.Companies.Add(company);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if(ex.InnerException != null && 
                    ex.InnerException.InnerException != null && 
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("Este NIF já se existe na base de dados!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }

            }

            return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);
        }

        private Company ToCompany(CompanyRequest request)
        {
            return new Company
            {
                AddedDate = request.AddedDate,
                Capital = request.Capital,
                CompanyAddedBy = request.CompanyAddedBy,
                CompanyAddress = request.CompanyAddress,
                CompanyEmail = request.CompanyEmail,
                CompanyId = request.CompanyId,
                CompanyLegalForm = request.CompanyLegalForm,
                CompanyName = request.CompanyName,
                CompanyNIF = request.CompanyNIF,
                CompanyNotes = request.CompanyNotes,
                CompanyPhone = request.CompanyPhone,
                CompanyProspectlStatus = request.CompanyProspectlStatus,
                CompanySector = request.CompanySector,
                CompanyWebsite = request.CompanyWebsite,
                Contacts = request.Contacts,
                Image = request.Image,
                Status = request.Status,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> DeleteCompany(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    return BadRequest("Esta empresa não pode ser apagada porque tem colaboradores relacionados!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }

            }

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.CompanyId == id) > 0;
        }
    }
}