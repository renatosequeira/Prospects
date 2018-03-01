namespace Prospects.Backend.Controllers.Companies.Helpers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Prospects.Backend.Models;
    using Prospects.Domain.Companies.Helpers;

    [Authorize]
    public class CompanyClassificationsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: CompanyClassifications
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyClassifications.ToListAsync());
        }

        // GET: CompanyClassifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyClassification companyClassification = await db.CompanyClassifications.FindAsync(id);
            if (companyClassification == null)
            {
                return HttpNotFound();
            }
            return View(companyClassification);
        }

        // GET: CompanyClassifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyClassifications/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyClassificationId,CompanyClassificationDescription,CompanyClassificationNotes")] CompanyClassification companyClassification)
        {
            if (ModelState.IsValid)
            {
                db.CompanyClassifications.Add(companyClassification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyClassification);
        }

        // GET: CompanyClassifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyClassification companyClassification = await db.CompanyClassifications.FindAsync(id);
            if (companyClassification == null)
            {
                return HttpNotFound();
            }
            return View(companyClassification);
        }

        // POST: CompanyClassifications/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyClassificationId,CompanyClassificationDescription,CompanyClassificationNotes")] CompanyClassification companyClassification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyClassification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyClassification);
        }

        // GET: CompanyClassifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyClassification companyClassification = await db.CompanyClassifications.FindAsync(id);
            if (companyClassification == null)
            {
                return HttpNotFound();
            }
            return View(companyClassification);
        }

        // POST: CompanyClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyClassification companyClassification = await db.CompanyClassifications.FindAsync(id);
            db.CompanyClassifications.Remove(companyClassification);
            await db.SaveChangesAsync();
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
