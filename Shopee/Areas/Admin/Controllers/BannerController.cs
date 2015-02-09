using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopee.Models;
using System.IO;
using Shopee.Support;

namespace Shopee.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/Banner/

        public ActionResult Index()
        {
            var lkpbanners = db.lkpBanners.Include("lkpSize");
            return View(lkpbanners.ToList());
        }

        //
        // GET: /Admin/Banner/Details/5

        public ActionResult Details(int id = 0)
        {
            lkpBanner lkpbanner = db.lkpBanners.Single(l => l.BannerID == id);
            if (lkpbanner == null)
            {
                return HttpNotFound();
            }
            return View(lkpbanner);
        }

        //
        // GET: /Admin/Banner/Create

        public ActionResult Create()
        {
            ViewBag.SizeID = new SelectList(db.lkpSizes, "SizeID", "SizeID");
            return View();
        }

        //
        // POST: /Admin/Banner/Create

        [HttpPost]
        public ActionResult Create(lkpBanner lkpbanner)
        {
            if (ModelState.IsValid)
            {
                db.lkpBanners.AddObject(lkpbanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SizeID = new SelectList(db.lkpSizes, "SizeID", "SizeID", lkpbanner.SizeID);
            return View(lkpbanner);
        }

        //
        // GET: /Admin/Banner/Edit/5

        public ActionResult Edit(int id = 0)
        {
            lkpBanner lkpbanner = db.lkpBanners.Single(l => l.BannerID == id);
            if (lkpbanner == null)
            {
                return HttpNotFound();
            }
            ViewBag.SizeID = new SelectList(db.lkpSizes, "SizeID", "SizeID", lkpbanner.SizeID);
            return View(lkpbanner);
        }

        //
        // POST: /Admin/Banner/Edit/5

        [HttpPost]
        public ActionResult Edit(lkpBanner lkpbanner)
        {
            if (ModelState.IsValid)
            {
                db.lkpBanners.Attach(lkpbanner);
                db.ObjectStateManager.ChangeObjectState(lkpbanner, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SizeID = new SelectList(db.lkpSizes, "SizeID", "SizeID", lkpbanner.SizeID);
            return View(lkpbanner);
        }

        //
        // POST: /Admin/Banner/uploadBanner
        [HttpPost]
        public ActionResult uploadBanner()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        string bannerImageFolder = Common.GetWebConfig("bannerImageFolder");
                        string BannerImageFolderFullPath = Server.MapPath(bannerImageFolder);
                        var path = Path.Combine(BannerImageFolderFullPath, fileName);
                        if (Common.CreateFolderIfNotExists(BannerImageFolderFullPath))
                        {
                            file.SaveAs(path);
                        }
                        path = Path.Combine(bannerImageFolder, fileName);
                        var objlkpbanner = new lkpBanner()
                        {
                            Active = true,
                            imagePath = path,
                            SizeID = 1
                        };

                        db.lkpBanners.AddObject(objlkpbanner);
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
                return Json(new { Message = "Internal Server Error" });
            }

            return Json(new { Message = "Saved Successfully" });
        }
        //
        // GET: /Admin/Banner/Delete/5

        public ActionResult Delete(int id = 0)
        {
            lkpBanner lkpbanner = db.lkpBanners.Single(l => l.BannerID == id);
            if (lkpbanner == null)
            {
                return HttpNotFound();
            }
            return View(lkpbanner);
        }

        //
        // POST: /Admin/Banner/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                lkpBanner lkpbanner = db.lkpBanners.Single(l => l.BannerID == id);
                db.lkpBanners.DeleteObject(lkpbanner);
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