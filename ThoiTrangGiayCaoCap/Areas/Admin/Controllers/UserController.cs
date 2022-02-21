using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrangGiayCaoCap.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserDAO userDAO = new UserDAO();

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(userDAO.getList("Index"));
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
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
                user.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                user.Create_At = DateTime.Now;
                userDAO.Insert(user);
                TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                return RedirectToAction("Index", "User");
            }

            return View(user);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
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
                user.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                user.Update_At = DateTime.Now;
                userDAO.Update(user);
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
                return RedirectToAction("Index", "User");
            }
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userDAO.getRow(id);
            userDAO.Delete(user);
            return RedirectToAction("Index");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã User Không Tồn Tại!");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "User");
            }
            user.Status = (user.Status == 1) ? 2 : 1;
            user.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            user.Update_At = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "User");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã User Không Tồn Tại!");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "User");
            }
            user.Status = 0;
            user.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            user.Update_At = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "User");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã User Không Tồn Tại!");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Trash", "User");
            }
            user.Status = 2;
            user.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            user.Update_At = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "User");
        }

        public ActionResult Trash()
        {
            return View(userDAO.getList("Trash"));
        }

        public String NameCustomer(int userid)
        {
            User user = userDAO.getRow(userid);
            if (user == null)
            {
                return "";

            }
            else
            {
                return user.Fullname;
            }
        }
    }
}
