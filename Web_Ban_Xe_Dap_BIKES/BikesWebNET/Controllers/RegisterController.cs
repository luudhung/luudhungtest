using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikesWebNET.Models;

namespace BikesWebNET.Controllers
{
    public class RegisterController : Controller
    {
        private BIKESEntities db = new BIKESEntities(); // Khởi tạo đối tượng để kết nối với cơ sở dữ liệu BIKESEntities

        // GET: Register
        public ActionResult Index()
        {
            return View(); // Trả về view cho trang đăng ký
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "idUser,idPermission,fullName,username,password,gender,identityCard,address,email,URLAvatar,phone")] User user)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            {
                int count = db.Users.Count() + 1; // Đếm số lượng người dùng hiện tại và tăng thêm 1 để tạo id mới
                user.idPermission = "R02"; // Gán quyền cho người dùng mới
                var id = 'U' + count.ToString(); // Tạo id mới cho người dùng
                user.idUser = id;

                db.Users.Add(user); // Thêm người dùng mới vào cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return Redirect("~/Home"); // Chuyển hướng người dùng đến trang chủ sau khi đăng ký thành công
            }
            else
            {
                // Nếu dữ liệu không hợp lệ, hiển thị lại form đăng ký với thông báo lỗi
                ViewBag.idPermission = new SelectList(db.Permissions, "idPermission", "namePermission", user.idPermission);
                return View(user);
            }
        }
    }
}
