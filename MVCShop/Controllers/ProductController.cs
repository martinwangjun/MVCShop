using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCShop.Models;
using PagedList;

namespace MVCShop.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: /product/
        public ActionResult Index(string searchBy, string searchString, int page = 1)
        {
            int pageSize = 20;
            List<SelectListItem> searchByList = new List<SelectListItem>{
                new SelectListItem{Text = "按货号查询", Value = "CAT", Selected = true},
                new SelectListItem{Text = "按sku号查询", Value = "SKU"}
            };

            ViewBag.searchString = searchString;
            ViewBag.searchByList = searchByList;
            ViewBag.searchBy = searchBy;

            var products = from p in db.products
                           select p;
          
            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchBy)
                {
                    case "CAT":
                        products = products.Where(s => s.f_catnbr.Contains(searchString));
                        break;
                    case "SKU":
                        products = products.Where(s => s.f_sku_code.Contains(searchString));
                        break;
                }
                
            }

            return View(products.OrderBy(p => p.f_product_id).ToPagedList(page, pageSize));
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="f_product_id,f_sku_code")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="f_product_id,f_sku_code")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
