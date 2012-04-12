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
    public class RolesController : Controller
    {
        private UserContext db = new UserContext();

        //
        // GET: /Admin/Roles/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(db.Roles.ToList());
        }

        //
        // GET: /Admin/Roles/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // GET: /Admin/Roles/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Roles/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(role);
        }
        
        //
        // GET: /Admin/Roles/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Admin/Roles/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Admin/Roles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Admin/Roles/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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