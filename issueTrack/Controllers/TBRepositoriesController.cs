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
    public class TBRepositoriesController : Controller
    {
        private testdbEntities db = new testdbEntities();

        // GET: TBRepositories
        public ActionResult Index()
        {
            var tBRepositories = db.TBRepositories.Include(t => t.TBUser);
            return View(tBRepositories.ToList());
        }

        // GET: TBRepositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBRepository tBRepository = db.TBRepositories.Find(id);
            if (tBRepository == null)
            {
                return HttpNotFound();
            }
            return View(tBRepository);
        }

        // GET: TBRepositories/Create
        public ActionResult Create()
        {
            ViewBag.FDOwner = new SelectList(db.TBUsers, "FDEmailOrPhone", "FDNickname");
            return View();
        }

        // POST: TBRepositories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FDRepositoryID,FDOwner,FDRepositoryName,FDDescription,FDCreationTimestamp")] TBRepository tBRepository)
        {
            if (ModelState.IsValid)
            {
                db.TBRepositories.Add(tBRepository);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FDOwner = new SelectList(db.TBUsers, "FDEmailOrPhone", "FDNickname", tBRepository.FDOwner);
            return View(tBRepository);
        }

        // GET: TBRepositories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBRepository tBRepository = db.TBRepositories.Find(id);
            if (tBRepository == null)
            {
                return HttpNotFound();
            }
            ViewBag.FDOwner = new SelectList(db.TBUsers, "FDEmailOrPhone", "FDNickname", tBRepository.FDOwner);
            return View(tBRepository);
        }

        // POST: TBRepositories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FDRepositoryID,FDOwner,FDRepositoryName,FDDescription,FDCreationTimestamp")] TBRepository tBRepository)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBRepository).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FDOwner = new SelectList(db.TBUsers, "FDEmailOrPhone", "FDNickname", tBRepository.FDOwner);
            return View(tBRepository);
        }

        // GET: TBRepositories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBRepository tBRepository = db.TBRepositories.Find(id);
            if (tBRepository == null)
            {
                return HttpNotFound();
            }
            return View(tBRepository);
        }

        // POST: TBRepositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBRepository tBRepository = db.TBRepositories.Find(id);
            db.TBRepositories.Remove(tBRepository);
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
