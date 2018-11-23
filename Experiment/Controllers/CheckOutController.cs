using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Experiment.Models;

namespace Experiment.Controllers
{
    public class CheckOutController : Controller
    {
        private GilgalbyteDbContext db = new GilgalbyteDbContext();

        // GET: CheckOut
        public async Task<ActionResult> Index()
        {
            return View(await db.CheckOuts.ToListAsync());
        }

        // GET: CheckOut/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = await db.CheckOuts.FindAsync(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // GET: CheckOut/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckOut/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CheckOutDate,Date,Time")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.CheckOuts.Add(checkOut);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(checkOut);
        }

        // GET: CheckOut/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = await db.CheckOuts.FindAsync(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOut/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CheckOutDate,Date,Time")] CheckOut checkOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkOut).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(checkOut);
        }

        // GET: CheckOut/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckOut checkOut = await db.CheckOuts.FindAsync(id);
            if (checkOut == null)
            {
                return HttpNotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CheckOut checkOut = await db.CheckOuts.FindAsync(id);
            db.CheckOuts.Remove(checkOut);
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
