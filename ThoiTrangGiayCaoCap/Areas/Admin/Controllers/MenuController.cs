using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThoiTrangGiayCaoCap.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();
        MenuDAO menuDAO = new MenuDAO();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.getList("Index");
            ViewBag.ListTopic = topicDAO.getList("Index");
            ViewBag.ListPage = postDAO.getList("Index","Page");
            List<Menu> menu = menuDAO.getList("Index");
            return View("Index", menu);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if(!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if (!string.IsNullOrEmpty(form["itemcat"]))
                {
                    var listitem = form["itemcat"];
                    var listarr = listitem.Split(',');
                    foreach(var row in listarr)
                    {
                        int id = int.Parse(row);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.TableId = category.Id;
                        menu.MenuType = "category";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Create_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Create_At = DateTime.Now;
                        menu.Status = 2;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm Menu Thành Công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa Chọn Danh Mục Sản Phẩm!");
                }
            }
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["itemtopic"]))
                {
                    var listitem = form["itemtopic"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.TableId = topic.Id;
                        menu.MenuType = "topic";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Create_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Create_At = DateTime.Now;
                        menu.Status = 2;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm Menu Thành Công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa Chọn Chủ Đề Bài Viết!");
                }
            }
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["itempage"]))
                {
                    var listitem = form["itempage"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.TableId = post.Id;
                        menu.MenuType = "page";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.Create_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Create_At = DateTime.Now;
                        menu.Status = 2;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm Menu Thành Công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa Chọn Trang Đơn!");
                }
            }
            
            // Thêm Custom
            if (!string.IsNullOrEmpty(form["ThemConfig"]))
            {
                if(!string.IsNullOrEmpty(form["name"])&& !string.IsNullOrEmpty(form["link"]))
                {
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.MenuType = "custom";
                    menu.Position = form["Position"];
                    menu.ParentId = 0;
                    menu.Orders = 0;
                    menu.Create_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                    menu.Create_At = DateTime.Now;
                    menu.Status = 2;
                    menuDAO.Insert(menu);
                    TempData["message"] = new XMessage("success", "Thêm Menu Thành Công!");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa Nhập Đủ Thông Tin!");
                }
            }
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
            Menu menu = menuDAO.getRow(id);
            return View("Edit", menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.ParentId == null)
                {
                    menu.ParentId = 0;
                }
                if (menu.Orders == null)
                {
                    menu.Orders = 1;
                }
                else
                {
                    menu.Orders += 1;
                }
                menu.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                menu.Update_At = DateTime.Now;
                menuDAO.Update(menu);
                TempData["message"] = new XMessage("success", "Cập Nhật Mẫu Tin Thành Công!");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
            return View(menu);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Menu Không Tồn Tại!");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            menu.Update_At = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Menu Không Tồn Tại!");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = 0;// Trạng Thái Rác bằng 0
            menu.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            menu.Update_At = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Trash()
        {
            List<Menu> menu = menuDAO.getList("Trash");
            return View("Trash", menu);
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Menu Không Tồn Tại!");
                return RedirectToAction("Trash", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Trash", "Menu");
            }
            menu.Status = 2;// Trạng Thái Rác bằng 0
            menu.Update_By = Convert.ToInt32(Session["UserId"].ToString());
            menu.Update_At = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Menu");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = menuDAO.getRow(id);
            menuDAO.Delete(menu);
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Menu");
        }
    }
}