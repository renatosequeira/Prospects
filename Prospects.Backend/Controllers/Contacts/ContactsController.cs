namespace Prospects.Backend.Controllers.Contacts
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Prospects.Backend.Models;
    using Prospects.Domain;
    using System.Linq;
    using System.Web.Routing;

    [Authorize]
    public class ContactsController : ApplicationBaseController
    {
        private DataContextLocal db = new DataContextLocal();

        public static DateTime _addedDate { get; set; }


        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            var contacts = db.Contacts.Include(c => c.Company);
            return View(await contacts.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create(int? comp)
        {
            if(comp == null)
            {
                ViewBag.CompanyId = new SelectList(db.Companies.OrderBy(c => c.CompanyName), "CompanyId", "CompanyName");
            }
            else
            {
                ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == comp), "CompanyId", "CompanyName");
            }

            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.AddedDate = DateTime.Today;
                contact.ContactAddedBy = User.Identity.Name;

                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Companies", action = "Details", Id = contact.CompanyId }));
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", contact.CompanyId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);

            _addedDate = contact.AddedDate;

            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", contact.CompanyId);
            return View(contact);
        }

        // POST: Contacts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Edit(Contact contact)
        {

            if (ModelState.IsValid)
            {
                contact.AddedDate = _addedDate;
                db.Entry(contact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Companies", action = "Details", Id = contact.CompanyId }));
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", contact.CompanyId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contact contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            //return RedirectToAction("Index");
            return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Companies", action = "Details", Id = contact.CompanyId }));
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
