using BikesWebNET.Models;  // Import namespace chứa các model của ứng dụng
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikesWebNET.Controllers
{
    // Định nghĩa một Controller trong MVC để quản lý các thao tác liên quan đến giỏ hàng
    public class CartController : Controller
    {
        private BIKESEntities db = new BIKESEntities(); // Khởi tạo đối tượng để kết nối với cơ sở dữ liệu BIKESEntities

        // Phương thức để lấy thông tin giỏ hàng từ Session
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Phương thức xử lý yêu cầu HTTP POST để thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddtoCart(String id, FormCollection form)
        {
            String size = form["Size"];
            var pro = db.Products.SingleOrDefault(s => s.idProduct == id);
            if (pro != null)
            {
                GetCart().AddCart(pro, size);
            }
            return RedirectToAction("ShowToCart", "Cart");
        }

        // Hiển thị thông tin giỏ hàng
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Products");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        // Cập nhật số lượng sản phẩm trong giỏ hàng
        public ActionResult UpdateQtyCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            String id_product = form["ID_Product"];
            int qty = int.Parse(form["Qty"]);
            cart.UpdateQtyShopping(id_product, qty);
            return RedirectToAction("ShowToCart", "Cart");
        }

        // Xóa một sản phẩm khỏi giỏ hàng
        [HttpPost]
        public ActionResult DeleteCartItem(String id, FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveCartItem(id);

            String detail = form["Detail"];
            if (detail != null)
            {
                return RedirectToAction("DetailCart", "Cart");
            }
            else
            {
                return RedirectToAction("ShowToCart", "Cart");
            }
        }

        // Hiển thị chi tiết giỏ hàng
        public ActionResult DetailCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Products");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        // Hiển thị thông tin về số lượng sản phẩm trong giỏ hàng
        public PartialViewResult BagCart()
        {
            int totalItem = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                totalItem = cart.CartCount();
            ViewBag.QtyCart = totalItem;
            return PartialView("BagCart");
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // Hiển thị trang thanh toán
        public ActionResult Checkout()
        {
            return View();
        }
    }
}
