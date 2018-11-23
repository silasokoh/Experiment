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
using Microsoft.AspNet.Identity;
using System.Globalization;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Experiment.Controllers
{
    public class CheckInController : Controller
    {
        private GilgalbyteDbContext db;
        private UserManager<MyUser> manager;
        private CultureInfo ci;
        public CheckInController()
        {
            db = new GilgalbyteDbContext();
            manager = new UserManager<MyUser>(new UserStore<MyUser>(db));
            ci = new CultureInfo("en-US");
        }

        // GET: CheckIn
        public async Task<ActionResult> Index()
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            var patient = await db.Patients.Include(u => u.User).Where(p => p.User.Id == currentUser.Id).FirstOrDefaultAsync();
            var schedules = (from s in db.Schedules where s.Patient.Id == patient.Id select s).ToArray();
            List<CheckIn> currentList = new List<CheckIn>();

            int i;
            for (i = 0; i < (schedules.Count()-1); i++)
            {
                var myCheckIn = (from c in db.CheckIns
                                where c.Schedule.Id == schedules[i].Id
                                select c).ToArray();
                currentList.Add(myCheckIn[i]);
            }

            foreach(var s in schedules)
            {
                var checkIns = (from c in db.CheckIns
                               where c.Schedule.Id == s.Id
                               select c).ToArray();


            }
            return View(currentList.ToList());                               
        }

        // GET: CheckIn/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = await db.CheckIns.FindAsync(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // GET: CheckIn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckIn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CheckInDate,Date,Time")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.CheckIns.Add(checkIn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(checkIn);
        }

        // GET: CheckIn/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = await db.CheckIns.FindAsync(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CheckInDate,Date,Time")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkIn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(checkIn);
        }

        // GET: CheckIn/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = await db.CheckIns.FindAsync(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CheckIn checkIn = await db.CheckIns.FindAsync(id);
            db.CheckIns.Remove(checkIn);
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
