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
    [Authorize]
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
            
        // GET: Modules/ModuleDetails/5
        public ActionResult ModuleDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        [Authorize(Roles = "Teacher")]
        // GET: Modules/AddModule
        public ActionResult AddModule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", id);
            var module = new Module() {CourseId = (int) id};
            return View(module);
        }

        // POST: Modules/AddModule
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddModule([Bind(Include = "ModuleId,Name,Description,StartDate,EndDate,CourseId")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(module);
                db.SaveChanges();
                return RedirectToAction("CourseDetails", "Courses", new { id = module.CourseId });
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", module.CourseId);
            return View(module);
        }

        // GET: Modules/EditModule/5
        [Authorize(Roles = "Teacher")]
        public ActionResult EditModule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", module.CourseId);
            return View(module);
        }

        // POST: Modules/EditModule/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult EditModule([Bind(Include = "ModuleId,Name,Description,StartDate,EndDate,CourseId")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CourseDetails", "Courses", new { id = module.CourseId });
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", module.CourseId);
            return View(module);
        }

        // GET: Modules/DeleteModule/5
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteModule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Modules.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/DeleteModule/5
        [Authorize(Roles = "Teacher")]
        [HttpPost, ActionName("DeleteModule")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Module module = db.Modules.Find(id);
            db.Modules.Remove(module);
            db.SaveChanges();
            return RedirectToAction("CourseDetails", "Courses", new { id = module.CourseId });
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
