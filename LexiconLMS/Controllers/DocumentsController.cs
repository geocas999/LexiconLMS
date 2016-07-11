using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LexiconLMS.Models;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace LexiconLMS.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents/DocumentDetails/5
        public ActionResult DocumentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var document = db.Documents.Find(id);
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
                course.CourseId = (int) courseId;
            }

            if (moduleId != null)
            {
                course.ModuleId = (int) moduleId;
            }

            if (activityId != null)
            {
                course.ActivityId = (int) activityId;
            }


            return View(course);
        }

        //2016-06-08/George C. / Added functionality for uploading a file + setting relevant properties for the uploade file
        // POST: Documents/AddDocument
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        //public ActionResult AddDocument (HttpPostedFileBase file_Uploader, Document document)
        public ActionResult AddDocument([Bind(Include = "FilePath,UploadedFile,DocumentType,Name,Type,Description,CourseId,ModuleId,ActivityId")] RegisterDocumentModel document, HttpPostedFileBase UploadedFile)
        {
            string fileName = string.Empty;
            string destinationPath = string.Empty;

            if (document != null)
            {
                
                //string uploadedFile = string.Empty;

                //List<AddDocumentModel> uploadFiles = new List<AddDocumentModel>();
                //List<Document> AddDocumentModel = new List<Document>()
                fileName = Path.GetFileName(document.UploadedFile.FileName);
                destinationPath = Path.Combine(Server.MapPath("~/LMSDocuments/"), fileName);
                document.UploadedFile.SaveAs(destinationPath);
                

                if (Session["document"] != null)
                {
                    var isFileNameRepete = ((List<Document>)Session["document"]).Find(x => x.Name == fileName);
                    if (isFileNameRepete == null)
                    {
                        //uploadFiles.Add(new AddDocumentModel { Name = fileName, FilePath = destinationPath });
                        ((List<Document>)Session["document"]).Add(new Document { Name = fileName, FilePath = destinationPath });
                        ViewBag.Message = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "File already exists";
                    }
                }
                else
                {
                    //uploadFiles.Add(new AddDocumentModel { Name = fileName, FilePath = destinationPath });
                    //Session["UpLoadedFile"] = uploadFiles;
                    ViewBag.Message = "File Uploaded Successfully";
                }
            }
            //    return View();
            //}

            if (ModelState.IsValid)
            {
                document.ModuleId = document.ModuleId == 0 ? null : document.ModuleId;
                document.CourseId = document.CourseId == 0 ? null : document.CourseId;
                document.ActivityId = document.ActivityId == 0 ? null : document.ActivityId;
                ////UserId,CourseId,ModuleId,ActivityId

                document.TimeStamp = DateTime.Now;

                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                document.UserId = user.Id;
                document.FilePath = destinationPath;
                document.Name = fileName;

                db.Documents.Add(new Document() { Name = document.Name, UserId = document.UserId, FilePath = document.FilePath, DocumentType = document.DocumentType, Description = document.Description, TimeStamp = document.TimeStamp,CourseId =document.CourseId, ModuleId = document.ModuleId, ActivityId=document.ActivityId, });
                db.SaveChanges();

                if (document.CourseId != null)
                {
                    return RedirectToAction("CourseDetails", "Courses", new {id = document.CourseId});
                }

                if (document.ModuleId != null)
                {
                    return RedirectToAction("ModuleDetails", "Modules", new {id = document.ModuleId});
                }

                if (document.ActivityId != null)
                {
                    return RedirectToAction("ActivityDetails", "Activities", new {id = document.ActivityId});
                }

                return RedirectToAction("CourseDetails", "Courses", new {id = document.CourseId});
                //return RedirectToAction("Index");
            }

            return RedirectToAction("AddDocument");
            //return View(document);
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
            var document = db.Documents.Find(id);

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
        public ActionResult EditDocument(
            [Bind(Include = "DocumentId,Name,Type,Description,TimeStamp,Uploader,CourseId,ModuleId,ActivityId")] Document document)
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
            var document = db.Documents.Find(id);
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
            var document = db.Documents.Find(id);
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