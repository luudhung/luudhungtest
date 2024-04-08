using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikesWebNET.Models;
using static BikesWebNET.Models.Product;

namespace BikesWebNET.Controllers
{
    public class CollectionsController : Controller
    {
        private BIKESEntities db = new BIKESEntities();

        // GET: Collections
        public ActionResult Index()
        {
            var query = db.Products.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());

        }

        // GET: Collections/Details/5
  
        public ActionResult Details(string id)
        {
            ProductDTODetail productDTO = new ProductDTODetail();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = from el in db.Products
                          where el.idProduct == id
                          select el;

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {

                var data = from p in product
                           select p;

                data.Include("ImageProducts").Include("Type");
                var datarelateto = (from p in db.Products
                                    join t in data on p.idType equals t.idType
                                    select p);
                datarelateto.Include("ImageProducts").Include("Type");
                var subData = (datarelateto.ToList()).Skip(3).Take(4);
                ViewBag.datarelateto = subData.ToList();
                ViewBag.List = data;
                return View(data.ToList());
            };
        }


        // collections/xedap
        public ActionResult xedap(string id)
        {
            id = "T01";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());


        }

        // collections/phukienthoitrang
        public ActionResult phukienthoitrang(string id)
        {
            id = "T02";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }

        // collections/khungsuon
        public ActionResult khungsuon(string id)
        {
            id = "T03";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());

        }

        // collections/ghidong
        public ActionResult ghidong(string id)
        {
            id = "T04";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }

        // collections/samlop
        public ActionResult samlop(string id)
        {
            id = "T05";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }

        // collections/yenxe
        public ActionResult yenxe(string id)
        {
            id = "T06";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }


        // collections/denxe
        public ActionResult denxe(string id)
        {
            id = "T07";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());

        }


        // collections/dongho
        public ActionResult dongho(string id)
        {
            id = "T08";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());

        }

        // collections/tuixedap
        public ActionResult tuixedap(string id)
        {
            id = "T09";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }


        // collections/chanchong
        public ActionResult chanchong(string id)
        {
            id = "T10";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }



        // collections/chanbun
        public ActionResult chanbun(string id)
        {
            id = "T11";
            var productList = (from s in db.Products
                               where s.idType == id
                               select s);

            var query = productList.Include(p => p.ImageProducts);
            ViewBag.list = query.ToList();
            return View(query.ToList());
        }
    }
}
