using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopee.Models;
using Shopee.Support;

namespace Shopee.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/Brand/

        public ActionResult Index()
        {
            return View(db.lkpBrands.Where(l => l.Active == true).ToList());
        }

        //
        // GET: /Admin/Brand/Details/5

        public ActionResult Details(int id = 0)
        {
            lkpBrand lkpbrand = db.lkpBrands.Single(l => l.BrandID == id);
            if (lkpbrand == null)
            {
                return HttpNotFound();
            }
            return View(lkpbrand);
        }

        //
        // GET: /Admin/Brand/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Brand/Create

        [HttpPost]
        public ActionResult Create(lkpBrand lkpbrand)
        {
            lkpbrand.Active = true;
            db.lkpBrands.AddObject(lkpbrand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Brand/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpBrand lkpbrand = db.lkpBrands.Single(l => l.BrandID == id);
            if (lkpbrand == null)
            {
                return HttpNotFound();
            }
            return View(lkpbrand);
        }

        //
        // POST: /Admin/Brand/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpBrand lkpbrand)
        {
             
            if (ModelState.IsValid)
            {                
                db.lkpBrands.Attach(lkpbrand);
                db.ObjectStateManager.ChangeObjectState(lkpbrand, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lkpbrand);
        }

        //
        // GET: /Admin/Brand/Delete/5

        public ActionResult Delete(int id = 0)
        {
            lkpBrand lkpbrand = db.lkpBrands.Single(l => l.BrandID == id);
            if (lkpbrand == null)
            {
                return HttpNotFound();
            }
            return View(lkpbrand);
        }

        //
        // POST: /Admin/Brand/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                lkpBrand lkpbrand = db.lkpBrands.Single(l => l.BrandID == id);
                lkpbrand.Active = false;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
                return Json(new APIErrorResponse() { Message = "Internal Server Error" });
            }

            return Json(new APISuccessResponse() { Message = "Success" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}