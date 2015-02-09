using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopee.Models;

namespace Shopee.Areas.Admin.Controllers
{
    public class MaterialController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/Material/

        public ActionResult Index()
        {
            return View(db.lkpMaterials.Where(m=> m.Active ==true ).ToList());
        }

         
        //
        // GET: /Admin/Material/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Material/Create

        [HttpPost]
        public ActionResult Create(lkpMaterial lkpmaterial)
        {
            if (ModelState.IsValid)
            {
                db.lkpMaterials.AddObject(lkpmaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lkpmaterial);
        }

        //
        // GET: /Admin/Material/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpMaterial lkpmaterial = db.lkpMaterials.Single(l => l.MaterialID == id);
            if (lkpmaterial == null)
            {
                return HttpNotFound();
            }
            return View(lkpmaterial);
        }

        //
        // POST: /Admin/Material/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpMaterial lkpmaterial)
        {
            if (ModelState.IsValid)
            {
                db.lkpMaterials.Attach(lkpmaterial);
                db.ObjectStateManager.ChangeObjectState(lkpmaterial, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lkpmaterial);
        }

         
        //
        // POST: /Admin/Material/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                lkpMaterial lkpmaterial = db.lkpMaterials.Single(l => l.MaterialID == id);
                lkpmaterial.Active = false;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
                return Json(new Shopee.Support.APIErrorResponse() { Message = "Internal Server Error" });
            }

            return Json(new Shopee.Support.APISuccessResponse() { Message = "Success" });
           
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}