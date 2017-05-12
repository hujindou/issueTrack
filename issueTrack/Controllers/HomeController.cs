using issueTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issueTrack.Controllers
{
    public class HomeController : Controller
    {
        private testdbEntities db = new testdbEntities();
        public ActionResult Index()
        {
            if(Session["nickname"] != null && Session["nickname"].ToString().Length >= Common.Const.NicknameLength_Min)
            {
                return View("LoginIndex",db.TBRepositories.OrderByDescending(r => r.FDCreationTimestamp).ToList());
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}