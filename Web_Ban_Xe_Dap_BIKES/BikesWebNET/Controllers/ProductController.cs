using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothesWebNET.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View(); // Phương thức này trả về một ActionResult cho trang Index của sản phẩm.
        }

        public ActionResult Detail()
        {
            return View(); // Phương thức này trả về một ActionResult cho trang Detail của sản phẩm.
        }
    }
}
