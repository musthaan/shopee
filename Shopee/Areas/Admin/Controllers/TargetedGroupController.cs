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
    public class TargetedGroupController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/TargetedGroup/

        public ActionResult Index()
        {
            return View(db.lkpTargetedGroups.Where(t=> t.Active ==true).ToList());
        }

        
        //
        // GET: /Admin/TargetedGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/TargetedGroup/Create

        [HttpPost]
        public ActionResult Create(lkpTargetedGroup lkptargetedgroup)
        {
            if (ModelState.IsValid)
            {
                db.lkpTargetedGroups.AddObject(lkptargetedgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lkptargetedgroup);
        }

        //
        // GET: /Admin/TargetedGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpTargetedGroup lkptargetedgroup = db.lkpTargetedGroups.Single(l => l.TargetedGroupID == id);
            if (lkptargetedgroup == null)
            {
                return HttpNotFound();
            }
            return View(lkptargetedgroup);
        }

        //
        // POST: /Admin/TargetedGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpTargetedGroup lkptargetedgroup)
        {
            if (ModelState.IsValid)
            {
                db.lkpTargetedGroups.Attach(lkptargetedgroup);
                db.ObjectStateManager.ChangeObjectState(lkptargetedgroup, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lkptargetedgroup);
        }
         
        //
        // POST: /Admin/TargetedGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        { 
            try
            {
                lkpTargetedGroup lkptargetedgroup = db.lkpTargetedGroups.Single(l => l.TargetedGroupID == id);
                lkptargetedgroup.Active = false;
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