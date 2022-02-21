using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace ThoiTrangGiayCaoCap.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View("Login");
        }
        [HttpPost]
        public ActionResult DoLogin( FormCollection filed)
        {
            ViewBag.Error = "";
            string username = filed["username"];
            string password = filed["password"];
            //Kiểm tra trong csdl có username
            User user = db.Users
                .Where(m => m.Roles == "Admin" && m.Status == 1 && (m.UserName == username || m.Email == username))
                .FirstOrDefault();
            if(user!=null)
            {
                if(user.CountError>=5 && user.Roles!="Admin")
                {
                    ViewBag.Error = "<p class='login-box-msg text-danger'>Bạn Nhập Sai Mật Khẩu Quá Nhiều Lần!</p>";
                }
                else
                {
                    //Mật khẩu
                    if (user.PassWord.Equals(password))
                    {
                        Session["UserAdmin"] = username;
                        Session["UserId"] = user.Id.ToString();
                        Session["FullName"] = user.Fullname;
                        Session["Img"] = user.Img;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        if (user.CountError == null)
                        {
                            user.CountError = 0;
                        }
                        else
                        {
                            user.CountError += 1;
                        }
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Error = "<p class='login-box-msg text-danger'>Mật Khẩu Không Chính Xác!</p>";
                    }
                }
            }
            else
            {
                ViewBag.Error = "<p class='login-box-msg text-danger'>Tài Khoản <strong>" + username + "</strong> Không Tồn Tại!</p>";
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] ="";
            Session["UserId"] = "";
            Session["FullName"] = "";
            Session["Img"] = "";
            return Redirect("~/Admin/login");
        }
    }
}