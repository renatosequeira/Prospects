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
    using System.Linq;

    [Authorize]
    public class CompaniesController : ApplicationBaseController
    {

        private DataContextLocal db = new DataContextLocal();

        // GET: Companies
        public async Task<ActionResult> Index(string searchBy, string search)
        {
            if (searchBy == "CompanyName")
            {
                return View(await db.Companies.Where(c => c.CompanyName.StartsWith(search) || search == null).ToListAsync());
            }
            else if(searchBy == "CompanySector")
            {
                return View(await db.Companies.Where(c => c.CompanySector.StartsWith(search) || search == null).ToListAsync());
            }
            else
            {
                switch (search)
                {
                    case "on":
                        return View(await db.Companies.Where(c => c.Status || search == null).ToListAsync());
                    case "ON":
                        return View(await db.Companies.Where(c => c.Status || search == null).ToListAsync());
                    case "off":
                        return View(await db.Companies.Where(c => c.Status == false || search == null).ToListAsync());
                    case "Off":
                        return View(await db.Companies.Where(c => c.Status == false || search == null).ToListAsync());
                    case "OFF":
                        return View(await db.Companies.Where(c => c.Status == false || search == null).ToListAsync());
                    default:
                        return View(await db.Companies.ToListAsync());
                        //return View(await db.Companies.Where(c => c.CompanyNIF == search || search == null).ToListAsync()); 
                }
            } 
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
            ViewBag.ActivitySectorId = new SelectList(db.ActivitySectors.OrderBy(c => c.Description), "ActivitySectorId", "Description");
            ViewBag.LegalFormId = new SelectList(db.LegalForms.OrderBy(c => c.LegalFormDescription), "LegalFormId", "LegalFormDescription");
            ViewBag.ComercialStatusId = new SelectList(db.ComercialStatus.OrderBy(c => c.CommercialStatusDescription), "ComercialStatusId", "CommercialStatusDescription");
            ViewBag.CompanyClassificationId = new SelectList(db.CompanyClassifications.OrderBy(c => c.CompanyClassificationDescription), "CompanyClassificationId", "CompanyClassificationDescription");

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
                //var folder = "~/Content/Images";
                var folder = "~/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var company = ToCompany(view);
                company.Image = pic;

                company.AddedDate = DateTime.Today;
                company.CompanyAddedBy = User.Identity.Name;

                if (string.IsNullOrEmpty(company.Latitude))
                {
                    company.Latitude = "0";
                }

                if (string.IsNullOrEmpty(company.Longitude))
                {
                    company.Longitude = "0";
                }

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
                Status = view.Status,
                Latitude = view.Latitude,
                Longitude = view.Longitude,
                CAEPrincipal = view.CAEPrincipal,
                ActivitySector = view.ActivitySector,
                ActivitySectorId = view.ActivitySectorId,
                LegalForm = view.LegalForm,
                LegalFormId = view.LegalFormId,
                StrategicClient = view.StrategicClient,
                ComercialStatusId = view.ComercialStatusId,
                ComercialStatus = view.ComercialStatus,
                CompanyClassification = view.CompanyClassification,
                CompanyClassificationId = view.CompanyClassificationId
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

            if(company.ComercialStatusId == null)
            {
                company.ComercialStatusId = 1;
            }

            if(company.ActivitySectorId == null)
            {
                company.ActivitySectorId = 10;
            }
            
            if(company.LegalFormId == null)
            {
                company.LegalFormId = 4;
            }

            if(company.ComercialStatusId == null)
            {
                company.ComercialStatusId = 6;
            }

            if(company.CompanyClassificationId == null)
            {
                company.CompanyClassificationId = 3;
            }

            ViewBag.ActivitySectorId = new SelectList(db.ActivitySectors.OrderBy(c => c.Description), "ActivitySectorId", "Description", company.ActivitySectorId);
            ViewBag.LegalFormId = new SelectList(db.LegalForms.OrderBy(c => c.LegalFormDescription), "LegalFormId", "LegalFormDescription", company.LegalFormId);
            ViewBag.ComercialStatusId = new SelectList(db.ComercialStatus.OrderBy(c => c.CommercialStatusDescription), "ComercialStatusId", "CommercialStatusDescription", company.ComercialStatusId);
            ViewBag.CompanyClassificationId = new SelectList(db.CompanyClassifications.OrderBy(
                c => c.CompanyClassificationDescription), 
                "CompanyClassificationId",
                "CompanyClassificationDescription", 
                company.CompanyClassificationId);
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
                Status = company.Status,
                Latitude = company.Latitude,
                Longitude = company.Longitude,
                CAEPrincipal = company.CAEPrincipal,
                ActivitySector = company.ActivitySector,
                ActivitySectorId = company.ActivitySectorId,
                LegalForm = company.LegalForm,
                LegalFormId = company.LegalFormId,
                StrategicClient = company.StrategicClient,
                ComercialStatus = company.ComercialStatus,
                ComercialStatusId = company.ComercialStatusId,
                CompanyClassificationId = company.CompanyClassificationId,
                CompanyClassification = company.CompanyClassification
            };
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyView view)
        {
            ViewBag.ActivitySectorId = new SelectList(db.ActivitySectors.OrderBy(c => c.Description), "ActivitySectorId", "Description", view.ActivitySectorId);
            ViewBag.LegalFormId = new SelectList(db.LegalForms.OrderBy(c => c.LegalFormDescription), "LegalFormId", "LegalFormDescription", view.LegalFormId);
            ViewBag.ComercialStatusId = new SelectList(db.ComercialStatus.OrderBy(c => c.CommercialStatusDescription), "ComercialStatusId", "CommercialStatusDescription", view.ComercialStatusId);
            ViewBag.CompanyClassificationId = new SelectList(db.CompanyClassifications.OrderBy(
                c => c.CompanyClassificationDescription),
                "CompanyClassificationId",
                "CompanyClassificationDescription",
                view.CompanyClassificationId);

            if (view.ActivitySectorId == 10)
            {
                ViewBag.Message = "Seleccione um setor de atividade valido";
                return View();
            }

            if (view.LegalFormId == 4)
            {
                ViewBag.Message = "Seleccione uma forma legal valido";
                return View();
            }

            if (view.ComercialStatusId == 6)
            {
                ViewBag.Message = "Seleccione um estado comercial valido";
                return View();
            }

            if (view.CompanyClassificationId == 3)
            {
                ViewBag.Message = "Seleccione uma classifcação valida";
                return View();
            }

            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Images";

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
