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
    public class OrderController : BaseController
    {
        private OrderDAO orderDAO = new OrderDAO();
        private OrderDetailDAO orderdetailDAO = new OrderDetailDAO();

        // GET: Admin/Order
        public ActionResult Index()
        {
            List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Phone,Email,Note,Create_At,Create_By,Update_At,Update_By,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                orderDAO.Insert(order);
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                orderDAO.Update(order);
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            OrderDetail orderdetail = orderdetailDAO.getRow(order.Id);
            if (orderDAO.Delete(order) == 1)
            {
                orderdetailDAO.Delete(orderdetail);
            }
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Đơn Hàng Không Tồn Tại!");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;// Trạng Thái Rác bằng 0
            order.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Update_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Đơn Hàng Không Tồn Tại!");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 2;// Trạng Thái Rác bằng 0
            order.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Update_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Huy(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Đơn Hàng Không Tồn Tại!");
                return RedirectToAction("Index");
            }
            if(order.Status==1||order.Status==2)
            {
                order.Status = 0;
                order.Update_At = DateTime.Now;
                order.Update_By = 1;
            }
            else
            {
                if(order.Status==3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Đang Vận Chuyển không Thể Hủy!");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Đã Giao không Thể Hủy!");
                }
                return RedirectToAction("Index");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đã Hủy Đơn Hàng Thành Công!");
            return RedirectToAction("Index");
        }
        public ActionResult DaXacMinh(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Đơn Hàng Không Tồn Tại!");
                return RedirectToAction("Index");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 3;
                order.Update_At = DateTime.Now;
                order.Update_By = 1;
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Đã Được Xác Minh Và Đang Trên Đường Vận Chuyển!");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Đã Được Xác Minh Và Đã Giao Khách Hàng!");
                }
                return RedirectToAction("Index");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đơn Hàng Đã Được Xác Minh Và Chuyển Sang Vận Chuyển!");
            return RedirectToAction("Index");
        }
        public ActionResult DangVanChuyen(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Đơn Hàng Không Tồn Tại!");
                return RedirectToAction("Index");
            }
            if (order.Status == 3)
            {
                order.Status = 4;
                order.Update_At = DateTime.Now;
                order.Update_By = 1;
            }
            else
            {
                if(order.Status == 1 ||order.Status == 2)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Chưa Được Xác Minh Nên Không Thể Vận Chuyển!");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Đã Được Vận Chuyển Và Đã Giao Khách Hàng!");
                }
                return RedirectToAction("Index");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đơn Hàng Đã Vận Chuyển Thành Công Và Đang Giao Cho Khách Hàng!");
            return RedirectToAction("Index");
        }

        public ActionResult ThanhCong(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Đơn Hàng Không Tồn Tại!");
                return RedirectToAction("Index");
            }
            if (order.Status == 4)
            {
                order.Update_At = DateTime.Now;
                order.Update_By = 1;
            }
            else
            {
                if (order.Status == 1 || order.Status == 2)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Chưa Được Xác Minh Nên Không Thể Giao Đến Khách Hàng!");
                }
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn Hàng Chưa Được Vận Chuyển Nên Không Thể Giao Đến Khách Hàng!");
                }
                return RedirectToAction("Index");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đơn Hàng Đã Được Giao Đến Cho Khách Hàng Thành Công!");
            return RedirectToAction("Index");
        }

    }
}
