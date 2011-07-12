using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PermsManager.Models;

namespace PermsManager.Controllers
{ 
    public class PrEntryController : Controller
    {
        private McPermissonsEntities db = new McPermissonsEntities();

        //
        // GET: /PrEntry/

        public ViewResult Index()
        {
            var prentries = db.PrEntries.Include("PrWorld");
            return View(prentries.ToList());
        }

        //
        // GET: /PrEntry/Details/5

        public ViewResult Details(int id)
        {
            PrEntry prentry = db.PrEntries.Single(p => p.entryid == id);
            if (prentry.type.Equals(1))
            {
                ViewBag.groups = prentry.Children.Where(p => p.Child.type.Equals(1));
                ViewBag.users = prentry.Children.Where(p => p.Child.type.Equals(0));
                ViewBag.permissions = prentry.PrPermissions.Where(p => p.entryid.Equals(id));
            }
            else
            {
                ViewBag.groups = prentry.Parents.Where(p => p.Parent.type.Equals(1));
                ViewBag.users = prentry.Parents.Where(p => p.Parent.type.Equals(0));
                ViewBag.permissions = prentry.PrPermissions.Where(p => p.entryid.Equals(id));
            }
            return View(prentry);
        }

        //
        // GET: /PrEntry/Create

        public ActionResult Create()
        {
            ViewBag.worldid = new SelectList(db.PrWorlds, "worldid", "worldname");
            return View();
        } 

        //
        // POST: /PrEntry/Create

        [HttpPost]
        public ActionResult Create(PrEntry prentry)
        {
            if (ModelState.IsValid)
            {
                db.PrEntries.AddObject(prentry);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.worldid = new SelectList(db.PrWorlds, "worldid", "worldname", prentry.worldid);
            return View(prentry);
        }
        
        //
        // GET: /PrEntry/Edit/5
 
        public ActionResult Edit(int id)
        {
            PrEntry prentry = db.PrEntries.Single(p => p.entryid == id);
            ViewBag.worldid = new SelectList(db.PrWorlds, "worldid", "worldname", prentry.worldid);
            return View(prentry);
        }

        //
        // POST: /PrEntry/Edit/5

        [HttpPost]
        public ActionResult Edit(PrEntry prentry)
        {
            if (ModelState.IsValid)
            {
                db.PrEntries.Attach(prentry);
                db.ObjectStateManager.ChangeObjectState(prentry, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = prentry.entryid });
            }
            ViewBag.worldid = new SelectList(db.PrWorlds, "worldid", "worldname", prentry.worldid);
            return View(prentry);
        }

        //
        // GET: /PrEntry/Delete/5
 
        public ActionResult Delete(int id)
        {
            PrEntry prentry = db.PrEntries.Single(p => p.entryid == id);
            return View(prentry);
        }

        //
        // POST: /PrEntry/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PrEntry prentry = db.PrEntries.Single(p => p.entryid == id);
            db.PrEntries.DeleteObject(prentry);
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