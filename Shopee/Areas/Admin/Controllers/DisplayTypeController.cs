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
    public class DisplayTypeController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/DisplayType/

        public ActionResult Index()
        {
            return View(db.lkpDisplayTypes.Where(d => d.Active == true).ToList());
        }

        //
        // GET: /Admin/DisplayType/Details/5

        public ActionResult Details(int id = 0)
        {
            lkpDisplayType lkpdisplaytype = db.lkpDisplayTypes.Single(l => l.DisplayTypeID == id);
            if (lkpdisplaytype == null)
            {
                return HttpNotFound();
            }
            return View(lkpdisplaytype);
        }

        //
        // GET: /Admin/DisplayType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/DisplayType/Create

        [HttpPost]
        public ActionResult Create(lkpDisplayType lkpdisplaytype)
        {
            if (ModelState.IsValid)
            {
                db.lkpDisplayTypes.AddObject(lkpdisplaytype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lkpdisplaytype);
        }

        //
        // GET: /Admin/DisplayType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpDisplayType lkpdisplaytype = db.lkpDisplayTypes.Single(l => l.DisplayTypeID == id);
            if (lkpdisplaytype == null)
            {
                return HttpNotFound();
            }
            return View(lkpdisplaytype);
        }

        //
        // POST: /Admin/DisplayType/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpDisplayType lkpdisplaytype)
        {
            if (ModelState.IsValid)
            {
                db.lkpDisplayTypes.Attach(lkpdisplaytype);
                db.ObjectStateManager.ChangeObjectState(lkpdisplaytype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lkpdisplaytype);
        }

        //
        // GET: /Admin/DisplayType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            lkpDisplayType lkpdisplaytype = db.lkpDisplayTypes.Single(l => l.DisplayTypeID == id);
            if (lkpdisplaytype == null)
            {
                return HttpNotFound();
            }
            return View(lkpdisplaytype);
        }

        //
        // POST: /Admin/DisplayType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                lkpDisplayType lkpdisplaytype = db.lkpDisplayTypes.Single(l => l.DisplayTypeID == id);
                lkpdisplaytype.Active = false;
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