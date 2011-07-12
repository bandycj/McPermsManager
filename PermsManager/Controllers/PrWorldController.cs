using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PermsManager.Models;
using System.Collections;

namespace PermsManager.Controllers
{ 
    public class PrWorldController : Controller
    {
        private McPermissonsEntities db = new McPermissonsEntities();

        //
        // GET: /PrWorld/

        public ViewResult Index()
        {
            return View(db.PrWorlds.ToList());
        }

        //
        // GET: /PrWorld/Details/5

        public ViewResult Details(int id)
        {
            PrWorld prworld = db.PrWorlds.Single(p => p.worldid == id);
            ViewBag.groups = prworld.PrEntries.Where(p => p.type.Equals(1));
            ViewBag.users = prworld.PrEntries.Where(p => p.type.Equals(0));
            
            return View(prworld);
        }

        //
        // GET: /PrWorld/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PrWorld/Create

        [HttpPost]
        public ActionResult Create(PrWorld prworld)
        {
            if (ModelState.IsValid)
            {
                db.PrWorlds.AddObject(prworld);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(prworld);
        }
        
        //
        // GET: /PrWorld/Edit/5
 
        public ActionResult Edit(int id)
        {
            PrWorld prworld = db.PrWorlds.Single(p => p.worldid == id);
            return View(prworld);
        }

        //
        // POST: /PrWorld/Edit/5

        [HttpPost]
        public ActionResult Edit(PrWorld prworld)
        {
            if (ModelState.IsValid)
            {
                db.PrWorlds.Attach(prworld);
                db.ObjectStateManager.ChangeObjectState(prworld, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prworld);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}