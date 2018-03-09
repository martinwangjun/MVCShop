using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCShop.Models;

namespace MVCShop.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerContext db = new CustomerContext();

        // GET: /Customer/
        public ActionResult Index()
        {
            return View(db.customers.ToList());
        }

        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Customer/Create
        public ActionResult Create(string province)
        {
            List<SelectListItem> typeList = new List<SelectListItem>{
                new SelectListItem{Text = "潜在客户", Value = "潜在客户", Selected = true},
                new SelectListItem{Text = "目标客户", Value = "目标客户"},
                new SelectListItem{Text = "发展中客户", Value = "发展中客户"},
                new SelectListItem{Text = "交易客户", Value = "交易客户"},
            };
            ViewBag.typeList = typeList;

            var ProvinceLst = new List<string>();

            var ProvinceQry = from d in db.c_address
                              orderby d.province
                              select d.province;
            province = "北京市";
            ProvinceLst.AddRange(ProvinceQry.Distinct());

            var CityLst = new List<string>();

            var CityQry = from d in db.c_address
                          where d.province == province
                          orderby d.city
                          select d.city;

            CityLst.AddRange(CityQry.Distinct());

            ViewBag.ProvinceLst = new SelectList(ProvinceLst);
            ViewBag.CityLst = new SelectList(CityLst);
            return View();
        }

        // POST: /Customer/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_id,customer_name,customer_type,province,city,is_KA")] customer customer)
        {

            
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }


        public string GetCity(string selectedProvince)
        {
             
            var CityLst = new List<string>();
            var CityQry = from d in db.c_address
                          where d.province == selectedProvince
                          orderby d.city
                          select d.city;
            CityLst.AddRange(CityQry.Distinct());
            string html = "<label class='control-label col-md-2' for=''>市</label>";
            html += "<div class='col-md-10'>";
            html += "<select name='city'>";
            foreach (string city in CityLst){
                html += "<option>";
                html += city;
                html += "</option>";
            }
            html += "</select></div>";
            return html;
            //ViewBag.CityLst = new SelectList(CityLst);
        }

        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="customer_id,customer_name,customer_type,is_KA")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
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
