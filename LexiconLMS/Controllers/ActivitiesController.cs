using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexiconLMS.Models;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.Module);
            return View(activities.ToList());
        }

        // GET: Activities/ActivityDetails/5
        [AllowAnonymous]
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult ActivityDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/AddActivity
        public ActionResult AddActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleId", "Name", id);
            var activity = new Activity() {ModuleId = (int) id};
            return View(activity);
        }

        // POST: Activities/AddActivity
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddActivity([Bind(Include = "ActivityId,Type,StartTime,EndTime,Description,ModuleId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("ModuleDetails", "Modules", new { id = activity.ModuleId });
            }

            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleId", "Name", activity.ModuleId);
            return View(activity);
        }

        // GET: Activities/EditActivity/5
        public ActionResult EditActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleId", "Name", activity.ModuleId);
            return View(activity);
        }

        // POST: Activities/EditActivity/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditActivity([Bind(Include = "ActivityId,Type,StartTime,EndTime,Description,ModuleId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleId", "Name", activity.ModuleId);
            return View(activity);
        }

        // GET: Activities/DeleteActivity/5
        public ActionResult DeleteActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/DeleteActivity/5
        [HttpPost, ActionName("DeleteActivity")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("ModuleDetails", "Modules", new { id = activity.ModuleId });
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
