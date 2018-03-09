using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCShop.Controllers
{
    public class AuthFiltersController : Controller
    {
        //
        // GET: /AuthFilters/
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        } 
	}
}