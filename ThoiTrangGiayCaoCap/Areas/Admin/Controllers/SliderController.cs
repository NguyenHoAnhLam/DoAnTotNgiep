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
    public class SliderController : BaseController
    {
        SliderDAO sliderDAO = new SliderDAO();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
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
                        string slug = XString.Str_Slug(slider.Name);
                        string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png" };
                        //Kiểm tra tập tin
                        if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                        {
                            ModelState.AddModelError("Err", "Kiểu tập tin không hợp lệ, kiểu tập tin hợp lệ là:" + string.Join(",", FileExtentions));
                        }
                        else
                        {
                            string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                            slider.Img = imgName;
                            string SSPath = "~/Public/images/Slider/";
                            string SPath = Path.Combine(Server.MapPath(SSPath), imgName);
                            img.SaveAs(SPath);

                            if (slider.Link == null)
                            {
                                slider.Link = "#";
                            }
                            if (slider.Position == null)
                            {
                                slider.Position = "Slideshow";
                            }
                            if (slider.Orders == null)
                            {
                                slider.Orders = 1;
                            }
                            else
                            {
                                slider.Orders += 1;
                            }
                            slider.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                            slider.Create_At = DateTime.Now;
                            sliderDAO.Insert(slider);
                            TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                            return RedirectToAction("Index", "Slider");
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("Err", "Thêm Không Thành Công");
                }
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
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
                        string slug = XString.Str_Slug(slider.Name);
                        string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png" };
                        //Kiểm tra tập tin
                        if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                        {
                            ModelState.AddModelError("Err", "Kiểu tập tin không hợp lệ, kiểu tập tin hợp lệ là:" + string.Join(",", FileExtentions));
                        }
                        else
                        {
                            string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                            slider.Img = imgName;
                            string SSPath = "~/Public/images/Slider/";
                            string SPath = Path.Combine(Server.MapPath(SSPath), imgName);
                            img.SaveAs(SPath);

                            if (slider.Link == null)
                            {
                                slider.Link = "#";
                            }
                            if (slider.Position == null)
                            {
                                slider.Position = "slideshow";
                            }
                            if (slider.Orders == null)
                            {
                                slider.Orders = 1;
                            }
                            else
                            {
                                slider.Orders += 1;
                            }
                            slider.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                            slider.Update_At = DateTime.Now;
                            sliderDAO.Update(slider);
                            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
                            return RedirectToAction("Index", "Slider");
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("Err", "Cập Nhật Không Thành Công");
                }
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            sliderDAO.Delete(slider);
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công!");
            return RedirectToAction("Trash","Slider");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Slider Không Tồn Tại!");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Slider Không Tồn Tại!");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 0;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Slider Không Tồn Tại!");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status =2;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult Trash()
        {
            return View(sliderDAO.getList("Trash"));
        }
    }
}
