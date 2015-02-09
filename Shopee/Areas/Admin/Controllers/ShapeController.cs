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
    public class ShapeController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/Shape/

        public ActionResult Index()
        {
            return View(db.lkpShapes.Where(s => s.Active == true).ToList());
        }

        //
        // GET: /Admin/Shape/Details/5

        public ActionResult Details(int id = 0)
        {
            lkpShape lkpshape = db.lkpShapes.Single(l => l.ShapeID == id);
            if (lkpshape == null)
            {
                return HttpNotFound();
            }
            return View(lkpshape);
        }

        //
        // GET: /Admin/Shape/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Shape/Create

        [HttpPost]
        public ActionResult Create(lkpShape lkpshape)
        {
            if (ModelState.IsValid)
            {
                db.lkpShapes.AddObject(lkpshape);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lkpshape);
        }

        //
        // GET: /Admin/Shape/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpShape lkpshape = db.lkpShapes.Single(l => l.ShapeID == id);
            if (lkpshape == null)
            {
                return HttpNotFound();
            }
            return View(lkpshape);
        }

        //
        // POST: /Admin/Shape/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpShape lkpshape)
        {
            if (ModelState.IsValid)
            {
                db.lkpShapes.Attach(lkpshape);
                db.ObjectStateManager.ChangeObjectState(lkpshape, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lkpshape);
        }

        //
        // GET: /Admin/Shape/Delete/5

        public ActionResult Delete(int id = 0)
        {
            lkpShape lkpshape = db.lkpShapes.Single(l => l.ShapeID == id);
            if (lkpshape == null)
            {
                return HttpNotFound();
            }
            return View(lkpshape);
        }

        //
        // POST: /Admin/Shape/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                lkpShape lkpshape = db.lkpShapes.Single(l => l.ShapeID == id);
                lkpshape.Active = false;
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