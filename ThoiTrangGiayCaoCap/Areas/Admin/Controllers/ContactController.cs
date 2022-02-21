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
    public class ContactController : Controller
    {
        ContactDAO contactDAO = new ContactDAO();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(contactDAO.getList("Index"));
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.Title == null)
                {
                    contact.Title = "Tiêu Đề";
                }
                contact.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                contact.Create_At = DateTime.Now;
                contactDAO.Insert(contact);
                TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                return RedirectToAction("Index", "Contact");
            }

            return View(contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.Title == null)
                {
                    contact.Title = "Tiêu Đề";
                }
                contact.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                contact.Update_At = DateTime.Now;
                contactDAO.Update(contact);
                TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
                return RedirectToAction("Index", "Contact");
            }
            return View(contact);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = contactDAO.getRow(id);
            contactDAO.Delete(contact);
            return RedirectToAction("Index");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Liên Hệ Không Tồn Tại!");
                return RedirectToAction("Index", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Contact");
            }
            contact.Status = (contact.Status == 1) ? 2 : 1;
            contact.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            contact.Update_At = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "Contact");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Liên Hệ Không Tồn Tại!");
                return RedirectToAction("Index", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Contact");
            }
            contact.Status = 0;
            contact.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            contact.Update_At = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "Contact");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Liên Hệ Không Tồn Tại!");
                return RedirectToAction("Trash", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Trash", "Contact");
            }
            contact.Status = 2;
            contact.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            contact.Update_At = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Contact");
        }
        public ActionResult Trash()
        {
            return View(contactDAO.getList("Trash"));
        }
    }
}
