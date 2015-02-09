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
    public class ProductController : Controller
    {
        private ShopeeEntities db = new ShopeeEntities();

        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            var products = db.Products.Include("lkpBrand").Include("lkpDisplayType").Include("lkpMaterial").Include("lkpShape").Include("lkpTargetedGroup").Include("lkpType");
            return View(products.Where(p => p.Active == true).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult search(SearchOptions options)
        {
            if (options.CurrentPage == 0)
            {
                options.CurrentPage = 1;
            }

            if (options.PageSize == 0)
            {
                options.PageSize = 25;
            }

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                var products = db.Products
                .Where(p =>
                    p.Active == true &&
                    p.ProductName.Contains(options.SearchText)
                    )
                    .OrderBy(p => p.ProductName)
                 .Take(options.PageSize)
                 .Skip((options.CurrentPage - 1) * options.PageSize)
                 .Select(p =>
                 new
                 {
                     p.Active,
                     p.BrandID,
                     p.Description,
                     p.DisplayTypeID,
                     p.EAN,
                     p.MaterialID,
                     p.ProductID,
                     p.ProductName,
                     p.ShapeID,
                     p.TargetedGroupID,
                     p.TypeID,
                     ProductOffers = p.ProductOffers.Select(productOffer => new
                     {
                         productOffer.Active,
                         productOffer.imagePath,
                         productOffer.ProductOfferID
                     }),
                     ProductImages = p.ProductImages.Select(productImages => new
                     {
                         productImages.Active,
                         productImages.ImagePath,
                         productImages.IsDefault,
                         productImages.ProductImageID,
                         productImages.SortOrder

                     }),
                     lkpBrand = new { p.lkpBrand.BrandName, p.lkpBrand.BrandID, p.lkpBrand.Active },
                     lkpDisplayType = new { p.lkpDisplayType.Active, p.lkpDisplayType.DisplayTypeName, p.lkpDisplayType.DisplayTypeID },
                     lkpMaterial = new { p.lkpMaterial.Active, p.lkpMaterial.MaterialID, p.lkpMaterial.MaterialName },
                     lkpShape = new { p.lkpShape.Active, p.lkpShape.ShapeID, p.lkpShape.ShapeName },
                     lkpTargetedGroup = new { p.lkpTargetedGroup.Active, p.lkpTargetedGroup.TargetedGroupID, p.lkpTargetedGroup.TargetedGroupName },
                     lkpType = new { p.lkpType.Active, p.lkpType.TypeID, p.lkpType.TypeName }

                 }

                 );




                return Json(new Shopee.Support.APISuccessSearchResponse()
                {
                    Message = "Success",
                    Data = products.ToList(),
                    CurrentPage = options.CurrentPage,
                    Order = options.Order,
                    PageSize = options.PageSize,
                    SortColumn = options.SortColumn
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new Shopee.Support.APISuccessResponse() { Message = "Success" }, JsonRequestBehavior.AllowGet);
        }





        //
        // GET: /Admin/Product/Details/5

        public ActionResult Details(long id = 0)
        {
            Product product = db.Products.Single(p => p.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Admin/Product/Create

        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.lkpBrands, "BrandID", "BrandName");
            ViewBag.DisplayTypeID = new SelectList(db.lkpDisplayTypes, "DisplayTypeID", "DisplayTypeName");
            ViewBag.MaterialID = new SelectList(db.lkpMaterials, "MaterialID", "MaterialName");
            ViewBag.ShapeID = new SelectList(db.lkpShapes, "ShapeID", "ShapeName");
            ViewBag.TargetedGroupID = new SelectList(db.lkpTargetedGroups, "TargetedGroupID", "TargetedGroupName");
            ViewBag.TypeID = new SelectList(db.lkpTypes, "TypeID", "TypeName");
            return View();
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.AddObject(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.lkpBrands, "BrandID", "BrandName", product.BrandID);
            ViewBag.DisplayTypeID = new SelectList(db.lkpDisplayTypes, "DisplayTypeID", "DisplayTypeName", product.DisplayTypeID);
            ViewBag.MaterialID = new SelectList(db.lkpMaterials, "MaterialID", "MaterialName", product.MaterialID);
            ViewBag.ShapeID = new SelectList(db.lkpShapes, "ShapeID", "ShapeName", product.ShapeID);
            ViewBag.TargetedGroupID = new SelectList(db.lkpTargetedGroups, "TargetedGroupID", "TargetedGroupName", product.TargetedGroupID);
            ViewBag.TypeID = new SelectList(db.lkpTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Product product = db.Products.Single(p => p.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.lkpBrands, "BrandID", "BrandName", product.BrandID);
            ViewBag.DisplayTypeID = new SelectList(db.lkpDisplayTypes, "DisplayTypeID", "DisplayTypeName", product.DisplayTypeID);
            ViewBag.MaterialID = new SelectList(db.lkpMaterials, "MaterialID", "MaterialName", product.MaterialID);
            ViewBag.ShapeID = new SelectList(db.lkpShapes, "ShapeID", "ShapeName", product.ShapeID);
            ViewBag.TargetedGroupID = new SelectList(db.lkpTargetedGroups, "TargetedGroupID", "TargetedGroupName", product.TargetedGroupID);
            ViewBag.TypeID = new SelectList(db.lkpTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Attach(product);
                db.ObjectStateManager.ChangeObjectState(product, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.lkpBrands, "BrandID", "BrandName", product.BrandID);
            ViewBag.DisplayTypeID = new SelectList(db.lkpDisplayTypes, "DisplayTypeID", "DisplayTypeName", product.DisplayTypeID);
            ViewBag.MaterialID = new SelectList(db.lkpMaterials, "MaterialID", "MaterialName", product.MaterialID);
            ViewBag.ShapeID = new SelectList(db.lkpShapes, "ShapeID", "ShapeName", product.ShapeID);
            ViewBag.TargetedGroupID = new SelectList(db.lkpTargetedGroups, "TargetedGroupID", "TargetedGroupName", product.TargetedGroupID);
            ViewBag.TypeID = new SelectList(db.lkpTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        //
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Product product = db.Products.Single(p => p.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Admin/Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Single(p => p.ProductID == id);
            db.Products.DeleteObject(product);
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