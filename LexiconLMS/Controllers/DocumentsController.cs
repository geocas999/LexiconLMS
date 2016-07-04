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
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        //2016-07-01, ym: nedan: ändrar på funktionen
        public ActionResult Create(int? courseId, int? moduleId, int? Activity)
        {
            
            var course = new RegisterDocumentModel();

            if (courseId != null)
            {
                course.CourseId = (int)courseId;

            }

            if (moduleId != null)
            {
                course.ModuleId = (int)moduleId;

            }

            if (Activity != null)
            {
                course.ActivityId = (int)Activity;

            }


            return View(course);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Teacher")]
        public ActionResult Create([Bind(Include = "DocumentId,Name,Type,Description,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {

                //UserId,CourseId,ModuleId,ActivityId

                document.TimeStamp = DateTime.Now;

                var user = db.Users.FirstOrDefault(u => u.UserName  == User.Identity.Name);
                document.UserId = user.Id;
             
                db.Documents.Add(document);
                db.SaveChanges();

                if (document.CourseId != null)
                {
                    return RedirectToAction("CourseDetails", "Courses", new { id = document.CourseId });

                }

                if (document.ModuleId != null)
                {
                    return RedirectToAction("Details", "Modules", new { id = document.ModuleId });

                }

                if (document.ActivityId != null)
                {
                    return RedirectToAction("Details", "Activities", new { id = document.ActivityId });

                }


                return RedirectToAction("Index");
            }

            return View(document);
        }

        //2016-07-01, ym: ovan: ändrar på funktionen


        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentId,Name,Type,Description,TimeStamp,Uploader,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
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
