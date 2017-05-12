using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace issueTrack.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public async Task<ActionResult> Index()
        {
            string sdf = $"Number of .NETs on dotnetfoundation.org:";
            System.Collections.Hashtable abc = null;
            System.Collections.Hashtable abc2 = new System.Collections.Hashtable();
            abc2.Add("abc", System.Threading.Thread.CurrentThread.ManagedThreadId);
            abc = await Task.Run<System.Collections.Hashtable>(() => computeModel());
            return View(abc);
        }

        private System.Collections.Hashtable computeModel()
        {
            //System.Threading.Thread.Sleep(5000);
            int k = 0;
            Random rnd = new Random();
            for(int i = 0; i < 10000; i++)
            {
                for(int j = 0; j < 10000; j++)
                {
                    if (rnd.Next() % 2 == 0)
                        k++;
                    else
                        k--;
                }
            }
            System.Collections.Hashtable abc = new System.Collections.Hashtable();
            abc.Add("abc", System.Threading.Thread.CurrentThread.ManagedThreadId);
            return abc;
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
