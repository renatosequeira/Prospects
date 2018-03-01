namespace Prospects.Backend.Controllers.Communications.Helpers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Prospects.Backend.Models;
    using Prospects.Domain.Communications.Helpers;

    [Authorize]
    public class CommunicationTypesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: CommunicationTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.CommunicationTypes.ToListAsync());
        }

        // GET: CommunicationTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationType communicationType = await db.CommunicationTypes.FindAsync(id);
            if (communicationType == null)
            {
                return HttpNotFound();
            }
            return View(communicationType);
        }

        // GET: CommunicationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommunicationTypes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CommunicationType communicationType)
        {
            if (ModelState.IsValid)
            {
                db.CommunicationTypes.Add(communicationType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(communicationType);
        }

        // GET: CommunicationTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationType communicationType = await db.CommunicationTypes.FindAsync(id);
            if (communicationType == null)
            {
                return HttpNotFound();
            }
            return View(communicationType);
        }

        // POST: CommunicationTypes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CommunicationTypeId,CommunicationTypeName")] CommunicationType communicationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(communicationType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(communicationType);
        }

        // GET: CommunicationTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunicationType communicationType = await db.CommunicationTypes.FindAsync(id);
            if (communicationType == null)
            {
                return HttpNotFound();
            }
            return View(communicationType);
        }

        // POST: CommunicationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CommunicationType communicationType = await db.CommunicationTypes.FindAsync(id);
            db.CommunicationTypes.Remove(communicationType);
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
