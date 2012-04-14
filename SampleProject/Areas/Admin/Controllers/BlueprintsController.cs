using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models.BlueprintModels;
using SampleProject.Models;

namespace SampleProject.Areas.Admin.Controllers
{ 
    public class BlueprintsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Admin/Blueprints/

        public ViewResult Index()
        {
            var blueprints = db.Blueprints.Include(b => b.User);
            return View(blueprints.ToList());
        }

        //
        // GET: /Admin/Blueprints/Details/5

        public ViewResult Details(int id)
        {
            Blueprint blueprint = db.Blueprints.Find(id);
            return View(blueprint);
        }

        //
        // GET: /Admin/Blueprints/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            return View();
        } 

        //
        // POST: /Admin/Blueprints/Create

        [HttpPost]
        public ActionResult Create(Blueprint blueprint)
        {
            if (ModelState.IsValid)
            {
                db.Blueprints.Add(blueprint);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", blueprint.UserId);
            return View(blueprint);
        }
        
        //
        // GET: /Admin/Blueprints/Edit/5
 
        public ActionResult Edit(int id)
        {
            Blueprint blueprint = db.Blueprints.Find(id);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", blueprint.UserId);
            return View(blueprint);
        }

        //
        // POST: /Admin/Blueprints/Edit/5

        [HttpPost]
        public ActionResult Edit(Blueprint blueprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blueprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", blueprint.UserId);
            return View(blueprint);
        }

        //
        // GET: /Admin/Blueprints/Delete/5
 
        public ActionResult Delete(int id)
        {
            Blueprint blueprint = db.Blueprints.Find(id);
            return View(blueprint);
        }

        //
        // POST: /Admin/Blueprints/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Blueprint blueprint = db.Blueprints.Find(id);
            db.Blueprints.Remove(blueprint);
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