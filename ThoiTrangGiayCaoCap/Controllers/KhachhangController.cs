using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrangGiayCaoCap.Controllers
{
    public class KhachhangController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: Khachhang
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection filed)
        {
            String username = filed["username"];
            String password = XString.ToMD5( filed["password"]);
            User row_user = userDAO.getRow(username, "Customer");
            string strError = "";
            if(row_user==null)
            {
                strError = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if(password.Equals(row_user.PassWord))
                {
                    Session["UserCustomer"] = username;
                    Session["CustomerId"] = row_user.Id;
                    return Redirect("~/");
                }
                else
                {
                    strError = password;
                }    
            }    
            ViewBag.Error = "<span style='color:red' > "+strError+ "</span>";
            return View("DangNhap");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Gender == null)
                {
                    user.Gender = 1;
                }
                if (user.Img == null)
                {
                    user.Img = "avatarme.jpg";
                }
                if (user.Roles == null)
                {
                    user.Roles = "Customer";

                }
                user.Status = 1;
                user.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                user.Create_At = DateTime.Now;
                userDAO.Insert(user);
                return RedirectToAction("DangKyThanhCong", "KhachHang");
            }
            return View(user);
        }
        public ActionResult DangKyThanhCong()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
            return RedirectToAction("DangNhap", "KhachHang");
        }

    }
}