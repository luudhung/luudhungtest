using BikesWebNET.Models;  // Import namespace chứa các model của ứng dụng
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace BikesWebNET.Controllers
{
    public class BillController : Controller
    {
        private BIKESEntities db = new BIKESEntities(); // Khởi tạo đối tượng để kết nối với cơ sở dữ liệu BIKESEntities

        // GET: Bill
        public ActionResult Index()
        {
            return View(); // Trả về view để hiển thị các hóa đơn
        }

        // Phương thức xử lý yêu cầu HTTP POST để tạo hóa đơn mới
        [HttpPost]
        public ActionResult PostBill(
            string idBill, string idUser, int Shipping, int Total, int totalQty, string nameBook, string email, int phone,
            string address, string PTTT, DetailBIll[] detailBill, bool status)
        {
            var idUserReal = ""; // Biến để lưu trữ ID người dùng thực tế

            // Kiểm tra xem có cookie người dùng trong yêu cầu hay không
            if (Request.Cookies["user"] != null)
            {
                idUserReal = Request.Cookies["user"].Value; // Lấy ID người dùng từ cookie
            }

            // Tạo một đối tượng hóa đơn mới và điền các thuộc tính của nó
            var bill = new Bill()
            {
                idBill = idBill,
                idUser = idUserReal != "" ? idUserReal : null,
                createdAt = DateTime.Now,
                Shipping = Shipping,
                Total = Total,
                totalQty = totalQty,
                nameBook = nameBook,
                email = email,
                phone = Convert.ToString(phone),
                address = address,
                PTTT = PTTT,
                status = status,
                DetailBIlls = detailBill,
            };

            // Thêm hóa đơn mới tạo vào context của cơ sở dữ liệu
            db.Bills.Add(bill);

            // Lưu các thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            // Trả về một phản hồi JSON cho biết việc đặt hàng thành công
            return Json("Đặt hàng thành công");
        }

        // Phương thức để kiểm tra tính hợp lệ của địa chỉ email sử dụng biểu thức chính quy
        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        // Phương thức để kiểm tra tính hợp lệ của số điện thoại Việt Nam sử dụng biểu thức chính quy
        public bool IsValidVietNamPhoneNumber(string phoneNum)
        {
            if (string.IsNullOrEmpty(phoneNum))
                return false;
            string sMailPattern = @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$";
            return Regex.IsMatch(phoneNum.Trim(), sMailPattern);
        }
    }
}
