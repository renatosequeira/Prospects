namespace Prospects.Backend.Controllers.Communications
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Prospects.Backend.Models;
    using Prospects.Domain.Communications;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System.Security.Principal;
    using System.Security.Claims;
    using System.Web.Routing;

    [Authorize]
    public class CommunicationsController : ApplicationBaseController
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Communications
        public async Task<ActionResult> Index()
        {
            var communications = db.Communications.Include(c => c.Contact);
            
            return View(await communications.ToListAsync());
        }

        // GET: Communications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = await db.Communications.FindAsync(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            return View(communication);
        }

        // GET: Communications/Create
        public ActionResult Create(int? cont)
        {
            if (cont == null)
            {
                ViewBag.ContactId = new SelectList(db.Contacts.OrderBy(c => c.ContactName), "ContactId", "ContactName");
                
            }
            else
            {
                ViewBag.ContactId = new SelectList(db.Contacts.Where(c => c.ContactId == cont), "ContactId", "ContactName");
            }

            ViewBag.CommunicationTypeId = new SelectList(db.CommunicationTypes.OrderBy(c => c.CommunicationTypeName), "CommunicationTypeId", "CommunicationTypeName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Communication communication)
        {

            if (ModelState.IsValid)
            {  
                communication.CommunicationResponsible = User.Identity.GetDisplayName();
              
                db.Communications.Add(communication);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Contacts", action = "Details", Id = communication.ContactId }));

            }

            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "ContactName", communication.ContactId);
            ViewBag.CommunicationTypeId = new SelectList(db.CommunicationTypes, "CommunicationTypeId", "CommunicationTypeName", communication.CommunicationTypeId);

            //return View(communication);

            return View(communication);

        }

        // GET: Communications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = await db.Communications.FindAsync(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "ContactName", communication.ContactId);
            return View(communication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Communication communication)
        {
            if (ModelState.IsValid)
            {
                communication.CommunicationResponsible = User.Identity.GetUserName();

                db.Entry(communication).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "ContactName", communication.ContactId);
            return View(communication);
        }

        // GET: Communications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communication communication = await db.Communications.FindAsync(id);
            if (communication == null)
            {
                return HttpNotFound();
            }
            return View(communication);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Communication communication = await db.Communications.FindAsync(id);
            db.Communications.Remove(communication);
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
