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
    public class PrPermissionsController : Controller
    {
        private McPermissonsEntities db = new McPermissonsEntities();

        //
        // GET: /PrPermissions/

        public ViewResult Index()
        {
            var prpermissions = db.PrPermissions.Include("PrEntry");
            return View(prpermissions.ToList());
        }

        //
        // GET: /PrPermissions/Details/5

        public ViewResult Details(int id)
        {
            PrPermission prpermission = db.PrPermissions.Single(p => p.permid == id);
            return View(prpermission);
        }

        //
        // GET: /PrPermissions/Create

        public ActionResult Create()
        {
            ViewBag.entryid = new SelectList(db.PrEntries, "entryid", "name");
            return View();
        } 

        //
        // POST: /PrPermissions/Create

        [HttpPost]
        public ActionResult Create(PrPermission prpermission)
        {
            if (ModelState.IsValid)
            {
                db.PrPermissions.AddObject(prpermission);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.entryid = new SelectList(db.PrEntries, "entryid", "name", prpermission.entryid);
            return View(prpermission);
        }
        
        //
        // GET: /PrPermissions/Edit/5
 
        public ActionResult Edit(int id)
        {
            PrPermission prpermission = db.PrPermissions.Single(p => p.permid == id);
            ViewBag.entryid = new SelectList(db.PrEntries, "entryid", "name", prpermission.entryid);
            return View(prpermission);
        }

        //
        // POST: /PrPermissions/Edit/5

        [HttpPost]
        public ActionResult Edit(PrPermission prpermission)
        {
            if (ModelState.IsValid)
            {
                db.PrPermissions.Attach(prpermission);
                db.ObjectStateManager.ChangeObjectState(prpermission, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.entryid = new SelectList(db.PrEntries, "entryid", "name", prpermission.entryid);
            return View(prpermission);
        }

        //
        // GET: /PrPermissions/Delete/5
 
        public ActionResult Delete(int id)
        {
            PrPermission prpermission = db.PrPermissions.Single(p => p.permid == id);
            return View(prpermission);
        }

        //
        // POST: /PrPermissions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PrPermission prpermission = db.PrPermissions.Single(p => p.permid == id);
            db.PrPermissions.DeleteObject(prpermission);
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