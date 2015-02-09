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
    public class lkpTypeController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/lkpType/

        public ActionResult Index()
        {
            return View(db.lkpTypes.Where(t=>t.Active==true).ToList());
        }

         

        //
        // GET: /Admin/lkpType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/lkpType/Create

        [HttpPost]
        public ActionResult Create(lkpType lkptype)
        {
            if (ModelState.IsValid)
            {
                db.lkpTypes.AddObject(lkptype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lkptype);
        }

        //
        // GET: /Admin/lkpType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpType lkptype = db.lkpTypes.Single(l => l.TypeID == id);
            if (lkptype == null)
            {
                return HttpNotFound();
            }
            return View(lkptype);
        }

        //
        // POST: /Admin/lkpType/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpType lkptype)
        {
            if (ModelState.IsValid)
            {
                db.lkpTypes.Attach(lkptype);
                db.ObjectStateManager.ChangeObjectState(lkptype, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lkptype);
        }

        

        //
        // POST: /Admin/lkpType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                lkpType lkptype = db.lkpTypes.Single(l => l.TypeID == id);
                lkptype.Active = false;
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