using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issueTrack.Models;
using System.Data.SqlClient;

namespace issueTrack.Controllers
{
    public class RepositoryController : Controller
    {
        private testdbEntities db = new testdbEntities();

        // GET: Repository
        public ActionResult Index()
        {
            var tBRepositories = db.TBRepositories.Include(t => t.TBUser);
            return View(tBRepositories.ToList());
        }

        public ActionResult ValidateRepositoryName(string repositoryName)
        {
            if(repositoryName == null)
            {
                return RedirectToAction("~/");
            }
            if (Session["userid"] != null && Session["nickname"] != null)
            {
                string owner = Session["userid"].ToString();
                TBRepository tmp = db.TBRepositories.FirstOrDefault(r => r.FDRepositoryName == repositoryName && r.FDOwner == owner);
                if(tmp != null)
                {
                    return Json("Repository already exist", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("notExist", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("~/");
            }
        }

        // GET: Repository/Details/5
        public ActionResult Details(int? id, int? pageNumber)
        {
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\123\1.txt", true);
            //db.Database.Log = new Action<string>(o => System.Diagnostics.Debug.WriteLine(o));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailViewModel m = new DetailViewModel();
            TBRepository tBRepository = db.TBRepositories.Find(id);
            m.RepositoryID = id.Value;
            if (Session["creator"] == null)
            {
                if (Session["userid"] != null && Session["nickname"] != null)
                {
                    m.CreatorName = Session["nickname"].ToString();
                }
            }
            else
            {
                m.CreatorName = Session["creator"].ToString();
            }
            m.Repository = tBRepository;
            m.AllCreators = db.TBCreators.Where(r => r.FDRepositoryID == m.RepositoryID).ToList();
            for (int i = 0; i < Math.Ceiling(db.TBIssues.Count() / 10.0); i++)
            {
                m.PageList.Add(i + 1);
            }
            if (pageNumber == null)
            {
                m.AllIssues = db.TBIssues.Where(r => r.FDRepositoryID == m.RepositoryID).OrderBy(r => r.FDCreationTimestamp).Take(10).ToList();
            }
            else
            {
                m.AllIssues = db.TBIssues.Where(r => r.FDRepositoryID == m.RepositoryID).OrderBy(r => r.FDCreationTimestamp).Skip((pageNumber.Value - 1) * 10).Take(10).ToList();
            }
            

            if (tBRepository == null)
            {
                return HttpNotFound();
            }
            //sw.Close();
            return View(m);
        }

        public ActionResult AddCreator(CreatorCreateViewModel p)
        {
            //AuthenticationAndAuthorization code add here
            db.TBCreators.Add(new TBCreator { FDRepositoryID = p.RepositoryID.Value, FDCreator = p.CreatorName , FDLoginMethod = Common.Const.LoginMethod_Default , FDToken = p.CreatorName });
            db.SaveChanges();
            return RedirectToAction("Details", "Repository", new { id = p.RepositoryID });
        }

        public ActionResult AddIssue(IssueCreateViewModel p)
        {
            //AuthenticationAndAuthorization code add here
            db.TBIssues.Add(new TBIssue { FDCreator = p.CreatorName , FDRepositoryID = p.RepositoryID.Value , FDIssueTitle = p.IssueTitle , FDIssueContent = p.IssueContent });
            db.SaveChanges();
            return RedirectToAction("Details", "Repository", new { id = p.RepositoryID });
        }

        public ActionResult Details2(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBRepository tBRepository = db.TBRepositories.FirstOrDefault(r => r.FDRepositoryName == name);
            if (tBRepository == null)
            {
                return HttpNotFound();
            }
            return View("Details",tBRepository);
        }

        // GET: Repository/Create
        //public ActionResult Create()
        //{
        //    ViewBag.FDOwner = new SelectList(db.TBUsers, "FDEmailOrPhone", "FDNickname");
        //    return View();
        //}

        // POST: Repository/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FDRepositoryID,FDOwner,FDRepositoryName,FDDescription,FDCreationTimestamp")] TBRepository tBRepository)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TBRepositories.Add(tBRepository);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.FDOwner = new SelectList(db.TBUsers, "FDEmailOrPhone", "FDNickname", tBRepository.FDOwner);
        //    return View(tBRepository);
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RepositoryCreationViewModel p)
        {
            if (ModelState.IsValid)
            {
                if(Session["userid"] != null && Session["nickname"] != null )
                {
                    TBRepository tmp = new TBRepository { FDRepositoryName = p.RepositoryName, FDDescription = p.RepositoryDescription };
                    tmp.FDOwner = Session["userid"].ToString();
                    db.TBRepositories.Add(tmp);
                    db.SaveChanges();
                    //SqlParameter FDOwner = new SqlParameter("FDOwner", SqlDbType.VarChar, Common.Const.EmailOrPhoneLength);
                    //FDOwner.Value = Session["userid"].ToString();
                    //SqlParameter FDRepositoryName = new SqlParameter("FDRepositoryName", SqlDbType.VarChar, Common.Const.RepositoryNameLength);
                    //FDRepositoryName.Value = p.RepositoryName;
                    //SqlParameter FDDescription = new SqlParameter("FDDescription", SqlDbType.VarChar, Common.Const.RepositoryDescriptionLength);
                    //FDDescription.Value = p.RepositoryDescription;

                    //SqlParameter output = new SqlParameter("outputObject", SqlDbType.Int);
                    //output.Value = 0;
                    ////output inserted not work here
                    //int repid = db.Database.ExecuteSqlCommand("insert into TBRepositories (FDOwner,FDRepositoryName,FDDescription) output INSERTED.FDRepositoryID  values (@FDOwner,@FDRepositoryName,@FDDescription)", FDOwner, FDRepositoryName, FDDescription);
                    TBCreator tmpCreator = new TBCreator { FDRepositoryID = tmp.FDRepositoryID, FDCreator = Session["nickname"].ToString(), FDToken = "owner", FDLoginMethod = Common.Const.LoginMethod_SUPERLOGIN };
                    //tmpCreator.FDRepositoryID = (int)output.Value;
                    db.TBCreators.Add(tmpCreator);
                    db.SaveChanges();
                    //System.Diagnostics.Debug.WriteLine("@@@@@@@@@@@@@@@@  " + tmp.FDRepositoryID);
                }
            }

            return RedirectToAction("../");
        }

        // GET: Repository/Edit/5
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

        // POST: Repository/Edit/5
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

        // GET: Repository/Delete/5
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

        // POST: Repository/Delete/5
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
