using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issueTrack.Models;

namespace issueTrack.Controllers
{
    public class IssueController : Controller
    {
        private testdbEntities db = new testdbEntities();

        // GET: Issue
        public ActionResult Index()
        {
            var tBIssues = db.TBIssues.Include(t => t.TBCreator);
            return View(tBIssues.ToList());
        }

        // GET: Issue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBIssue tBIssue = db.TBIssues.Find(id);
            if (tBIssue == null)
            {
                return HttpNotFound();
            }
            return View(tBIssue);
        }

        // GET: Issue/Create
        public ActionResult Create()
        {
            ViewBag.FDRepositoryID = new SelectList(db.TBCreators, "FDRepositoryID", "FDToken");
            return View();
        }

        // POST: Issue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FDIssueID,FDCreator,FDRepositoryID,FDIssueTitle,FDIssueContent,FDState,FDCreationTimestamp")] TBIssue tBIssue)
        {
            if (ModelState.IsValid)
            {
                db.TBIssues.Add(tBIssue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FDRepositoryID = new SelectList(db.TBCreators, "FDRepositoryID", "FDToken", tBIssue.FDRepositoryID);
            return View(tBIssue);
        }

        // GET: Issue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBIssue tBIssue = db.TBIssues.Find(id);
            if (tBIssue == null)
            {
                return HttpNotFound();
            }
            ViewBag.FDRepositoryID = new SelectList(db.TBCreators, "FDRepositoryID", "FDToken", tBIssue.FDRepositoryID);
            return View(tBIssue);
        }

        // POST: Issue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FDIssueID,FDCreator,FDRepositoryID,FDIssueTitle,FDIssueContent,FDState,FDCreationTimestamp")] TBIssue tBIssue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBIssue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FDRepositoryID = new SelectList(db.TBCreators, "FDRepositoryID", "FDToken", tBIssue.FDRepositoryID);
            return View(tBIssue);
        }

        // GET: Issue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBIssue tBIssue = db.TBIssues.Find(id);
            if (tBIssue == null)
            {
                return HttpNotFound();
            }
            return View(tBIssue);
        }

        // POST: Issue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBIssue tBIssue = db.TBIssues.Find(id);
            db.TBIssues.Remove(tBIssue);
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
