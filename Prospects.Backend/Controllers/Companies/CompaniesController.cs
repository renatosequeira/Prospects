namespace Prospects.Backend.Controllers.Companies
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Prospects.Backend.Models;
    using Prospects.Domain.Companies;
    using Prospects.Backend.Helpers;

    [Authorize]
    public class CompaniesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Companies
        public async Task<ActionResult> Index()
        {
            return View(await db.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company company = await db.Companies.FindAsync(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var company = ToCompany(view);
                company.Image = pic;

                company.AddedDate = DateTime.Today;

                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Company ToCompany(CompanyView view)
        {
            return new Company
            {
                AddedDate = view.AddedDate,
                Capital = view.Capital,
                CompanyAddedBy = view.CompanyAddedBy,
                CompanyAddress = view.CompanyAddress,
                CompanyEmail = view.CompanyEmail,
                CompanyId = view.CompanyId,
                CompanyLegalForm = view.CompanyLegalForm,
                CompanyName = view.CompanyName,
                CompanyNIF = view.CompanyNIF,
                CompanyNotes = view.CompanyNotes,
                CompanyPhone = view.CompanyPhone,
                CompanyProspectlStatus = view.CompanyProspectlStatus,
                CompanySector = view.CompanySector,
                CompanyWebsite = view.CompanyWebsite,
                Contacts = view.Contacts,
                Image = view.Image,
                Status = view.Status
            };
        }

        // GET: Companies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var company = await db.Companies.FindAsync(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            var view = ToView(company);

            return View(view);
        }

        private CompanyView ToView(Company company)
        {
            return new CompanyView
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
                Contacts = company.Contacts,
                Image = company.Image,
                Status = company.Status
            };
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var company = ToCompany(view);
                company.Image = pic;

                company.AddedDate = DateTime.Today;

                db.Entry(company).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Companies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Company company = await db.Companies.FindAsync(id);

            if (company.Contacts.Count > 0)
            {
                ViewBag.Message = "Esta empresa tem contactos associados e não pode ser apagada!";
                return View();
            }
            else
            {
                db.Companies.Remove(company);
                await db.SaveChangesAsync();
                
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
