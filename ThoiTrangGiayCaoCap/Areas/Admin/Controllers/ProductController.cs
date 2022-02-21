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
    public class ProductController : BaseController
    {
        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();


        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListProCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
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
                        string slug = XString.Str_Slug(product.Name);
                        string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png" };
                        //Kiểm tra tập tin
                        if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                        {
                            ModelState.AddModelError("Err", "Kiểu tập tin không hợp lệ, kiểu tập tin hợp lệ là:" + string.Join(",", FileExtentions));
                        }
                        else
                        {
                            string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                            product.Img = imgName;
                            string SSPath = "~/Public/images/Products/";
                            string SPath = Path.Combine(Server.MapPath(SSPath), imgName);
                            img.SaveAs(SPath);

                            if (product.Number == null)
                            {
                                product.Number = 1;
                            }
                            if (product.Metadesc == null)
                            {
                                product.Metadesc = "Metadesc";
                            }
                            if (product.Metakey == null)
                            {
                                product.Metakey = "Metakey";
                            }
                            product.Create_By = Convert.ToInt32(Session["UserId"].ToString());
                            product.Create_At = DateTime.Now;
                            product.Slug = slug;
                            productDAO.Insert(product);
                            TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                            return RedirectToAction("Index", "Product");
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("Err", "Thêm Không Thành Công");
                }
            }
            ViewBag.ListProCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListProCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
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
                        string slug = XString.Str_Slug(product.Name);
                        string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png" };
                        //Kiểm tra tập tin
                        if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                        {
                            ModelState.AddModelError("Err", "Kiểu tập tin không hợp lệ, kiểu tập tin hợp lệ là:" + string.Join(",", FileExtentions));
                        }
                        else
                        {
                            string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                            product.Img = imgName;
                            string SSPath = "~/Public/images/Products/";
                            string SPath = Path.Combine(Server.MapPath(SSPath), imgName);
                            img.SaveAs(SPath);

                            if (product.Number == null)
                            {
                                product.Number = 1;
                            }
                            if (product.Metadesc == null)
                            {
                                product.Metadesc = "Metadesc";
                            }
                            if (product.Metakey == null)
                            {
                                product.Metakey = "Metakey";
                            }
                            product.Update_By = Convert.ToInt32(Session["UserId"].ToString());
                            product.Update_At = DateTime.Now;
                            product.Slug = slug;
                            productDAO.Update(product);
                            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
                            return RedirectToAction("Index", "Product");
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("Err", "Cập Nhật Không Thành Công");
                }
            }
            ViewBag.ListProCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success", "Xóa Mẫu Tin Thành Công!");
            return RedirectToAction("Trash","Product");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Nhà Cung Cấp Không Tồn Tại!");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Nhà Cung Cấp Không Tồn Tại!");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa Vào Thùng Rác Thành Công!");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Nhà Cung Cấp Không Tồn Tại!");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi Phục Mẫu Tin Thành Công!");
            return RedirectToAction("Trash", "Product");
        }
        public String ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if(product==null)
            {
                return "";
            } 
            else
            {
                return product.Img;
            }    
        }
        public String ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}
