using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models.UserModels;
using SampleProject.Models;

namespace SampleProject.Areas.Admin.Controllers
{ 
    public class OpenIdController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Admin/OpenId/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            var openids = db.OpenIds.Include(o => o.User);
            return View(openids.ToList());
        }

        //
        // GET: /Admin/OpenId/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            OpenId openid = db.OpenIds.Find(id);
            return View(openid);
        }

        //
        // GET: /Admin/OpenId/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            return View();
        } 

        //
        // POST: /Admin/OpenId/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(OpenId openid)
        {
            if (ModelState.IsValid)
            {
                db.OpenIds.Add(openid);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", openid.UserId);
            return View(openid);
        }
        
        //
        // GET: /Admin/OpenId/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            OpenId openid = db.OpenIds.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", openid.UserId);
            return View(openid);
        }

        //
        // POST: /Admin/OpenId/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(OpenId openid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", openid.UserId);
            return View(openid);
        }

        //
        // GET: /Admin/OpenId/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            OpenId openid = db.OpenIds.Find(id);
            return View(openid);
        }

        //
        // POST: /Admin/OpenId/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            OpenId openid = db.OpenIds.Find(id);
            db.OpenIds.Remove(openid);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}