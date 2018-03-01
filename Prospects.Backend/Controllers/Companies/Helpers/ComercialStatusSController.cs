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
    public class ComercialStatusSController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ComercialStatusS
        public async Task<ActionResult> Index()
        {
            return View(await db.ComercialStatus.ToListAsync());
        }

        // GET: ComercialStatusS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComercialStatus comercialStatus = await db.ComercialStatus.FindAsync(id);
            if (comercialStatus == null)
            {
                return HttpNotFound();
            }
            return View(comercialStatus);
        }

        // GET: ComercialStatusS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComercialStatusS/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ComercialStatusId,CommercialStatusDescription")] ComercialStatus comercialStatus)
        {
            if (ModelState.IsValid)
            {
                db.ComercialStatus.Add(comercialStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(comercialStatus);
        }

        // GET: ComercialStatusS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComercialStatus comercialStatus = await db.ComercialStatus.FindAsync(id);
            if (comercialStatus == null)
            {
                return HttpNotFound();
            }
            return View(comercialStatus);
        }

        // POST: ComercialStatusS/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ComercialStatusId,CommercialStatusDescription")] ComercialStatus comercialStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comercialStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(comercialStatus);
        }

        // GET: ComercialStatusS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComercialStatus comercialStatus = await db.ComercialStatus.FindAsync(id);
            if (comercialStatus == null)
            {
                return HttpNotFound();
            }
            return View(comercialStatus);
        }

        // POST: ComercialStatusS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ComercialStatus comercialStatus = await db.ComercialStatus.FindAsync(id);
            db.ComercialStatus.Remove(comercialStatus);
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
