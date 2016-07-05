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
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents/DocumentDetails/5
        public ActionResult DocumentDetails(int? id)
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

        // GET: Documents/AddDocument
        //2016-07-01, ym: nedan: ändrar på funktionen
        [Authorize(Roles = "Teacher")]
        public ActionResult AddDocument(int? courseId, int? moduleId, int? activityId)
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

            if (activityId != null)
            {
                course.ActivityId = (int)activityId;

            }


            return View(course);
        }

        // POST: Documents/AddDocument
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddDocument([Bind(Include = "DocumentId,Name,Type,Description,CourseId,ModuleId,ActivityId")] Document document)
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
                    return RedirectToAction("ModuleDetails", "Modules", new { id = document.ModuleId });

                }

                if (document.ActivityId != null)
                {
                    return RedirectToAction("ActivityDetails", "Activities", new { id = document.ActivityId });

                }


                return RedirectToAction("Index");
            }

            return View(document);
        }

        //2016-07-01, ym: ovan: ändrar på funktionen
        
        // GET: Documents/EditDocument/5
        [Authorize(Roles = "Teacher")]
        public ActionResult EditDocument(int? id)
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

        // POST: Documents/EditDocument/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult EditDocument([Bind(Include = "DocumentId,Name,Type,Description,TimeStamp,Uploader,CourseId,ModuleId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/DeleteDocument/5
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteDocument(int? id)
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

        // POST: Documents/DeleteDocument/5
        [HttpPost, ActionName("DeleteDocument")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
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
