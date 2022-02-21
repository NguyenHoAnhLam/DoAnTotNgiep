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
using System.IO;

namespace ThoiTrangGiayCaoCap.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        PostDAO postDAO = new PostDAO();
        TopicDAO topicDAO = new TopicDAO();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.getList("Index","Post"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.ListPostTop = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var img = Request.Files["img"];
                    if (img.ContentLength == 0)
                    {
                        ModelState.AddModelError("Err", "Chưa Chọn Hình Ảnh");
                    }
                    else
                    {
                        string slug = XString.Str_Slug(post.Title);
                        string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png" };
                        //Kiểm tra tập tin
                        if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                        {
                            ModelState.AddModelError("Err", "Kiểu tập tin không hợp lệ, kiểu tập tin hợp lệ là:" + string.Join(",", FileExtentions));
                        }
                        else
                        {
                            string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                            post.Img = imgName;
                            string SSPath = "~/Public/images/Post/";
                            string SPath = Path.Combine(Server.MapPath(SSPath), imgName);
                            img.SaveAs(SPath);

                            post.Type = "Post";
                            post.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                            post.Create_At = DateTime.Now;
                            post.Slug = slug;
                            postDAO.Insert(post);
                            TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                            return RedirectToAction("Index", "Post");
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("Err", "Thêm Không Thành Công");
                }
            }
            ViewBag.ListPostTop = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListPostTop = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var img = Request.Files["img"];
                    if (img.ContentLength == 0)
                    {
                        ModelState.AddModelError("Err", "Chưa Chọn Hình Ảnh");
                    }
                    else
                    {
                        string slug = XString.Str_Slug(post.Title);
                        string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png" };
                        //Kiểm tra tập tin
                        if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                        {
                            ModelState.AddModelError("Err", "Kiểu tập tin không hợp lệ, kiểu tập tin hợp lệ là:" + string.Join(",", FileExtentions));
                        }
                        else
                        {
                            string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                            post.Img = imgName;
                            string SSPath = "~/Public/images/Post/";
                            string SPath = Path.Combine(Server.MapPath(SSPath), imgName);
                            img.SaveAs(SPath);

                            post.Type = "Post";
                            post.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                            post.Update_At = DateTime.Now;
                            post.Slug = slug;
                            postDAO.Update(post);
                            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
                            return RedirectToAction("Index", "Post");
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("Err", "Cập Nhật Không Thành Công");
                }
            }
            ViewBag.ListPostTop = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            postDAO.Delete(post);
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Post");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Bài Viết Không Tồn Tại!");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Bài Viết Không Tồn Tại!");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Post");
            }
            post.Status = 0;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Bài Viết Không Tồn Tại!");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Trash", "Post");
            }
            post.Status = 2;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Post");
        }
        public ActionResult Trash()
        {
            return View(postDAO.getList("Trash","Post"));
        }
    }
}
