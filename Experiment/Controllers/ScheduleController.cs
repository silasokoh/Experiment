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
    public class ScheduleController : Controller
    {

        private GilgalbyteDbContext db;
        private UserManager<MyUser> manager;
        private System.Timers.Timer myTimer;
        private Schedule schedule;
        private MyUser currentUser;
        private CultureInfo ci;
        public ScheduleController()
        {
            db = new GilgalbyteDbContext();
            manager = new UserManager<MyUser>(new UserStore<MyUser>(db));
            schedule = new Schedule();
            currentUser = new MyUser();
            myTimer = new System.Timers.Timer();
            ci = new CultureInfo("en-US");
        }
        // GET: Schedule
        public async Task<ActionResult> Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var patient = await db.Patients.Include(u => u.User).Where(p => p.User.Id == currentUser.Id).FirstOrDefaultAsync();
            return View(await db.Schedules.Include(p => p.Patient).Where(s => s.Patient.Id == patient.Id).ToListAsync());
        }

        // GET: Schedule/Details/5
        public async Task<ActionResult> Details(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PIN,Scheduled,Name,ScheduleDate")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        // GET: Schedule/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PIN,Scheduled,Name,ScheduleDate")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Schedule schedule = await db.Schedules.FindAsync(id);
            db.Schedules.Remove(schedule);
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
