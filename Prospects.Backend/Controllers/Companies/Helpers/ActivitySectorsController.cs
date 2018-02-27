using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prospects.Backend.Models;
using Prospects.Domain.Companies.Helpers;

namespace Prospects.Backend.Controllers.Companies.Helpers
{
    public class ActivitySectorsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ActivitySectors
        public async Task<ActionResult> Index()
        {
            return View(await db.ActivitySectors.ToListAsync());
        }

        // GET: ActivitySectors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySector activitySector = await db.ActivitySectors.FindAsync(id);
            if (activitySector == null)
            {
                return HttpNotFound();
            }
            return View(activitySector);
        }

        // GET: ActivitySectors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitySectors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ActivitySectorId,Description,Notes")] ActivitySector activitySector)
        {
            if (ModelState.IsValid)
            {
                db.ActivitySectors.Add(activitySector);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activitySector);
        }

        // GET: ActivitySectors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySector activitySector = await db.ActivitySectors.FindAsync(id);
            if (activitySector == null)
            {
                return HttpNotFound();
            }
            return View(activitySector);
        }

        // POST: ActivitySectors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ActivitySectorId,Description,Notes")] ActivitySector activitySector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activitySector).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activitySector);
        }

        // GET: ActivitySectors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivitySector activitySector = await db.ActivitySectors.FindAsync(id);
            if (activitySector == null)
            {
                return HttpNotFound();
            }
            return View(activitySector);
        }

        // POST: ActivitySectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivitySector activitySector = await db.ActivitySectors.FindAsync(id);
            db.ActivitySectors.Remove(activitySector);
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
