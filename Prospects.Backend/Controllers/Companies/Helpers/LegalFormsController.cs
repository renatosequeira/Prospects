using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prospects.Domain;
using Prospects.Domain.Companies.Helpers;
using Prospects.Backend.Models;

namespace Prospects.Backend.Controllers.Companies.Helpers
{
    [Authorize]
    public class LegalFormsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: LegalForms
        public async Task<ActionResult> Index()
        {
            return View(await db.LegalForms.ToListAsync());
        }

        // GET: LegalForms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegalForm legalForm = await db.LegalForms.FindAsync(id);
            if (legalForm == null)
            {
                return HttpNotFound();
            }
            return View(legalForm);
        }

        // GET: LegalForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LegalForms/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LegalFormId,LegalFormDescription,LegalFormNotes")] LegalForm legalForm)
        {
            if (ModelState.IsValid)
            {
                db.LegalForms.Add(legalForm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(legalForm);
        }

        // GET: LegalForms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegalForm legalForm = await db.LegalForms.FindAsync(id);
            if (legalForm == null)
            {
                return HttpNotFound();
            }
            return View(legalForm);
        }

        // POST: LegalForms/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LegalFormId,LegalFormDescription,LegalFormNotes")] LegalForm legalForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(legalForm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(legalForm);
        }

        // GET: LegalForms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LegalForm legalForm = await db.LegalForms.FindAsync(id);
            if (legalForm == null)
            {
                return HttpNotFound();
            }
            return View(legalForm);
        }

        // POST: LegalForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LegalForm legalForm = await db.LegalForms.FindAsync(id);
            db.LegalForms.Remove(legalForm);
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
